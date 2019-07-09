namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Defines the severity levels of an error.
    /// </summary>
    public enum ErrorSeverityLevel
    {
        /// <summary>
        /// The error is non-fatal.
        /// </summary>
        NonFatal,
        
        /// <summary>
        /// The error is fatal.
        /// </summary>
        Fatal
    }

    /// <summary>
    /// Defines the processor states.
    /// </summary>
    public enum ProcessorState
    {
        /// <summary>
        /// The processor has encountered an error.
        /// </summary>
        Error = -1,

        /// <summary>
        /// The processor has been created.
        /// </summary>
        Created = 0,

        /// <summary>
        /// The processor is stopping.
        /// </summary>
        Stopping,

        /// <summary>
        /// The processor has stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// The processor is starting.
        /// </summary>
        Starting,

        /// <summary>
        /// The processor has started.
        /// </summary>
        Started,

        /// <summary>
        /// The processor is busy.
        /// </summary>
        Busy
    }
}