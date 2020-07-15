using static AutomationFoundation.Interop.NativeMethods;

namespace AutomationFoundation.Interop
{
    /// <summary>
    /// Identifies methods on the Kernel32.dll within the operating system.
    /// </summary>
    internal interface IKernel32
    {
        /// <summary>
        /// Adds or removes an application-defined <paramref name="handlerRoutine"/> function from the list of handler functions for the calling process.
        /// </summary>
        /// <param name="handlerRoutine">A pointer to the application-defined <see cref="HandlerRoutine"/> function to be added or removed. This parameter can be NULL.</param>
        /// <param name="add">If this parameter is true, the handler is added; if it is false, the handler is removed.</param>
        /// <returns>true if the call was successful, otherwise false.</returns>
        bool SetConsoleCtrlHandler(HandlerRoutine handlerRoutine, bool add);
    }
}