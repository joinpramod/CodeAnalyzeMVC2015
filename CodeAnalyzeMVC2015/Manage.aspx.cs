using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeAnalyzeMVC2015
{
    public partial class Manage : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                user = (Users)Session["User"];
                if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
                {
                    ConnManager connManager = new ConnManager();
                    connManager.OpenConnection();
                    
                    if(txtSQL.Text.ToLower().StartsWith("select"))
                    {
                        DataSet DSQuestions = new DataSet();
                        DSQuestions = connManager.GetData(txtSQL.Text);
                        connManager.DisposeConn();
                        if (DSQuestions != null)
                        {
                            if (DSQuestions.Tables[0].Rows.Count > 0)
                            {
                                GridView1.DataSource = DSQuestions;
                                GridView1.DataBind();
                            }
                        }
                    }
                    else
                    {
                        SqlCommand comm = new SqlCommand(txtSQL.Text, connManager);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}
