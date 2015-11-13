using System.Web.Mvc;
using CodeAnalyzeMVC2015.Models;
using System.Data;
using System.Collections.Generic;
using CodeAnalyzeMVC2015.AppCode;
using System;
using System.Linq;
using System.Web;

namespace CodeAnalyzeMVC2015.Controllers
{

    public class AccountController : BaseController
    {
        private Users user = new Users();

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult ProcessLogin(string txtEMailId, string txtPassword)
        {

            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable DSUserList = new DataTable();
            DSUserList = connManager.GetDataTable("select * from users where email = '" + txtEMailId + "' and Password = '" + txtPassword + "'");
           
            if (DSUserList.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                Users user = new Users();
                user.UserId = double.Parse(DSUserList.Rows[0]["UserId"].ToString());
                user.FirstName = DSUserList.Rows[0]["FirstName"].ToString();
                user.LastName = DSUserList.Rows[0]["LastName"].ToString();
                user.Email = DSUserList.Rows[0]["EMail"].ToString();
                user.Address = DSUserList.Rows[0]["Address"].ToString();
                user.ImageURL = DSUserList.Rows[0]["ImageURL"].ToString();
                Session["User"] = user;
                Session["user.Email"] = user.Email;
                ViewBag.UserEmail = user.Email;

                List<ArticleModel> articles = new List<ArticleModel>();
                articles = connManager.GetArticles("Select * from VwArticles order by articleId desc");
                connManager.DisposeConn();

                PagingInfo info = new PagingInfo();
                info.SortField = " ";
                info.SortDirection = " ";
                info.PageSize = 10;
                info.PageCount = Convert.ToInt32(Math.Ceiling((double)(articles.Count / info.PageSize)));
                info.CurrentPageIndex = 0;
                var query = articles.OrderBy(c => c.ArticleID).Take(info.PageSize);
                ViewBag.PagingInfo = info;

                return View("../Articles/Index", query.ToList());

            }
        }


        [ReCaptcha]
        public ActionResult Users(Users rm)
        {
            return View(rm);
        }

        public ActionResult CreateEditUser(Users user, HttpPostedFileBase fileUserPhoto)
        {
            //AddEdit user
            Session["User"] = user;
            return View("ViewUser", user);
        }

        public ActionResult ViewUser(Users user)
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
            }
            return View(user);
        }

        public ActionResult ChangePassword(string txtNewPassword)
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                ViewBag.OldPassword = user.Password;
            }
            return View(user);
        }

        public ActionResult MyQues()
        {
            List<QuestionModel> questions = new List<QuestionModel>();


            QuestionModel ques;
            for (int i = 0; i < 10; i++)
            {
                ques = new QuestionModel();
                ques.QuestionID = i.ToString();
                ques.QuestionTitle = "Question" + i.ToString();
                questions.Add(ques);
            }


            PagingInfo info = new PagingInfo();
            info.SortField = " ";
            info.SortDirection = " ";
            info.PageSize = 10;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(questions.Count() / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = questions.OrderBy(c => c.QuestionID).Take(info.PageSize);
            ViewBag.PagingInfo = info;

            return View(query.ToList());
        }

        [HttpPost]
        public ActionResult MyQues(PagingInfo info)
        {
            List<QuestionModel> questions = new List<QuestionModel>();
            QuestionModel ques;
            for (int i = 0; i < 10; i++)
            {
                ques = new QuestionModel();
                ques.QuestionID = i.ToString();
                ques.QuestionTitle = "Question" + i.ToString();
                questions.Add(ques);
            }

            IQueryable<QuestionModel> query = questions.AsQueryable();
            query = query.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize);
            ViewBag.PagingInfo = info;
            List<QuestionModel> model = query.ToList();
            return View(model);
        }
    }
}