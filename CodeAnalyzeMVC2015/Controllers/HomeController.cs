using CodeAnalyzeMVC2015.AppCode;
using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class HomeController : BaseController
    {
        private Users user = new Users();

        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            // ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Rewards()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PostingGuidelines()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Disclaimer()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

       

    }
}