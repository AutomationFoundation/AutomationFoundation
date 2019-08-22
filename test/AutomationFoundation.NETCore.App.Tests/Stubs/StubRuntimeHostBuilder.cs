﻿using System;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.NETCore.App.Tests.Stubs
{
    public class StubRuntimeHostBuilder : IRuntimeHostBuilder
    {

        public IRuntimeHost Build()
        {
            throw new NotImplementedException();
        }

        public IRuntimeHostBuilder ConfigureServices(Action<IServiceCollection> callback)
        {
            throw new NotImplementedException();
        }

        public IRuntimeHostBuilder UseStartup<TStartup>() where TStartup : IStartup
        {
            throw new NotImplementedException();
        }

        public IRuntimeHostBuilder UseStartup(IStartup startup)
        {
            throw new NotImplementedException();
        }
    }
}