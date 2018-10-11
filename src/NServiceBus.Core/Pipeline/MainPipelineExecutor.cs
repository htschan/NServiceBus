namespace NServiceBus
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Pipeline;
    using Transport;

    class MainPipelineExecutor : IPipelineExecutor
    {
        public MainPipelineExecutor(IServiceProvider builder, IEventAggregator eventAggregator, IPipelineCache pipelineCache, IPipeline<ITransportReceiveContext> mainPipeline)
        {
            this.mainPipeline = mainPipeline;
            this.pipelineCache = pipelineCache;
            this.builder = builder;
            this.eventAggregator = eventAggregator;
        }

        public async Task Invoke(MessageContext messageContext)
        {
            var pipelineStartedAt = DateTime.UtcNow;

            using (var serviceScope = builder.CreateScope())
            {
                var rootContext = new RootContext(serviceScope.ServiceProvider, pipelineCache, eventAggregator);

                var message = new IncomingMessage(messageContext.MessageId, messageContext.Headers, messageContext.Body);
                var context = new TransportReceiveContext(message, messageContext.TransportTransaction, messageContext.ReceiveCancellationTokenSource, rootContext);

                context.Extensions.Merge(messageContext.Extensions);

                await mainPipeline.Invoke(context).ConfigureAwait(false);

                await context.RaiseNotification(new ReceivePipelineCompleted(message, pipelineStartedAt, DateTime.UtcNow)).ConfigureAwait(false);
            }
        }

        IEventAggregator eventAggregator;
        IServiceProvider builder;
        IPipelineCache pipelineCache;
        IPipeline<ITransportReceiveContext> mainPipeline;
    }
}