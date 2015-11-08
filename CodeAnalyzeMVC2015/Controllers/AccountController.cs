using System.Web.Mvc;
using CodeAnalyzeMVC2015.Models;
using System.Data;
using System.Collections.Generic;

namespace CodeAnalyzeMVC2015.Controllers
{

    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult ProcessLogin(string txtEMailId, string txtPassword)
        {

            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable DSUserList = new DataTable();
            DSUserList = connManager.GetDataTable("select * from users where email = '" + txtEMailId + "' and Password = '" + txtPassword + "'");
           
            if (DSUserList.Rows.Count == 0)
            {
                //   ClientScriptManager cr = Page.ClientScript;
                //   string scriptStr = "alert('Invalid Username and Password');";
                //   cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
                return null;
            }
            else
            {
                Users user = new Users();
                user.UserId = double.Parse(DSUserList.Rows[0]["UserId"].ToString());
                user.FirstName = DSUserList.Rows[0]["FirstName"].ToString();
                user.LastName = DSUserList.Rows[0]["LastName"].ToString();
                user.Email = DSUserList.Rows[0]["EMail"].ToString();
                user.Address = DSUserList.Rows[0]["Address"].ToString();
                user.ImageURL = DSUserList.Rows[0]["ImageURL"].ToString();
                Session["User"] = user;
                Session["user.Email"] = user.Email;
                ViewBag.UserEmail = user.Email;
                //Label lblFirstName = (Label)this.Master.FindControl("lblFirstName");
                //Panel pnlWelcome = (Panel)this.Master.FindControl("pnlWelcome");
                //LinkButton lnkLogOut = (LinkButton)this.Master.FindControl("lnkLogOut");
                //LinkButton lnkSignIn = (LinkButton)this.Master.FindControl("lnkSignIn");


                //lblFirstName.Text = user.FirstName + " " + user.LastName + ", " + user.Details;
                //pnlWelcome.Visible = true;
                //lnkLogOut.Visible = true;
                //lnkSignIn.Visible = false;
                List<ArticleModel> articles = new List<ArticleModel>();
                articles = connManager.GetArticles("Select * from VwArticles order by articleId desc");
                connManager.DisposeConn();
                return View("../Articles/Index", articles);

            }


        }

    }
}