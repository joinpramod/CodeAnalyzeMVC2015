using CodeAnalyzeMVC2015;
using System;
using System.Data;

using System.Data.SqlClient;


public partial class Suggestions : System.Web.UI.Page
    {
        Users user = new Users();


        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Users)Session["User"];
        }


        protected void PreviewButton_Click(object sender, EventArgs e)
        {
            double dblQuestionID = 0;
            ClsSuggestion suggestion = new ClsSuggestion();
            SqlConnection LclConn = new SqlConnection();
            SqlTransaction SetTransaction = null;
            bool IsinTransaction = false;
            if (LclConn.State != ConnectionState.Open)
            {
                suggestion.SetConnection = suggestion.OpenConnection(LclConn);
                SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                IsinTransaction = true;
            }
            else
            {
                suggestion.SetConnection = LclConn;
            }
            suggestion.OptionID = 1;
            suggestion.Suggestion = EditorAskQuestion.Text;
            suggestion.CreatedDate = DateTime.Now;

            if (Session["User"] != null)
                suggestion.CreatedUser = user.UserId;
            else
            {
                double dblUser = 0;
                //if (!UserExists(ref dblUser))
                //    suggestion.CreatedUser = CreateUser(txtEMail.Text);
                //else
                suggestion.CreatedUser = dblUser;
            }

            bool result = suggestion.CreateSuggestion(ref dblQuestionID, SetTransaction);

            if (IsinTransaction && result)
            {
                SetTransaction.Commit();
                Mail mail = new Mail();
                mail.Body = EditorAskQuestion.Text;
                if (Session["User"] != null)
                    mail.FromAdd = "admin@codeanalyze.com";
                else
                    mail.FromAdd = txtEMail.Text;
                mail.Subject = "Suggestion";
                mail.ToAdd = "admin@codeanalyze.com";

                mail.SendMail();
            }
            else
            {
                SetTransaction.Rollback();
            }
            suggestion.CloseConnection(LclConn);

            lblSuggestion.Visible = true;
            lblSuggestion.Text = "Thank you very much.";


            //ClientScriptManager cr = Page.ClientScript;
            //string scriptStr = "alert('Thank you very much.');";
            //cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
        }


        //private double CreateUser(string strEmail)
        //{
        //    double dblUserID = 0;
        //    user = new Users();
        //    SqlConnection LclConn = new SqlConnection();
        //    SqlTransaction SetTransaction = null;
        //    bool IsinTransaction = false;
        //    if (LclConn.State != ConnectionState.Open)
        //    {
        //        user.SetConnection = user.OpenConnection(LclConn);
        //        SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
        //        IsinTransaction = true;
        //    }
        //    else
        //    {
        //        user.SetConnection = LclConn;
        //    }
        //    user.Email = txtEMail.Text.Trim();
        //    user.OptionID = 1;
        //    user.CreatedDateTime = DateTime.Now;
        //    bool result = user.CreateUsers(ref dblUserID, SetTransaction);
        //    if (IsinTransaction && result)
        //    {
        //        SetTransaction.Commit();
        //    }
        //    else
        //    {
        //        SetTransaction.Rollback();
        //    }
        //    user.CloseConnection(LclConn);
        //    return dblUserID;
        //}


        //private bool UserExists(ref double userId)
        //{
        //    ConnManager connManager = new ConnManager();
        //    connManager.OpenConnection();
        //    DataSet dsUserExists = connManager.GetData("Select * from Users where EMail = '" + txtEMail.Text + "'");
        //    connManager.DisposeConn();
        //    if (dsUserExists.Tables[0].Rows.Count > 0)
        //    {
        //        userId = double.Parse(dsUserExists.Tables[0].Rows[0]["Userid"].ToString());
        //        return true;

        //    }
        //    else
        //        return false;
        //}


    }
