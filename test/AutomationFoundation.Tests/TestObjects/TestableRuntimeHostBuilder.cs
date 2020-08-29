using System;
using AutomationFoundation.Hosting;
using AutomationFoundation.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.TestObjects
{
    internal class TestableRuntimeHostBuilder : RuntimeHostBuilder
    {
        private readonly Func<IServiceCollection> serviceCollection;

        public Func<IServiceProvider, IStartup> StartupResolver { get; set; }
        public Func<IStartup, IServiceCollection, IServiceProvider> ApplicationServicesResolver { get; set; }
        public Func<IServiceProvider, IRuntimeBuilder> RuntimeBuilderResolver { get; set; }

        public TestableRuntimeHostBuilder(Func<IServiceCollection> serviceCollection = null)
        {
            this.serviceCollection = serviceCollection;
        }

        protected override IStartup ResolveStartupInstance(IServiceProvider serviceProvider)
        {
            if (StartupResolver != null)
            {
                return StartupResolver(serviceProvider);
            }

            return base.ResolveStartupInstance(serviceProvider);
        }

        protected override IServiceProvider ConfigureApplicationServices(IStartup startup, IServiceCollection serviceCollection)
        {
            if (ApplicationServicesResolver != null)
            {
                return ApplicationServicesResolver(startup, serviceCollection);
            }

            return base.ConfigureApplicationServices(startup, serviceCollection);
        }

        protected override IRuntimeBuilder CreateRuntimeBuilder(IServiceProvider applicationServices)
        {
            if (RuntimeBuilderResolver != null)
            {
                return RuntimeBuilderResolver(applicationServices);
            }

            return base.CreateRuntimeBuilder(applicationServices);
        }

        protected override IRuntimeHost CreateRuntimeHost(IRuntime runtime, IHostingEnvironment environment, IServiceProvider applicationServices)
        {
            throw new NotImplementedException();
        }

        protected override IServiceCollection CreateServiceCollection()
        {
            if (serviceCollection != null)
            {
                return serviceCollection();
            }

            return base.CreateServiceCollection();
        }
    }
}