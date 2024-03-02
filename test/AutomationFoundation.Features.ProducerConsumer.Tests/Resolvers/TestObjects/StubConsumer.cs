/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects
{
    public class StubConsumer : IConsumer<object>
    {
        public Task ConsumeAsync(IProducerConsumerContext<object> context)
        {
            throw new System.NotImplementedException();
        }
After:
namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects;

public class StubConsumer : IConsumer<object>
{
    public Task ConsumeAsync(IProducerConsumerContext<object> context)
    {
        throw new System.NotImplementedException();
*/
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects;

public class StubConsumer : IConsumer<object>
{
    public Task ConsumeAsync(IProducerConsumerContext<object> context)
    {
        throw new System.NotImplementedException();
    }
}