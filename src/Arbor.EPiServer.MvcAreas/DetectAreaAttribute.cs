using System.Web.Mvc;

namespace Arbor.EPiServer.MvcAreas
{
    public class DetectAreaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string areaName = AreaTable.GetAreaForController(filterContext.Controller.GetType().FullName);
            if (areaName != null)
            {
                filterContext.RouteData.DataTokens["area"] = areaName;
            }
        }
    }
}
