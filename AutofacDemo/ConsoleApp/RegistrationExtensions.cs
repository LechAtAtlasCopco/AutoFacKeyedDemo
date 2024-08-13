using System.Reflection;
using Autofac;
using Autofac.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp;

public static class RegistrationExtensions
{
    public static IRegistrationBuilder<TLimit, TReflectionActivatorData, TRegistrationStyle>
        WithFromKeyedServices<TLimit, TReflectionActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TReflectionActivatorData, TRegistrationStyle> builder)
        where TReflectionActivatorData : ReflectionActivatorData
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.WithParameter(
            (parameterInfo, componentContext) =>
            {
                var filter = CustomAttributeExtensions.GetCustomAttributes<FromKeyedServicesAttribute>((ParameterInfo)parameterInfo, true).FirstOrDefault();
                if (filter == null)
                {
                    throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
                }

                var isRegistered = componentContext.ComponentRegistry.IsRegistered(new Autofac.Core.KeyedService(filter.Key, parameterInfo.ParameterType));
                return isRegistered;
            },
            (parameterInfo, context) =>
            {
                var filter = CustomAttributeExtensions.GetCustomAttributes<FromKeyedServicesAttribute>((ParameterInfo)parameterInfo, true).First();
                if (filter.Key is string key && context.TryResolveKeyed(key, parameterInfo.ParameterType, out var value))
                {
                    return value;
                }

                throw new InvalidOperationException($"No service registered with key: {filter.Key}");
            });
    }
}