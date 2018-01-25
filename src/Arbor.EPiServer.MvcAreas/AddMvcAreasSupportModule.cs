using System.Web;
using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace Arbor.EPiServer.MvcAreas
{
    public class AddMvcAreasSupportModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if (AreaConfiguration.Settings.EnableAreaDetectionByController)
            {
                GlobalFilters.Filters.Add(ServiceLocator.Current.GetInstance<DetectAreaAttribute>());
            }

            if (AreaConfiguration.Settings.EnableAreaDetectionBySite)
            {
                GlobalFilters.Filters.Add(ServiceLocator.Current.GetInstance<SwitchToAreaAttribute>());
            }

            var contentRouteEvents = ServiceLocator.Current.GetInstance<IContentRouteEvents>();

            contentRouteEvents.RoutingContent += OnRoutingContent;
        }

        public void Uninitialize(InitializationEngine context) {}

        private void OnRoutingContent(object sender, RoutingEventArgs e)
        {
            PartialViewsInAreasRegistrar.Register(new HttpContextWrapper(HttpContext.Current));
            var contentRouteEvents = ServiceLocator.Current.GetInstance<IContentRouteEvents>();

            contentRouteEvents.RoutingContent -= OnRoutingContent;
        }
    }
}
