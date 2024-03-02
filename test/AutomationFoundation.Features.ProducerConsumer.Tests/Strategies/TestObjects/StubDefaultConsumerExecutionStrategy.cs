using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

/* Unmerged change from project 'AutomationFoundation.Features.ProducerConsumer.Tests(net472)'
Before:
namespace AutomationFoundation.Features.ProducerConsumer.Strategies.TestObjects
{
    public class StubDefaultConsumerExecutionStrategy<TItem> : DefaultConsumerExecutionStrategy<TItem>
    {
        private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onStartedCallback;
        private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onConsumeCallback;
        private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onCompletedCallback;
        private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onExitCallback;

        private IProducerConsumerContext<TItem> overrideContext;
        private bool isContextOverridden;

        public StubDefaultConsumerExecutionStrategy(IConsumerResolver<TItem> consumerFactory, Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onStartedCallback = null, Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onConsumeCallback = null,
            Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onCompletedCallback = null,
            Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onExitCallback = null)
            : base(consumerFactory)
        {
            this.onStartedCallback = onStartedCallback;
            this.onConsumeCallback = onConsumeCallback;
            this.onCompletedCallback = onCompletedCallback;
            this.onExitCallback = onExitCallback;
        }

        public void SetOverrideContext(IProducerConsumerContext<TItem> context)
        {
            overrideContext = context;
            isContextOverridden = true;
        }

        private IProducerConsumerContext<TItem> GetContext(IProducerConsumerContext<TItem> context)
        {
            if (isContextOverridden)
            {
                return overrideContext;
            }

            return context;
        }

        protected override void OnStarted(IProducerConsumerContext<TItem> context)
        {
            var c = GetContext(context);

            base.OnStarted(c);
            onStartedCallback?.Invoke(this, c);
        }

        protected override void CreateConsumer(IProducerConsumerContext<TItem> context)
        {
            var c = GetContext(context);

            base.CreateConsumer(c);
        }

        protected override async Task ConsumeAsync(IProducerConsumerContext<TItem> context)
        {
            var c = GetContext(context);

            await base.ConsumeAsync(c);
            onConsumeCallback?.Invoke(this, c);
        }

        protected override void OnCompleted(IProducerConsumerContext<TItem> context)
        {
            var c = GetContext(context);

            base.OnCompleted(c);
            onCompletedCallback?.Invoke(this, c);
        }

        protected override void OnExit(IProducerConsumerContext<TItem> context)
        {
            var c = GetContext(context);

            base.OnExit(c);
            onExitCallback?.Invoke(this, c);
        }
After:
namespace AutomationFoundation.Features.ProducerConsumer.Strategies.TestObjects;

public class StubDefaultConsumerExecutionStrategy<TItem> : DefaultConsumerExecutionStrategy<TItem>
{
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onStartedCallback;
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onConsumeCallback;
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onCompletedCallback;
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onExitCallback;

    private IProducerConsumerContext<TItem> overrideContext;
    private bool isContextOverridden;

    public StubDefaultConsumerExecutionStrategy(IConsumerResolver<TItem> consumerFactory, Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onStartedCallback = null, Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onConsumeCallback = null,
        Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onCompletedCallback = null,
        Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onExitCallback = null)
        : base(consumerFactory)
    {
        this.onStartedCallback = onStartedCallback;
        this.onConsumeCallback = onConsumeCallback;
        this.onCompletedCallback = onCompletedCallback;
        this.onExitCallback = onExitCallback;
    }

    public void SetOverrideContext(IProducerConsumerContext<TItem> context)
    {
        overrideContext = context;
        isContextOverridden = true;
    }

    private IProducerConsumerContext<TItem> GetContext(IProducerConsumerContext<TItem> context)
    {
        if (isContextOverridden)
        {
            return overrideContext;
        }

        return context;
    }

    protected override void OnStarted(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.OnStarted(c);
        onStartedCallback?.Invoke(this, c);
    }

    protected override void CreateConsumer(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.CreateConsumer(c);
    }

    protected override async Task ConsumeAsync(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        await base.ConsumeAsync(c);
        onConsumeCallback?.Invoke(this, c);
    }

    protected override void OnCompleted(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.OnCompleted(c);
        onCompletedCallback?.Invoke(this, c);
    }

    protected override void OnExit(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.OnExit(c);
        onExitCallback?.Invoke(this, c);
*/

namespace AutomationFoundation.Features.ProducerConsumer.Strategies.TestObjects;

public class StubDefaultConsumerExecutionStrategy<TItem> : DefaultConsumerExecutionStrategy<TItem>
{
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onStartedCallback;
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onConsumeCallback;
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onCompletedCallback;
    private readonly Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onExitCallback;

    private IProducerConsumerContext<TItem> overrideContext;
    private bool isContextOverridden;

    public StubDefaultConsumerExecutionStrategy(IConsumerResolver<TItem> consumerFactory, Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onStartedCallback = null, Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onConsumeCallback = null,
        Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onCompletedCallback = null,
        Action<StubDefaultConsumerExecutionStrategy<TItem>, IProducerConsumerContext<TItem>> onExitCallback = null)
        : base(consumerFactory)
    {
        this.onStartedCallback = onStartedCallback;
        this.onConsumeCallback = onConsumeCallback;
        this.onCompletedCallback = onCompletedCallback;
        this.onExitCallback = onExitCallback;
    }

    public void SetOverrideContext(IProducerConsumerContext<TItem> context)
    {
        overrideContext = context;
        isContextOverridden = true;
    }

    private IProducerConsumerContext<TItem> GetContext(IProducerConsumerContext<TItem> context)
    {
        if (isContextOverridden)
        {
            return overrideContext;
        }

        return context;
    }

    protected override void OnStarted(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.OnStarted(c);
        onStartedCallback?.Invoke(this, c);
    }

    protected override void CreateConsumer(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.CreateConsumer(c);
    }

    protected override async Task ConsumeAsync(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        await base.ConsumeAsync(c);
        onConsumeCallback?.Invoke(this, c);
    }

    protected override void OnCompleted(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.OnCompleted(c);
        onCompletedCallback?.Invoke(this, c);
    }

    protected override void OnExit(IProducerConsumerContext<TItem> context)
    {
        var c = GetContext(context);

        base.OnExit(c);
        onExitCallback?.Invoke(this, c);
    }
}