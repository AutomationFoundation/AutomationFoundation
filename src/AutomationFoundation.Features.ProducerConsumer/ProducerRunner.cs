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
        private readonly IProducer<TItem> producer;
        private readonly bool alwaysExecuteOnDefaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerRunner{T}"/> class.
        /// </summary>
        /// <param name="scopeFactory">The factory for creating service scopes.</param>
        /// <param name="producer">The producer to run.</param>
        /// <param name="synchronizationPolicy">The policy used to synchronize the producer and consumer engines to prevent over producing work.</param>
        /// <param name="alwaysExecuteOnDefaultValue">true to always execute the callback, even if the value produced is the default; otherwise false.</param>
        public ProducerRunner(IServiceScopeFactory scopeFactory, IProducer<TItem> producer, ISynchronizationPolicy synchronizationPolicy, bool alwaysExecuteOnDefaultValue)
        {
            this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            this.producer = producer ?? throw new ArgumentNullException(nameof(producer));
            this.synchronizationPolicy = synchronizationPolicy;
            this.alwaysExecuteOnDefaultValue = alwaysExecuteOnDefaultValue;
        }

        /// <inheritdoc />
        public async Task<bool> RunAsync(Action<ProducerConsumerContext<TItem>> onProducedCallback, CancellationToken cancellationToken)
        {
            if (onProducedCallback == null)
            {
                throw new ArgumentNullException(nameof(onProducedCallback));
            }

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
                        SynchronizationLock = AcquireSynchronizationLock(cancellationToken),
                        Producer = producer
                    };

                    context.Item = await ProduceItemAsync(cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The synchronization lock.</returns>
        protected virtual ISynchronizationLock AcquireSynchronizationLock(CancellationToken cancellationToken)
        {
            return synchronizationPolicy?.AcquireLock(cancellationToken);
        }

        /// <summary>
        /// Produces an item asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task for producing the item.</returns>
        protected virtual Task<TItem> ProduceItemAsync(CancellationToken cancellationToken)
        {
            return producer.ProduceAsync(cancellationToken);
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
            return alwaysExecuteOnDefaultValue || !Equals(context.Item, default);
        }
    }
}