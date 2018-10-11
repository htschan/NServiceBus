namespace NServiceBus
{
    using System;

    class RootContext : BehaviorContext
    {
        public RootContext(IServiceProvider builder, IPipelineCache pipelineCache, IEventAggregator eventAggregator) : base(null)
        {
            Set(builder);
            Set(pipelineCache);
            Set(eventAggregator);
        }
    }
}