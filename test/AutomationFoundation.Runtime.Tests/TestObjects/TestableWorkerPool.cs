using System;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading;

namespace AutomationFoundation.Runtime.TestObjects;

internal class TestableWorkerPool : WorkerPool
{
    public Action<IWorker> OnCreatePooledWorkerCallback { get; set; }
    public Func<IRuntimeWorker, IRuntimeWorker> OnBaseCreatePooledWorkerCallback { get; set; }

    public TestableWorkerPool(IWorkerCache cache, IWorkerCacheMonitor cacheMonitor)
        : base(cache, cacheMonitor)
    {
    }

    protected override IWorker CreatePooledWorker(IRuntimeWorker worker)
    {
        OnCreatePooledWorkerCallback?.Invoke(worker);

        return base.CreatePooledWorker(OnBaseCreatePooledWorkerCallback(worker));
    }
}