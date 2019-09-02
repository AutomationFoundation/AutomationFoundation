namespace AutomationFoundation.Features.ProducerConsumer.Engines
{
    public class StubEngine : Engine
    {
        public void ThisShouldCauseAnExceptionAfterDispose()
        {
            GuardMustNotBeDisposed();
        }
    }
}