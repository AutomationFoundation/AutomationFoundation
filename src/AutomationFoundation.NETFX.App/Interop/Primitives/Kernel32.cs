using static AutomationFoundation.Interop.NativeMethods;

namespace AutomationFoundation.Interop.Primitives
{
    internal class Kernel32 : IKernel32
    {
        public bool SetConsoleCtrlHandler(HandlerRoutine handlerRoutine, bool add)
        {
            return UnsafeNativeMethods.SetConsoleCtrlHandler(handlerRoutine, add);
        }
    }
}