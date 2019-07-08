using AutomationFoundation;
using AutomationFoundation.Hosting.Abstractions.Builder;
using AutomationFoundation.Runtime;
using ConsoleRunner.Model;

namespace ConsoleRunner.Abstractions
{
    public interface IProcessorBuilder
    {
        Processor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config);
    }
}