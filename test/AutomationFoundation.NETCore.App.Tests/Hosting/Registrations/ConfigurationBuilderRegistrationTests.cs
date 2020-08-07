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
        private Mock<IConfigurationBuilder> builder;
        private Mock<IServiceCollection> services;

        private ConfigurationBuilderRegistration target;

        [SetUp]
        public void Init()
        {
            builder = new Mock<IConfigurationBuilder>();
            services = new Mock<IServiceCollection>();

            target = new ConfigurationBuilderRegistration(builder.Object, services.Object, _ => { });
        }

        [Test]
        public void ThrowsAnExceptionWhenBuilderIsNullForConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _ = new ConfigurationBuilderRegistration(null, services.Object, _ => { }));
        }

        [Test]
        public void ThrowsAnExceptionWhenServicesIsNullForConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _ = new ConfigurationBuilderRegistration(builder.Object, null, _ => { }));
        }

        [Test]
        public void ThrowsAnExceptionWhenCallbackIsNullForConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new ConfigurationBuilderRegistration(builder.Object, services.Object, null));
        }

        [Test]
        public void ThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConfigurationBuilderRegistration.OnConfigureServicesCallback(null, c => { }));
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
            ConfigurationBuilderRegistration.OnConfigureServicesCallback(services.Object, c => { called = true; });

            Assert.True(called);
        }

        [Test]
        public void ThrowsExceptionWhenConfigurationIsNull()
        {
            Assert.Throws<BuildException>(() => target.Register());
            builder.Verify(o => o.Build(), Times.Once);
        }

        [Test]
        public void RegistersTheConfigurationAsSingleton()
        {
            var configuration = new Mock<IConfigurationRoot>();
            builder.Setup(o => o.Build()).Returns(configuration.Object);

            target.Register();

            builder.Verify(o => o.Build(), Times.Once);
        }
    }
}