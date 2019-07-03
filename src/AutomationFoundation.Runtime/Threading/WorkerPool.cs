﻿using System;
using AutomationFoundation.Runtime.Abstractions.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Internal;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;
using AutomationFoundation.Runtime.Threading.Internal;
using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading
{
    /// <summary>
    /// Provides a pool of workers.
    /// </summary>
    public class WorkerPool : DisposableObject, IWorkerPool
    {
        private readonly IWorkerCacheMonitor cacheMonitor;
        private readonly IWorkerCache cache;

        /// <summary>
        /// Creates a new worker pool.
        /// </summary>
        /// <returns>The worker pool.</returns>
        public static WorkerPool Create()
        {
            return Create(new WorkerPoolOptions
            {
                PollingInterval = TimeSpan.FromMinutes(1),
                MaximumDuration = TimeSpan.FromHours(1),
                Duration = TimeSpan.FromMinutes(5)
            });
        }

        /// <summary>
        /// Creates a new worker pool.
        /// </summary>
        /// <param name="options">The options to use when creating the pool.</param>
        /// <returns>The worker pool.</returns>
        public static WorkerPool Create(WorkerPoolOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            else if (options.Duration <= TimeSpan.Zero)
            {
                throw new ArgumentException("The options must have a duration of greater than zero.", nameof(options));
            }
            else if (options.MaximumDuration <= TimeSpan.Zero || options.MaximumDuration < options.Duration)
            {
                throw new ArgumentException("The options must have a maximum duration of greater than zero, and must be greater than or equal to duration.", nameof(options));
            }
            else if (options.PollingInterval <= TimeSpan.Zero)
            {
                throw new ArgumentException("The options must have a polling interval of greater than zero.", nameof(options));
            }

            Timer timer = null;
            WorkerCache cache = null;
            WorkerCacheMonitor cacheMonitor = null;

            try
            {
                timer = new Timer();

                cache = new WorkerCache();
                cacheMonitor = new WorkerCacheMonitor(
                    timer,
                    cache,
                    options);

                cacheMonitor.Start();

                return new WorkerPool(cache, cacheMonitor);
            }
            catch (Exception)
            {
                timer?.Dispose();
                cache?.Dispose();
                cacheMonitor?.Dispose();

                throw;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerPool"/> class.
        /// </summary>
        /// <param name="cache">The cache of workers available for the pool.</param>
        /// <param name="cacheMonitor">The monitor which will track the cache.</param>
        internal WorkerPool(IWorkerCache cache, IWorkerCacheMonitor cacheMonitor)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.cacheMonitor = cacheMonitor ?? throw new ArgumentNullException(nameof(cacheMonitor));
        }

        /// <inheritdoc />
        public IWorker Get(Action onRunCallback, Action postCompletedCallback)
        {
            if (onRunCallback == null)
            {
                throw new ArgumentNullException(nameof(onRunCallback));
            }

            IRuntimeWorker worker = null;

            try
            {
                worker = cache.Get();
                if (worker != null)
                {
                    try
                    {
                        worker.Initialize(new WorkerExecutionContext
                        {
                            OnRunCallback = onRunCallback,
                            PostCompletedCallback = postCompletedCallback
                        });

                        return CreatePooledWorker(worker);
                    }
                    catch (Exception)
                    {
                        worker.Reset();
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                if (worker != null)
                {
                    cache.Release(worker);
                }

                throw;
            }

            return null;
        }

        /// <summary>
        /// Creates a pooled worker.
        /// </summary>
        /// <param name="worker">The worker which is being managed by the pool.</param>
        /// <returns>The pooled worker instance.</returns>
        protected virtual IWorker CreatePooledWorker(IRuntimeWorker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            return new PooledWorker(cache, worker);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cache.DisposeIfNecessary();
                cacheMonitor.DisposeIfNecessary();
            }

            base.Dispose(disposing);
        }
    }
}