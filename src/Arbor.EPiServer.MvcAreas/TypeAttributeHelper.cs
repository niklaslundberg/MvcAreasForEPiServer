using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Arbor.EPiServer.MvcAreas
{
    internal class TypeAttributeHelper
    {
        public static IEnumerable<Type> GetTypesChildOf<T>(Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            var allTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                allTypes.AddRange(GetTypesChildOfInAssembly(typeof(T), assembly));
            }

            return allTypes;
        }

        private static IEnumerable<Type> GetTypesChildOfInAssembly(Type type, Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(t => t.IsSubclassOf(type) && !t.IsAbstract);
            }
            catch (Exception)
            {
                // there could be situations when type could not be loaded
                // this may happen if we are visiting *all* loaded assemblies in application domain
                return new List<Type>();
            }
        }
    }
}
