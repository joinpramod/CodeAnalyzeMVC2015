using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class CodeDemosController : Controller
    {
        //
        // GET: /CodeDemos/

        public ActionResult Articles()
        {
            string articleId = RouteData.Values["Id"].ToString();

            return View("../CodeDemos/" + articleId);
        }

        [HttpPost]
        public ActionResult Save()
        {
            //string strEMail = Request.Form["hfUserEMail1"];
            ViewBag.DemoMessage = "Data saved";
            return View("../CodeDemos/" + 20183);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            //string strEMail = Request.Form["hfUserEMail1"];
            ViewBag.DemoMessage = "Action cancelled";
            string articleId = ViewBag.ArticleId;
            return View("../CodeDemos/" + 20183);
        }

    }
}
