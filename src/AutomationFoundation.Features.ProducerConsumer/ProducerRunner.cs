using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides a runner for producing objects.
    /// </summary>
    /// <typeparam name="TItem">The type of item being produced.</typeparam>
    public class ProducerRunner<TItem> : IProducerRunner<TItem>
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ISynchronizationPolicy synchronizationPolicy;
        private readonly Func<IServiceScope, IProducer<TItem>> producerFactory;
        private readonly bool alwaysExecuteOnDefaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerRunner{T}"/> class.
        /// </summary>
        /// <param name="scopeFactory">The factory for creating service scopes.</param>
        /// <param name="producerFactory">The factory for creating producers.</param>
        /// <param name="synchronizationPolicy">The policy used to synchronize the producer and consumer engines to prevent over producing work.</param>
        /// <param name="alwaysExecuteOnDefaultValue">true to always execute the callback, even if the value produced is the default; otherwise false.</param>
        public ProducerRunner(IServiceScopeFactory scopeFactory, Func<IServiceScope, IProducer<TItem>> producerFactory, ISynchronizationPolicy synchronizationPolicy, bool alwaysExecuteOnDefaultValue)
        {
            this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            this.producerFactory = producerFactory ?? throw new ArgumentNullException(nameof(producerFactory));
            this.synchronizationPolicy = synchronizationPolicy;
            this.alwaysExecuteOnDefaultValue = alwaysExecuteOnDefaultValue;
        }

        /// <inheritdoc />
        public Task<bool> RunAsync(Action<ProducerConsumerContext<TItem>> onProducedCallback, CancellationToken cancellationToken)
        {
            if (onProducedCallback == null)
            {
                throw new ArgumentNullException(nameof(onProducedCallback));
            }

            return RunAsyncImpl(onProducedCallback, cancellationToken);
        }

        private async Task<bool> RunAsyncImpl(Action<ProducerConsumerContext<TItem>> onProducedCallback, CancellationToken cancellationToken)
        {
            IServiceScope scope = null;

            try
            {
                scope = CreateChildScope();

                ProducerConsumerContext<TItem> context = null;
                var called = false;

                try
                {
                    var id = GenerateIdentifier(scope);

                    context = new ProducerConsumerContext<TItem>(id, scope)
                    {
                        CancellationToken = cancellationToken
                    };

                    await AcquireSynchronizationLockAsync(context);
                    await CreateProducerAsync(context);
                    await ProduceAsync(context);

                    if (ShouldExecuteCallback(context))
                    {
                        onProducedCallback(context);
                        called = true;
                    }
                }
                finally
                {
                    if (!called)
                    {
                        context?.Dispose();
                    }
                }

                return called;
            }
            catch (Exception)
            {
                scope?.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Acquires a synchronization lock for the item to be produced.
        /// </summary>
        /// <param name="context">The contextual information for the item which was produced.</param>
        /// <returns>The synchronization lock.</returns>
        protected virtual Task AcquireSynchronizationLockAsync(ProducerConsumerContext<TItem> context)
        {
            if (synchronizationPolicy != null)
            {
               context.SynchronizationLock = synchronizationPolicy.AcquireLock(context.CancellationToken);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates a producer.
        /// </summary>
        /// <param name="context">The contextual information for the item which was produced.</param>
        protected virtual Task CreateProducerAsync(ProducerConsumerContext<TItem> context)
        {
            var producer = producerFactory(context.ServiceScope);
            if (producer == null)
            {
                throw new InvalidOperationException("The producer was not created.");
            }

            context.Producer = producer;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Produces an item.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected virtual async Task ProduceAsync(ProducerConsumerContext<TItem> context)
        {
            context.Producer = producerFactory(context.ServiceScope);
            context.Item = await context.Producer.ProduceAsync(context.CancellationToken);
        }

        /// <summary>
        /// Creates the child scope.
        /// </summary>
        /// <returns>The child scope.</returns>
        protected virtual IServiceScope CreateChildScope()
        {
            var scope = scopeFactory.CreateScope();
            if (scope == null)
            {
                throw new RuntimeException("The scope factory did not return a scope.");
            }

            return scope;
        }

        /// <summary>
        /// Generates a new identifier.
        /// </summary>
        /// <param name="scope">The scope to use when creating the identifier.</param>
        /// <returns>The new identifier.</returns>
        protected virtual Guid GenerateIdentifier(IServiceScope scope)
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Determines whether the callback should be executed.
        /// </summary>
        /// <param name="context">The contextual information for the item which was produced.</param>
        /// <returns>true if the callback should be executed, otherwise false.</returns>
        protected virtual bool ShouldExecuteCallback(ProducerConsumerContext<TItem> context)
        {
            return alwaysExecuteOnDefaultValue || !Equals(context.Item, default(TItem));
        }
    }
}