using System;
using AutomationFoundation.Runtime;

namespace AutomationFoundation.Hosting.TestObjects
{
    public class StubStartup : IStartup
    {
        private readonly Action<IRuntimeBuilder> onConfigureProcessorsCallback;

        public StubStartup(Action<IRuntimeBuilder> onConfigureProcessorsCallback = null)
        {
            this.onConfigureProcessorsCallback = onConfigureProcessorsCallback;
        }

        public void ConfigureProcessors(IRuntimeBuilder runtimeBuilder, IHostingEnvironment environment)
        {
            onConfigureProcessorsCallback?.Invoke(runtimeBuilder);
        }
    }
}
