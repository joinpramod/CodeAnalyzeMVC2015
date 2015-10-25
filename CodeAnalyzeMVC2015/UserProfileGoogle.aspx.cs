using CodeAnalyzeMVC2015;
using System;
using System.Web.UI;


public partial class UserProfileGoogle : System.Web.UI.Page
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

            ClientScriptManager cr = Page.ClientScript;

            // cr.RegisterStartupScript(GetType(), "Javascript", "javascript:OpenGoogleLoginPopup();", true);

            cr.RegisterStartupScript(GetType(), "Javascript", "javascript:GetHashValue();", true);

            //Panel _pnlLogin = (Panel)this.Master.FindControl("pnlLogin");
            //Panel _pnlMarquee = (Panel)this.Master.FindControl("pnlMarquee");

            //_pnlLogin.Visible = false;
            //_pnlMarquee.Visible = true;

            //if (!this.IsPostBack)
            //{
            //    Client_ID = "882773719808-khnohb473csa52jmlc477mgjn9rnrpb6.apps.googleusercontent.com";  //ConfigurationSettings.AppSettings["google_clientId"].ToString();
            //    Return_url = "http://codeanalyze.com/Topics.aspx"; //ConfigurationSettings.AppSettings["google_RedirectUrl"].ToString();
            //}
            //LinkButton btnUserProfile = (LinkButton)this.Master.FindControl("btnUserProfile");
            //btnUserProfile.Font.Size = 24;
        }




    }
