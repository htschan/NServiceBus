namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Pipeline;
    using Settings;

    class PipelineCache : IPipelineCache
    {
        public PipelineCache(IServiceProvider builder, ReadOnlySettings settings)
        {
            FromMainPipeline<IAuditContext>(builder, settings);
            FromMainPipeline<IDispatchContext>(builder, settings);
            FromMainPipeline<IOutgoingPublishContext>(builder, settings);
            FromMainPipeline<ISubscribeContext>(builder, settings);
            FromMainPipeline<IUnsubscribeContext>(builder, settings);
            FromMainPipeline<IOutgoingSendContext>(builder, settings);
            FromMainPipeline<IOutgoingReplyContext>(builder, settings);
            FromMainPipeline<IRoutingContext>(builder, settings);
            FromMainPipeline<IBatchDispatchContext>(builder, settings);
            FromMainPipeline<IForwardingContext>(builder, settings);
        }

        public IPipeline<TContext> Pipeline<TContext>()
            where TContext : IBehaviorContext
        {
            if (pipelines.TryGetValue(typeof(TContext), out var lazyPipeline))
            {
                return (IPipeline<TContext>) lazyPipeline.Value;
            }
            throw new InvalidOperationException("Custom pipelines are not supported.");
        }

        void FromMainPipeline<TContext>(IServiceProvider builder, ReadOnlySettings settings)
            where TContext : IBehaviorContext
        {
            var lazyPipeline = new Lazy<IPipeline>(() =>
            {
                var pipelinesCollection = settings.Get<PipelineConfiguration>();
                var pipeline = new Pipeline<TContext>(builder, pipelinesCollection.Modifications);
                return pipeline;
            }, LazyThreadSafetyMode.ExecutionAndPublication);
            pipelines.Add(typeof(TContext), lazyPipeline);
        }

        Dictionary<Type, Lazy<IPipeline>> pipelines = new Dictionary<Type, Lazy<IPipeline>>();
    }
}
