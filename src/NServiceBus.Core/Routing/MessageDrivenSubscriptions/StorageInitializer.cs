namespace NServiceBus
{
    using System.Threading.Tasks;
    using Features;
    using Microsoft.Extensions.DependencyInjection;
    using Unicast.Subscriptions.MessageDrivenSubscriptions;

    class StorageInitializer : Feature
    {
        public StorageInitializer()
        {
            EnableByDefault();
        }

        protected internal override void Setup(FeatureConfigurationContext context)
        {
            context.Container.AddSingleton<CallInit>();

            context.RegisterStartupTask(b => b.Build<CallInit>());
        }

        class CallInit : FeatureStartupTask
        {
            public IInitializableSubscriptionStorage SubscriptionStorage { get; set; }

            protected override Task OnStart(IMessageSession session)
            {
                SubscriptionStorage?.Init();
                return TaskEx.CompletedTask;
            }

            protected override Task OnStop(IMessageSession session)
            {
                return TaskEx.CompletedTask;
            }
        }
    }
}