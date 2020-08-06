﻿using System;
using AutomationFoundation.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation
{
    [TestFixture]
    public class StartupExtensionsTests
    {
        private Mock<IStartup> startup;
        private IServiceCollection services;

        [SetUp]
        public void Init()
        {
            startup = new Mock<IStartup>();
            services = new ServiceCollection();
        }

        [Test]
        public void ThrowsAnExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => startup.Object.TryConfigureContainer(null));
        }

        [Test]
        public void MustCallConfigureServicesWhenImplementsIConfigureServices()
        {
            var configureServices = startup.As<IConfigureServices>();

            var target = startup.Object;
            target.TryConfigureContainer(services);

            configureServices.Verify(o => o.ConfigureServices(services));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheServiceProviderFactoryIsNotRegistered()
        {
            startup.As<IConfigureContainer<object>>();

            var target = startup.Object;
            Assert.Throws<InvalidOperationException>(() => target.TryConfigureContainer(services));
        }

        [Test]
        public void MustConfigureContainerWhenImplementsIConfigureContainer()
        {
            var containerBuilder = new object();

            var factory = new Mock<IServiceProviderFactory<object>>();
            factory.Setup(o => o.CreateBuilder(services)).Returns(containerBuilder).Verifiable();
         
            services.AddTransient(sp => factory.Object);

            var configureContainer = startup.As<IConfigureContainer<object>>();

            var target = startup.Object;
            target.TryConfigureContainer(services);

            configureContainer.Verify(o => o.ConfigureContainer(containerBuilder));
            factory.Verify(o => o.CreateServiceProvider(containerBuilder));
            factory.Verify();
        }
    }
}