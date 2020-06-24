using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Provides contextual information of work being processed by the runtime.
    /// </summary>
    public abstract class ProcessingContext : IProcessingContext
    {
        private static readonly AsyncLocal<IProcessingContext> Local = new AsyncLocal<IProcessingContext>();

        /// <summary>
        /// Gets the current processing context.
        /// </summary>
        public static IProcessingContext Current
        {
            get
            {
                lock (Local)
                {
                    return Local.Value;
                }
            }
        }

        /// <summary>
        /// Clears the current processing context.
        /// </summary>
        public static void Clear()
        {
            lock (Local)
            {
                Local.Value = null;
            }
        }

        /// <summary>
        /// Sets the current processing context.
        /// </summary>
        /// <param name="context">The context which is being processed.</param>
        public static void SetCurrent(IProcessingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            lock (Local)
            {
                Local.Value = context;
            }
        }

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public IServiceScope LifetimeScope { get; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        /// <inheritdoc />
        public IProcessor Processor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingContext"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the context.</param>
        /// <param name="serviceScope">The scope of the request used for dependency injection.</param>
        protected ProcessingContext(Guid id, IServiceScope serviceScope)
        {
            Id = id;
            LifetimeScope = serviceScope;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ProcessingContext"/> class.
        /// </summary>
        ~ProcessingContext()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LifetimeScope.Dispose();
            }
        }
    }
}