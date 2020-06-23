using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime;

namespace ConsoleRunner.Infrastructure.WorkProcessors
{
    public class TestProcessor : Processor
    {
        private readonly Monitor monitor;

        private CancellationTokenSource cancellationSource;
        private Task runner;

        public TestProcessor(string name) 
            : base(name)
        {
            monitor = new Monitor(name);
        }

        protected override Task OnStartAsync(CancellationToken cancellationToken)
        {
            cancellationSource?.Dispose();
            cancellationSource = new CancellationTokenSource();

            var task = new Task<Task>(async () => await RunAsync(cancellationSource.Token), cancellationToken,
                TaskCreationOptions.LongRunning);
            task.Start();

            runner = task.Unwrap();

            monitor.Start();
            return Task.CompletedTask;
        }

        private Task RunAsync(CancellationToken runningToken)
        {
            while (!runningToken.IsCancellationRequested)
            {
                monitor.Increment();
            }

            return Task.CompletedTask;
        }

        protected override async Task OnStopAsync(CancellationToken cancellationToken)
        {
            cancellationSource.Cancel();

            await runner;

            monitor.Stop();
        }
    }
}