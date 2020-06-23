using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Builders;
using ConsoleRunner.Abstractions;
using ConsoleRunner.Model;

namespace ConsoleRunner.Infrastructure.WorkProcessors
{
    public class TestProcessorBuilder : IApplicationProcessorBuilder
    {
        public Processor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config)
        {
            return new TestProcessor(config.Name);
        }
    }
}