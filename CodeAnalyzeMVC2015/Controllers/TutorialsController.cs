using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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

        public ActionResult EBook()
        {
            DataTable dtFeeds = new DataTable();
            dtFeeds.Columns.Add("Title");
            dtFeeds.Columns.Add("ImageURL");
            dtFeeds.Columns.Add("URL");
            dtFeeds.Columns.Add("DateTime");
            WebRequest rssReq = WebRequest.Create("http://feeds.feedburner.com/oreilly/news?xml");

            rssReq.Timeout = 5000;

            //Get the WebResponse
            WebResponse rep = rssReq.GetResponse();

            //Read the Response in a XMLTextReader
            XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());

            //Create a new DataSet
            DataSet ds = new DataSet();

            //Read the Response into the DataSet
            ds.ReadXml(xtr);

            DataTable dtSummary = ds.Tables["summary"];
            DataTable dtEntry = ds.Tables["entry"];


            XmlDocument xDoc = new XmlDocument();

            XmlElement elem = xDoc.CreateElement("codeanalyze");

            xDoc.AppendChild(elem);


            for (int counter = 0; counter < dtEntry.Rows.Count; counter++)
            {
                DataRow dr = dtFeeds.NewRow();

                elem.InnerXml = dtSummary.Rows[counter][1].ToString().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "");

                if (!string.IsNullOrEmpty(xDoc.FirstChild.ChildNodes[3].InnerText))
                    dr["Title"] = xDoc.FirstChild.ChildNodes[3].InnerText;
                else
                    dr["Title"] = "";
                dr["ImageURL"] = xDoc.FirstChild.ChildNodes[1].InnerXml.Replace("alt=\"\"", "alt=\"\" style=\"height:200px;width:150px\"");
                dr["URL"] = dtEntry.Rows[counter][4].ToString();
                dr["DateTime"] = DateTime.Parse(dtEntry.Rows[counter][3].ToString()).ToShortDateString();
                dtFeeds.Rows.Add(dr);

            }
            return View(dtFeeds);
        }

    }
}