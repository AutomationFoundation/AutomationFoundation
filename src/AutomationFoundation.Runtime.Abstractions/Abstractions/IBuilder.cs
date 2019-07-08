namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies an object which builds an object.
    /// </summary>
    /// <typeparam name="T">The type of object to be built.</typeparam>
    public interface IBuilder<out T>
    {
        /// <summary>
        /// Builds the object.
        /// </summary>
        /// <returns>The new object which was built.</returns>
        T Build();
    }
}