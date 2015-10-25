using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using CodeAnalyzeMVC2015;

public partial class MasterPageNoGoog : System.Web.UI.MasterPage
    {
        public string Email_address = "";
        public string Google_ID = "";
        public string firstName = "";
        public string LastName = "";
        public string Client_ID = "";
        public string Return_url = "";

        Users user = new Users();
        //double userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            //HtmlMeta metaDescription = new HtmlMeta();
            //metaDescription.Name = "description";
            //metaDescription.Content = "Code Analyze is coding forum for finding solution to various coding issues in any language, technology, software or hardware.";
            //    metaDescription.Content = "Get Amazon gift cards of your respective country for code blogging as appreciation. Try now.";
            //    Page.Header.Controls.Add(metaDescription);
            //    HtmlMeta metaKeywords = new HtmlMeta();
            //   metaKeywords.Name = "keywords";
            //    metaKeywords.Content = "Java, C#, PHP, Android, JQuery, XCode, XML, SQL, ASP.NET, HTML5 n many more";
            //    Page.Header.Controls.Add(metaKeywords);

            BindRecentPosts("Select top 3 * from VwArticles order by articleId desc");
            BindPopularPosts("Select top 3 * from VwArticles order by thumbsup desc");

        }


        protected void btnReferFriend_Click(object sender, EventArgs e)
        {
            string strFrom = " freinds";
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                if (!string.IsNullOrEmpty(user.FirstName))
                    strFrom = user.FirstName;
                else
                    strFrom = user.Email;
            }

            Mail mail = new Mail();


            string EMailBody = File.ReadAllText(Server.MapPath("EMailBody.txt"));

            string strCA = "<a id=HyperLink1 style=font-size: medium; font-weight: bold; color:White href=http://codeanalyze.com>CodeAnalyze</a>";

            mail.Body = string.Format(EMailBody, "You have been refered to " + strCA + " by one of your " + strFrom + ". Get Rewards Amazon Gift Cards for code blogging. Do take a look");


            mail.FromAdd = "admin@codeanalyze.com";
            mail.Subject = "Referred to CodeAnalyze -" + user.Email;
            mail.ToAdd = txtReferEMail.Text;
            mail.IsBodyHtml = true;
            mail.SendMail();

            txtReferEMail.Text = "Done";
        }


        protected override void OnPreRender(EventArgs e)
        {
            Panel pnlEMail = (Panel)ContentPlaceHolder1.FindControl("pnlEMail");
            Panel pnlMyQuesAns = (Panel)ContentPlaceHolder1.FindControl("pnlMyQuesAns");
            // Panel pnlUser = (Panel)ContentPlaceHolder1.FindControl("pnlUser");
            Panel pnlRegister = (Panel)ContentPlaceHolder1.FindControl("pnlRegister");
            Panel pnlProfile = (Panel)ContentPlaceHolder1.FindControl("pnlProfile");
            HiddenField hf = (HiddenField)ContentPlaceHolder1.FindControl("HFMode");
            Label lblSolution = (Label)ContentPlaceHolder1.FindControl("lblSolution");
            Label lblAck = (Label)ContentPlaceHolder1.FindControl("lblAck");
            HiddenField hfUserEMail = (HiddenField)ContentPlaceHolder1.FindControl("hfUserEMail");


            user = (Users)Session["User"];

            if (pnlRegister != null && pnlProfile != null)
            {
                if (Session["User"] == null)
                {
                    pnlRegister.Visible = true;
                    pnlProfile.Visible = false;
                }
                else
                {
                    if (hf != null && hf.Value == "Edit")
                    {
                        pnlRegister.Visible = true;
                        pnlProfile.Visible = false;
                    }
                    else
                    {
                        BindLabels();
                        pnlRegister.Visible = false;
                        pnlProfile.Visible = true;
                    }
                }
            }

            if (pnlMyQuesAns != null)   //&& pnlUser != null
            {
                if (Session["User"] == null)
                {
                    pnlMyQuesAns.Visible = false;
                    // pnlUser.Visible = true;
                }
                else
                {
                    pnlMyQuesAns.Visible = true;

                    //if (EMailExists(user))
                    //    pnlUser.Visible = false;
                    //else
                    //    pnlUser.Visible = true;

                }
            }

            if (pnlEMail != null)
            {
                if (Session["User"] == null)
                    pnlEMail.Visible = true;
                else
                {
                    if (EMailExists(user))
                        pnlEMail.Visible = false;
                    else
                        pnlEMail.Visible = true;
                }
            }

            if (Session["User"] == null)
            {

                pnlWelcome.Visible = false;
                lnkLogOut.Visible = false;
                btnUserProfile.Text = "Register";
                if (!Request.RawUrl.Contains("UserProfile"))
                {
                    //  pnlMarquee.Visible = false;
                    pnlLogin.Visible = false;  //true
                }
                if (lblAck != null)
                {
                    lblAck.Visible = true;
                    lblAck.Font.Bold = true;
                    //  lblAck.Text = "Please sign in to post";
                }
                lnkSignIn.Visible = true;
            }
            else
            {
                lnkSignIn.Visible = false;
                // pnlMarquee.Visible = true;
                pnlLogin.Visible = false;
                pnlWelcome.Visible = true;
                lnkLogOut.Visible = true;
                user = (Users)Session["User"];
                if (user.FirstName != null)
                    lblFirstName.Text = user.FirstName + " " + user.LastName + ", " + user.Details;
                else
                {
                    lblFirstName.Text = user.Email;

                }
                btnUserProfile.Text = "My Profile";

                if (hfUserEMail != null)
                    hfUserEMail.Value = user.Email;
                //if(lblAck != null)
                //lblAck.Visible = false;
            }

        }


        private void BindRecentPosts(string strQuery)
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
                    GVRecentPosts.DataSource = DSQuestions;
                    GVRecentPosts.DataBind();
                }
                else
                {
                    GVRecentPosts.DataSource = null;
                    GVRecentPosts.DataBind();
                }
            }

        }


        private void BindPopularPosts(string strQuery)
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
                    GVPopularPosts.DataSource = DSQuestions;
                    GVPopularPosts.DataBind();
                }
                else
                {
                    GVPopularPosts.DataSource = null;
                    GVPopularPosts.DataBind();
                }
            }

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Session["Facebook"] = null;
            Session.RemoveAll();
            Response.Redirect("Articles.aspx");
            // Response.Redirect(this.Request.RawUrl);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSUserList = new DataSet();
            DSUserList = connManager.GetData("select * from users where email = '" + txtEMailId.Text + "' and Password = '" + txtPassword.Text + "'");
            connManager.DisposeConn();
            if (DSUserList.Tables[0].Rows.Count == 0)
            {
                // ClientScriptManager cr = Page.ClientScript;
                // string scriptStr = "alert('Invalid Username and Password');";
                // cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
            }
            else
            {
                user = new Users();
                user.UserId = double.Parse(DSUserList.Tables[0].Rows[0]["UserId"].ToString());
                user.FirstName = DSUserList.Tables[0].Rows[0]["FirstName"].ToString();
                user.LastName = DSUserList.Tables[0].Rows[0]["LastName"].ToString();
                user.Email = DSUserList.Tables[0].Rows[0]["EMail"].ToString();
                user.Address = DSUserList.Tables[0].Rows[0]["Address"].ToString();
                user.ImageURL = DSUserList.Tables[0].Rows[0]["ImageURL"].ToString();
                Session["User"] = user;
                Session["user.Email"] = user.Email;
                lblFirstName.Text = user.FirstName + " " + user.LastName + ", " + user.Details;
                pnlLogin.Visible = false;
                pnlWelcome.Visible = true;
                lnkLogOut.Visible = true;
                // pnlMarquee.Visible = true;
                Response.Redirect(this.Request.RawUrl);
            }
        }

        //protected void SignInWithLinkedIn(object sender, EventArgs e)
        //{
        //    if (Session["User"] == null)
        //        Response.Redirect("LinkedIn.aspx");
        //    else
        //    {
        //        ClientScriptManager cr = Page.ClientScript;
        //        string scriptStr = "alert('You have already signed in');";
        //        cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
        //    }
        //}

        private void BindLabels()
        {
            Label lblFirstName = (Label)ContentPlaceHolder1.FindControl("lblFirstName");
            Label lblLastName = (Label)ContentPlaceHolder1.FindControl("lblLastName");
            Label lblEMail = (Label)ContentPlaceHolder1.FindControl("lblEMail");
            Label lblAddress = (Label)ContentPlaceHolder1.FindControl("lblAddress");
            Label lblQuestions = (Label)ContentPlaceHolder1.FindControl("lblQuestions");
            Label lblAnswers = (Label)ContentPlaceHolder1.FindControl("lblAnswers");
            Label lblArticles = (Label)ContentPlaceHolder1.FindControl("lblArticles");
            Image imgProfile = (Image)ContentPlaceHolder1.FindControl("imgProfile");
            try
            {
                DataSet dsUser = new DataSet();
                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();
                user = (Users)(Session["User"]);
                dsUser = connManager.GetData("select * from Users where UserID = " + user.UserId);

                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    lblFirstName.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["FirstName"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["FirstName"].ToString()));
                    lblLastName.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["LastName"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["LastName"].ToString()));
                    lblEMail.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["EMail"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["EMail"].ToString()));
                    lblAddress.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["Address"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["Address"].ToString()));

                    DataSet dsQuestions = new DataSet();
                    dsQuestions = connManager.GetData("Select Count(*) from Question where AskedUser = " + user.UserId + "");
                    if (dsQuestions.Tables.Count > 0 && dsQuestions.Tables[0].Rows.Count > 0)
                        lblQuestions.Text = dsQuestions.Tables[0].Rows[0][0].ToString();
                    else
                        lblQuestions.Text = "";
                    DataSet dsAnswers = new DataSet();
                    dsAnswers = connManager.GetData("Select Count(*) from Replies where RepliedUser = " + user.UserId + "");
                    if (dsAnswers.Tables.Count > 0 && dsAnswers.Tables[0].Rows.Count > 0)
                        lblAnswers.Text = dsAnswers.Tables[0].Rows[0][0].ToString();
                    else
                        lblAnswers.Text = "";


                    dsAnswers = connManager.GetData("Select Count(*) from Articles where UserId = " + user.UserId + "");
                    if (dsAnswers.Tables.Count > 0 && dsAnswers.Tables[0].Rows.Count > 0)
                        lblArticles.Text = dsAnswers.Tables[0].Rows[0][0].ToString();
                    else
                        lblArticles.Text = "";

                    imgProfile.ImageUrl = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["ImageURL"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["ImageURL"].ToString()));
                }
                connManager.DisposeConn();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkForgotPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }

        private bool EMailExists(Users user)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet dsUserExists = connManager.GetData("Select * from Users where Password is null and EMail is null and UserId = " + user.UserId.ToString());
            connManager.DisposeConn();
            if (dsUserExists.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }

        protected void JoinChat(object sender, EventArgs e)
        {
            Response.Write("window.open(url,'name','height=470,width=760')");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChatRoom.aspx");
        }

        protected void header_Click(object sender, EventArgs e)
        {
            // btnTopics.Font.Size = 28;
            // Response.Redirect("Topics.aspx");
        }

        protected void ButtonClick(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            if (button.ID.Equals("lnkHome"))
                Response.Redirect("Articles.aspx");
            else if (button.ID.Equals("lnkSuggestion"))
                Response.Redirect("Suggestions.aspx");
            else if (button.ID.Equals("btnUserProfile"))
                Response.Redirect("UserProfile.aspx");
            else if (button.ID.Equals("lnkSignIn"))
                Response.Redirect("Login.aspx");

            else if (button.ID.Equals("lnkViewArticles"))
                Response.Redirect("Articles.aspx");
            else if (button.ID.Equals("lnkPostArticle"))
                Response.Redirect("PostArticles.aspx");
            else if (button.ID.Equals("btnAskQuestion"))
                Response.Redirect("AskQuestions.aspx");


            else if (button.ID.Equals("btnUserProfile1"))
                Response.Redirect("UserProfile.aspx");

            else if (button.ID.Equals("btnUnanswered"))
                Response.Redirect("UnAnswered.aspx");

            else if (button.ID.Equals("btnTopics"))
                Response.Redirect("Topics.aspx");

            else if (button.ID.Equals("btnCodeChat"))
                Response.Redirect("ChatRoom.aspx");

            else if (button.ID.Equals("btnInfo"))
                Response.Redirect("Info.aspx");

            else if (button.ID.Equals("btnTopics"))
                Response.Redirect("Topics.aspx");

            else if (button.ID.Equals("btnAngularJS"))
                Response.Redirect("AngularJS.aspx");


            else if (button.ID.Equals("btnHadoop"))
                Response.Redirect("Hadoop.aspx");
        }


        //protected void GVRecentPosts_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
        //        Label LblRecentPostsId = (Label)(row.FindControl("LblRecentPostsId"));
        //        //Response.Redirect("Soln.aspx?QId=" + Encryption.EncryptQueryString(LblQuestionId.Text) + "");
        //        LinkButton LblQT = (LinkButton)(row.FindControl("lnkArticleTitle"));
        //        //Response.Redirect("VA.aspx?QId=" + LblRecentPostsId.Text + "&QT=" + LblQT.Text);

        //        Response.Redirect(String.Format("va.aspx/{0}/{1}", LblRecentPostsId.Text, LblQT.Text));
        //        //Response.Write("<script>");
        //        //Response.Write("window.open('VA.aspx?QId=" + LblRecentPostsId.Text + "&QT=" + LblQT.Text + "','_blank')");
        //        //Response.Write("</script>");


        //}


    }
