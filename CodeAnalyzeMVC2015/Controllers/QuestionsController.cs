using CodeAnalyzeMVC2015.Models;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    //[RoutePrefix("Questions")]
    public class QuestionsController : BaseController
    {
        private Users user = new Users();
        //[Route("")]
        public ActionResult UnAns(string ddType)
        {
            List<QuestionModel> questions = new List<QuestionModel>();
            //if (ModelState.IsValid)
            //{
                string strSQL = string.Empty;
                if (!string.IsNullOrEmpty(ddType))
                    strSQL = "Select * from Question Where QuestionId > 37861 and QuestionTypeId = " + ddType;
                else
                    strSQL = "Select * from Question Where QuestionId > 37861 order by questionid desc";

                ConnManager connManager = new ConnManager();
                questions = connManager.GetQuestions(strSQL);
            //}

            ConnManager conn = new ConnManager();
            List<QuestionType> items = conn.GetQuestionType();
            ViewBag.DDItems = items;
            return View(questions);
        }

        public ActionResult Post()
        {
            ConnManager conn = new ConnManager();
            List<QuestionType> items = conn.GetQuestionType();
            QuestionType types = new QuestionType();
            types.Types = items;
            return View(types);
        }

        //[Route("{Id}/{Title}")]
        public ActionResult Soln(string SolutionEditor)
        {
            if (Request.Form["Submit"] != null)
            {
                return InsertAns(SolutionEditor);
            }
            if (Request.Form["Delete"] != null)
            {
                string Id = RouteData.Values["Id"].ToString();
                string RId = Session["DeleteReplyId"].ToString(); // RouteData.Values["RId"].ToString();
                string Title = RouteData.Values["Title"].ToString();                
                return DeleteReply(Id, Title);
            }
            else if(Request.Form.Keys.Count == 3)
            {
                string Id = Request.Form.Keys[0].ToString();
                string RId = Request.Form.Keys[1].ToString();
                string VoteType = Request.Form.Keys[2].ToString();

                if (VoteType.Equals("UP"))
                {
                    return UpVote(Id, RId, null);
                }
                else //if (VoteType.Equals("DOWN"))
                {
                    return DownVote(Id, RId, null);
                }
            }
            else
            {
                VwSolutionsModel model = SetDefaults();
                return View(model);
            }           
        }

        public ActionResult InsertQuestion(string txtTitle, string ddType, string EditorAskQuestion)
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                double dblQuestionID = 0;
                Question question = new Question();
                SqlConnection LclConn = new SqlConnection();
                SqlTransaction SetTransaction = null;
                bool IsinTransaction = false;
                if (LclConn.State != ConnectionState.Open)
                {
                    question.SetConnection = question.OpenConnection(LclConn);
                    SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                    IsinTransaction = true;
                }
                else
                {
                    question.SetConnection = LclConn;
                }
                question.QuestionTitle = txtTitle;
                question.QuestionTypeId = int.Parse(ddType);
                question.OptionID = 1;

                EditorAskQuestion = EditorAskQuestion.Replace("&lt;", "<");
                EditorAskQuestion = EditorAskQuestion.Replace("&gt;", ">");
                EditorAskQuestion = EditorAskQuestion.Replace("&amp;", "&");
                EditorAskQuestion = EditorAskQuestion.Replace("&apos;", "'");

                EditorAskQuestion = EditorAskQuestion.Replace("<div>", "");
                EditorAskQuestion = EditorAskQuestion.Replace("</div>", "");
                EditorAskQuestion = EditorAskQuestion.Replace("<html>", "");
                EditorAskQuestion = EditorAskQuestion.Replace("</html>", "");
                EditorAskQuestion = EditorAskQuestion.Replace("<body>", "");
                EditorAskQuestion = EditorAskQuestion.Replace("</body>", "");

                if (EditorAskQuestion.Length > 10000)
                {
                    EditorAskQuestion = EditorAskQuestion.Substring(0, 10000);
                }

                question.QuestionDetails = Sanitizer.GetSafeHtml(EditorAskQuestion);
                question.AskedDateTime = DateTime.Now;
                question.AskedUser = user.UserId;

                bool result = question.CreateQuestion(ref dblQuestionID, SetTransaction);

                if (IsinTransaction && result)
                {
                    SetTransaction.Commit();
                    Mail mail = new Mail();
                    mail.Body = "<a>www.codeanalyze.com/Soln.aspx?QId=" + dblQuestionID.ToString() + "&QT=" + txtTitle + "</a>";
                    mail.FromAdd = "admin@codeanalyze.com";
                    mail.Subject = txtTitle;
                    mail.ToAdd = "admin@codeanalyze.com";
                    mail.IsBodyHtml = true;

                    if (user.Email != "admin@codeanalyze.com")
                    {
                        mail.SendMail();
                    }
                }
                else
                {
                    SetTransaction.Rollback();
                }
                question.CloseConnection(LclConn);


                string title = txtTitle;
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9 -]");
                txtTitle = rgx.Replace(txtTitle, "");


                string strAck = "Question posted successfully, we will email you when users post answers.<br /> View your posted question ";
                if (Request.Url.ToString().Contains("localhost"))
                    strAck += "<a style=\"color:blue;text-decoration:underline\" href=\"/CodeAnalyzeMVC2015/Questions/Soln/" + dblQuestionID.ToString() + "/" + txtTitle.ToString().Replace(" ", "-") + "\">here</a>";
                else
                    strAck += "<a style=\"color:blue;text-decoration:underline\" href=\"http://codeanalyze.com/Questions/Soln/" + dblQuestionID.ToString() + "/" + txtTitle.ToString().Replace(" ", "-") + "\">here</a>";
                strAck += "<br />";

                ViewBag.Ack = strAck;
            }

            ConnManager conn = new ConnManager();
            List<QuestionType> items = conn.GetQuestionType();
            QuestionType types = new QuestionType();
            types.Types = items;
            return View("../Questions/Post", types);
        }

        public ActionResult InsertAns(string SolutionEditor)
        {
            VwSolutionsModel model = new VwSolutionsModel();
            string strContent = SolutionEditor;

            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                string quesID = RouteData.Values["id"].ToString(); 

                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();

                double dblReplyID = 0;
                Replies replies = new Replies();
                SqlConnection LclConn = new SqlConnection();
                SqlTransaction SetTransaction = null;
                bool IsinTransaction = false;
                if (LclConn.State != ConnectionState.Open)
                {
                    replies.SetConnection = replies.OpenConnection(LclConn);
                    SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                    IsinTransaction = true;
                }
                else
                {
                    replies.SetConnection = LclConn;
                }

                replies.OptionID = 1;
                replies.QuestionId = double.Parse(quesID.ToString());

                strContent = strContent.Replace("&lt;", "<");
                strContent = strContent.Replace("&gt;", ">");
                strContent = strContent.Replace("&amp;", "&");
                strContent = strContent.Replace("&apos;", "'");

                strContent = strContent.Replace("<div>", "");
                strContent = strContent.Replace("</div>", "");
                strContent = strContent.Replace("<html>", "");
                strContent = strContent.Replace("</html>", "");
                strContent = strContent.Replace("<body>", "");
                strContent = strContent.Replace("</body>", "");

                if (user.FirstName.Equals("Admin"))
                {
                    replies.Reply = strContent;
                }
                else
                {
                    replies.Reply = Sanitizer.GetSafeHtml(strContent);
                }

                replies.RepliedDate = DateTime.Now;
                replies.RepliedUser = user.UserId;
                bool result = replies.CreateReplies(ref dblReplyID, SetTransaction);


                if (IsinTransaction && result)
                {
                    SetTransaction.Commit();
                    if (!Session["AskedUserEMail"].ToString().Contains("codeanalyze.com"))
                    {
                        Mail mail = new Mail();

                        string EMailBody = System.IO.File.ReadAllText(Server.MapPath("../../../EMailBody.txt"));

                        string strLink = "www.codeanalyze.com/Questions/Index/Title/" + quesID.ToString() + "/" + model.QuestionTitle + "";

                        string strBody = "Your question on CodeAnalyse has been answered by one of the users. Check now <a href=" + strLink + "\\>" + model.QuestionTitle + "</a>";

                        mail.Body = string.Format(EMailBody, strBody);


                        mail.FromAdd = "admin@codeanalyze.com";
                        mail.Subject = "Code Analyze - Received response for " + model.QuestionTitle;
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
                replies.CloseConnection(LclConn);
                ViewBag.ReplyId = dblReplyID;
                model = SetDefaults();
                //GetQuestionData(quesID.ToString(), ref model);
                //BindSolution("Select * from VwSolutions where QuestionId = " + quesID.ToString(), null);                
                //ViewBag.lblAck = string.Empty;
            }
            else
            {
                ViewBag.lblAck = "Please sign in to post your question.";
            }
            return View("../Questions/Soln", model);
        }

        public ActionResult DeleteReply(string Id, string Title)
        {
            VwSolutionsModel model = new VwSolutionsModel();
            if (Session["DeleteReplyId"] != null)
            {
                if (Id != null)
                {
                    ConnManager conn = new ConnManager();
                    conn.DeleteReply(Session["DeleteReplyId"].ToString());
                    model = SetDefaults();
                }
                Session["DeleteReplyId"] = null;
            }
            return View("../Questions/Soln", model);
        }

        public ActionResult UpVote(string Id, string RId, string Title)
        {
            ProcessVotes("Up", RId, Id);
            RouteData.Values["id"] = Id;
            VwSolutionsModel model = SetDefaults();
            return View("../Questions/Soln", model);
        }

        public ActionResult DownVote(string Id, string RId, string Title)
        {
            ProcessVotes("Down", RId, Id);
            RouteData.Values["id"] = Id;
            VwSolutionsModel model = SetDefaults();
            return View("../Questions/Soln", model);
        }

        private VwSolutionsModel SetDefaults()
        {
            //if (Session["User"] != null)
            //{
            //    user = (Users)Session["User"];
            //    ViewBag.UserEMail = user.Email;
            //}

            string quesID;
            string questionTitle = string.Empty;

            quesID = RouteData.Values["id"].ToString();

            VwSolutionsModel model = new VwSolutionsModel();
            GetQuestionData(quesID.ToString(), ref model);

            questionTitle = model.QuestionTitle;

            if(string.IsNullOrEmpty(questionTitle))
            {
                questionTitle = RouteData.Values["title"].ToString();
            }

            ViewBag.Description = questionTitle.Replace("-", " ");
            ViewBag.keywords = questionTitle.Replace("-", " ");

            if (quesID != null)
            {
                BindSolution("Select * from VwSolutions where QuestionId =" + quesID.ToString(), ref model);
            }

            if (user.Email != null)
            {
                ViewBag.lblAck = string.Empty;
                ViewBag.hfUserEMail = user.Email;
            }
            else
            {
              //  ViewBag.lblAck = "Please sign in to post your answer.";
                ViewBag.hfUserEMail = string.Empty;
            }
            return model;
        }

        private void GetQuestionData(string strQuestionId, ref VwSolutionsModel model)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable dsQuestion = connManager.GetQuestion(strQuestionId);
            connManager.DisposeConn();
            long quesID;

            string strQuestionDetails = String.Empty;

            if (dsQuestion != null)
            {
                if (dsQuestion.Rows.Count > 0)
                {
                    quesID = long.Parse(dsQuestion.Rows[0]["QuestionId"].ToString());
                    model.QuestionID = quesID.ToString();
                    if (!dsQuestion.Rows[0]["EMail"].ToString().Contains("codeanalyze.com"))
                    {
                        model.AskedUser = "Posted By: " + dsQuestion.Rows[0]["FirstName"].ToString() + " ";
                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["LastName"].ToString()))
                        {
                            model.AskedUser = model.AskedUser + "" + dsQuestion.Rows[0]["LastName"].ToString() + "";
                        }

                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["ImageURL"].ToString()))
                            model.ImageURL = dsQuestion.Rows[0]["ImageURL"].ToString();
                        //else
                        //    model.ImageURL = "~/Images/Person.JPG";
                    }
                    //else
                    //{
                    //    model.ImageURL = "~/Images/Person.JPG";
                    //}


                    Session["AskedUserEMail"] = dsQuestion.Rows[0]["EMail"].ToString();
                    model.QuestionTitle = dsQuestion.Rows[0]["QuestionTitle"].ToString();

                    strQuestionDetails = dsQuestion.Rows[0]["Question"].ToString().Replace("font-size: x-small", "font-size: medium");
                    strQuestionDetails = StringClean(strQuestionDetails);

                    model.QuestionDetails = "<table style=\"width:100%\"><tr><td>" + strQuestionDetails + "</td></tr></table>";
                    ViewBag.QuestionDetails = model.QuestionDetails;

                    model.QuestionViews = "<b>" + dsQuestion.Rows[0]["Views"].ToString() + "<b>";

                }
            }
        }

        private void BindSolution(string strQuery, ref VwSolutionsModel model)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable dsSolution = connManager.GetDataTable(strQuery);
            string quesID = RouteData.Values["id"].ToString();
            string strReplyId = "";
            string lblUp, lblDown = "0";
            string tblReplies = "<table style=\"word-wrap:normal; word-break:break-all; width:98%\">";
            string strDeleteRow = string.Empty;
            string strTitle = string.Empty;

            if (RouteData.Values["Title"]!=null )
            { 
                strTitle = RouteData.Values["Title"].ToString();
            }
            else
            {
                strTitle = model.QuestionTitle;
            }

            if (dsSolution != null && dsSolution.Rows.Count > 0)
            {
                for (int i = 0; i < dsSolution.Rows.Count; i++)
                {

                    lblUp = dsSolution.Rows[i]["ThumbsUp"].ToString();
                    lblDown = dsSolution.Rows[i]["ThumbsDown"].ToString();

                    strReplyId = dsSolution.Rows[i]["ReplyID"].ToString();
                    //Response no user details
                    string htrResponseNoByDetailsOuterRow = "<tr>";
                    string htcResponseNoByDetailsOuterCell = "<td style=\"background-color:#4fa4d5\">";

                    #region table
                    string htmlTblResponseNoByDetails = "<table style=\"width:100%\">";

                    string htmlRowResponseNoByDetails = "<tr style=\"width:100%\">";

                    string htcUserImage = "<td> ";
                    if (!string.IsNullOrEmpty(dsSolution.Rows[i]["ImageURL"].ToString()))
                    {
                        if (Request.Url.ToString().Contains("localhost"))
                            htcUserImage += "<img src=\"/CodeAnalyzeMVC2015" + dsSolution.Rows[i]["ImageURL"].ToString().Replace("~", "") + "\" style=\"height:30px;width:30px\" />";
                        else
                            htcUserImage += "<img src=\"" + dsSolution.Rows[i]["ImageURL"].ToString().Replace("~", "").Replace("/CodeAnalyzeMVC2015", "") + "\" style=\"height:30px;width:30px\" />";
                    }
                    else
                    {
                        if (Request.Url.ToString().Contains("localhost"))
                            htcUserImage += "<img src=\"/CodeAnalyzeMVC2015/Images/Person.JPG\" style=\"height:25px;width:25px\" />";
                        else
                            htcUserImage += "<img src=\"/Images/Person.JPG\" style=\"height:25px;width:25px\" />";
                    }
                    htcUserImage += "</td>";


                    string htcResponseNoByDetails = "<td valign=\"middle\">";


                    #region responseNoBy
                    string strFirstName = "";
                    string strAnswers = "";
                    string strRepliedDate = "";

                    string strUserId = dsSolution.Rows[i]["UserId"].ToString();
                    if (!string.IsNullOrEmpty(dsSolution.Rows[i]["FirstName"].ToString()))
                        strFirstName = dsSolution.Rows[i]["FirstName"].ToString();
                    else
                        strFirstName = dsSolution.Rows[i]["EMail"].ToString().Split('@')[0];
                    strRepliedDate = dsSolution.Rows[i]["RepliedDate"].ToString().Split('@')[0];

                    strRepliedDate = DateTime.Parse(strRepliedDate).ToShortDateString();

                    DataTable dsCount = new DataTable();
                    dsCount = connManager.GetDataTable("SELECT COUNT(*) FROM VwSolutions WHERE (RepliedUser = " + strUserId + ") AND (AskedUser <> " + strUserId + ")");

                    if (dsCount != null && dsCount.Rows.Count > 0)
                    {
                        if (dsCount.Rows[0][0].ToString() != "0")
                        {
                            if (dsCount != null && dsCount.Rows.Count > 0)
                                strAnswers = dsCount.Rows[0][0].ToString();
                            else
                                strAnswers = "<b>none</b>";


                            if (!dsSolution.Rows[i]["EMail"].ToString().Contains("codeanalyze.com"))
                                htcResponseNoByDetails += "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";   // Total replies by user: " + strAnswers + ".";
                            else
                                htcResponseNoByDetails += "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                            //htc4.InnerHtml = "Comment No <b>" + (i + 1).ToString();
                        }
                        else
                            if (!strFirstName.ToLower().Equals("admin"))
                            htcResponseNoByDetails += "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                        else
                            htcResponseNoByDetails += "Response No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + "";
                    }
                    else
                        if (!strFirstName.ToLower().Equals("admin"))
                        htcResponseNoByDetails += "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                    else
                        htcResponseNoByDetails += "Response No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + " ";

                    #endregion
                    htcResponseNoByDetails += "</td>";

                    htmlRowResponseNoByDetails += htcUserImage;
                    htmlRowResponseNoByDetails += htcResponseNoByDetails;
                    htmlRowResponseNoByDetails += AddThumbsUpDown(i, quesID, strReplyId, lblUp, lblDown);


                    htmlRowResponseNoByDetails += "</tr>";
                    htmlTblResponseNoByDetails += htmlRowResponseNoByDetails + "</table>";
                    #endregion

                    htcResponseNoByDetailsOuterCell += htmlTblResponseNoByDetails + "</td>";
                    htrResponseNoByDetailsOuterRow += htcResponseNoByDetailsOuterCell + "</tr>";


                    //Solution Row
                    string htmlRowSolutionContent = "<tr>";
                    string htcReplyContent = "<td style=\"font-family:Calibri\" >";
                    strReplyId = dsSolution.Rows[i]["ReplyId"].ToString();
                    #region Reply
                    string strReply = dsSolution.Rows[i]["Reply"].ToString().Replace("font-size: x-small", "font-size: 16px");

                    strReply = StringClean(strReply);

                    #endregion
                    htcReplyContent += strReply + "</td>";
                    htmlRowSolutionContent += htcReplyContent + "</tr>";



                    if (ViewBag.ReplyId != null && strReplyId == Convert.ToString(ViewBag.ReplyId))
                    {
                        strDeleteRow += "<tr><td align=\"right\" style=\"color:red;font-weight:bold;font-family:Calibri;font-size:18px;\">";

                        //if (Request.Url.ToString().Contains("localhost"))
                            // strDeleteRow += "<a href=\"/CodeAnalyzeMVC2015/Questions/Soln/" + quesID + "/" + strTitle + "\" style=\"color:red;font-weight:bold;font-family:Calibri;font-size:18px;border:solid;border-width:1px;border-color:black\">Delete</a>";
                            strDeleteRow += "<input type=\"submit\" name=\"Delete\" value=\"Delete\"; style=\"color:red;font-weight:bold;font-family:Calibri;font-size:18px;border:solid;border-width:1px;border-color:black\">";
                        //else

                            // strDeleteRow += "<a href=\"http://codeanalyze.com/Questions/Soln/" + quesID + "/" + strTitle + "\"  style=\"color:red;font-weight:bold;font-family:Calibri;font-size:18px;border:solid;border-width:1px;border-color:black\">Delete</a>";

                            strDeleteRow += "</td></tr>";
                        Session["DeleteReplyId"] = strReplyId;
                    }

                    tblReplies += htrResponseNoByDetailsOuterRow + strDeleteRow + htmlRowSolutionContent;

                    tblReplies += "<tr><td><br /></td></tr>";

                }
                tblReplies += "</table>";
                model.AnswerDetails = tblReplies;
                ViewBag.AnswerDetails = tblReplies;
            }
            else
            {
                ViewBag.AnswerDetails = null;
            }
            
            connManager.DisposeConn();

        }

        private static string StringClean(string strReply)
        {

            strReply = strReply.Replace("\r\n            #codestart", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");
            strReply = strReply.Replace("#codeend\r\n        ", "</pre>");

            strReply = strReply.Replace("#codestart", "<pre>");
            strReply = strReply.Replace("#codeend", "</pre>");

            strReply = strReply.Replace("<br>\r\n", "\r\n");
            strReply = strReply.Replace("\r\n", "#####");
            strReply = strReply.Replace("<br>", "<br />");

            //foreach (Match regExp in Regex.Matches(strReply, @"\<pre\>(.*?)\<br />(.*?)\</pre\>", RegexOptions.IgnoreCase))
            //{
            //    strReply = strReply.Replace(regExp.Value, regExp.Value.Replace("<br />", ""));
            //}

            foreach (Match regExp in Regex.Matches(strReply, @"\<pre\>(.*?)\&nbsp; (.*?)\</pre\>", RegexOptions.IgnoreCase))
            {
                strReply = strReply.Replace(regExp.Value, regExp.Value.Replace("&nbsp; ", " "));
            }

            foreach (Match regExp in Regex.Matches(strReply, @"\<pre\>(.*?)\##########(.*?)\</pre\>", RegexOptions.IgnoreCase))
            {
                strReply = strReply.Replace(regExp.Value, regExp.Value.Replace("##########", "#####"));
            }

            strReply = strReply.Replace("<pre>", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");

            strReply = strReply.Replace("#####", "\r\n");
            return strReply;
        }

        private string AddThumbsUpDown(int i, string quesID, string Replyid, string lblUp, string lblDown)
        {
            string strThumbsUpDown = string.Empty;
            string htcThumpsUp = string.Empty;
            string htcThumpsDown = string.Empty;
            string upvote = "UP";
            string downvote = "DOWN";

            if (string.IsNullOrEmpty(lblUp))
                lblUp = "0";

            string strUpVoteLink = string.Empty;

            //if (Request.Url.ToString().Contains("localhost"))
            //    strUpVoteLink = "<a href=\"/CodeAnalyzeMVC2015/Questions/Soln/" + quesID + "/" + Replyid + "/" + strTitle + " name=\"lnkThumpsUp" + i.ToString() + "\" id=\"lnkThumpsUp" + i.ToString() + "\">";
            //else
            //    strUpVoteLink = "<a href=\"http://codeanalyze.com/Questions/Soln/" + quesID + "/" + Replyid + "/" + strTitle + " name=\"lnkThumpsUp" + i.ToString() + "\" id=\"lnkThumpsUp" + i.ToString() + "\">";
            //string strThumpsUp = "<td align=\"right\">" + strUpVoteLink + "<img src=\"/CodeAnalyzeMVC2015/Images/ThumpsUp.png\" style=\"height:30px;width:30px\" /></a>";

            strUpVoteLink = "<input type=\"image\" onclick=\"PostVotes('" + quesID + "', '" + Replyid + "', '" + upvote + "')\" value=\"Test\" src=\"/CodeAnalyzeMVC2015/Images/ThumpsUp.png\" style=\"height:30px;width:30px\" name=\"lnkThumpsDown" + i.ToString() + "\" id=\"lnkThumpsDown" + i.ToString() + "\" />";


            string strThumpsUp = "<td align=\"right\">" + strUpVoteLink;
            strThumpsUp += "</td><td>" + lblUp + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";
            htcThumpsUp += strThumpsUp;


            if (string.IsNullOrEmpty(lblDown))
                lblDown = "0";

            string strDownVoteLink = string.Empty;
            //if (Request.Url.ToString().Contains("localhost"))
            //    strDownVoteLink = "<a href=\"/CodeAnalyzeMVC2015/Questions/Soln/" + quesID + "/" + Replyid + "/" + strTitle + " name=\"lnkThumpsDown" + i.ToString() + "\" id=\"lnkThumpsDown" + i.ToString() + "\">";
            //else
            //    strDownVoteLink = "<a href=\"http://codeanalyze.com/Questions/Soln/" + quesID + "/" + Replyid + "/" + strTitle + " name=\"lnkThumpsDown" + i.ToString() + "\" id=\"lnkThumpsDown" + i.ToString() + "\">";

            strDownVoteLink = "<input type=\"image\" onclick=\"PostVotes('" + quesID + "', '" + Replyid + "', '" + downvote + "')\" value=\"Test\" src=\"/CodeAnalyzeMVC2015/Images/ThumpsDown.png\" style=\"height:30px;width:30px\" name=\"lnkThumpsDown" + i.ToString() + "\" id=\"lnkThumpsDown" + i.ToString() + "\" />";

            //string strThumpsDown = "<td align=\"right\">" + strDownVoteLink + "<img src=\"/CodeAnalyzeMVC2015/Images/ThumpsDown.png\" style=\"height:30px;width:30px\" /></a>";

            string strThumpsDown = "<td align=\"right\">" + strDownVoteLink;
            strThumpsDown += "</td><td>" + lblDown + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";
            htcThumpsDown += strThumpsDown;

            strThumbsUpDown += htcThumpsUp + htcThumpsDown;


            return strThumbsUpDown;
        }

        private void ProcessVotes(string LikeType, string ReplyId, string quesID)
        {
            List<string> lstReplies = (List<string>)Session["Replies"];
            string strQuery = "";
            int votes = 0;
            if (lstReplies == null)
            {
                lstReplies = new List<string>();
            }

            if (!lstReplies.Contains(ReplyId))
            {

                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();

                DataTable dsVotes = connManager.GetDataTable("Select ThumbsUp, ThumbsDown from Replies where ReplyId = " + ReplyId);

                if (dsVotes != null && dsVotes.Rows.Count > 0)
                {
                    if (LikeType.Equals("Up"))
                    {
                        if (string.IsNullOrEmpty(dsVotes.Rows[0]["ThumbsUp"].ToString()))
                            votes = votes + 1;
                        else
                            votes = int.Parse(dsVotes.Rows[0]["ThumbsUp"].ToString()) + 1;

                        strQuery = "Update Replies set ThumbsUp = " + votes + " where ReplyId = " + ReplyId;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dsVotes.Rows[0]["ThumbsDown"].ToString()))
                            votes = votes - 1;
                        else
                            votes = int.Parse(dsVotes.Rows[0]["ThumbsDown"].ToString()) + 1;

                        strQuery = "Update Replies set ThumbsDown = " + votes + " where ReplyId = " + ReplyId;
                    }
                }

                SqlCommand command = new SqlCommand(strQuery, connManager.DataCon);

                command.ExecuteNonQuery();
                connManager.DisposeConn();

                lstReplies.Add(ReplyId);
                Session["Replies"] = lstReplies;
            }
            //BindQuestionAskedUserData("Select * from VwQuestions where QuestionId = " + quesID.ToString() + "");
        }

    }
}