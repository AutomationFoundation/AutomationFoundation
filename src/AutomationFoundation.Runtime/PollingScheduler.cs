using System;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime;

/// <summary>
/// Provides a scheduler for a pre-determined interval.
/// </summary>
public class PollingScheduler : IScheduler
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PollingScheduler"/> class.
    /// </summary>
    /// <param name="interval">The interval used to schedule the next execution.</param>
    /// <param name="calculateFromStartedOffset">Optional. true if the next execution should be calculated using the started datetime, otherwise false to use the completed datetime.</param>
    public PollingScheduler(TimeSpan interval, bool calculateFromStartedOffset = false)
    {
        if (interval <= TimeSpan.Zero)
        {
            throw new ArgumentException("The interval must be greater than zero.");
        }

        Interval = interval;
        CalculateFromStartedOffset = calculateFromStartedOffset;
    }

    /// <summary>
    /// Gets the interval.
    /// </summary>
    public TimeSpan Interval { get; }

    /// <summary>
    /// Gets a value indicating whether the next execution should be calculated from the started datetime offset.
    /// </summary>
    public bool CalculateFromStartedOffset { get; }

    /// <summary>
    /// Calculates the next execution date and time.
    /// </summary>
    /// <paramref name="started">The date and time the operation started.</paramref>
    /// <paramref name="completed">The date and time the operation completed.</paramref>
    /// <returns>The next execution date and time.</returns>
    public DateTimeOffset CalculateNextExecution(DateTimeOffset started, DateTimeOffset completed)
    {
        if (CalculateFromStartedOffset)
        {
            return started.Add(Interval);
        }

        return completed.Add(Interval);
    }
}