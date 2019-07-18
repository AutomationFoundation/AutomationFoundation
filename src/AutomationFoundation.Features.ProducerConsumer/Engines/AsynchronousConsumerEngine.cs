﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Abstractions.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Features.ProducerConsumer.Engines
{
    /// <summary>
    /// Provides a consumer engine which consumes objects asynchronously.
    /// </summary>
    /// <typeparam name="TItem">The type of item being consumed.</typeparam>
    public class AsynchronousConsumerEngine<TItem> : DisposableObject, IConsumerEngine<TItem>, IStoppable
    {
        private readonly object syncRoot = new object();

        private readonly ISet<IWorker> workers = new HashSet<IWorker>();
        private readonly IConsumerRunner<TItem> runner;
        private readonly IWorkerPool pool;
        private readonly IErrorHandler errorHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsynchronousConsumerEngine{TItem}"/> class.
        /// </summary>
        /// <param name="pool">The pool of workers available to consume the objects.</param>
        /// <param name="runner">The consumer runner which will consume the objects produced.</param>
        /// <param name="errorHandler">The error handler to use if errors within the engine.</param>
        public AsynchronousConsumerEngine(IWorkerPool pool, IConsumerRunner<TItem> runner, IErrorHandler errorHandler)
        {
            this.runner = runner ?? throw new ArgumentNullException(nameof(runner));
            this.pool = pool ?? throw new ArgumentNullException(nameof(pool));
            this.errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
        }

        /// <inheritdoc />
        public void Consume(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            GuardMustNotBeDisposed();

            using (var task = ConsumeAsyncImpl(context))
            {
                task.Start();
                task.Wait();
            }
        }

        private async Task ConsumeAsyncImpl(ProducerConsumerContext<TItem> context)
        {
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
        protected virtual IWorker CreateWorker(ProducerConsumerContext<TItem> context)
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
        protected virtual void OnConsume(ProducerConsumerContext<TItem> context)
        {
            using (var task = runner.RunAsync(context))
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