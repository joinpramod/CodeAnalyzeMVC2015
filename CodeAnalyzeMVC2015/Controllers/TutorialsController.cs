using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class TutorialsController : BaseController
    {
        //
        // GET: /Tutorials/

        public ActionResult Basics()
        {
            ViewBag.keywords = "C# tutorial for beginners quick and easy";
            List<QuestionModel> questions = new List<QuestionModel>();

            if (ModelState.IsValid)
            {
                ConnManager connManager = new ConnManager();
                questions = connManager.GetQuestions("Select top 190 * from Question order by questionid");
            }
            return View(questions);

        }

        public ActionResult AngularJS()
        {
            ViewBag.keywords = "AngularJS tutorial for beginners quick and complete";
            return View();
        }

        public ActionResult Hadoop()
        {
            ViewBag.keywords = "Hadoop Basic Commands Tutorial - CodeAnalyze";
            return View();
        }

        public ActionResult XCode()
        {
            ViewBag.keywords = "XCode tutorial for beginners quick and easy";
            return View();
        }

        public ActionResult Android()
        {
            ViewBag.keywords = "Android Basic Intro Tutorial - CodeAnalyze";
            return View();
        }

        public ActionResult HadoopInt()
        {
            ViewBag.keywords = "Hadoop Interview Questions and Answers";
            return View();
        }

        public ActionResult XCodeInt()
        {
            ViewBag.keywords = "XCode Interview Questions and Answers";
            return View();
        }

        public ActionResult AndroidInt()
        {
            ViewBag.keywords = "Android Interview Questions and Answers";
            return View();
        }

    }
}