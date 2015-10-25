using CodeAnalyzeMVC2015;
using System;
using System.Data;
using System.IO;



public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ConnManager con = new ConnManager();
            DataSet dsUser = con.GetData("Select * from Users where Email = '" + txtEMail.Text + "'");
            con.DisposeConn();
            if (dsUser.Tables[0].Rows.Count <= 0)
            {
                //ClientScriptManager cr = Page.ClientScript;
                //string scriptStr = "alert('No such EMail Id exists');";
                //cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
                lblForgotPasswordMsg.Visible = true;
                lblForgotPasswordMsg.Text = "No such EMail Id exists";
                return;
            }
            if (!string.IsNullOrEmpty(dsUser.Tables[0].Rows[0]["Password"].ToString()))
            {
                Mail mail = new Mail();
                mail.IsBodyHtml = true;
                string EMailBody = File.ReadAllText(Server.MapPath("EMailBody.txt"));

                mail.Body = string.Format(EMailBody, "Your CodeAnalyze account password is " + dsUser.Tables[0].Rows[0]["Password"].ToString());


                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "Code Analyze account password";
                mail.ToAdd = dsUser.Tables[0].Rows[0]["EMail"].ToString();
                mail.SendMail();


                //ClientScriptManager cr = Page.ClientScript;
                //string scriptStr = "alert('Password mailed successfully');";
                //cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
                lblForgotPasswordMsg.Visible = true;
                lblForgotPasswordMsg.Text = "Password mailed successfully";
                return;
            }
            else
            {

                lblForgotPasswordMsg.Visible = true;
                lblForgotPasswordMsg.Text = "You have created your profile thorugh one of the social sites. Please use the same channel to login. Google Or Facebook Or LinkedIn";

                //lblForgotPasswordMsg.Visible = false;
                //Users user = new Users();
                //user.UserId = double.Parse(dsUser.Tables[0].Rows[0]["UserId"].ToString());
                //Session["User"] = user;
                //Response.Redirect("UserProfile.aspx?Source=ForgotPassword");
            }
        }
    }
