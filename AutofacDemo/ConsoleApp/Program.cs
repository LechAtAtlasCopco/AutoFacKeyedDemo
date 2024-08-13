using System;
using System.Threading.Channels;
using Autofac;
using Autofac.Features.AttributeFilters;
using ClassLibrary;

namespace ConsoleApp;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");


        var builder = new ContainerBuilder();

        var names = new List<string>
            { "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Hannah", "Ivan", "Judy" };

        var empty = new List<string>();


        builder.RegisterInstance(names)
            .Keyed<List<string>>("Source")
            .SingleInstance();

        builder.RegisterInstance(empty)
            .Keyed<List<string>>("Target")
            .SingleInstance();

        builder.RegisterType<Worker>()
            .WithFromKeyedServices();
        //.WithAttributeFiltering();
        var container = builder.Build();
        var list1 = container.ResolveKeyed<List<string>>("Source");
        var list2 = container.ResolveKeyed<List<string>>("Target");
        var worker = container.Resolve<Worker>();


        // builder.RegisterType<Worker>().WithParameter(
        //     (parameterInfo, componentContext) =>
        //     {
        //         var filter = parameterInfo.GetCustomAttributes<FromKeyedServicesAttribute>(true).FirstOrDefault();
        //         if (filter == null)
        //         {
        //             throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
        //         }
        //
        //         var isRegistered = componentContext.ComponentRegistry.IsRegistered(new Autofac.Core.KeyedService(filter.Key, parameterInfo.ParameterType));
        //         return isRegistered;
        //     },
        //     (parameterInfo, context) =>
        //     {
        //         var filter = parameterInfo.GetCustomAttributes<FromKeyedServicesAttribute>(true).First();
        //         if (filter.Key is string key && context.TryResolveKeyed(key, parameterInfo.ParameterType, out var value))
        //         {
        //             return value;
        //         }
        //
        //         throw new InvalidOperationException($"No service registered with key: {filter.Key}");
        //     });

        // .WithParameter(
        //     (p, c) =>
        //     {
        //         var filter = p.GetCustomAttributes<ParameterFilterAttribute>(true).FirstOrDefault();
        //         var result = filter is not null && filter.CanResolveParameter(p, c);
        //         return result;
        //     },
        //     (p, c) =>
        //     {
        //         var filter = p.GetCustomAttributes<ParameterFilterAttribute>(true).First();
        //         var result = filter.ResolveParameter(p, c);
        //         return result;
        //     })
    }
}