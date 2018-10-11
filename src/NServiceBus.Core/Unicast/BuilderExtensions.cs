namespace NServiceBus.Unicast
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="IServiceProvider" />.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Applies the action on the instances of <typeparamref name="T" />.
        /// </summary>
        public static void ForEach<T>(this IServiceProvider builder, Action<T> action)
        {
            Guard.AgainstNull(nameof(builder), builder);
            Guard.AgainstNull(nameof(action), action);
            foreach (var t in builder.BuildAll<T>())
            {
                action(t);
            }
        }
    }
}