using Autofac.Features.AttributeFilters;
using Microsoft.Extensions.DependencyInjection;

namespace ClassLibrary
{
    public class Worker
    {
        // public Worker([KeyFilter("Source")] List<string> source, [KeyFilter("Target")] List<string> target)
        public Worker([FromKeyedServices("Source")] List<string> source, [FromKeyedServices("Target")] List<string> target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);

            Console.WriteLine("Source List");
            foreach (var name in source)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("Target List");
            foreach (var name in target)
            {
                Console.WriteLine(name);
            }
        }
    }
}