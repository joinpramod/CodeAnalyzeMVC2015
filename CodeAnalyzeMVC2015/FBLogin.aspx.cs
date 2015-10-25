using System;
using System.IO;
using System.Net;
using System.Web.Services;



    public partial class FBLogin : System.Web.UI.Page
    {
        public string Email_address = "";
        public string Google_ID = "";
        public string firstName = "";
        public string LastName = "";
        public string Client_ID = "";
        public string Return_url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                Client_ID = "882773719808-khnohb473csa52jmlc477mgjn9rnrpb6.apps.googleusercontent.com";  //ConfigurationSettings.AppSettings["google_clientId"].ToString();
                Return_url = "https://codeanalyze.com/oauth2callback"; //ConfigurationSettings.AppSettings["google_RedirectUrl"].ToString();

            }
            if (Request.QueryString["access_token"] != null)
            {

                String URI = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + Request.QueryString["access_token"].ToString();

                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(URI);
                string b;

                /*I have not used any JSON parser because I do not want to use any extra dll/3rd party dll*/
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

            }

        }


        [WebMethod]
        public static string FacebookLogin()
        {
            string Name = "Hello Kumar";
            return Name;
        }

    }
