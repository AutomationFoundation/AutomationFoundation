using System;
using AutomationFoundation.Hosting;
using AutomationFoundation.Hosting.Abstractions.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.NETCore.App.Hosting
{
    [TestFixture]
    public class RuntimeHostBuilderExtensionsTests
    {
        [Test]
        public void ThrowAnExceptionWhenTheBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => RuntimeHostBuilderExtensions
                .ConfigureAppConfiguration(null, b => { }));
        }

        [Test]
        public void ThrowAnExceptionWhenCallbackIsNull()
        {
            var runtimeHostBuilder = new Mock<IRuntimeHostBuilder>();

            Assert.Throws<ArgumentNullException>(() => runtimeHostBuilder.Object
                .ConfigureAppConfiguration(null));
        }
        
        [Test]
        public void RegisterTheConfigurationServiceCallbackAsExpected()
        {
            var target = new Mock<IRuntimeHostBuilder>();
            target.Object.ConfigureAppConfiguration(services => { });

            target.Verify(o => o.ConfigureServices(It.IsAny<Action<IServiceCollection>>()), Times.Once);
        }
    }
}