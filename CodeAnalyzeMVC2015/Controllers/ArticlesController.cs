using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class ArticlesController : Controller
    {

        public ActionResult Index()
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            if (ModelState.IsValid)
            {
                ConnManager connManager = new ConnManager();
                articles = connManager.GetArticles("Select * from VwArticles order by articleId desc");

                HtmlMeta metaDescription = new HtmlMeta();
                metaDescription.Name = "description";
                metaDescription.Content = "Get Amazon gift cards of your respective country for code blogging as appreciation. Try now.";
                // Page.Header.Controls.Add(metaDescription);
                HtmlMeta metaKeywords = new HtmlMeta();
                metaKeywords.Name = "keywords";
                metaKeywords.Content = "Java, C#, PHP, Android, JQuery, XCode, XML, SQL, ASP.NET, HTML5 n many more";
                //  Page.Header.Controls.Add(metaKeywords);

            }
            return View(articles);
        }


        public ActionResult VA(string Id, string Title)
        {
            return View();
        }



        public ActionResult Search(string txtArticleTitle)
        {
            string strSQL = "Select * from VwArticles Where ArticleId > 0 ";

            if (!string.IsNullOrEmpty(txtArticleTitle))
            {
                strSQL += " and ArticleTitle like '%" + txtArticleTitle + "%' ";
            }
            strSQL += " order by InsertedDate desc";

            List<ArticleModel> articles = new List<ArticleModel>();
            if (ModelState.IsValid)
            {
                ConnManager connManager = new ConnManager();
                articles = connManager.GetArticles(strSQL);
            }
            return View(articles);
        }

    }
}