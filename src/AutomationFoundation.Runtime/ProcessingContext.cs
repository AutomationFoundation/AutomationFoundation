using System;
using System.Threading;

namespace AutomationFoundation
{
    /// <summary>
    /// Provides contextual information of work being processed by the runtime.
    /// </summary>
    public class ProcessingContext
    {
        private static readonly AsyncLocal<ProcessingContext> Local = new AsyncLocal<ProcessingContext>();

        /// <summary>
        /// Gets the current processing context.
        /// </summary>
        public static ProcessingContext Current
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
        public static void SetCurrent(ProcessingContext context)
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

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingContext"/> class.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        public ProcessingContext(Guid id)
        {
            Id = id;
        }
    }
}