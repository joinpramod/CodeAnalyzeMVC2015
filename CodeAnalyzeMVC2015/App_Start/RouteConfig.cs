using System.Web.Mvc;
using System.Web.Routing;

namespace CodeAnalyzeMVC2015
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
              name: "UpVote",
              url: "{controller}/{action}/{Id}/{RId}/{Title}");

            routes.MapRoute(
                name: "Soln",
                url: "{controller}/{action}/{Id}/{Title}");


            routes.MapRoute(
             name: "UnAns",
             url: "{controller}/{action}/{txtQuesType}");


            //routes.MapRoute(
            //name: "InsertAns",
            //url: "{controller}/{action}/{hiddenId}/{SolutionEditor}");


            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}",
            defaults: new
            {
                controller = "Questions",
                action = "Post"
            });
        }
    }
}
