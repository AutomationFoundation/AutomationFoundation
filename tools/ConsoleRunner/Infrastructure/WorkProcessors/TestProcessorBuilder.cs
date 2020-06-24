using AutomationFoundation.Runtime;
using ConsoleRunner.Abstractions;
using ConsoleRunner.Model;

namespace ConsoleRunner.Infrastructure.WorkProcessors
{
    public class TestProcessorBuilder : IApplicationProcessorBuilder
    {
        public IProcessor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config)
        {
            return new TestProcessor(config.Name);
        }
    }
}