using System;

namespace AutomationFoundation.Runtime.TestObjects;

public class StubProcessor : Processor
{
    private Action onStartCallback;
    private Action onStopCallback;
    private Action onDispose;

    public StubProcessor()
        : this(Guid.NewGuid().ToString())
    {
    }

    public StubProcessor(string name)
        : base(name)
    {
    }

    protected override void OnStart()
    {
        onStartCallback?.Invoke();
    }

    protected override void OnStop()
    {
        onStopCallback?.Invoke();
    }

    protected override void Dispose(bool disposing)
    {
        onDispose?.Invoke();
        base.Dispose(disposing);
    }

    public void SetupCallbacks(Action onStartCallback = null, Action onStopCallback = null, Action onDispose = null)
    {
        this.onStartCallback = onStartCallback;
        this.onStopCallback = onStopCallback;
        this.onDispose = onDispose;
    }

    public void ExecuteGuardMustNotAlreadyBeStarted()
    {
        GuardMustNotAlreadyBeStarted();
    }

    public void ExecuteGuardMustNotAlreadyBeStopped()
    {
        GuardMustNotAlreadyBeStopped();
    }

    public void SetState(ProcessorState value)
    {
        State = value;
    }
}