using System;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Tests.Stubs
{
    public class StubWorker : Worker
    {
        public Action<TaskCreationOptions> ValidateCreationOptionsCallback { get; set; }

        public new bool Disposed => base.Disposed;

        protected override TaskCreationOptions DetermineCreationOptions(WorkerExecutionContext context)
        {
            var result = base.DetermineCreationOptions(context);
            ValidateCreationOptionsCallback(result);

            return result;
        }
    }
}