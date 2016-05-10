using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CodeAnalyzeMVC2015;

public partial class ArticleDetails : System.Web.UI.UserControl
    {
        Users user = new Users();
        long articleID;
        string articleTitle;


        protected void Page_Load(object sender, EventArgs e)
        {

            //user = (Users)Session["User"];
            //if (!long.TryParse(Page.RouteData.Values["id1"].ToString(), out articleID))
            //    long.TryParse(Encryption.DecryptQueryString(Page.RouteData.Values["id1"].ToString()), out articleID);

            //if (Page.RouteData.Values["id2"].ToString() != null)
            //    articleTitle = Page.RouteData.Values["id2"].ToString();

            user = (Users)Session["User"];
            if (!long.TryParse(Request.QueryString["QId"], out articleID))
                long.TryParse(Encryption.DecryptQueryString(Request.QueryString["QId"]), out articleID);

            if (Request.QueryString["QT"] != null)
                articleTitle = Request.QueryString["QT"].ToString();


            if (!string.IsNullOrEmpty(articleTitle))
                Page.Title = articleTitle.Replace("-", " ");
            HtmlMeta metaDescription = new HtmlMeta();
            metaDescription.Name = "description";
            if (!string.IsNullOrEmpty(articleTitle))
                metaDescription.Content = articleTitle.Replace("-", " ");
            Page.Header.Controls.Add(metaDescription);
            HtmlMeta metaKeywords = new HtmlMeta();
            metaKeywords.Name = "keywords";
            if (!string.IsNullOrEmpty(articleTitle))
                metaKeywords.Content = articleTitle.Replace("-", " ");
            Page.Header.Controls.Add(metaKeywords);


            // if (!IsPostBack)
            // {
            if (articleID != null && articleID != 0)
            {
                //BindQuestionAskedUserData("Select * from VwArticles where ArticleId = " + articleID.ToString() + "");
                GetArticleData(articleID.ToString());
                //Page.Title = "Code Analyze - " + questionTitle;
                //Page.Title = articleTitle;
                //HtmlMeta metaDescription = new HtmlMeta();
                //metaDescription.Name = "description";
                //metaDescription.Content = articleTitle;
                //Page.Header.Controls.Add(metaDescription);
                //HtmlMeta metaKeywords = new HtmlMeta();
                //metaKeywords.Name = "keywords";
                //metaKeywords.Content = articleTitle;
                //Page.Header.Controls.Add(metaKeywords);
                //if (user != null)
                //    pnlMyQuesAns.Visible = true;
                //else
                //    pnlMyQuesAns.Visible = false;
            }
            else
            {
                // pnlSolutionText.Visible = true;
            }
            // }

            if (user != null)
            {
                lblAck.Visible = false;
                hfUserEMail.Value = user.Email;
            }
            else
            {
                lblAck.Visible = true;
                lblAck.Font.Bold = true;
                lblAck.Text = "Please sign in to comment.";
                hfUserEMail.Value = "";
            }


        }

        protected void PreviewButton_Click(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                if (!string.IsNullOrEmpty(txtReply.Text))
                {
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
                    articleComments.ReplyText = txtReply.Text;

                    articleComments.InsertedDate = DateTime.Now;

                    bool result = articleComments.CreateArticleComments(ref dblReplyID, SetTransaction);


                    if (IsinTransaction && result)
                    {
                        SetTransaction.Commit();

                        if (!Session["AskedUserEMail"].ToString().Contains("codeanalyze.com"))
                        {
                            Mail mail = new Mail();
                            string strLink = "www.codeanalyze.com/VA.aspx?QId=" + articleID.ToString() + "&QT=" + articleTitle + "";
                            mail.Body = "<a href=" + strLink + "\\>Click here to view solution to: " + articleTitle + "</a>";
                            mail.FromAdd = "admin@codeanalyze.com";
                            mail.Subject = "Code Analyze - Received response for " + articleTitle;
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
                    BindComments("Select * from VwArticleReplies where ArticleId = " + articleID.ToString());
                    Session["votes"] = "false";
                    lblAck.Visible = false;
                }
                else
                {
                    lblAck.Visible = true;
                    lblAck.Font.Bold = true;
                    lblAck.Text = "Please enter comments.";
                }
            }
            else
            {
                lblAck.Visible = true;
                lblAck.Font.Bold = true;
                lblAck.Text = "Please sign in to post your comment.";
            }
        }

        protected void lnkBtnSourceCode_Click(object sender, EventArgs e)
        {
            Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + hfSourceFile.Value);
            //Response.AppendHeader("Content-Cength", file.Length.ToString());
            Response.ContentType = "application/x-zip-compressed";
            Response.WriteFile(Server.MapPath("~/Articles/" + hfSourceFile.Value));
            //Response.Flush();
            Response.End();
        }


        private void GetArticleData(string strArticleId)
        {

            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable dsQuestion = connManager.GetArticle(strArticleId);

            connManager.DisposeConn();
            if (dsQuestion != null)
            {
                if (dsQuestion.Rows.Count > 0)
                {
                    articleID = long.Parse(dsQuestion.Rows[0]["ArticleId"].ToString());

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
                            //lblAskedUser.Text = "Posted By: <b>" + dsQuestion.Tables[0].Rows[0]["EMail"].ToString().Split('@')[0] + "<b>";
                        }


                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["ImageURL"].ToString()))
                        {
                            imgAskedUser.ImageUrl = dsQuestion.Rows[0]["ImageURL"].ToString();
                            imgAskedUser.Height = 130;
                            imgAskedUser.Width = 150;
                            imgAskedUser.Visible = true;
                        }
                        else
                        {
                            imgAskedUser.Visible = false;
                            imgAskedUser.ImageUrl = "~/Images/Person.JPG";
                            imgAskedUser.Height = 25;
                            imgAskedUser.Width = 25;
                        }

                        if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["ImageURL"].ToString()))
                        {
                            lblAskedUserDetails.Text = dsQuestion.Rows[0]["Details"].ToString();
                            lblAskedUserDetails.Visible = true;
                        }
                        else
                        {
                            lblAskedUserDetails.Visible = false;
                        }
                    }
                    else
                    {
                        imgAskedUser.Visible = false;
                        lblAskedUserDetails.Visible = false;
                    }

                    Session["AskedUserEMail"] = dsQuestion.Rows[0]["EMail"].ToString();


                    lblArticleTitle.Text = "<b>" + dsQuestion.Rows[0]["ArticleTitle"].ToString() + "<b>";



                    //if(string.IsNullOrEmpty(dsQuestion.Tables[0].Rows[0]["Views"].ToString()))
                    //{
                    //    lblViews.Text = "     ";
                    //}
                    //else
                    //{
                    lblViews.Text = dsQuestion.Rows[0]["Views"].ToString();

                    //}//articleDetail.Attributes["src"] = Server.MapPath("~/Articles/HTMLPage.htm");

                    //articleDetail.Attributes["src"] = "../../Articles/" + dsQuestion.Rows[0]["WordFile"].ToString();  //HTMLPage.htm";
                    // articleDetail.Attributes["src"] = "../../Articles/" + dsQuestion.Tables[0].Rows[0]["WordFile"].ToString();  //HTMLPage.htm";

                    //DataTable dt1 = new DataTable();
                    //dt1.Columns.Add("html");
                    //DataRow dr = dt1.NewRow();
                    //dr["html"] = "../../Articles/" + dsQuestion.Rows[0]["WordFile"].ToString();

                    //dr["html"] = "Articles/" + dsQuestion.Rows[0]["WordFile"].ToString();

                    using (FileStream fs = new FileStream(Server.MapPath("Articles/" + dsQuestion.Rows[0]["WordFile"].ToString()), FileMode.Open, FileAccess.Read))
                    {
                        using (TextReader tr = new StreamReader(fs))
                        {
                            string content = tr.ReadToEnd();
                            // dr["html"] = content;
                            //litHtml.Text = content;
                            divContent.InnerHtml = content;
                        }
                    }

                    //dt1.Rows.Add(dr);
                    //dtlhtml.DataSource = dt1;
                    //dtlhtml.DataBind();

                    if (dsQuestion.Rows[0]["SourceFile"] != DBNull.Value && !string.IsNullOrEmpty(dsQuestion.Rows[0]["SourceFile"].ToString()))
                    {
                        hfSourceFile.Value = dsQuestion.Rows[0]["SourceFile"].ToString();
                        lnkBtnSourceCode.Enabled = true;
                        lnkBtnSourceCode.ForeColor = Color.Blue;
                        lnkBtnSourceCode.Text = "Download Source Code";
                    }
                    else
                    {
                        lnkBtnSourceCode.Enabled = false;
                        lnkBtnSourceCode.ForeColor = Color.Gray;
                        lnkBtnSourceCode.Text = "No Source Code Uploaded";
                    }

                    //divArticleDetails.InnerHtml = Server.MapPath("~/Articles/HTMLPage.htm");

                    if (!string.IsNullOrEmpty(dsQuestion.Rows[0]["YouTubURL"].ToString()))
                    {
                        iframeVideo.Attributes["src"] = "//www.youtube.com/embed?listType=playlist&list=PLr5xM_46LGUtyEyyGilUu3YH5FTNo7SMH";
                        pnlVideo.Visible = true;
                    }
                    else
                    {
                        pnlVideo.Visible = false;
                    }
                    lblThumbsUp.Text = dsQuestion.Rows[0]["ThumbsUp"].ToString();
                    lblThumbsDown.Text = dsQuestion.Rows[0]["ThumbsDown"].ToString();


                    BindComments("Select * from VwArticleReplies where ArticleId =" + articleID.ToString());
                }
            }



        }

        private void BindComments(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet dsSolution = connManager.GetData(strQuery);
            // tblReplies.Width = "650px";
            string strReplyId = "";

            if (dsSolution != null)
            {
                for (int i = 0; i < dsSolution.Tables[0].Rows.Count; i++)
                {
                    HtmlTableRow htmlRowSolutionContent = new HtmlTableRow();
                    HtmlTableCell htcReplyContent = new HtmlTableCell();

                    strReplyId = dsSolution.Tables[0].Rows[i]["ReplyId"].ToString();

                    htcReplyContent.InnerHtml = dsSolution.Tables[0].Rows[i]["ReplyText"].ToString().Replace("font-size: x-small", "font-size: 16px");
                    htcReplyContent.InnerHtml = htcReplyContent.InnerHtml.Replace("<pre>", "<pre class=prestyle>");
                    htcReplyContent.Style.Add("font-family", "Calibri");
                    htcReplyContent.ColSpan = 2;
                    htmlRowSolutionContent.Cells.Add(htcReplyContent);

                    HtmlTableRow htrResponseNoByDetailsOuterRow = new HtmlTableRow();
                    HtmlTableCell htcResponseNoByDetailsOuterCell = new HtmlTableCell();

                    HtmlTable htmlTblResponseNoByDetails = new HtmlTable();
                    //    htmlTblResponseNoByDetails.Width = "650px";
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
                    strRepliedDate = dsSolution.Tables[0].Rows[i]["InsertedDate"].ToString().Split('@')[0];


                    DataSet dsCount = new DataSet();
                    dsCount = connManager.GetData("SELECT COUNT(*) FROM VwArticleReplies WHERE (ArticleId = " + strUserId + ") ");

                    if (dsCount != null && dsCount.Tables.Count > 0 && dsCount.Tables[0].Rows.Count != 0)
                    {
                        if (dsCount.Tables[0].Rows[0][0].ToString() != "0")
                        {
                            if (dsCount != null && dsCount.Tables.Count > 0)
                                strAnswers = dsCount.Tables[0].Rows[0][0].ToString();
                            else
                                strAnswers = "<b>none</b>";


                            if (!dsSolution.Tables[0].Rows[i]["EMail"].ToString().Contains("codeanalyze.com"))
                                htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";   // Total replies by user: " + strAnswers + ".";
                            else
                            {
                                if (!strFirstName.ToLower().Equals("admin"))
                                    htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                                else
                                    htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + "";

                            }
                            //htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                            //htc4.InnerHtml = "Comment No <b>" + (i + 1).ToString();
                        }
                        else
                            if (!strFirstName.ToLower().Equals("admin"))
                            htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                        else
                            htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + "";
                    }
                    else
                        if (!strFirstName.ToLower().Equals("admin"))
                        htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> by <b>" + strFirstName + "</b>  " + strRepliedDate + "";
                    else
                        htcResponseNoByDetails.InnerHtml = "Comment No <b>" + (i + 1).ToString() + "</b> " + strRepliedDate + " ";


                    htcResponseNoByDetails.VAlign = "middle";
                    htmlRowResponseNoByDetails.Cells.Add(htcUserImage);
                    htmlRowResponseNoByDetails.Cells.Add(htcResponseNoByDetails);



                    htmlTblResponseNoByDetails.Rows.Add(htmlRowResponseNoByDetails);
                    htcResponseNoByDetailsOuterCell.Controls.Add(htmlTblResponseNoByDetails);
                    htcResponseNoByDetailsOuterCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F5E8AA");

                    htrResponseNoByDetailsOuterRow.Cells.Add(htcResponseNoByDetailsOuterCell);

                    //     tblReplies.Width = "650px";
                    tblReplies.Rows.Add(htrResponseNoByDetailsOuterRow);
                    tblReplies.Rows.Add(htmlRowSolutionContent);

                    htcBreak.InnerHtml = "<br />";
                    htmlRowBreak.Cells.Add(htcBreak);
                    tblReplies.Rows.Add(htmlRowBreak);

                }
                // pnlSolution.Visible = true;
            }
            connManager.DisposeConn();

        }


        private void ProcessVotes(string LikeType, string ReplyId)
        {
            List<string> lstReplies = (List<string>)Session["Articles"];
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

                DataSet dsVotes = connManager.GetData("Select ThumbsUp, ThumbsDown from CodeArticles where ArticleId = " + articleID);

                if (dsVotes != null && dsVotes.Tables[0].Rows.Count > 0)
                {
                    if (LikeType.Equals("Up"))
                    {
                        if (string.IsNullOrEmpty(dsVotes.Tables[0].Rows[0]["ThumbsUp"].ToString()))
                            votes = votes + 1;
                        else
                            votes = int.Parse(dsVotes.Tables[0].Rows[0]["ThumbsUp"].ToString()) + 1;

                        strQuery = "Update CodeArticles set ThumbsUp = " + votes + " where ArticleId = " + articleID;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dsVotes.Tables[0].Rows[0]["ThumbsDown"].ToString()))
                            votes = votes - 1;
                        else
                            votes = int.Parse(dsVotes.Tables[0].Rows[0]["ThumbsDown"].ToString()) + 1;

                        strQuery = "Update CodeArticles set ThumbsDown = " + votes + " where ArticleId = " + articleID;
                    }
                }

                SqlCommand command = new SqlCommand(strQuery, connManager.DataCon);



                command.ExecuteNonQuery();
                connManager.DisposeConn();

                lstReplies.Add(ReplyId);
                Session["Articles"] = lstReplies;

            }

            GetArticleData(articleID.ToString());
        }



        protected void btnThumbsDown_Click(object sender, ImageClickEventArgs e)
        {
            ProcessVotes("Down", Request.QueryString["ReplyId"]);
        }
        protected void btnThumbsUp_Click(object sender, ImageClickEventArgs e)
        {
            ProcessVotes("Up", Request.QueryString["ReplyId"]);
        }
    }
