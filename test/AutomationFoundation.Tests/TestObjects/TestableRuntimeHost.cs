using System;
using AutomationFoundation.Hosting;
using AutomationFoundation.Runtime;

namespace AutomationFoundation.TestObjects
{
    public class TestableRuntimeHost : RuntimeHostBase
    {
        public TestableRuntimeHost(IRuntime runtime, IHostingEnvironment environment, IServiceProvider applicationServices) 
            : base(runtime, environment, applicationServices)
        {
        }
    }
}