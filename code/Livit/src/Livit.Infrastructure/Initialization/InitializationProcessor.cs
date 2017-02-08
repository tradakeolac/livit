namespace Livit.Infrastructure.Initialization
{
    using Attributes;
    using Helpers;
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
                        if (initialization)
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
            var concreteTypes = AssemblyHelper.LoadCodingAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => initializableType.IsAssignableFrom(p)
                && p.GetTypeInfo().IsClass
                && p.GetTypeInfo().GetCustomAttributes(false).Count(a => a is InitializableModuleAttribute) > 0);
            return concreteTypes;
        }
    }
}