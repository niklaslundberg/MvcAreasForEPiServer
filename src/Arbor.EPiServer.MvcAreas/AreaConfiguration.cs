using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Arbor.EPiServer.MvcAreas
{
    public static class AreaConfiguration
    {
        public static AreaConfigurationSettings Settings { get; } = new AreaConfigurationSettings();

        public static void RegisterAllAreas(Action<AreaConfigurationSettings> configuration)
        {
            configuration(Settings);

            RegisterAllAreas();
        }

        public static void RegisterAllAreas()
        {
            AreaRegistration.RegisterAllAreas();

            IEnumerable<Type> areas = TypeAttributeHelper.GetTypesChildOf<AreaRegistration>();

            foreach (Type area in areas)
            {
                AreaRegistration areaRegistration = AreaTable.AddArea(area);

                string ns = area.Namespace;
                if (string.IsNullOrEmpty(ns))
                {
                    continue;
                }

                IEnumerable<Type> controllersInArea = TypeAttributeHelper.GetTypesChildOf<Controller>()
                    .Where(t => t.Namespace != null && t.Namespace.StartsWith(ns, StringComparison.OrdinalIgnoreCase));
                controllersInArea.ToList()
                    .ForEach(t => AreaTable.RegisterController(t.FullName, areaRegistration.AreaName));
            }
        }
    }
}