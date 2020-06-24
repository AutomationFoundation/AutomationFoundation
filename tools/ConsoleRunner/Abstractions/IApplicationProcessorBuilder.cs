using AutomationFoundation;
using AutomationFoundation.Runtime;
using ConsoleRunner.Model;

namespace ConsoleRunner.Abstractions
{
    public interface IApplicationProcessorBuilder
    {
        IProcessor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config);
    }
}