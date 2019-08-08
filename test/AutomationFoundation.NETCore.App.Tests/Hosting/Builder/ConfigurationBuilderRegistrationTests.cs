using System;
using AutomationFoundation.Hosting.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.NETCore.App.Tests.Hosting.Builder
{
    [TestFixture]
    public class ConfigurationBuilderRegistrationTests
    {
        [Test]
        public void ThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConfigurationBuilderRegistration.Build(null, builder => { }));
        }

        [Test]
        public void ThrowsExceptionWhenCallbackIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => ConfigurationBuilderRegistration.Build(new Mock<IServiceCollection>().Object, null));
        }

        [Test]
        public void BuildsTheConfigurationAsExpected()
        {
            var services = new Mock<IServiceCollection>();

            ConfigurationBuilderRegistration.Build(services.Object, Assert.NotNull);
        }
    }
}