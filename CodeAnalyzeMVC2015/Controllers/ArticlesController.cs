using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    [RoutePrefix("Articles")]
    public class ArticlesController : BaseController
    {
        private Users user = new Users();

        //[Route("~/")]
        //[Route("")]
        public ActionResult Index()
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            if (ModelState.IsValid)
            {
                ConnManager connManager = new ConnManager();
                articles = connManager.GetArticles("Select * from VwArticles order by articleId desc");
            }
            return View(articles);
        }

        public ActionResult Post(string txtYouTubeLink, HttpPostedFileBase fileArticleWordFile, HttpPostedFileBase fileArticleSourceCode)
        {

            if (Session["User"] != null && fileArticleWordFile != null)
            {
                Mail mail = new Mail();
                mail.Body = "<a>New article from " + user.Email + ", file name is " + fileArticleWordFile.FileName + " Youtube URL - " + txtYouTubeLink + " </a>";
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "New Article from " + user.Email;
                mail.ToAdd = "articles@codeanalyze.com";


                string strFileName = System.IO.Path.GetFileName(fileArticleWordFile.FileName);
                Attachment attachFile = new Attachment(fileArticleWordFile.InputStream, strFileName);
                mail.FileAttachment = attachFile;

                if (fileArticleSourceCode != null)
                {
                    strFileName = System.IO.Path.GetFileName(fileArticleSourceCode.FileName);
                    attachFile = new Attachment(fileArticleSourceCode.InputStream, strFileName);
                    mail.SourceFileAttachment = attachFile;
                }

                mail.IsBodyHtml = true;

                if (user.Email != "admin@codeanalyze.com")
                {
                    mail.SendMail();
                }


                ViewBag.Acknowledgement = "Article emailed successfully. We will get back to you if needed. Thanks much for your post. Appreciate it. ";
            }



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

        //[Route("{Id}/{Title}")]
        public ActionResult Details(string txtReply)
        {
            if (string.IsNullOrEmpty(txtReply))
            {
                VwArticlesModel model = SetDefaults();
                return View(model);
            }
            else
            {
                return InsertComment(txtReply);
            }
        }

        private VwArticlesModel SetDefaults()
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
            }

            string articleID;
            string articleTitle = string.Empty;

            articleID = RouteData.Values["Id"].ToString();

            VwArticlesModel model = new VwArticlesModel();
            GetArticleData(articleID.ToString(), ref model);

            articleTitle = model.ArticleTitle.ToString();
            ViewBag.Description = articleTitle.Replace("-", " ");
            ViewBag.keywords = articleTitle.Replace("-", " ");

            if (articleID != null)
            {
                BindComments("Select * from VwArticleReplies where ArticleId =" + articleID.ToString(), ref model);
            }

            if (user.Email != null)
            {
                ViewBag.lblAck = string.Empty;
                ViewBag.hfUserEMail = user.Email;
            }
            else
            {
                ViewBag.lblAck = "Please sign in to post your answer.";
                ViewBag.hfUserEMail = string.Empty;
            }
            return model;
        }

        public ActionResult InsertComment(string Comment)
        {
            VwArticlesModel model = new VwArticlesModel();
            if (Session["User"] != null)
            {
                if (!string.IsNullOrEmpty(Comment))
                {
                    user = (Users)Session["User"];
                    string articleID = RouteData.Values["Id"].ToString();

                    double dblReplyID = 0;
                    ArticleComments articleComments = new ArticleComments();
                    SqlConnection LclConn = new SqlConnection();
                    SqlTransaction SetTransaction = null;
                    bool IsinTransaction = false;
                    if (LclConn.State != ConnectionState.Open)
                    {
                        articleComments.SetConnection = articleComments.OpenConnection(LclConn);
                        SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                        IsinTransaction = true;
                    }
                    else
                    {
                        articleComments.SetConnection = LclConn;
                    }

                    articleComments.OptionID = 1;
                    articleComments.ReplyUserId = user.UserId;
                    articleComments.ArticleId = double.Parse(articleID.ToString());
                    articleComments.ReplyText = Comment;

                    articleComments.InsertedDate = DateTime.Now;

                    bool result = articleComments.CreateArticleComments(ref dblReplyID, SetTransaction);


                    if (IsinTransaction && result)
                    {
                        SetTransaction.Commit();

                        if (!Session["AskedUserEMail"].ToString().Contains("codeanalyze.com"))
                        {
                            Mail mail = new Mail();
                            string strLink = "www.codeanalyze.com/VA.aspx?QId=" + articleID.ToString() + "&QT=" + model.ArticleTitle + "";
                            mail.Body = "<a href=" + strLink + "\\>Click here to view solution to: " + model.ArticleTitle + "</a>";
                            mail.FromAdd = "admin@codeanalyze.com";
                            mail.Subject = "Code Analyze - Received response for " + model.ArticleTitle;
                            mail.ToAdd = Session["AskedUserEMail"].ToString();
                            mail.CCAdds = "admin@codeanalyze.com";
                            mail.IsBodyHtml = true;
                            mail.SendMail();
                        }
                    }
                    else
                    {
                        SetTransaction.Rollback();
                    }
                    articleComments.CloseConnection(LclConn);
                    ViewBag.ReplyId = dblReplyID;
                    model = SetDefaults();
                }
                else
                {
                    ViewBag.lblAck = "Please sign in to post your comment.";
                }
              
            }
            return View("../Articles/Details", model);
        }

        //[HttpPost]
        //public ActionResult PostArticle(string txtYouTubeLink, IEnumerable<HttpPostedFileBase> files)
        //{
        //    foreach (var file in files)
        //    {
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            file.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
        //        }
        //    }
        //    ViewBag.Acknowledgement = "";
        //    return View("Post");
        //}

        //protected void lnkBtnSourceCode_Click(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + hfSourceFile.Value);
        //    Response.ContentType = "application/x-zip-compressed";
        //    Response.WriteFile(Server.MapPath("~/Articles/" + hfSourceFile.Value));
        //    Response.End();
        //}

        public ActionResult UpVote()
        {
            string artsID = RouteData.Values["Id"].ToString();
            ProcessVotes("Up", artsID);
            VwArticlesModel model = SetDefaults();
            return View("../Articles/Details", model);
        }

        public ActionResult DownVote()
        {
            string artsID = RouteData.Values["Id"].ToString();
            ProcessVotes("Down", artsID);
            VwArticlesModel model = SetDefaults();
            return View("../Articles/Details", model);
        }

        private void GetArticleData(string strArticleId, ref VwArticlesModel model)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable dsQuestion = connManager.GetArticle(strArticleId);

            connManager.DisposeConn();
            if (dsQuestion != null)
            {
                if (dsQuestion.Rows.Count > 0)
                {
                    model.ArticleID = long.Parse(dsQuestion.Rows[0]["ArticleId"].ToString());

                    if (!dsQuestion.Rows[0]["EMail"].ToString().Contains("codeanalyze.com"))
                    {
                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["FirstName"].ToString()))
                        {
                            model.AskedUser = "Posted By: <b>" + dsQuestion.Rows[0]["FirstName"].ToString() + "<b>";
                            if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["LastName"].ToString()))
                            {
                                model.AskedUser = model.AskedUser + " <b>" + dsQuestion.Rows[0]["LastName"].ToString() + "<b>";
                            }
                        }

                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["ImageURL"].ToString()))
                        {
                            model.ImageURL = dsQuestion.Rows[0]["ImageURL"].ToString();
                        }


                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["ImageURL"].ToString()))
                        {
                            model.AskedUserDetails = dsQuestion.Rows[0]["Details"].ToString();
                        }
                    }


                    Session["AskedUserEMail"] = dsQuestion.Rows[0]["EMail"].ToString();
                    model.ArticleTitle = "<b>" + dsQuestion.Rows[0]["ArticleTitle"].ToString() + "<b>";
                    model.ArticleViews = dsQuestion.Rows[0]["Views"].ToString();



                   string filepath =  Server.MapPath("/CodeAnalyzeMVC2015/Articles/" + dsQuestion.Rows[0]["WordFile"].ToString()).Replace("\\Details\\Articles", "");

                    using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                    {
                        using (TextReader tr = new StreamReader(fs))
                        {
                            string content = tr.ReadToEnd();
                            model.ArticleDetails = content;
                        }
                    }

                    //if (dsQuestion.Rows[0]["SourceFile"] != DBNull.Value && !string.IsNullOrEmpty(dsQuestion.Rows[0]["SourceFile"].ToString()))
                    //{
                    //    hfSourceFile.Value = dsQuestion.Rows[0]["SourceFile"].ToString();
                    //    lnkBtnSourceCode.Enabled = true;
                    //    lnkBtnSourceCode.ForeColor = Color.Blue;
                    //    lnkBtnSourceCode.Text = "Download Source Code";
                    //}
                    //else
                    //{
                    //    lnkBtnSourceCode.Enabled = false;
                    //    lnkBtnSourceCode.ForeColor = Color.Gray;
                    //    lnkBtnSourceCode.Text = "No Source Code Uploaded";
                    //}


                    if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["YouTubURL"].ToString()))
                    {
                        model.iframeVideoURL = "//www.youtube.com/embed?listType=playlist&list=PLr5xM_46LGUtyEyyGilUu3YH5FTNo7SMH";
                    }


                    model.ThumbsUp = dsQuestion.Rows[0]["ThumbsUp"].ToString();
                    model.ThumbsDown = dsQuestion.Rows[0]["ThumbsDown"].ToString();

                }
            }
        }

        private void BindComments(string strQuery, ref VwArticlesModel model)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable dsSolution = connManager.GetDataTable(strQuery);
            string strReplyId = "";
            string tblReplies = "<table width=\"100%\" style=\"word-wrap:normal; word-break:break-all\" cell-padding=\"0\" cell-spacing=\"0\">";

            if (dsSolution != null)
            {
                for (int i = 0; i < dsSolution.Rows.Count; i++)
                {

                    //Response no user details
                    string htrResponseNoByDetailsOuterRow = "<tr>";
                    string htcResponseNoByDetailsOuterCell = "<td style=\"background-color:#4fa4d5\">";

                    string htmlTblResponseNoByDetails = "<table style=\"width:100%\">";

                    #region table
                    string htmlRowResponseNoByDetails = "<tr>";

                    string htcUserImage = "<td>";
                    if (!string.IsNullOrEmpty(dsSolution.Rows[i]["ImageURL"].ToString()))
                    {
                        if (Request.Url.ToString().Contains("localhost"))
                            htcUserImage += "<img src=\"/CodeAnalyzeMVC2015/" + dsSolution.Rows[i]["ImageURL"].ToString().Replace("~", "") + "\" style=\"height:30px;width:30px\" />";
                        else
                            htcUserImage += "<img src=\"/codeanalyze.com/" + dsSolution.Rows[i]["ImageURL"].ToString().Replace("~", "") + "\" style=\"height:30px;width:30px\" />";
                    }
                    else
                    {
                        if (Request.Url.ToString().Contains("localhost"))
                            htcUserImage += "<img src=\"/CodeAnalyzeMVC2015/Images/Person.JPG\" style=\"height:25px;width:25px\" />";
                        else
                            htcUserImage += "<img src=\"/codeanalyze.com/Images/Person.JPG\" style=\"height:25px;width:25px\" />";
                    }
                    htcUserImage += "</td>";

                    #region responseNoBy
                    string htcResponseNoByDetails = "<td valign=\"middle\">";
                    string strFirstName = "";
                    string strAnswers = "";
                    string strRepliedDate = "";

                    string strUserId = dsSolution.Rows[i]["UserId"].ToString();
                    if (!string.IsNullOrEmpty(dsSolution.Rows[i]["FirstName"].ToString()))
                        strFirstName = dsSolution.Rows[i]["FirstName"].ToString();
                    else
                        strFirstName = dsSolution.Rows[i]["EMail"].ToString().Split('@')[0];
                    strRepliedDate = dsSolution.Rows[i]["InsertedDate"].ToString().Split('@')[0];


                    DataTable dsCount = new DataTable();
                    dsCount = connManager.GetDataTable("SELECT COUNT(*) FROM VwArticleReplies WHERE (ArticleId = " + strUserId + ") ");

                    if (dsCount != null && dsCount.Rows.Count != 0)
                    {
                        if (dsCount.Rows[0][0].ToString() != "0")
                        {
                            if (dsCount != null)
                                strAnswers = dsCount.Rows[0][0].ToString();
                            else
                                strAnswers = "<b>none</b>";


                            if (!dsSolution.Rows[i]["EMail"].ToString().Contains("codeanalyze.com"))
                                htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";   // Total replies by user: " + strAnswers + ".";
                            else
                            {
                                if (!strFirstName.ToLower().Equals("admin"))
                                    htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                                else
                                    htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + "";

                            }
                        }
                        else
                            if (!strFirstName.ToLower().Equals("admin"))
                            htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                        else
                            htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + "";
                    }
                    else
                        if (!strFirstName.ToLower().Equals("admin"))
                        htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                    else
                        htcResponseNoByDetails += "Comment No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + " ";


                    htcResponseNoByDetails += "</td>";

                    htmlRowResponseNoByDetails += htcUserImage;
                    htmlRowResponseNoByDetails += htcResponseNoByDetails;
                    #endregion

                    htmlRowResponseNoByDetails += "</tr>";
                    #endregion

                    htmlTblResponseNoByDetails += htmlRowResponseNoByDetails + "</table>";

                    htcResponseNoByDetailsOuterCell += htmlTblResponseNoByDetails + "</td>";
                    htrResponseNoByDetailsOuterRow += htcResponseNoByDetailsOuterCell + "</tr>";


                    string htmlRowSolutionContent = string.Empty;
                    string htcReplyContent = string.Empty;
                    strReplyId = dsSolution.Rows[i]["ReplyId"].ToString();
                    htcReplyContent += "<td>" + dsSolution.Rows[i]["ReplyText"].ToString() + "</td>";
                    htmlRowSolutionContent += "<tr>" + htcReplyContent + "</tr>";


                    tblReplies += htrResponseNoByDetailsOuterRow +  htmlRowSolutionContent;

                    tblReplies += "<tr><td><br /></td></tr>";

                }
            }

            tblReplies += "</table>";
            model.ArticleReplies = tblReplies;
            connManager.DisposeConn();

        }

        private void ProcessVotes(string LikeType, string articleID)
        {
            List<string> lstReplies = (List<string>)Session["Articles"];
            string strQuery = "";
            int votes = 0;
            if (lstReplies == null)
            {
                lstReplies = new List<string>();
            }

            if (!lstReplies.Contains(articleID))
            {

                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();

                DataTable dsVotes = connManager.GetDataTable("Select ThumbsUp, ThumbsDown from CodeArticles where ArticleId = " + articleID);

                if (dsVotes != null && dsVotes.Rows.Count > 0)
                {
                    if (LikeType.Equals("Up"))
                    {
                        if (string.IsNullOrEmpty(dsVotes.Rows[0]["ThumbsUp"].ToString()))
                            votes = votes + 1;
                        else
                            votes = int.Parse(dsVotes.Rows[0]["ThumbsUp"].ToString()) + 1;

                        strQuery = "Update CodeArticles set ThumbsUp = " + votes + " where ArticleId = " + articleID;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dsVotes.Rows[0]["ThumbsDown"].ToString()))
                            votes = votes - 1;
                        else
                            votes = int.Parse(dsVotes.Rows[0]["ThumbsDown"].ToString()) + 1;

                        strQuery = "Update CodeArticles set ThumbsDown = " + votes + " where ArticleId = " + articleID;
                    }
                }

                SqlCommand command = new SqlCommand(strQuery, connManager.DataCon);



                command.ExecuteNonQuery();
                connManager.DisposeConn();

                lstReplies.Add(articleID);
                Session["Articles"] = lstReplies;

            }

            //GetArticleData(articleID.ToString());
        }
    }
}