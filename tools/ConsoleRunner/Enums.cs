namespace ConsoleRunner
{
    public enum LoggingLevel
    {
        Off,
        Critical,
        Error,
        Warning,
        Information,
        Debug,
        All
    }

    public enum ProcessorType
    {
        Test,
        ScheduledJob,
        Task
    }
}