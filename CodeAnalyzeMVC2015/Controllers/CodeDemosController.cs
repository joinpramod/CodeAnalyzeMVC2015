using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class CodeDemosController : Controller
    {
        //
        // GET: /CodeDemos/

        public ActionResult Articles(string strId)
        {
            string articleId = string.Empty;
            if (RouteData.Values.Count > 0 && RouteData.Values["Id"] != null)
            {
                articleId = RouteData.Values["Id"].ToString();
            }
            else
            {
                articleId = strId;
            }
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

        [HttpPost]
        public ActionResult DynamicTextBox(string[] txtBoxes)
        {
            string txtBoxValues = "";
            foreach (string textboxValue in txtBoxes)
            {
                txtBoxValues += textboxValue + ", ";
            }
            ViewBag.DemoMessage = txtBoxValues;

            string articleId = string.Empty;
            if (RouteData.Values.Count > 0 && RouteData.Values["Id"] != null)
            {
                articleId = RouteData.Values["Id"].ToString();
            }

            return Articles(articleId);
        }

    }
}
