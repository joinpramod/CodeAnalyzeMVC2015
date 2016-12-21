using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            TempData["Message"] = "Data saved";
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Cancel()
        {
            //string strEMail = Request.Form["hfUserEMail1"];
            TempData["Message"] = "Action cancelled";
            return RedirectToAction("Index");
        }

    }
}
