namespace AutomationFoundation.Interop
{
    /// <summary>
    /// Defines the constants within the ConsoleApi.h included with the Windows SDK.
    /// </summary>
    internal static class ConsoleApi
    {
        /// <summary>
        /// A signal that the system sends to all processes attached to a console when the user closes the console.
        /// </summary>
        public const int CTRL_CLOSE_EVENT = 2;

        /// <summary>
        /// A signal that the system sends to all console processes when a user is logging off.
        /// </summary>
        public const int CTRL_LOGOFF_EVENT = 5;
    }
}