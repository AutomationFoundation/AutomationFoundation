using System;
using AutomationFoundation.Hosting.Registrations;
using AutomationFoundation.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.NETCore.App.Hosting.Registrations
{
    [TestFixture]
    public class ConfigurationBuilderRegistrationTests
    {
        [Test]
        public void ThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConfigurationBuilderRegistration.OnConfigureServicesCallback(null, builder => { }));
        }

        [Test]
        public void ThrowsExceptionWhenCallbackIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConfigurationBuilderRegistration.OnConfigureServicesCallback(new Mock<IServiceCollection>().Object, null));
        }

        [Test]
        public void ExecutesTheCallbackAsExpected()
        {
            var called = false;
            var services = new Mock<IServiceCollection>();

            ConfigurationBuilderRegistration.OnConfigureServicesCallback(services.Object, builder => { called = true; });

            Assert.True(called);
        }

        [Test]
        public void ThrowsExceptionWhenConfigurationIsNull()
        {
            var builder = new Mock<IConfigurationBuilder>();
            var services = new Mock<IServiceCollection>();

            var target = new ConfigurationBuilderRegistration(builder.Object, services.Object, _ => { });

            Assert.Throws<BuildException>(() => target.Register());
            builder.Verify(o => o.Build(), Times.Once);
        }

        [Test]
        public void RegistersTheConfigurationAsSingleton()
        {
            var configuration = new Mock<IConfigurationRoot>();

            var builder = new Mock<IConfigurationBuilder>();
            builder.Setup(o => o.Build()).Returns(configuration.Object);

            var services = new Mock<IServiceCollection>();

            var target = new ConfigurationBuilderRegistration(builder.Object, services.Object, _ => { });
            target.Register();

            builder.Verify(o => o.Build(), Times.Once);
        }
    }
}