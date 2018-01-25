using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EPiServer.Configuration;

namespace Arbor.EPiServer.MvcAreas
{
    public static class AreaConfiguration
    {
        public static readonly AreaConfigurationSettings Settings = new AreaConfigurationSettings();

        public static void RegisterAllAreas(Action<AreaConfigurationSettings> configuration, Assembly[] assemblies)
        {
            configuration(Settings);

            RegisterAllAreas(assemblies);
        }

        public static void RegisterAllAreas(Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            AreaRegistration.RegisterAllAreas();

            IEnumerable<Type> areas = TypeAttributeHelper.GetTypesChildOf<AreaRegistration>(assemblies);

            foreach (Type area in areas)
            {
                AreaRegistration areaRegistration = AreaTable.AddArea(area);

                string ns = area.Namespace;
                if (string.IsNullOrEmpty(ns))
                {
                    continue;
                }

                IEnumerable<Type> controllersInArea = TypeAttributeHelper.GetTypesChildOf<Controller>(assemblies)
                    .Where(t => t.Namespace != null && t.Namespace.StartsWith(ns, StringComparison.OrdinalIgnoreCase));
                controllersInArea.ToList()
                    .ForEach(t => AreaTable.RegisterController(t.FullName, areaRegistration.AreaName));
            }
        }
    }
}