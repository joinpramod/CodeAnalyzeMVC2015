using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class QuestionsController : Controller
    {
        public ActionResult Index(string Id, string Title)
        {
            return View();
        }


        public ActionResult UnAns()
        {
            string txtQuesType = string.Empty;

            if (RouteData.Values["id"] != null && !string.IsNullOrEmpty(RouteData.Values["id"].ToString()))
            {
                txtQuesType = RouteData.Values["id"].ToString();
            }

            List<QuestionModel> questions = new List<QuestionModel>();
            if (ModelState.IsValid)
            {
                string strSQL = string.Empty;
                if (!string.IsNullOrEmpty(txtQuesType))
                    strSQL = "Select * from Question Where QuestionId > 37861 and QuestionTypeId = " + txtQuesType;
                else
                    strSQL = "Select * from Question Where QuestionId > 37861";

                ConnManager connManager = new ConnManager();
                questions = connManager.GetQuestions(strSQL);
            }
            return View(questions);
        }


        //public ActionResult QuestionTypeList()
        //{
        //    List<QuestionType> quesTypes = new List<QuestionType>();
        //    if (ModelState.IsValid)
        //    {
        //        string strSQL = string.Empty;

        //        strSQL = "Select  QuestionTypeId, QuestionType from QuestionType";

        //        ConnManager connManager = new ConnManager();
        //        quesTypes = connManager.GetQuestionType(strSQL);
        //    }

        //    QuestionType qt = new QuestionType();
        //    qt.Types = new SelectList(quesTypes);

        //    var model = qt;

        //    return View(model);

        //}
    }
}