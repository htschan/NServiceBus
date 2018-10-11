namespace NServiceBus.Features
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// In-memory Gateway.
    /// </summary>
    public class InMemoryGatewayPersistence : Feature
    {
        internal InMemoryGatewayPersistence()
        {
            DependsOn("NServiceBus.Features.Gateway");
        }

        /// <summary>
        /// See <see cref="Feature.Setup" />.
        /// </summary>
        protected internal override void Setup(FeatureConfigurationContext context)
        {
            context.Container.AddSingleton<InMemoryGatewayDeduplication>();
        }
    }
}