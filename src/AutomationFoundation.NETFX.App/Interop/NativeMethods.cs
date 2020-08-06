namespace AutomationFoundation.Interop
{
    internal static class NativeMethods
    {
        /// <summary>
        /// A console process uses this function to handle control signals received by the process. When the signal is received, the system creates a new thread in the process to execute the function.
        /// </summary>
        /// <param name="dwCtrlType">The type of control signal received by the handler.</param>
        /// <returns>true if the function has handled the control signal. If it returns false, the next handler function in the list of handlers is executed.</returns>
        public delegate bool HandlerRoutine(int dwCtrlType);
    }
}