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

            routes.MapRoute(
          name: "UpVote",
          url: "{controller}/{action}/{Id}/{RId}/{Title}");

            routes.MapRoute(
                name: "Soln",
                url: "{controller}/{action}/{Id}/{Title}");

            routes.MapRoute(
             name: "UnAns",
             url: "{controller}/{action}/{txtQuesType}");


            routes.MapRoute(
           name: "InsertAns",
           url: "{controller}/{action}/{SolutionEditor}/{hiddenId}");


            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}",
            defaults: new
            {
                controller = "Questions",
                action = "UnAns"
            });


        }
    }
}
