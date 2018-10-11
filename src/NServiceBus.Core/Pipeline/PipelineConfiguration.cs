namespace NServiceBus
{
    using Microsoft.Extensions.DependencyInjection;

    class PipelineConfiguration
    {
        public void RegisterBehaviorsInContainer(IServiceCollection container)
        {
            foreach (var registeredBehavior in Modifications.Replacements)
            {
                container.AddTransient(registeredBehavior.BehaviorType);
            }

            foreach (var step in Modifications.Additions)
            {
                step.ApplyContainerRegistration(container);
            }
        }

        public PipelineModifications Modifications = new PipelineModifications();
    }
}