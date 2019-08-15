using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Features.ProducerConsumer.Configuration;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Features.ProducerConsumer.Engines
{
    /// <summary>
    /// Provides a producer engine which uses a schedule to check the producer for work availability.
    /// </summary>
    /// <typeparam name="TItem">The type of item being produced.</typeparam>
    public class ScheduledProducerEngine<TItem> : DisposableObject, IProducerEngine<TItem>
    {
        private readonly IProducerExecutionStrategy<TItem> runner;
        private readonly IErrorHandler errorHandler;
        private readonly IScheduler scheduler;
        private readonly ScheduledEngineOptions options;

        private CancellationSource cancellationSource;
        private Action<ProducerConsumerContext<TItem>> onProducedCallback;

        private bool initialized;
        private Task task;

        /// <summary>
        /// Gets a value indicating whether the engine is currently running.
        /// </summary>
        public bool IsRunning => task != null && !task.IsCanceled && !task.IsCompleted && !task.IsFaulted;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledProducerEngine{TItem}"/> class.
        /// </summary>
        /// <param name="runner">The runner to execute.</param>
        /// <param name="errorHandler">The error handler to use if errors within the engine.</param>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="options">The engine configuration options.</param>
        public ScheduledProducerEngine(IProducerExecutionStrategy<TItem> runner, IErrorHandler errorHandler, IScheduler scheduler, ScheduledEngineOptions options)
        {
            this.runner = runner ?? throw new ArgumentNullException(nameof(runner));
            this.errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public void Initialize(Action<ProducerConsumerContext<TItem>> onProducedCallback, CancellationToken cancellationToken)
        {
            if (onProducedCallback == null)
            {
                throw new ArgumentNullException(nameof(onProducedCallback));
            }

            GuardMustNotBeDisposed();
            GuardMustNotBeInitialized();

            cancellationSource?.Dispose();
            cancellationSource = new CancellationSource(cancellationToken);

            this.onProducedCallback = onProducedCallback;

            initialized = true;
        }

        private void GuardMustNotBeInitialized()
        {
            if (initialized)
            {
                throw new InvalidOperationException("The engine has already been initialized.");
            }
        }

        /// <inheritdoc />
        public Task StartAsync()
        {
            GuardMustNotBeDisposed();
            GuardMustBeInitialized();

            task = new Task(async() => await RunAsync(), TaskCreationOptions.LongRunning);
            task.Start();

            return Task.CompletedTask;
        }

        private void GuardMustBeInitialized()
        {
            if (!initialized)
            {
                throw new InvalidOperationException("The engine has not been initialized.");
            }
        }

        private async Task RunAsync()
        {
            while (ShouldContinueExecution())
            {
                var started = DateTimeOffset.Now;
                var found = false;

                do
                {
                    try
                    {
                        found = await runner.ExecuteAsync(onProducedCallback, cancellationSource.CancellationToken);
                    }
                    catch (Exception ex)
                    {
                        errorHandler.Handle(ex, ErrorSeverityLevel.NonFatal);
                    }
                } while (found && options.ContinueUntilEmpty
                               && ShouldContinueExecution());

                if (ShouldContinueExecution())
                {
                    WaitUntilNextExecution(started, DateTimeOffset.Now);
                }
            }
        }

        private bool ShouldContinueExecution()
        {
            return !cancellationSource.IsCancellationRequested;
        }

        private void WaitUntilNextExecution(DateTimeOffset started, DateTimeOffset lastCompleted)
        {
            var delay = CalculateDelayUntilNextExecution(started, lastCompleted);
            if (delay == TimeSpan.Zero)
            {
                return;
            }

            cancellationSource.CancellationToken.WaitHandle.WaitOne(delay);
        }

        private TimeSpan CalculateDelayUntilNextExecution(DateTimeOffset started, DateTimeOffset lastCompleted)
        {
            var delay = scheduler.CalculateNextExecution(started, lastCompleted) - lastCompleted;
            if (delay < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }

            return delay;
        }

        /// <inheritdoc />
        public Task StopAsync()
        {
            GuardMustNotBeDisposed();
            GuardMustBeInitialized();

            cancellationSource.RequestImmediateCancellation();

            return Task.WhenAll(task);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cancellationSource?.Dispose();
                task?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}