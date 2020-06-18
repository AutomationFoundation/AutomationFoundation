namespace AutomationFoundation.Interop
{
    /// <summary>
    /// A console process uses this function to handle control signals received by the process. When the signal is received, the system creates a new thread in the process to execute the function.
    /// </summary>
    /// <param name="dwCtrlType">The type of control signal received by the handler.</param>
    /// <returns></returns>
    internal delegate bool HandlerRoutine(int dwCtrlType);

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