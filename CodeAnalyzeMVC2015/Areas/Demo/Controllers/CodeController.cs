using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Areas.Demo.Controllers
{
    public class CodeController : Controller
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
            return View(articleId);
        }

        [HttpPost]
        public ActionResult Save()
        {
            //string strEMail = Request.Form["hfUserEMail1"];
            ViewBag.DemoMessage = "Data saved";
            return View("20183");
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            //string strEMail = Request.Form["hfUserEMail1"];
            ViewBag.DemoMessage = "Action cancelled";
            string articleId = ViewBag.ArticleId;
            return View("20183");
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

            return Articles("20184");
        }

    }
}
