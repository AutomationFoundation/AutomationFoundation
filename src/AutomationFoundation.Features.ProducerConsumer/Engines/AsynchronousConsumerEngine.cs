using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Abstractions.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Features.ProducerConsumer.Engines
{
    /// <summary>
    /// Provides a consumer engine which consumes objects asynchronously.
    /// </summary>
    public class AsynchronousConsumerEngine : DisposableObject, IConsumerEngine, IStoppable
    {
        private readonly object syncRoot = new object();

        private readonly ISet<IWorker> workers = new HashSet<IWorker>();
        private readonly IConsumerRunner runner;
        private readonly IWorkerPool pool;
        private readonly IErrorHandler errorHandler;

        private CancellationSource cancellationSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsynchronousConsumerEngine"/> class.
        /// </summary>
        /// <param name="pool">The pool of workers available to consume the objects.</param>
        /// <param name="runner">The consumer runner which will consume the objects produced.</param>
        /// <param name="errorHandler">The error handler to use if errors within the engine.</param>
        public AsynchronousConsumerEngine(IWorkerPool pool, IConsumerRunner runner, IErrorHandler errorHandler)
        {
            this.runner = runner ?? throw new ArgumentNullException(nameof(runner));
            this.pool = pool ?? throw new ArgumentNullException(nameof(pool));
            this.errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
        }

        /// <inheritdoc />
        public async void Consume(ProducerConsumerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            GuardMustNotBeDisposed();

            IWorker worker = null;

            try
            {
                worker = CreateWorker(context);
                await worker.RunAsync();
            }
            catch (Exception ex)
            {
                errorHandler.Handle(ex, ErrorSeverityLevel.NonFatal);
            }
            finally
            {
                ReleaseWorker(worker);
            }
        }

        /// <summary>
        /// Creates a new worker.
        /// </summary>
        /// <param name="context">The contextual information about what was produced.</param>
        /// <returns>The worker used to work the produced item.</returns>
        protected virtual IWorker CreateWorker(ProducerConsumerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            lock (syncRoot)
            {
                IWorker worker = null;

                try
                {
                    worker = pool.Get(() => OnConsume(context), null);
                    workers.Add(worker);

                    return worker;
                }
                catch (Exception)
                {
                    worker?.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// Occurs when an item is being consumed.
        /// </summary>
        /// <param name="context">The contextual information about what was produced.</param>
        protected virtual void OnConsume(ProducerConsumerContext context)
        {
            using (var task = runner.Run(context, cancellationSource.CancellationToken))
            {
                Task.WaitAll(task);
            }
        }

        /// <summary>
        /// Releases the worker.
        /// </summary>
        /// <param name="worker">The worker to release.</param>
        protected virtual void ReleaseWorker(IWorker worker)
        {
            if (worker == null)
            {
                return;
            }

            lock (syncRoot)
            {
                workers.Remove(worker);
                worker.Dispose();
            }
        }

        /// <inheritdoc />
        public void Initialize(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            cancellationSource?.Dispose();
            cancellationSource = new CancellationSource(cancellationToken);
        }

        /// <inheritdoc />
        public Task StopAsync()
        {
            GuardMustNotBeDisposed();

            Task[] tasks;
            lock (syncRoot)
            {
                tasks = workers.Select(o => o.WaitForCompletionAsync()).ToArray();
            }

            return Task.WhenAll(tasks);
        }
    }
}