using AutomationFoundation.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.TestObjects
{
    public class TestableStartup : IStartup, IConfigureServices, IConfigureContainer<object>
    {
        public bool ConfiguredProcessors { get; private set; }
        public bool ConfiguredServices { get; private set; }
        public bool ConfiguredContainer { get; private set; }

        public void ConfigureProcessors(IRuntimeBuilder runtimeBuilder, IHostingEnvironment environment)
        {
            ConfiguredProcessors = true;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfiguredServices = true;
        }

        public void ConfigureContainer(object builder)
        {
            ConfiguredContainer = true;
        }
    }
}