using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using CodeAnalyzeMVC2015.Controllers;

namespace CodeAnalyzeMVC2015.Models
{
    [RoutePrefix("Master")]
    public class MasterController : BaseController
    {

        [AllowAnonymous]
        [ChildActionOnly]
        [Route("PopularPosts")]
        public ActionResult PopularPosts()
        {
          //  CheckUserLogin();
            List<ArticleModel> articles = GetArticles("Select top 3 * from VwArticles order by thumbsup desc");
            return PartialView("PopularPosts", articles);
        }



        public List<ArticleModel> GetArticles(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSQuestions = new DataSet();
            DSQuestions = connManager.GetData(strQuery);
            connManager.DisposeConn();

            List<ArticleModel> articles = new List<ArticleModel>();
            ArticleModel article;
            foreach (DataRow row in DSQuestions.Tables[0].Rows)
            {
                article = new ArticleModel();
                article.ArticleID = row["ArticleID"].ToString();
                article.ArticleTitle = row["ArticleTitle"].ToString();
                article.InsertedDate = row["InsertedDate"].ToString();
                article.ThumbsUp = row["ThumbsUp"].ToString();
                article.ThumbsDown = row["ThumbsDown"].ToString();
                article.Views = row["Views"].ToString();
                articles.Add(article);
            }
            return articles;
       }



        [AllowAnonymous]
        [ChildActionOnly]
        [Route("RecentPosts")]
        public ActionResult RecentPosts()
        {
           // CheckUserLogin();
            List<ArticleModel> articles = GetArticles("Select top 3 * from VwArticles order by articleId desc");
            return PartialView("RecentPosts", articles);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session["User"] = null;
            Session["Facebook"] = null;
            Session.RemoveAll();
            List<ArticleModel> articles = new List<ArticleModel>();
            ConnManager connManager = new ConnManager();
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
            ViewBag.UserEmail = null;
            
            Response.Redirect("../Articles/Index");


            return View("../Articles/Index", articles);
        }


        [AllowAnonymous]
        public ActionResult ReferFriend(string txtReferEMail)
        {
                string strFrom = " freinds";
                //if (Session["User"] != null)
                //{
                    //user = (Users)Session["User"];
                    //if (!string.IsNullOrEmpty(user.FirstName))
                    //    strFrom = user.FirstName;
                    //else
                    //    strFrom = user.Email;
                //}
                Mail mail = new Mail();

                string EMailBody = System.IO.File.ReadAllText(Server.MapPath("../EMailBody.txt"));
                string strCA = "<a id=HyperLink1 style=font-size: medium; font-weight: bold; color:White href=http://codeanalyze.com>CodeAnalyze</a>";
                mail.Body = string.Format(EMailBody, "You have been refered to " + strCA + " by one of your " + strFrom + ". Get Rewards Amazon Gift Cards for code blogging. Do take a look");

                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "Referred to CodeAnalyze";
                mail.ToAdd = txtReferEMail;
                mail.IsBodyHtml = true;
                mail.SendMail();
                txtReferEMail = "Done";
                Response.Redirect("../Articles/Index");
            return null;
            //}

        }



        public void CheckUserLogin()
        {
            if (Session["User"] != null)
            {
                Users user = (Users)Session["User"];

                ViewBag.lblFirstName = user.FirstName;
                ViewBag.UserEmail = user.Email;
                ViewBag.IsUserLoggedIn = false;
            }
            else
            {
                ViewBag.UserEmail = "";
                ViewBag.lblFirstName = "";
                ViewBag.IsUserLoggedIn = true;
            }
        }




    }
}