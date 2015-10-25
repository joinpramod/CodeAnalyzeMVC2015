using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using CodeAnalyzeMVC2015;

public partial class Login : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {

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
                ClientScriptManager cr = Page.ClientScript;
                string scriptStr = "alert('Invalid Username and Password');";
                cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
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

                Label lblFirstName = (Label)this.Master.FindControl("lblFirstName");
                Panel pnlWelcome = (Panel)this.Master.FindControl("pnlWelcome");
                LinkButton lnkLogOut = (LinkButton)this.Master.FindControl("lnkLogOut");
                LinkButton lnkSignIn = (LinkButton)this.Master.FindControl("lnkSignIn");


                lblFirstName.Text = user.FirstName + " " + user.LastName + ", " + user.Details;
                pnlLogin.Visible = false;
                pnlWelcome.Visible = true;
                lnkLogOut.Visible = true;
                lnkSignIn.Visible = false;

                // pnlMarquee.Visible = true;
                //Response.Redirect(this.Request.RawUrl);
                Response.Redirect("UserProfile.aspx");

            }
        }


        protected void lnkForgotPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
