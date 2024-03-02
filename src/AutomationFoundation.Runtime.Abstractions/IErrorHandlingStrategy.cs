namespace AutomationFoundation.Runtime;

/// <summary>
/// Identifies a strategy for handling errors.
/// </summary>
public interface IErrorHandlingStrategy
{
    /// <summary>
    /// Handles the error.
    /// </summary>
    /// <param name="context">The context containing error information.</param>
    void Handle(ErrorHandlingContext context);
}