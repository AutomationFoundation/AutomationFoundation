namespace AutomationFoundation.Runtime.Abstractions.Builders
{
    /// <summary>
    /// Identifies an object which can build another object.
    /// </summary>
    /// <typeparam name="T">The type of object which is being built.</typeparam>
    public interface IBuilder<out T>
    {
        /// <summary>
        /// Builds the object.
        /// </summary>
        /// <returns>The new object instance.</returns>
        T Build();
    }
}