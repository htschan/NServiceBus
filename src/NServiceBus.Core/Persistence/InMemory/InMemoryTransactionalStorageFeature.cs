namespace NServiceBus
{
    using Features;
    using Microsoft.Extensions.DependencyInjection;

    class InMemoryTransactionalStorageFeature : Feature
    {
        /// <summary>
        /// Called when the features is activated.
        /// </summary>
        protected internal override void Setup(FeatureConfigurationContext context)
        {
            context.Container.AddSingleton<InMemorySynchronizedStorage>();
            context.Container.AddSingleton<InMemoryTransactionalSynchronizedStorageAdapter>();
        }
    }
}