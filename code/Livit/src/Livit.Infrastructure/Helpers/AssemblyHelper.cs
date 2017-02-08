namespace Livit.Infrastructure.Helpers
{
    using Microsoft.Extensions.DependencyModel;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class AssemblyHelper
    {
        public static IEnumerable<Assembly> LoadCodingAssemblies()
        {
            var dependencies = DependencyContext.Default.CompileLibraries;
            var assemblies = new List<Assembly>();
            foreach (var library in dependencies)
            {
                if (library.Name.ToLower().StartsWith("livit.", StringComparison.Ordinal))
                {
                    var asem = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(asem);
                }
            }

            return assemblies;
        }
    }
}