using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeAnalyzeMVC2015
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}"
            //);

           // routes.MapRoute(
           //    name: "Default",
           //    url: "{controller}/{action}/{id}",
           //    defaults: new { controller = "Questions", action = "UnAns", txtQuesType = UrlParameter.Optional }
           //);

            routes.MapRoute(
              name: "Questions",
              url: "{controller}/{action}/{ID}/{Title}",
              defaults: new { controller = "Questions", action = "Index", ID = UrlParameter.Optional, Title = UrlParameter.Optional }
              );
        }
    }
}
