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

    }
}
