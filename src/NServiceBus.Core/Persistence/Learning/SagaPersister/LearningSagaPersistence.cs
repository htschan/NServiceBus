namespace NServiceBus.Features
{
    using System;
    using System.IO;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus.Sagas;

    class LearningSagaPersistence : Feature
    {
        internal LearningSagaPersistence()
        {
            DependsOn<Sagas>();
            Defaults(s => s.Set<ISagaIdGenerator>(new LearningSagaIdGenerator()));
            Defaults(s => s.SetDefault(StorageLocationKey, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".sagas")));
        }

        protected internal override void Setup(FeatureConfigurationContext context)
        {
            var storageLocation = context.Settings.Get<string>(StorageLocationKey);

            var allSagas = context.Settings.Get<SagaMetadataCollection>();

            var sagaManifests = new SagaManifestCollection(allSagas, storageLocation);

            context.Container.AddSingleton(b => new LearningSynchronizedStorage(sagaManifests));
            context.Container.AddSingleton<LearningStorageAdapter>();

            context.Container.AddSingleton(b => new LearningSagaPersister());
        }

        internal static string StorageLocationKey = "LearningSagaPersistence.StorageLocation";
    }
}