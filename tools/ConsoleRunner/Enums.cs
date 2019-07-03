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

    public enum ProcessorTypeEnum
    {
        ProducerConsumer,
        ScheduledJob,
        Task
    }
}