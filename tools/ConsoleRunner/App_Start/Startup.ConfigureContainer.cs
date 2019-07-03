using Autofac;
using AutomationFoundation.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using ConsoleRunner.Infrastructure;

namespace ConsoleRunner
{
    internal partial class Startup : IConfigureServices, IConfigureContainer<ContainerBuilder>
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Transient));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ContainerBindings>();
        }
    }
}