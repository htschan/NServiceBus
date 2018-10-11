namespace NServiceBus
{
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// 
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this IServiceCollection serviceCollection)
        {
            return serviceCollection.Any(d => d.ServiceType == typeof(T));
        }
    }
}