using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Builders;
using ConsoleRunner.Model;

namespace ConsoleRunner.Abstractions;

public interface IApplicationProcessorBuilder
{
    Processor Build(IRuntimeBuilder runtimeBuilder, AppProcessor config);
}