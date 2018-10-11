namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// 
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="typeToBuild"></param>
        /// <returns></returns>
        public static object Build(this IServiceProvider serviceProvider, Type typeToBuild)
        {
            return serviceProvider.GetService(typeToBuild);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static T Build<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IEnumerable<T> BuildAll<T>(this IServiceProvider serviceProvider)
        {
            var result = serviceProvider.GetServices(typeof(T));

            return result.Cast<T>();
        }
    }
}