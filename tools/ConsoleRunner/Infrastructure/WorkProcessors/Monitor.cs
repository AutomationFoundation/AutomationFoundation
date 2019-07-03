using System;
using System.Threading;
using ConsoleRunner.Abstractions;

namespace ConsoleRunner.Infrastructure.WorkProcessors
{
    public class Monitor
    {
        private readonly TimeSpan ThirtySeconds = new TimeSpan(0, 0, 0, 30);

        private readonly Timer timer;
        private readonly IConsoleWriter writer;
        private readonly string name;
        private int count;

        public Monitor(string name, IConsoleWriter writer)
        {
            this.timer = new Timer(OnTimerCallback);
            this.name = name;
            this.writer = writer;
        }

        public void Start()
        {
            timer.Change(ThirtySeconds, ThirtySeconds);
        }

        public void Stop()
        {
            timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);        
        }

        private void OnTimerCallback(object state)
        {
            var temp = count;
            Reset();

            var itemsPerSecond = temp / ThirtySeconds.TotalSeconds;
            writer.WriteLine($"{DateTime.Now:hh:mm:ss}: {itemsPerSecond}/sec");
        }

        public void Increment()
        {
            Interlocked.Increment(ref count);
        }

        private void Reset()
        {
            Interlocked.Exchange(ref count, 0);
        }
    }
}