namespace AutomationFoundation.Runtime.Stubs
{
    public class StubEngine : Engine
    {
        public void ThisShouldCauseAnExceptionAfterDispose()
        {
            GuardMustNotBeDisposed();
        }
    }
}