using System;

namespace AutomationFoundation.Runtime.Abstractions;

/// <summary>
/// Identifies a scheduler.
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// Calculates the next execution date and time.
    /// </summary>
    /// <paramref name="started">The date and time the operation started.</paramref>
    /// <paramref name="completed">The date and time the operation completed.</paramref>
    /// <returns>The next execution date and time.</returns>
    DateTimeOffset CalculateNextExecution(DateTimeOffset started, DateTimeOffset completed);
}