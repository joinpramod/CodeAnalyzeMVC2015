using System;
using System.Data.SqlClient;
using System.Data;
using CodeAnalyzeMVC2015;

public partial class ChangePassword : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                user = (Users)(Session["User"]);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
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
            user.OptionID = 5;
            user.Password = txtPassword.Text;
            user.ModifiedDateTime = DateTime.Now;
            dblUserID = user.UserId;

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
            lblUserRegMsg.Visible = true;
            lblUserRegMsg.Text = "Password changed successfully";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
