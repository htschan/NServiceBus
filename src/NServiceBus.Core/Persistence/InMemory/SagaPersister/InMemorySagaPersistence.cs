namespace NServiceBus.Features
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Used to configure in memory saga persistence.
    /// </summary>
    public class InMemorySagaPersistence : Feature
    {
        internal InMemorySagaPersistence()
        {
            DependsOn<Sagas>();
            Defaults(s => s.EnableFeature(typeof(InMemoryTransactionalStorageFeature)));
        }

        /// <summary>
        /// See <see cref="Feature.Setup" />.
        /// </summary>
        protected internal override void Setup(FeatureConfigurationContext context)
        {
            context.Container.AddSingleton<InMemorySagaPersister>();
        }
    }
}