using System;
using System.Collections.Generic;

namespace AutomationFoundation.Runtime;

/// <summary>
/// Provides a composite strategy for error handling across multiple strategies. 
/// </summary>
public class CompositeErrorHandlingStrategy : ICompositeErrorHandlingStrategy
{
    private readonly IList<IErrorHandlingStrategy> strategies = new List<IErrorHandlingStrategy>();

    /// <summary>
    /// Creates a <see cref="CompositeErrorHandlingStrategy"/> from the strategies provided.
    /// </summary>
    /// <param name="strategies">The strategies to add to the composite result.</param>
    /// <returns>A <see cref="CompositeErrorHandlingStrategy"/> instance.</returns>
    public static CompositeErrorHandlingStrategy Create(params IErrorHandlingStrategy[] strategies)
    {
        if (strategies == null)
        {
            throw new ArgumentNullException(nameof(strategies));
        }

        return Create((IEnumerable<IErrorHandlingStrategy>)strategies);
    }

    /// <summary>
    /// Creates a <see cref="CompositeErrorHandlingStrategy"/> from the strategies provided.
    /// </summary>
    /// <param name="strategies">The strategies to add to the composite result.</param>
    /// <returns>A <see cref="CompositeErrorHandlingStrategy"/> instance.</returns>
    public static CompositeErrorHandlingStrategy Create(IEnumerable<IErrorHandlingStrategy> strategies)
    {
        if (strategies == null)
        {
            throw new ArgumentNullException(nameof(strategies));
        }

        var result = new CompositeErrorHandlingStrategy();

        foreach (var strategy in strategies)
        {
            result.AddStrategy(strategy);
        }

        return result;
    }

    /// <inheritdoc />
    public void Handle(ErrorHandlingContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        foreach (var strategy in strategies)
        {
            strategy.Handle(context);
        }
    }

    /// <inheritdoc />
    public void AddStrategy(IErrorHandlingStrategy strategy)
    {
        if (strategy == null)
        {
            throw new ArgumentNullException(nameof(strategy));
        }
        else if (strategies.Contains(strategy))
        {
            throw new ArgumentException("The strategy has already been added to the composite strategy.");
        }

        strategies.Add(strategy);
    }

    /// <inheritdoc />
    public void RemoveStrategy(IErrorHandlingStrategy strategy)
    {
        if (strategy == null)
        {
            throw new ArgumentNullException(nameof(strategy));
        }
        else if (!strategies.Contains(strategy))
        {
            throw new ArgumentException("The strategy does not exist within the composite strategy.");
        }

        strategies.Remove(strategy);
    }
}