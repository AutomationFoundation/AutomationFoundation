﻿using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects
{
    public class StubConsumer : IConsumer<object>
    {
        public Task ConsumeAsync(IProducerConsumerContext<object> context)
        {
            throw new System.NotImplementedException();
        }
    }
}