using Autofac;
using AutomationFoundation.Hosting;
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