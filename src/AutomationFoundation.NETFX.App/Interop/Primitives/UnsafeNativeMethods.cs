using System.Runtime.InteropServices;

namespace AutomationFoundation.Interop.Primitives
{
    internal static class UnsafeNativeMethods
    {
        private const string Kernel32 = "Kernel32.dll";

        [DllImport(Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine handlerRoutine, bool add);
    }
}