using System;
using System.Threading;

namespace AutomationFoundation.Runtime.Synchronization.Primitives;

/// <summary>
/// Provides a resource lock using a manual reset event.
/// </summary>
public class ManualResetLock : SynchronizationLock
{
    private readonly ManualResetEventSlim resetEvent;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManualResetLock"/> class.
    /// </summary>
    /// <param name="resetEvent">The manual reset event</param>
    public ManualResetLock(ManualResetEventSlim resetEvent)
    {
        this.resetEvent = resetEvent ?? throw new ArgumentNullException(nameof(resetEvent));
    }

    /// <inheritdoc />
    protected override void ReleaseLock()
    {
        resetEvent.Set();
    }
}