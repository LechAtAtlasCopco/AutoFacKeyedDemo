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

        builder.RegisterType<Worker>().WithAttributeFiltering();


        var container = builder.Build();
        var list1 = container.ResolveKeyed<List<string>>("Source");
        var list2 = container.ResolveKeyed<List<string>>("Target");
        var worker = container.Resolve<Worker>();
    }
}