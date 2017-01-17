using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Areas.Demo
{
    public class DemoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Demo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
              name: "DemoRoute",
              url: "Demo/{controller}/{action}/{Id}/{Title}");

            context.MapRoute(
                "Demo_default",
                "Demo/{controller}/{action}",
                new
                {
                    controller = "Code",
                    action = "Articles"
                }
            );
        }
    }
}