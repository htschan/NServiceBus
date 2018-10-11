namespace NServiceBus.Features
{
    using System;
    using System.Threading.Tasks;

    class FeatureRunner
    {
        public FeatureRunner(FeatureActivator featureActivator)
        {
            this.featureActivator = featureActivator;
        }

        public Task Start(IServiceProvider builder, IMessageSession messageSession)
        {
            return featureActivator.StartFeatures(builder, messageSession);
        }

        public Task Stop(IMessageSession messageSession)
        {
            return featureActivator.StopFeatures(messageSession);
        }

        FeatureActivator featureActivator;
    }
}