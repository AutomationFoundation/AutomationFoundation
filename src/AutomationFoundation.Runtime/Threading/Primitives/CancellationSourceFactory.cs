using System.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Provides a mechanism for creating a cancellation source.
    /// </summary>
    public class CancellationSourceFactory : ICancellationSourceFactory
    {
        /// <inheritdoc />
        public ICancellationSource Create(CancellationToken cancellationToken)
        {
            return new CancellationSource(cancellationToken);
        }
    }
}