using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
   
namespace AspMvcWebshop.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            if (this.GetType() != typeof(ErrorController))
            {
                var errorRoute = new RouteData();
                errorRoute.Values.Add("controller", "Error");
                errorRoute.Values.Add("action", "Http404");
                errorRoute.Values.Add("url", HttpContext.Request.Url.OriginalString);
                View("Http404").ExecuteResult(this.ControllerContext);
            }
        }
        public ActionResult Http404()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}