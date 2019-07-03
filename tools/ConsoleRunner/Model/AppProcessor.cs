namespace ConsoleRunner.Model
{
    public class AppProcessor
    {
        public long AppProcessorId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public ProcessorTypeEnum ProcessorType { get; set; }
        public int? ProducerCount { get; set; }
        public int? ConsumerCount { get; set; }
    }
}