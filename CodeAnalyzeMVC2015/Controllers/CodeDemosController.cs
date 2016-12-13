using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class CodeDemosController : Controller
    {
        //
        // GET: /CodeDemos/

        public ActionResult Index(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
                articleId = "20174";
            return View(articleId);
        }

    }
}
