using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeAnalyzeMVC2015
{
    public partial class ManageWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Users user = new Users();

            user = (Users)Session["User"];
            if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
            {

            }
            else
            {
                Response.Redirect("http://www.codeanalyze.com");
            }

        }
    }
}