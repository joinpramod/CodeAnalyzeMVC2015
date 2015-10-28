using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Microsoft.Security.Application;
using CodeAnalyzeMVC2015;
using System.Text.RegularExpressions;

public partial class Solutions : System.Web.UI.UserControl
{
    Users user = new Users();
    long quesID;
    string questionTitle;


    //protected override void OnPreInit(EventArgs e)
    //{
    //    //base.OnPreInit(e);
    //    if (Session["OpenIndex"] != null && Session["OpenIndex"] == "true")
    //    {
    //        this.MasterPageFile = "MasterPage2.master";
    //        //Session["OpenIndex"] = null;
    //    }
    //    else
    //        this.MasterPageFile = "MasterPage.master";
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        user = (Users)Session["User"];
        if (!long.TryParse(Request.QueryString["QId"], out quesID))
            long.TryParse(Encryption.DecryptQueryString(Request.QueryString["QId"]), out quesID);

        if (Request.QueryString["QT"] != null)
            questionTitle = Request.QueryString["QT"].ToString();

        if (!string.IsNullOrEmpty(questionTitle))
            Page.Title = questionTitle.Replace("-", " ");
        HtmlMeta metaDescription = new HtmlMeta();
        metaDescription.Name = "description";
        if (!string.IsNullOrEmpty(questionTitle))
            metaDescription.Content = questionTitle.Replace("-", " ");
        Page.Header.Controls.Add(metaDescription);
        HtmlMeta metaKeywords = new HtmlMeta();
        metaKeywords.Name = "keywords";
        if (!string.IsNullOrEmpty(questionTitle))
            metaKeywords.Content = questionTitle.Replace("-", " ");
        Page.Header.Controls.Add(metaKeywords);
        GetQuestionData(quesID.ToString());

        if (!IsPostBack)
        {
            if (quesID != null && quesID != 0)
            {
                //BindQuestionAskedUserData("Select * from VwQuestions where QuestionId = " + quesID.ToString() + "");

                BindSolution("Select * from VwSolutions where QuestionId =" + quesID.ToString(), null);
                //Page.Title = "Code Analyze - " + questionTitle;
                //Page.Title = questionTitle;
                //HtmlMeta metaDescription = new HtmlMeta();
                //metaDescription.Name = "description";
                //metaDescription.Content = questionTitle;
                //Page.Header.Controls.Add(metaDescription);
                //HtmlMeta metaKeywords = new HtmlMeta();
                //metaKeywords.Name = "keywords";
                //metaKeywords.Content = questionTitle;
                //Page.Header.Controls.Add(metaKeywords);
                //if (user != null)
                //    pnlMyQuesAns.Visible = true;
                //else
                //    pnlMyQuesAns.Visible = false;

                // pnlSolutionText.Visible = false;
            }
            else
            {
                // pnlSolutionText.Visible = true;
            }
        }



        if (user != null)
        {
            lblAck.Visible = false;
            hfUserEMail.Value = user.Email;
        }
        else
        {
            lblAck.Visible = true;
            lblAck.Font.Bold = true;
            lblAck.Text = "Please sign in to post your answer.";
            lblAck.ForeColor = Color.Red;
            hfUserEMail.Value = "";
        }


        if (Request.QueryString["ReplyId"] != null && Request.QueryString["Votetype"] != null)
        {
            if (Request.QueryString["Votetype"].ToString().Equals("Up"))
            {
                ProcessVotes("Up", Request.QueryString["ReplyId"]);
            }
            else
            {
                ProcessVotes("Down", Request.QueryString["ReplyId"]);
            }
        }

        if (Request.QueryString["ActionType"] != null && Request.QueryString["ActionType"] == "Delete")
        {
            if (Request.QueryString["ReplyId"] != null)
            {
                DeleteReply(Request.QueryString["ReplyId"].ToString());
                BindSolution("Select * from VwSolutions where QuestionId =" + Request.QueryString["QId"].ToString() + "", null);
            }
        }


