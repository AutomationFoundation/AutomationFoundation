using System;
using Microsoft.Extensions.DependencyInjection;

/* Unmerged change from project 'AutomationFoundation.Runtime.Tests(net472)'
Before:
namespace AutomationFoundation.Runtime.TestObjects
{
    public class StubProcessingContext : ProcessingContext
    {
        public StubProcessingContext(Guid id, IServiceScope serviceScope) : base(id, serviceScope)
        {
        }
After:
namespace AutomationFoundation.Runtime.TestObjects;

public class StubProcessingContext : ProcessingContext
{
    public StubProcessingContext(Guid id, IServiceScope serviceScope) : base(id, serviceScope)
    {
*/

namespace AutomationFoundation.Runtime.TestObjects;

public class StubProcessingContext : ProcessingContext
{
    public StubProcessingContext(Guid id, IServiceScope serviceScope) : base(id, serviceScope)
    {
    }
}