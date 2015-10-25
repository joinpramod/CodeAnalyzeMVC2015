using CodeAnalyzeMVC2015;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


public partial class LinkedIn : System.Web.UI.Page
    {
        private oAuthLinkedIn _oauth = new oAuthLinkedIn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["AuthURL"] = _oauth.AuthorizationLinkGet();
                Session["requestToken"] = _oauth.Token;
                Session["requestTokenSecret"] = _oauth.TokenSecret;
                hypAuthToken.Text = "Click here to get your 'Linked In' security code for only this time.";
            }
        }



        protected void GetToken(object sender, EventArgs e)
        {
            Response.Write("<script>");
            Response.Write("window.open('" + Session["AuthURL"].ToString() + "','_blank')");
            Response.Write("</script>");


        }

        protected void btnGetAccessToken_Click(object sender, EventArgs e)
        {
            _oauth.Token = Session["requestToken"].ToString();
            _oauth.TokenSecret = Session["requestTokenSecret"].ToString();
            _oauth.Verifier = txtoAuth_verifier.Text;
            _oauth.AccessTokenGet(Session["requestToken"].ToString());
            string response = _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,email-address", null);

            if (!string.IsNullOrEmpty(response))
            {
                response = response.Replace("\n", "");
                response = response.Replace(">  ", ">");

                Users linkedInuser = new Users();
                XmlDocument xmld = new XmlDocument();
                xmld.LoadXml(response);
                XmlNodeReader nodeReader = new XmlNodeReader(xmld);
                while (nodeReader.Read())
                {
                    if (nodeReader.Name == "first-name" && nodeReader.NodeType != XmlNodeType.EndElement)
                    {
                        linkedInuser.FirstName = nodeReader.ReadElementString();
                        linkedInuser.LastName = nodeReader.ReadElementString();
                        linkedInuser.Details = nodeReader.ReadElementString();
                    }
                }

                string strId = linkedInuser.FirstName + "." + linkedInuser.LastName;
                if (strId != null)
                {
                    double dblLinkedInUser = 0.0;
                    if (!UserExists(ref dblLinkedInUser, linkedInuser))
                    {
                        dblLinkedInUser = CreateUser(linkedInuser);
                    }
                    linkedInuser.UserId = dblLinkedInUser;
                    Session["User"] = linkedInuser;
                }
                Response.Redirect("AskQuestions.aspx");
            }
        }


        private double CreateUser(Users user)
        {
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

            return dblUserID;
        }


        private bool UserExists(ref double userId, Users user)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet dsUserExists = connManager.GetData("Select * from Users where FirstName = '" + user.FirstName + "' and LastName = '" + user.LastName + "' and Details = '" + user.Details + "'");
            connManager.DisposeConn();
            if (dsUserExists.Tables[0].Rows.Count > 0)
            {
                userId = double.Parse(dsUserExists.Tables[0].Rows[0]["Userid"].ToString());
                return true;
            }
            else
                return false;

        }
    }
