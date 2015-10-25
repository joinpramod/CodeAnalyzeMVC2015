using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using Microsoft.Security.Application;
using CodeAnalyzeMVC2015;

public partial class AskQuestions : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Editor1.Bo


            user = (Users)Session["User"];

            if (!IsPostBack)
            {

                if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
                {
                    GVQuestions.Visible = true;
                    GVQuestions.Columns[2].Visible = true;
                    BindQuestions("Select top 100 * from Question order by AskedDateTime desc");
                    hfUserEMail.Value = user.Email;
                }
                else
                {
                    GVQuestions.Visible = false;
                    GVQuestions.Columns[2].Visible = false;
                    hfUserEMail.Value = "";
                }

                BindQuestionType("Select * from QuestionType");

                LinkButton btnAskQuestion = (LinkButton)this.Master.FindControl("btnAskQuestion");
                //btnAskQuestion.Font.Size = 24;
                btnAskQuestion.ForeColor = Color.Yellow;

                //HtmlMeta metaDescription = new HtmlMeta();
                //metaDescription.Name = "description";
                ////metaDescription.Content = "Code Analyze is coding forum for finding solution to various coding issues in any language, technology, software or hardware.";
                //metaDescription.Content = "Code Analyze is a simple coding forum to get code of frequently used logics and functionalities.  Also users will get rewarded for posting.";
                //Page.Header.Controls.Add(metaDescription);
                //HtmlMeta metaKeywords = new HtmlMeta();
                //metaKeywords.Name = "keywords";
                //metaKeywords.Content = "c#, java, php, asp.net, swings, jquery, ajax, sharepoint, javascript, sql, css, n many more";
                //Page.Header.Controls.Add(metaKeywords);
            }



            //if (Session["User"] == null)
            //{
            //    lblAck.Visible = true;
            //    lblAck.Font.Bold = true;
            //    lblAck.Text = "Please sign in to post your question.";
            //}
            //else
            //{
            //    lblAck.Visible = false;
            //}
        }

        private void BindQuestions(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSQuestions = new DataSet();
            DSQuestions = connManager.GetData(strQuery);
            connManager.DisposeConn();
            if (DSQuestions != null)
            {
                if (DSQuestions.Tables[0].Rows.Count > 0)
                {
                    GVQuestions.DataSource = DSQuestions;
                    GVQuestions.DataBind();
                }
            }
            else
            {
                GVQuestions.DataSource = null;
                GVQuestions.DataBind();
                ClientScriptManager cr = Page.ClientScript;
                string scriptStr = "alert('No records exists.');";
                cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
            }
        }



        private void BindQuestionType(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSQuestions = new DataSet();
            DSQuestions = connManager.GetData(strQuery);
            connManager.DisposeConn();
            if (DSQuestions != null)
            {
                if (DSQuestions.Tables[0].Rows.Count > 0)
                {
                    ddType.DataSource = DSQuestions;
                    ddType.DataBind();
                }
            }

        }


        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            //string strDateCheck = "";
            //if (!string.IsNullOrEmpty(txtAskedDate.Text))
            //{
            //    DateTime dateTime = DateTime.Parse(txtAskedDate.Text);
            //    dateTime = dateTime.AddDays(1);

            //if (ddDateType.SelectedValue == "On" && !string.IsNullOrEmpty(txtAskedDate.Text))
            //    strDateCheck = " ((AskedDateTime > '" + txtAskedDate.Text + " 00:00:00') and (AskedDateTime < '" + dateTime.ToShortDateString() + " 00:00:00'))";
            //else
            //    strDateCheck = " (AskedDateTime > '" + txtAskedDate.Text + " 00:00:00')";
            //}

            //if (!string.IsNullOrEmpty(strDateCheck) && !string.IsNullOrEmpty(txtQuestionTitle.Text))
            //BindQuestions("Select * from Question where QuestionTitle like '%" + txtQuestionTitle.Text + "%' and " + strDateCheck + "");
            //else if (string.IsNullOrEmpty(strDateCheck) && !string.IsNullOrEmpty(txtQuestionTitle.Text))
            //    BindQuestions("Select * from Question where QuestionTitle like '%" + txtQuestionTitle.Text + "%'");
            //else if (!string.IsNullOrEmpty(strDateCheck) && string.IsNullOrEmpty(txtQuestionTitle.Text))
            //    BindQuestions("Select * from Question where " + strDateCheck + "");


            BindQuestions("Select * from Question where QuestionTitle like '%" + txtQuestionTitle.Text + "%'");
            GVQuestions.Visible = true;
            //GVQuestions.AllowPaging = false;
            //GVQuestions.PageSize = 50;

        }

        protected void PreviewButton_Click(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                //if (!EditorAskQuestion.Content.Equals("&lt;br&gt;"))
                //{

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
                //string strTitle = txtTitle.Text.Replace("<", " < ");
                //strTitle = strTitle.Replace(">", " > ");
                //strTitle = strTitle.Replace(":", " : ");
                //strTitle = strTitle.Replace("<", " < ");
                //question.QuestionTitle = strTitle;

                question.QuestionTitle = txtTitle.Text;

                question.QuestionTypeId = int.Parse(ddType.SelectedValue);
                question.OptionID = 1;

                //EditorAskQuestion.Content = EditorAskQuestion.Content.Replace("&amp;", "&")
                //    .Replace("&lt;", "<")
                //    .Replace("&gt;", ">")
                //    .Replace("&apos;", "'")
                //    .Replace("&quot;", "\"");

                //question.QuestionDetails = customEditor2.Content;
                //question.QuestionDetails = Utilities.ExpandUrls(EditorAskQuestion.Content);

                if (EditorAskQuestion.Content.Length > 10000)
                {
                    EditorAskQuestion.Content = EditorAskQuestion.Content.Substring(0, 10000);
                }

                question.QuestionDetails = Sanitizer.GetSafeHtml(EditorAskQuestion.Content);
                // question.QuestionDetails = EditorAskQuestion.Content;


                question.AskedDateTime = DateTime.Now;

                //if (Session["User"] != null)
                // {
                //if (string.IsNullOrEmpty(user.Email))
                //{
                //    LinkedInUser(user.UserId, user);
                //    user.Email = txtEMail.Text;
                //}
                question.AskedUser = user.UserId;
                //}
                //else
                //{
                //    double dblUser = 0;
                //    //if (!UserExists(ref dblUser))
                //    //    question.AskedUser = CreateUser(txtEMail.Text);
                //    //else
                //        question.AskedUser = dblUser;
                //}

                bool result = question.CreateQuestion(ref dblQuestionID, SetTransaction);

                if (IsinTransaction && result)
                {
                    SetTransaction.Commit();
                    Mail mail = new Mail();
                    mail.Body = "<a>www.codeanalyze.com/Soln.aspx?QId=" + dblQuestionID.ToString() + "&QT=" + txtTitle.Text + "</a>";
                    mail.FromAdd = "admin@codeanalyze.com";
                    mail.Subject = txtTitle.Text;
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
                lblAck.Visible = true;
                lblAck.Text = "Question posted successfully, you will emailed when users post answers.";
                lnkSolution.Visible = true;
                lnkSolution.Text = "View your posted question";
                //lnkSolution.PostBackUrl = "~/Soln.aspx?QId=" + dblQuestionID.ToString() + "&QT=" + txtTitle.Text;

                //lnkSolution.PostBackUrl = string.Format("Soln.aspx/{0}/{1}", dblQuestionID.ToString(), txtTitle.Text.ToString().Replace(" ", "-"));
                lnkSolution.PostBackUrl = string.Format("Soln.aspx?QId={0}&QT={1}", dblQuestionID.ToString(), txtTitle.Text.ToString().Replace(" ", "-"));
                //}
                //lblAck.Text = "Question posted successfully, our experts will answer your question shortly, you will be informed by a email.";
                lblAck.Visible = true;
            }
            else
            {
                lblAck.Visible = true;
                //   lblAck.Font.Bold = true;
                //   lblAck.Text = "Please sign in to post your question.";
            }
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
            user.Email = strEmail.Trim();
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
            //DataSet dsUserExists = connManager.GetData("Select * from Users where EMail = '" + txtEMail.Text + "'");
            DataSet dsUserExists = new DataSet();
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
            connManager.DisposeConn();
            if (LinkedInUserExists.Tables[0].Rows.Count > 0)
            {
                DataSet updateDS = new DataSet();  // connManager.GetData("Update Users set EMail = '" + txtEMail.Text + "' where UserId = " + userId.ToString());
            }
            connManager.DisposeConn();
        }

        protected void GVQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Question")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label LblQuestionId = (Label)(row.FindControl("LblQuestionId"));
                //Response.Redirect("Soln.aspx?QId=" + Encryption.EncryptQueryString(LblQuestionId.Text) + "");
                LinkButton LblQT = (LinkButton)(row.FindControl("LblQuestion"));

                Response.Write("<script>");
                Response.Write("window.open('Soln.aspx?QId=" + LblQuestionId.Text + "&QT=" + LblQT.Text + "','_blank')");
                Response.Write("</script>");


            }
            if (e.CommandName == "Deletes")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label LblQuestionId = (Label)(row.FindControl("LblQuestionId"));
                SqlConnection LclConn = new SqlConnection();
                SqlTransaction SetTransaction = null;
                bool IsinTransaction = false;
                Question ques = new Question();
                if (LclConn.State != ConnectionState.Open)
                {
                    ques.SetConnection = ques.OpenConnection(LclConn);
                    SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                    IsinTransaction = true;
                }
                else
                {
                    ques.SetConnection = LclConn;
                }

                ques.OptionID = 3;
                ques.QuestionId = double.Parse(LblQuestionId.Text);
                double dblQuesId = double.Parse(LblQuestionId.Text);
                bool res = ques.CreateQuestion(ref dblQuesId, SetTransaction);
                if (IsinTransaction && res)
                {
                    SetTransaction.Commit();
                }
                else
                {
                    SetTransaction.Rollback();
                }
                ques.CloseConnection(LclConn);
            }

        }

        protected void GVQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVQuestions.PageIndex = e.NewPageIndex;
            BindQuestions("Select top(100) * from Question order by AskedDateTime desc");
        }
    }
