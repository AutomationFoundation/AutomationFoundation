namespace AutomationFoundation.Runtime.Synchronization.Policies;

/// <summary>
/// Provides a synchronization policy which allows only a single thread to access a protected resource.
/// </summary>
public class OneThreadPerResourcePolicy : OneOrMoreThreadsPerResourcePolicy
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneThreadPerResourcePolicy"/> class.
    /// </summary>
    public OneThreadPerResourcePolicy()
        : base(1)
    {
    }
}