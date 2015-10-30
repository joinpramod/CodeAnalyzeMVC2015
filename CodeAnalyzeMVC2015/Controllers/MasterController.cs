using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace CodeAnalyzeMVC2015.Models
{
    public class MasterController : Controller
    {

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult PopularPosts()
        {
            List<ArticlesModel> articles = GetArticles("Select top 3 * from VwArticles order by thumbsup desc");
            return PartialView("PopularPosts", articles);
        }



        public List<ArticlesModel> GetArticles(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSQuestions = new DataSet();
            DSQuestions = connManager.GetData(strQuery);
            connManager.DisposeConn();

            List<ArticlesModel> articles = new List<ArticlesModel>();
            ArticlesModel article;
            foreach (DataRow row in DSQuestions.Tables[0].Rows)
            {
                article = new ArticlesModel();
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
        public ActionResult RecentPosts()
        {
            List<ArticlesModel> articles = GetArticles("Select top 3 * from VwArticles order by articleId desc");
            return PartialView("RecentPosts", articles);
        }

    }
}