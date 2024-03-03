using System;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Runtime.TestObjects;

public class StubProcessingContext : ProcessingContext
{
    public StubProcessingContext(Guid id, IServiceScope serviceScope) : base(id, serviceScope)
    {
    }
}