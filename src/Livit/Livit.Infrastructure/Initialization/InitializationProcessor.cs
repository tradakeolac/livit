namespace Livit.Infrastructure.Initialization
{
    using Attributes;
    using Microsoft.Extensions.DependencyModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class InitializationProcessor
    {
        public static void Initialize(InitializationContext context)
        {
            DoAction(context, true);
        }

        public static void Uninitialize(InitializationContext context)
        {
            DoAction(context, false);
        }

        private static void DoAction(InitializationContext context, bool initialization)
        {
            var concreteTypes = GetInitializableModules();

            if (concreteTypes != null && concreteTypes.Count() > 0)
            {
                foreach (var type in concreteTypes)
                {
                    var moduleInstance = Activator.CreateInstance(type) as IInitializableModule;

                    if (moduleInstance != null)
                    {
                        if(initialization)
                        {
                            // Initialize
                            moduleInstance.Initialize(context);
                        }
                        else
                        {
                            // Initialize
                            moduleInstance.UnInitialize(context);
                        }
                    }
                }
            }
        }

        private static IEnumerable<Type> GetInitializableModules()
        {
            var initializableType = typeof(IInitializableModule);
            var concreteTypes = GetExecutingAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => initializableType.IsAssignableFrom(p)
                && p.GetTypeInfo().IsClass
                && p.GetTypeInfo().GetCustomAttributes(false).Count(a => a is InitializableModuleAttribute) > 0);
            return concreteTypes;
        }

        private static IEnumerable<Assembly> GetExecutingAssemblies()
        {
            var dependencies = DependencyContext.Default.CompileLibraries;
            var assemblies = new List<Assembly>();
            foreach(var library in dependencies)
            {
                if(library.Name.StartsWith("Livit."))
                {
                    var asem = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(asem);
                }
            }

            return assemblies;
        }
    }
}
