using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Builders;
using ConsoleRunner.Model;

namespace ConsoleRunner.Abstractions
{
    public interface IApplicationProcessorBuilder
    {
        IProcessor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config);
    }
}