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
            List<QuestionModel> questions = new List<QuestionModel>();

            if (ModelState.IsValid)
            {
                ConnManager connManager = new ConnManager();
                questions = connManager.GetQuestions("Select top 200 * from Question order by questionid");
            }
            return View(questions);

        }

        public ActionResult AngularJS()
        {
            return View();
        }

        public ActionResult Hadoop()
        {
            return View();
        }
    }
}