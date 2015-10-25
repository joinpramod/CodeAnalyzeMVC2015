using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Drawing;
using CodeAnalyzeMVC2015;

public partial class UserProfile : System.Web.UI.Page
    {

        public string Email_address = "";
        public string Google_ID = "";
        public string firstName = "";
        public string LastName = "";
        public string Client_ID = "";
        public string Return_url = "";
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {

            // Panel _pnlMarquee = (Panel)this.Master.FindControl("pnlMarquee"); 
            // _pnlMarquee.Visible = true;

            if (Request.QueryString["access_token"] != null)
            {
                double _userId = 0;

                WebClient webClient = new WebClient();
                String URI = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + Request.QueryString["access_token"].ToString();

                Stream stream = webClient.OpenRead(URI);
                string b;

                using (StreamReader br = new StreamReader(stream))
                {
                    b = br.ReadToEnd();
                }

                b = b.Replace("id", "").Replace("email", "");
                b = b.Replace("given_name", "");
                b = b.Replace("family_name", "").Replace("link", "").Replace("picture", "");
                b = b.Replace("gender", "").Replace("locale", "").Replace(":", "");
                b = b.Replace("\"", "").Replace("name", "").Replace("{", "").Replace("}", "");

                Array ar = b.Split(",".ToCharArray());
                for (int p = 0; p < ar.Length; p++)
                {
                    ar.SetValue(ar.GetValue(p).ToString().Trim(), p);
                }

                Email_address = ar.GetValue(1).ToString();
                //Google_ID = ar.GetValue(0).ToString();
                firstName = ar.GetValue(4).ToString();
                LastName = ar.GetValue(5).ToString();

                if (!user.UserExists(Email_address, ref _userId))
                {
                    user = user.CreateUser(Email_address, firstName, LastName);
                    SendNewUserRegEMail();
                    SendEMail(Email_address, firstName, LastName);
                }
                else
                {
                    user.Email = Email_address;
                    user.UserId = _userId;
                }
                Session["User"] = user;
                Session["user.Email"] = user.Email;
            }


            //userprofile
            else if (Request.QueryString["facebook"] == "true")
            {
                double _userId = 0;
                string _strFBName = Request.QueryString["fbname"];
                string _strFBEMail = Request.QueryString["fbemail"];

                if (!user.UserExists(_strFBEMail, ref _userId))
                {
                    user = user.CreateUser(_strFBEMail, _strFBName, "");
                    SendNewUserRegEMail();
                    SendEMail(Email_address, firstName, LastName);
                }
                else
                {
                    user.Email = _strFBEMail;
                    user.UserId = _userId;
                }

                Session["User"] = user;
                Session["user.Email"] = user.Email;
            }

            if (!IsPostBack)
            {

                if (Request.QueryString["facebook"] == "true" || Request.QueryString["google"] == "true")
                {
                    BindLabels();
                    pnlMyQuesAns.Visible = true;

                    Panel _pnlLogin = (Panel)this.Master.FindControl("pnlLogin");
                    _pnlLogin.Visible = false;

                }
                else
                {
                    pnlMyQuesAns.Visible = false;
                    string strSource = Request.QueryString["Source"];
                    if (Session["User"] == null)
                    {
                        HFMode.Value = "New";
                        //pnlPassword.Visible = true;
                        //pnlChangePassword.Visible = false;
                        btnCancelChangePassword.Visible = false;

                        Panel _pnlLogin = (Panel)this.Master.FindControl("pnlLogin");
                        _pnlLogin.Visible = false; // true;
                        pnlSocial.Visible = true;
                        ConfigureCapcha();
                    }
                    else
                    {
                        //pnlPassword.Visible = false;
                        if (strSource != "ForgotPassword")
                        {
                            BindLabels();
                            pnlMyQuesAns.Visible = true;
                        }
                        else
                        {
                            BindTextBoxes();
                            pnlMyQuesAns.Visible = false;
                        }

                        Panel _pnlLogin = (Panel)this.Master.FindControl("pnlLogin");
                        _pnlLogin.Visible = false;

                    }
                }

                LinkButton btnUserProfile = (LinkButton)this.Master.FindControl("btnUserProfile");
                // btnUserProfile.Font.Size = 24;
                btnUserProfile.ForeColor = Color.Yellow;

            }
        }

        private void ConfigureCapcha()
        {

            Response.Cookies["Captcha"]["value"] = (Guid.NewGuid().ToString()).Substring(0, 5);
            imgcaptcha.ImageUrl = "captcha.aspx";
            imgcaptcha.Height = 50;
            imgcaptcha.Width = 180;
            //CaptchaControl1.BackColor = System.Drawing.Color.FromName("White");
            //CaptchaControl1.FontColor = System.Drawing.Color.FromName("Black");
            //CaptchaControl1.LineColor = System.Drawing.Color.FromName("Blue");
            //CaptchaControl1.NoiseColor = System.Drawing.Color.FromName("Yellow");
            //CaptchaControl1.CaptchaBackgroundNoise = (MSCaptcha.CaptchaImage.backgroundNoiseLevel)Enum.Parse(typeof(MSCaptcha.CaptchaImage.backgroundNoiseLevel), "Low");
            //CaptchaControl1.CaptchaLineNoise = (MSCaptcha.CaptchaImage.lineNoiseLevel)Enum.Parse(typeof(MSCaptcha.CaptchaImage.lineNoiseLevel), "Low");
            //CaptchaControl1.CaptchaFontWarping = (MSCaptcha.CaptchaImage.fontWarpFactor)Enum.Parse(typeof(MSCaptcha.CaptchaImage.fontWarpFactor), "Low");
            //CaptchaControl1.CaptchaChars = "ACDEFGHJKLNPQRTUVXYZ2346789";
            //CaptchaControl1.CaptchaLength = Convert.ToInt32("5");
            ////CaptchaControl1.ImageTag = "border=0";
            //CaptchaControl1.CaptchaHeight = 50;
            //CaptchaControl1.CaptchaWidth = 180;
            //CaptchaControl1.Width = 180;
            //CaptchaControl1.Height = 50;
            ////CaptchaControl1.CaptchaWidth = 180;
            //CaptchaControl1.CaptchaMaxTimeout = 180;
            //CaptchaControl1.CaptchaMinTimeout = 3;
            ////Always assign ArithmeticFunction BEFORE the Arithmetic itself. Setting Arithmetic property redraws the captcha.
            //CaptchaControl1.ArithmeticFunction = (MSCaptcha.CaptchaControl.arithmeticOperation)Enum.Parse(typeof(MSCaptcha.CaptchaControl.arithmeticOperation), "Addition");
            //// if (CaptchaControl1.Arithmetic != false) CaptchaControl1.Arithmetic = true;//This changes max length in characters. Make sure your control length supports 7 characters, although there could be less.

        }


        private void BindLabels()
        {
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
                    lblDetails.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["Details"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["Details"].ToString()));

                    DataSet dsQuestions = new DataSet();
                    dsQuestions = connManager.GetData("Select Count(*) from Question where AskedUser = " + user.UserId + "");
                    lblQuestions.Text = dsQuestions.Tables[0].Rows[0][0].ToString();
                    DataSet dsAnswers = new DataSet();
                    dsAnswers = connManager.GetData("Select Count(*) from Replies where RepliedUser = " + user.UserId + "");
                    lblAnswers.Text = dsAnswers.Tables[0].Rows[0][0].ToString();


                    dsAnswers = connManager.GetData("Select Count(*) from Articles where UserId = " + user.UserId + "");
                    if (dsAnswers.Tables.Count > 0 && dsAnswers.Tables[0].Rows.Count > 0)
                        lblArticles.Text = dsAnswers.Tables[0].Rows[0][0].ToString();
                    else
                        lblArticles.Text = "";

                    if (dsUser.Tables[0].Rows[0]["ImageURL"].ToString() == "")
                    {
                        imgProfile.Visible = false;
                    }
                    else
                    {
                        imgProfile.Visible = true;
                        imgProfile.ImageUrl = dsUser.Tables[0].Rows[0]["ImageURL"].ToString();
                    }

                    //imgProfile.ImageUrl = ((dsUser.Tables[0].Rows[0]["ImageURL"].ToString() == "") ? "~/Images/Person.JPG" : (dsUser.Tables[0].Rows[0]["ImageURL"].ToString()));
                }
                connManager.DisposeConn();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            BindTextBoxes();
            //pnlChangePassword.Visible = true;
            pnlPassword.Visible = false;
            pnlSocial.Visible = false;
            ConfigureCapcha();
        }

        protected void CancelChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
            //  pnlChangePassword.Visible = true;
            //pnlPassword.Visible = false;
        }

        public void BindTextBoxes()
        {
            HFMode.Value = "Edit";
            Users user = new Users();
            user = (Users)Session["User"];
            try
            {
                DataSet dsUser = new DataSet();
                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();
                user = (Users)(Session["User"]);
                dsUser = connManager.GetData("select * from Users where UserID = " + user.UserId);
                connManager.DisposeConn();
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    txFirsttName.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["FirstName"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["FirstName"].ToString()));
                    txtLastName.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["LastName"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["LastName"].ToString()));
                    txtEMail.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["EMail"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["EMail"].ToString()));
                    txtAddress.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["Address"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["Address"].ToString()));
                    txtDetails.Text = ((System.Convert.IsDBNull(dsUser.Tables[0].Rows[0]["Details"].ToString()) == true) ? "" : (dsUser.Tables[0].Rows[0]["Details"].ToString()));
                }
                pnlRegister.Visible = true;
                pnlProfile.Visible = false;
            }
            catch (Exception ex)
            {
                Mail mail = new Mail();
                mail.Body = ex.Message;
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "Error in UserProfile.aspx";
                mail.ToAdd = "admin@codeanalyze.com";

                mail.SendMail();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userImageFileName;

            //if (pnlPassword.Visible)
            //{
            // if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
            //{
            // lblUserRegMsg.Visible = true;
            //  lblUserRegMsg.Text = "Passwords don't match.";
            // return;
            //}
            //}

            //CaptchaControl1.ValidateCaptcha(txtCapcha.Text);

            if (txtCapcha.Text.ToString() != Request.Cookies["Captcha"]["value"])
            {
                lblUserRegMsg.Text = "Entered text was incorrect for Capcha, please try again";
                lblUserRegMsg.Visible = true;
                ConfigureCapcha();
                return;
            }

            if (FileUpload1.PostedFile.ContentLength > 1048576)
            {
                lblUserRegMsg.Visible = true;
                lblUserRegMsg.Text = "Image file size should be less than 1 mb";
                return;
            }
            else
            {
                lblUserRegMsg.Visible = false;
                try
                {
                    user = new Users();

                    ConnManager con = new ConnManager();
                    DataSet dsUser = con.GetData("Select * from Users where Email = '" + txtEMail.Text + "'");
                    con.DisposeConn();
                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        if (HFMode.Value != "Edit")
                        {
                            //ClientScriptManager cr = Page.ClientScript;
                            //string scriptStr = "alert('EMail id already exists. If you have forgotten password, please click forgot password link.');";
                            //cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);

                            lblUserRegMsg.Visible = true;
                            lblUserRegMsg.Text = "EMail id already exists. If you have forgotten password, please click forgot password link on the Sign In page.";
                            return;
                        }
                        user.UserId = double.Parse(dsUser.Tables[0].Rows[0]["UserId"].ToString());
                    }


                    double dblUserID = 0;
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
                    user.FirstName = txFirsttName.Text.Trim();
                    user.LastName = txtLastName.Text.Trim();
                    user.Email = txtEMail.Text.Trim();
                    user.Address = txtAddress.Text.Trim();
                    user.Details = txtDetails.Text;

                    user.Password = txtPassword.Text;

                    //if (pnlPassword.Visible)
                    //{
                    //    user.Password = txtPassword.Text;
                    //}
                    //lblUserRegMsg.Visible = true;
                    if (FileUpload1.PostedFile.FileName != "")
                    {
                        try
                        {
                            user.ImageURL = "~/Images/" + FileUpload1.FileName;
                            //lblUserRegMsg.Text = lblUserRegMsg.Text + user.ImageURL;
                            if (!File.Exists(Server.MapPath("Images\\") + FileUpload1.FileName))
                            {
                                ///lblUserRegMsg.Text = lblUserRegMsg.Text + Server.MapPath("Images\\") + FileUpload1.FileName;

                                FileUpload1.PostedFile.SaveAs(Server.MapPath("Images\\") + FileUpload1.FileName);

                                //lblUserRegMsg.Text = lblUserRegMsg.Text + Server.MapPath("Images\\") + FileUpload1.FileName;
                            }
                            else
                            {

                                userImageFileName = Server.MapPath("Images\\1") + FileUpload1.FileName;
                                while (File.Exists(userImageFileName))
                                {
                                    //Path.GetFileNameWithoutExtension(FileUpload1.FileName);
                                    userImageFileName = userImageFileName.Replace("Images\\1", "Images\\11");

                                }
                                FileUpload1.PostedFile.SaveAs(userImageFileName);
                            }

                        }
                        catch (Exception ex)
                        {
                            lblUserRegMsg.Text = ex.ToString();
                            lblUserRegMsg.Visible = true;
                        }
                    }
                    //users.Company = "";
                    //users.Details = 1;
                    //users.Status = 1;


                    if (HFMode.Value == "New")
                    {
                        user.OptionID = 1;
                        user.CreatedDateTime = DateTime.Now;
                    }
                    else if (HFMode.Value == "Edit")
                    {
                        // if (pnlPassword.Visible)
                        //  user.OptionID = 2;
                        //  else
                        user.OptionID = 5;

                        user.ModifiedDateTime = DateTime.Now;
                        dblUserID = user.UserId;
                    }

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


                    //ClientScriptManager cr2 = Page.ClientScript;
                    // string scriptStr2;
                    lblUserRegMsg.Visible = true;

                    if (HFMode.Value == "New")
                    {
                        lblUserRegMsg.Text = "User Registered Successfully. Please login.";

                        SendNewUserRegEMail();
                        SendEMail(user.Email, user.FirstName, user.LastName);
                    }
                    else
                        lblUserRegMsg.Text = "User Updated Successfully.";

                    HFMode.Value = "New";

                    //cr2.RegisterStartupScript(this.GetType(), "test", scriptStr2, true);
                    txFirsttName.Text = "";
                    txtLastName.Text = "";
                    txtEMail.Text = "";
                    txtAddress.Text = "";
                }

                catch (Exception ex)
                {
                    //ClientScriptManager cr2 = Page.ClientScript;
                    //string scriptStr2 = "alert('There was an exception, please try again.');";
                    //cr2.RegisterStartupScript(this.GetType(), "test", scriptStr2, true);
                    lblUserRegMsg.Visible = true;
                    lblUserRegMsg.Text = "There was an exception, please try again.";
                    txFirsttName.Text = "";
                    txtLastName.Text = "";
                    txtEMail.Text = "";

                }

            }
        }

        private void SendNewUserRegEMail()
        {
            try
            {
                Mail mail = new Mail();
                string EMailBody = File.ReadAllText(Server.MapPath("EMailBody.txt"));
                mail.Body = string.Format(EMailBody, "Welcome to CodeAnalyze. We appreciate your time for posting code that help many.");
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "Welcome to CodeAnalyze - Blogger Rewards";
                mail.ToAdd = user.Email;
                mail.IsBodyHtml = true;
                mail.SendMail();
            }
            catch
            {


            }
        }


        private void SendEMail(string Email_address, string firstName, string LastName)
        {
            try
            {
                Mail mail = new Mail();
                mail.Body = "new user email id " + Email_address + " name " + firstName;
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "New User registered";
                mail.ToAdd = "admin@codeanalyze.com";
                mail.IsBodyHtml = true;
                mail.SendMail();
            }
            catch
            {


            }
        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }

        protected void changePassword(object sender, EventArgs e)
        {
            //pnlPassword.Visible = true;
            //pnlChangePassword.Visible = false;
        }
    }
