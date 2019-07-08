﻿using System;
using System.Collections.Generic;
using AutomationFoundation.Hosting.Abstractions.Builder;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Hosting.Builder
{
    internal class DefaultRuntimeBuilder : IRuntimeBuilder
    {
        private readonly IList<IBuilder<Processor>> processorBuilders = new List<IBuilder<Processor>>();
        private readonly IList<Processor> processors = new List<Processor>();

        public IServiceProvider ApplicationServices { get; }

        public DefaultRuntimeBuilder(IServiceProvider applicationServices)
        {
            ApplicationServices = applicationServices ?? throw new ArgumentNullException(nameof(applicationServices));
        }

        public void RegisterProcessor(Processor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            if (processors.Contains(processor))
            {
                throw new ArgumentException("The processor has already been added.", nameof(processor));
            }

            processors.Add(processor);
        }

        public IRuntimeBuilder RegisterProcessor<T>(IProcessorBuilder<T> builder) where T : Processor
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (processorBuilders.Contains(builder))
            {
                throw new ArgumentException("The builder has already been added.", nameof(builder));
            }

            processorBuilders.Add(builder);
            return this;
        }

        public IRuntime Build()
        {
            AutomationRuntime runtime = null;

            try
            {
                runtime = new AutomationRuntime();

                foreach (var processorBuilder in processorBuilders)
                {
                    var processor = processorBuilder.Build();
                    if (processor == null)
                    {
                        throw new InvalidOperationException("The processor was not built.");
                    }

                    runtime.Processors.Add(processor);
                }

                foreach (var processor in processors)
                {
                    runtime.Processors.Add(processor);
                }

                return runtime;
            }
            catch (Exception)
            {
                runtime?.Dispose();
                throw;
            }
        }
    }
}