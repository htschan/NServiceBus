namespace NServiceBus.Features
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    class CustomIDataBus : Feature
    {
        public CustomIDataBus()
        {
            DependsOn<DataBus>();
        }

        protected internal override void Setup(FeatureConfigurationContext context)
        {
            context.Container.AddSingleton(context.Settings.Get<Type>("CustomDataBusType"));
        }
    }
}