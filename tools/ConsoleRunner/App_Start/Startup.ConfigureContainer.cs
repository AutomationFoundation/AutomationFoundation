using Autofac;
using AutomationFoundation.Hosting.Abstractions;
using ConsoleRunner.Infrastructure;

namespace ConsoleRunner
{
    internal partial class Startup : IConfigureContainer<ContainerBuilder>
    { 
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ContainerBindings>();
        }
    }
}