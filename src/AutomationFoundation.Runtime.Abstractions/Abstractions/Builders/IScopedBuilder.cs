using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Runtime.Abstractions.Builders
{
    /// <summary>
    /// Identifies an object which can build another object within a specific scope.
    /// </summary>
    /// <typeparam name="T">The type of object which is being built.</typeparam>
    public interface IScopedBuilder<out T>
    {
        /// <summary>
        /// Builds the object.
        /// </summary>
        /// <param name="scope">The scope of the object being built.</param>
        /// <returns>The new object instance.</returns>
        T Build(IServiceScope scope);
    }
}