namespace AutomationFoundation.Runtime.TestObjects;

public class StubEngine : Engine
{
    public void ThisShouldCauseAnExceptionAfterDispose()
    {
        GuardMustNotBeDisposed();
    }
}