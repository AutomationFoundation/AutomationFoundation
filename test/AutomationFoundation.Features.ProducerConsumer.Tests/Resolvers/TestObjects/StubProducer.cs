/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects
{
    public class StubProducer : IProducer<object>
    {
        public Task<object> ProduceAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
After:
namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects;

public class StubProducer : IProducer<object>
{
    public Task<object> ProduceAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
*/
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer.Resolvers.TestObjects;

public class StubProducer : IProducer<object>
{
    public Task<object> ProduceAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}