        // Page.RegisterHiddenField("__EVENTTARGET", "btnSearch");
    }

    protected void PreviewButton_Click(object sender, EventArgs e)
    {
        //if (Session["AskedUserEMail"] == null)
        //{
        //    lblSessionTimeOut.Text = "Session time out";
        //    lblSessionTimeOut.Visible = true;
        //    return;
        //}
        //lblSessionTimeOut.Visible = false;
        string strContent = SolutionEditor.Content;
        if (Session["User"] != null)
        {

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

            //SolutionEditor.Content = SolutionEditor.Content.Replace("&amp;", "&")
            //    .Replace("&lt;", "<")
            //    .Replace("&gt;", ">")
            //    .Replace("&apos;", "'")
            //    .Replace("&quot;", "\"");

            //replies.Reply = customEditor2.Content;
            //replies.Reply = Utilities.ExpandUrls(SolutionEditor.Content);


            //    SolutionEditor.Content = SolutionEditor.Content.Replace("#codestart", "<pre class=\"prettyprint\" style=\"font-size:16px;\">");
            //    SolutionEditor.Content = SolutionEditor.Content.Replace("#codeend", "</pre>");

            strContent = strContent.Replace("<div>", "");
            strContent = strContent.Replace("</div>", "");

            if (user.FirstName.Equals("Admin"))
            {
                replies.Reply = strContent;
            }
            else
            {
                if (strContent.Length > 10000)
                {
                    SolutionEditor.Content = strContent.Substring(0, 10000);
                }

                replies.Reply = Sanitizer.GetSafeHtml(strContent);
            }



            //replies.Reply = SolutionEditor.Content;
            // replies.Reply = ExpandUrls(SolutionEditor.Text);
            replies.RepliedDate = DateTime.Now;

            if (Session["User"] != null)
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    LinkedInUser(user.UserId, user);
                    user.Email = txtEMail.Text;
                }
                replies.RepliedUser = user.UserId;
            }
            else
            {
                double dblUser = 0;
                if (!UserExists(ref dblUser))
                    replies.RepliedUser = CreateUser(txtEMail.Text);
                else
                    replies.RepliedUser = dblUser;
            }
            bool result = replies.CreateReplies(ref dblReplyID, SetTransaction);


            if (IsinTransaction && result)
            {
                SetTransaction.Commit();

                if (!Session["AskedUserEMail"].ToString().Contains("codeanalyze.com"))
                {
                    Mail mail = new Mail();

                    string EMailBody = File.ReadAllText(Server.MapPath("EMailBody.txt"));

                    string strLink = "www.codeanalyze.com/Soln.aspx?QId=" + quesID.ToString() + "&QT=" + questionTitle + "";

                    string strBody = "Your question on CodeAnalyse has been answered by one of the users. Check now <a href=" + strLink + "\\>" + questionTitle + "</a>";

                    mail.Body = string.Format(EMailBody, strBody);


                    mail.FromAdd = "admin@codeanalyze.com";
                    mail.Subject = "Code Analyze - Received response for " + questionTitle;
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
            BindSolution("Select * from VwSolutions where QuestionId = " + quesID.ToString(), dblReplyID.ToString());
            GetQuestionData(quesID.ToString());
            Session["votes"] = "false";
            lblAck.Visible = false;
        }
        else
        {
            lblAck.Visible = true;
            lblAck.Font.Bold = true;
            lblAck.Text = "Please sign in to post your question.";
        }
    }

    private void BindQuestionAskedUserData(string strQuery)
    {
        ConnManager connManager = new ConnManager();
        connManager.OpenConnection();
        DataSet dsQuestion = connManager.GetData(strQuery);
        connManager.DisposeConn();
        if (dsQuestion != null)
        {
            if (dsQuestion.Tables[0].Rows.Count > 0)
            {
                quesID = long.Parse(dsQuestion.Tables[0].Rows[0]["QuestionId"].ToString());

                if (!dsQuestion.Tables[0].Rows[0]["EMail"].ToString().Contains("codeanalyze.com"))
                {
                    if (!string.IsNullOrEmpty(dsQuestion.Tables[0].Rows[0]["FirstName"].ToString()))
                    {
                        lblAskedUser.Visible = true;
                        lblAskedUser.Text = "Posted By: <b>" + dsQuestion.Tables[0].Rows[0]["FirstName"].ToString() + "<b>";
                        if (!string.IsNullOrEmpty(dsQuestion.Tables[0].Rows[0]["LastName"].ToString()))
                        {
                            lblAskedUser.Text = lblAskedUser.Text + " <b>" + dsQuestion.Tables[0].Rows[0]["LastName"].ToString() + "<b>";
                        }
                    }
                    else
                    {
                        lblAskedUser.Visible = false;
                        //lblAskedUser.Text = "Posted By: <b>" + dsQuestion.Tables[0].Rows[0]["EMail"].ToString().Split('@')[0] + "<b>";
                    }


                    if (!string.IsNullOrEmpty(dsQuestion.Tables[0].Rows[0]["ImageURL"].ToString()))
                        imgAskedUser.ImageUrl = dsQuestion.Tables[0].Rows[0]["ImageURL"].ToString();
                    else
                        imgAskedUser.ImageUrl = "~/Images/Person.JPG";
                    imgAskedUser.Visible = true;
                }
                else
                    imgAskedUser.Visible = false;


                Session["AskedUserEMail"] = dsQuestion.Tables[0].Rows[0]["EMail"].ToString();


                //Session["QuestionTitle"] = dsQuestion.Tables[0].Rows[0]["QuestionTitle"].ToString();
                lblQuestionTitle.Text = "<b>" + dsQuestion.Tables[0].Rows[0]["QuestionTitle"].ToString() + "<b>";


                divQuestionDetails.InnerHtml = dsQuestion.Tables[0].Rows[0]["Question"].ToString();//.Replace("font-size: x-small", "font-size: medium");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("\r\n            #codestart", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#codeend\r\n        ", "</pre>");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#codestart", "<pre>");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#codeend", "</pre>");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("\r\n", "#####");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<br>", "<br />");

                foreach (Match regExp in Regex.Matches(divQuestionDetails.InnerHtml, @"\<pre\>(.*?)\<br />(.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("<br />", ""));
                }

                foreach (Match regExp in Regex.Matches(divQuestionDetails.InnerHtml, @"\<pre\>(.*?)\&nbsp; (.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("&nbsp; ", " "));
                }

                foreach (Match regExp in Regex.Matches(divQuestionDetails.InnerHtml, @"\<pre\>(.*?)\##########(.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("##########", "#####"));
                }


                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<pre>", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#####", "\r\n");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<html>", "");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("</html>", "");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<body>", "");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("</body>", "");

                divQuestionDetails.Style.Add("font-family", "Calibri");

                //divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<pre>", "<pre class=prestyle>");

                lblViews.Text = "24"; //"<b>" + dsQuestion.Tables[0].Rows[0]["Views"].ToString() + "<b>";

                // divQuestionDetails.Style.Add("width", "100%");
                BindSolution("Select * from VwSolutions where QuestionId =" + quesID.ToString(), null);
            }
        }



    }


    private void GetQuestionData(string strQuestionId)
    {
        ConnManager connManager = new ConnManager();
        connManager.OpenConnection();
        DataTable dsQuestion = connManager.GetQuestion(strQuestionId);
        connManager.DisposeConn();
        if (dsQuestion != null)
        {
            if (dsQuestion.Rows.Count > 0)
            {
                quesID = long.Parse(dsQuestion.Rows[0]["QuestionId"].ToString());

                if (!dsQuestion.Rows[0]["EMail"].ToString().Contains("codeanalyze.com"))
                {
                    if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["FirstName"].ToString()))
                    {
                        lblAskedUser.Visible = true;
                        lblAskedUser.Text = "Posted By: <b>" + dsQuestion.Rows[0]["FirstName"].ToString() + "<b>";
                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["LastName"].ToString()))
                        {
                            lblAskedUser.Text = lblAskedUser.Text + " <b>" + dsQuestion.Rows[0]["LastName"].ToString() + "<b>";
                        }
                    }
                    else
                    {
                        lblAskedUser.Visible = false;
                        //lblAskedUser.Text = "Posted By: <b>" + dsQuestion.Rows[0]["EMail"].ToString().Split('@')[0] + "<b>";
                    }


                    if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["ImageURL"].ToString()))
                        imgAskedUser.ImageUrl = dsQuestion.Rows[0]["ImageURL"].ToString();
                    else
                        imgAskedUser.ImageUrl = "~/Images/Person.JPG";
                    imgAskedUser.Visible = true;
                }
                else
                    imgAskedUser.Visible = false;


                Session["AskedUserEMail"] = dsQuestion.Rows[0]["EMail"].ToString();


                //Session["QuestionTitle"] = dsQuestion.Rows[0]["QuestionTitle"].ToString();
                lblQuestionTitle.Text = "<b>" + dsQuestion.Rows[0]["QuestionTitle"].ToString() + "<b>";
                divQuestionDetails.InnerHtml = dsQuestion.Rows[0]["Question"].ToString().Replace("font-size: x-small", "font-size: medium");
                //divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.ToString().Replace("<b>", "");
                //divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.ToString().Replace("</b>", "");
                divQuestionDetails.Style.Add("font-family", "Calibri");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("\r\n            #codestart", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#codeend\r\n        ", "</pre>");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#codestart", "<pre>");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#codeend", "</pre>");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("\r\n", "#####");

                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<br>", "<br />");

                foreach (Match regExp in Regex.Matches(divQuestionDetails.InnerHtml, @"\<pre\>(.*?)\<br />(.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("<br />", ""));
                }

                foreach (Match regExp in Regex.Matches(divQuestionDetails.InnerHtml, @"\<pre\>(.*?)\&nbsp; (.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("&nbsp; ", " "));
                }

                foreach (Match regExp in Regex.Matches(divQuestionDetails.InnerHtml, @"\<pre\>(.*?)\##########(.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("##########", "#####"));
                }


                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<pre>", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("#####", "\r\n");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<html>", "");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("</html>", "");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<body>", "");
                divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("</body>", "");


                //  divQuestionDetails.InnerHtml = divQuestionDetails.InnerHtml.Replace("<pre>", "<pre class=prestyle>");

                lblViews.Text = "<b>" + dsQuestion.Rows[0]["Views"].ToString() + "<b>";

                // divQuestionDetails.Style.Add("width", "100%");

            }
        }
    }

    private void BindSolution(string strQuery, string replyId)
    {
        ConnManager connManager = new ConnManager();
        connManager.OpenConnection();
        DataSet dsSolution = connManager.GetData(strQuery);
        tblReplies.Width = "100%";
        string strReplyId = "";
        string lblUp, lblDown = "0";

        if (dsSolution != null)
        {
            for (int i = 0; i < dsSolution.Tables[0].Rows.Count; i++)
            {
                HtmlTableRow htmlRowSolutionContent = new HtmlTableRow();
                HtmlTableCell htcReplyContent = new HtmlTableCell();

                strReplyId = dsSolution.Tables[0].Rows[i]["ReplyId"].ToString();
                lblUp = dsSolution.Tables[0].Rows[i]["ThumbsUp"].ToString();
                lblDown = dsSolution.Tables[0].Rows[i]["ThumbsDown"].ToString();
                //  viewsCount= dsSolution.Tables[0].Rows[i]["Views"].ToString();
                htcReplyContent.InnerHtml = dsSolution.Tables[0].Rows[i]["Reply"].ToString().Replace("font-size: x-small", "font-size: 16px");

                //  htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("<pre>", "<pre class=prestyle>");


                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("\r\n            #codestart", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");
                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("#codeend\r\n        ", "</pre>");

                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("#codestart", "<pre>");
                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("#codeend", "</pre>");

                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("\r\n", "#####");


                foreach (Match regExp in Regex.Matches(htcReplyContent.InnerHtml, @"\<pre\>(.*?)\<br />(.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("<br />", ""));
                }

                foreach (Match regExp in Regex.Matches(htcReplyContent.InnerHtml, @"\<pre\>(.*?)\&nbsp; (.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("&nbsp; ", " "));
                }

                foreach (Match regExp in Regex.Matches(htcReplyContent.InnerHtml, @"\<pre\>(.*?)\##########(.*?)\</pre\>", RegexOptions.IgnoreCase))
                {
                    htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace(regExp.Value, regExp.Value.Replace("##########", "#####"));
                }

                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("<pre>", "<pre class=\"prettyprint\" style=\"font-size:14px;\">");

                htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("#####", "\r\n");

                // htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("#codeend", "</pre>");

                //  htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("<br />", "");


                //this is source code control
                htcReplyContent.Style.Add("font-family", "Calibri");
                htcReplyContent.ColSpan = 2;
                htmlRowSolutionContent.Cells.Add(htcReplyContent);

                HtmlTableRow htrResponseNoByDetailsOuterRow = new HtmlTableRow();
                HtmlTableCell htcResponseNoByDetailsOuterCell = new HtmlTableCell();

                HtmlTable htmlTblResponseNoByDetails = new HtmlTable();
                htmlTblResponseNoByDetails.Width = "100%";
                HtmlTableRow htmlRowResponseNoByDetails = new HtmlTableRow();

                HtmlTableCell htcUserImage = new HtmlTableCell();
                HtmlTableCell htcResponseNoByDetails = new HtmlTableCell();
                HtmlImage htmlImage = new HtmlImage();
                HtmlTableRow htmlRowBreak = new HtmlTableRow();
                HtmlTableCell htcBreak = new HtmlTableCell();
                if (!string.IsNullOrEmpty(dsSolution.Tables[0].Rows[i]["ImageURL"].ToString()))
                    htmlImage.Src = dsSolution.Tables[0].Rows[i]["ImageURL"].ToString();
                else
                    htmlImage.Src = "~/Images/Person.JPG";


                htcUserImage.Controls.Add(htmlImage);
                htcUserImage.Width = "25px";
                htmlImage.Height = 25;
                htmlImage.Width = 25;

                string strFirstName = "";
                string strAnswers = "";
                string strRepliedDate = "";

                string strUserId = dsSolution.Tables[0].Rows[i]["UserId"].ToString();
                if (!string.IsNullOrEmpty(dsSolution.Tables[0].Rows[i]["FirstName"].ToString()))
                    strFirstName = dsSolution.Tables[0].Rows[i]["FirstName"].ToString();
                else
                    strFirstName = dsSolution.Tables[0].Rows[i]["EMail"].ToString().Split('@')[0];
                strRepliedDate = dsSolution.Tables[0].Rows[i]["RepliedDate"].ToString().Split('@')[0];

                strRepliedDate = DateTime.Parse(strRepliedDate).ToShortDateString();

                DataSet dsCount = new DataSet();
                dsCount = connManager.GetData("SELECT COUNT(*) FROM VwSolutions WHERE (RepliedUser = " + strUserId + ") AND (AskedUser <> " + strUserId + ")");

                if (dsCount != null && dsCount.Tables.Count > 0 && dsCount.Tables[0].Rows.Count != 0)
                {
                    if (dsCount.Tables[0].Rows[0][0].ToString() != "0")
                    {
                        if (dsCount != null && dsCount.Tables.Count > 0)
                            strAnswers = dsCount.Tables[0].Rows[0][0].ToString();
                        else
                            strAnswers = "<b>none</b>";


                        if (!dsSolution.Tables[0].Rows[i]["EMail"].ToString().Contains("codeanalyze.com"))
                            htcResponseNoByDetails.InnerHtml = "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";   // Total replies by user: " + strAnswers + ".";
                        else
                            htcResponseNoByDetails.InnerHtml = "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                        //htc4.InnerHtml = "Comment No <b>" + (i + 1).ToString();
                    }
                    else
                        if (!strFirstName.ToLower().Equals("admin"))
                        htcResponseNoByDetails.InnerHtml = "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                    else
                        htcResponseNoByDetails.InnerHtml = "Response No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + "";
                }
                else
                    if (!strFirstName.ToLower().Equals("admin"))
                    htcResponseNoByDetails.InnerHtml = "Response No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                else
                    htcResponseNoByDetails.InnerHtml = "Response No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + " ";


                htcResponseNoByDetails.VAlign = "middle";
                htmlRowResponseNoByDetails.Cells.Add(htcUserImage);
                htmlRowResponseNoByDetails.Cells.Add(htcResponseNoByDetails);

                htmlRowResponseNoByDetails = AddThumbsUpDown(htmlRowResponseNoByDetails, i, strReplyId, lblUp, lblDown);


                htmlTblResponseNoByDetails.Rows.Add(htmlRowResponseNoByDetails);
                htcResponseNoByDetailsOuterCell.Controls.Add(htmlTblResponseNoByDetails);
                htcResponseNoByDetailsOuterCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#4fa4d5");

                htrResponseNoByDetailsOuterRow.Cells.Add(htcResponseNoByDetailsOuterCell);

                tblReplies.Width = "100%";
                tblReplies.Rows.Add(htrResponseNoByDetailsOuterRow);

                if (strReplyId == replyId)
                {
                    HtmlTableRow htmlRowDeleteButton = new HtmlTableRow();
                    HtmlTableCell htcDeleteButton = new HtmlTableCell();

                    Button btnDelete = new Button();
                    btnDelete.ForeColor = Color.Red;
                    btnDelete.Font.Bold = true;
                    btnDelete.Font.Size = 12;
                    btnDelete.ID = "btnDelete" + i;
                    btnDelete.Text = "Delete";
                    btnDelete.PostBackUrl = "Soln.aspx?ActionType=Delete&ReplyId=" + replyId + "&QId=" + quesID;
                    htcDeleteButton.Controls.Add(btnDelete);
                    htcDeleteButton.Align = "right";
                    htmlRowDeleteButton.Cells.Add(htcDeleteButton);
                    tblReplies.Rows.Add(htmlRowDeleteButton);
                }


                tblReplies.Rows.Add(htmlRowSolutionContent);

                htcBreak.InnerHtml = "<br />";
                htmlRowBreak.Cells.Add(htcBreak);
                tblReplies.Rows.Add(htmlRowBreak);

                tblReplies.Style.Add("word-wrap", "normal");
                tblReplies.Style.Add("word-break", "break-all");


            }
            pnlSolution.Visible = true;
        }
        connManager.DisposeConn();

    }


    private HtmlTableRow AddThumbsUpDown(HtmlTableRow htmlRowResponseNoByDetails, int i, string Replyid, string lblUp, string lblDown)
    {

        //HtmlTableCell htcViews = new HtmlTableCell();

        HtmlTableCell htcThumpsUp = new HtmlTableCell();
        HtmlTableCell htcThumpsDown = new HtmlTableCell();
        HtmlTableCell htcHFReplyId = new HtmlTableCell();



        // Image imgViews = new Image();
        // imgViews.ImageUrl = "eyesol.png";
        // imgViews.Height = 30;
        // imgViews.Width = 30;
        // imgViews.ID = "btnViews" + i;
        // //btnUp.Click += _DefaultUp_Click;
        // //btnUp.PostBackUrl = "Soln.aspx?Votetype=Up&ReplyId=" + Replyid + "&QId=" + quesID;
        // //btnUp.OnClientClick = "VoteUp(" + Replyid + ", " + quesID + ")";
        // htcViews.Controls.Add(imgViews);

        // //Label lblSpace1 = new Label();
        // //lblSpace1.Text = " ";
        // //htcThumpsUp.Controls.Add(lblSpace1);

        // Label lblViews = new Label();
        //// if (string.IsNullOrEmpty(lblViews))
        //  //   lblViews. = " ";
        // lblViews.Text = viewsCount;
        // lblViews.Font.Bold = true;
        // lblViews.Font.Size = 11;
        // lblViews.Font.Name = "Calibri";
        // lblViews.ID = "lblUp" + i;
        // //  htcThumpsUp.Align = "left";
        // htcViews.Controls.Add(lblViews);
        // // htcThumpsUp.Width = "80px";

        // Label lblSpace3= new Label();
        // lblSpace3.Text = "  ";
        // htcViews.Controls.Add(lblSpace3);




        ImageButton btnUp = new ImageButton();
        btnUp.ImageUrl = "ThumpsUp.png";
        btnUp.Height = 25;
        btnUp.Width = 25;
        btnUp.ID = "btnUp" + i;
        //btnUp.Click += _DefaultUp_Click;
        btnUp.PostBackUrl = "Soln.aspx?Votetype=Up&ReplyId=" + Replyid + "&QId=" + quesID;
        //btnUp.OnClientClick = "VoteUp(" + Replyid + ", " + quesID + ")";
        htcThumpsUp.Controls.Add(btnUp);

        //Label lblSpace1 = new Label();
        //lblSpace1.Text = " ";
        //htcThumpsUp.Controls.Add(lblSpace1);

        Label lblUpCount = new Label();
        if (string.IsNullOrEmpty(lblUp))
            lblUp = "0";
        lblUpCount.Text = lblUp;
        lblUpCount.Font.Bold = true;
        lblUpCount.Font.Size = 11;
        lblUpCount.Font.Name = "Calibri";
        lblUpCount.ID = "lblUp" + i;
        //  htcThumpsUp.Align = "left";
        htcThumpsUp.Controls.Add(lblUpCount);
        // htcThumpsUp.Width = "80px";

        Label lblSpace2 = new Label();
        lblSpace2.Text = "  ";
        htcThumpsUp.Controls.Add(lblSpace2);

        ImageButton btnDown = new ImageButton();
        btnDown.ImageUrl = "ThumpsDown.png";
        btnDown.Height = 25;
        btnDown.Width = 25;
        btnDown.ID = "btnDown" + i;
        //btnDown.Click += _DefaultDown_Click;
        btnDown.PostBackUrl = "Soln.aspx?Votetype=Down&ReplyId=" + Replyid + "&QId=" + quesID;
        //btnDown.OnClientClick = "VoteDown(" + Replyid + ", " + quesID + ")";
        htcThumpsDown.Controls.Add(btnDown);

        Label lblSpace4 = new Label();
        lblSpace4.Text = " ";
        htcThumpsDown.Controls.Add(lblSpace4);

        Label lblDownCount = new Label();
        if (string.IsNullOrEmpty(lblDown))
            lblDown = "0";
        lblDownCount.Text = lblDown;
        lblDownCount.Font.Bold = true;
        lblDownCount.Font.Size = 11;
        lblDownCount.Font.Name = "Calibri";
        lblDownCount.ID = "lblDown" + i;
        htcThumpsDown.Controls.Add(lblDownCount);

        //   htmlRowResponseNoByDetails.Cells.Add(htcViews);
        htmlRowResponseNoByDetails.Cells.Add(htcThumpsUp);
        htmlRowResponseNoByDetails.Cells.Add(htcThumpsDown);


        HiddenField hfReplyId = new HiddenField();
        hfReplyId.ID = "hfReplyId" + i;
        hfReplyId.Value = Replyid;
        htcHFReplyId.Controls.Add(hfReplyId);

        htmlRowResponseNoByDetails.Cells.Add(htcHFReplyId);

        return htmlRowResponseNoByDetails;
    }

    //private void _DefaultUp_Click(object sender, EventArgs e)
    //{
    //    ImageButton btnSender = (ImageButton)sender;
    //    string hfFieldName = "hfReplyId" + btnSender.ID.Replace("btnUp", "");
    //    HiddenField hfReplyId = (HiddenField)this.FindControl(hfFieldName);

    //    string lblUpCountName = "lblUp" + btnSender.ID.Replace("btnUp", "");
    //    Label lblUpCount = (Label)this.FindControl(lblUpCountName);

    //    ProcessVotes("Up", hfReplyId.Value, lblUpCount.Text);
    //}


    //private void _DefaultDown_Click(object sender, EventArgs e)
    //{
    //    ImageButton btnSender = (ImageButton)sender;
    //    string hfFieldName = "hfReplyId" + btnSender.ID.Replace("btnDown", "");
    //    HiddenField hfReplyId = (HiddenField)this.FindControl(hfFieldName);

    //    string lblDownCountName = "lblDown" + btnSender.ID.Replace("btnDown", "");
    //    Label lblDownCount = (Label)this.FindControl(lblDownCountName);

    //    ProcessVotes("Down", hfReplyId.Value, lblDownCount.Text);
    //}

    private void ProcessVotes(string LikeType, string ReplyId)
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

            DataSet dsVotes = connManager.GetData("Select ThumbsUp, ThumbsDown from Replies where ReplyId = " + ReplyId);

            if (dsVotes != null && dsVotes.Tables[0].Rows.Count > 0)
            {
                if (LikeType.Equals("Up"))
                {
                    if (string.IsNullOrEmpty(dsVotes.Tables[0].Rows[0]["ThumbsUp"].ToString()))
                        votes = votes + 1;
                    else
                        votes = int.Parse(dsVotes.Tables[0].Rows[0]["ThumbsUp"].ToString()) + 1;

                    strQuery = "Update Replies set ThumbsUp = " + votes + " where ReplyId = " + ReplyId;
                }
                else
                {
                    if (string.IsNullOrEmpty(dsVotes.Tables[0].Rows[0]["ThumbsDown"].ToString()))
                        votes = votes - 1;
                    else
                        votes = int.Parse(dsVotes.Tables[0].Rows[0]["ThumbsDown"].ToString()) + 1;

                    strQuery = "Update Replies set ThumbsDown = " + votes + " where ReplyId = " + ReplyId;
                }
            }

            SqlCommand command = new SqlCommand(strQuery, connManager.DataCon);



            command.ExecuteNonQuery();
            connManager.DisposeConn();

            lstReplies.Add(ReplyId);
            Session["Replies"] = lstReplies;
            // Session["votes"] = "true";
            // Response.Redirect("Soln.aspx?QId=" + quesID.ToString());
            // BindSolution("Select * from VwSolutions where QuestionId = " + quesID.ToString());

        }

        BindQuestionAskedUserData("Select * from VwQuestions where QuestionId = " + quesID.ToString() + "");
    }


    private double CreateUser(string strEmail)
    {
        double dblUserID = 0;
        user = new Users();
        SqlConnection LclConn = new SqlConnection();
        SqlTransaction SetTransaction = null;
        bool IsinTransaction = false;
        if (LclConn.State != ConnectionState.Open)
        {
            user.SetConnection = user.OpenConnection(LclConn);
            SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
            IsinTransaction = true;
        }
        else
        {
            user.SetConnection = LclConn;
        }
        user.Email = txtEMail.Text.Trim();
        user.OptionID = 1;
        user.CreatedDateTime = DateTime.Now;
        bool result = user.CreateUsers(ref dblUserID, SetTransaction);
        if (IsinTransaction && result)
        {
            SetTransaction.Commit();
        }
        else
        {
            SetTransaction.Rollback();
        }
        user.CloseConnection(LclConn);
        return dblUserID;
    }

    private bool UserExists(ref double userId)
    {
        ConnManager connManager = new ConnManager();
        connManager.OpenConnection();
        DataSet dsUserExists = connManager.GetData("Select * from Users where EMail = '" + txtEMail.Text + "'");
        connManager.DisposeConn();
        if (dsUserExists.Tables[0].Rows.Count > 0)
        {
            userId = double.Parse(dsUserExists.Tables[0].Rows[0]["Userid"].ToString());
            return true;
        }
        else
            return false;
    }

    private void LinkedInUser(double userId, Users user)
    {
        ConnManager connManager = new ConnManager();
        connManager.OpenConnection();
        DataSet LinkedInUserExists = connManager.GetData("Select * from Users where FirstName = '" + user.FirstName + "' and LastName = '" + user.LastName + "' and Details = '" + user.Details + "'");
        if (LinkedInUserExists.Tables[0].Rows.Count > 0)
        {
            DataSet updateDS = connManager.GetData("Update Users set EMail = '" + txtEMail.Text + "' where UserId = " + userId.ToString());
        }
        connManager.DisposeConn();
    }

    private void DeleteReply(string replyId)
    {
        ConnManager connManager = new ConnManager();
        connManager.OpenConnection();
        string strQuery = "Delete from Replies where ReplyId = " + replyId;
        SqlCommand command = new SqlCommand(strQuery, connManager.DataCon);
        command.ExecuteNonQuery();
        connManager.DisposeConn();
    }


}
