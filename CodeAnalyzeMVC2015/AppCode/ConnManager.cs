using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace CodeAnalyzeMVC2015
{
    public class ConnManager
    {
        private string ConnString;
        public SqlConnection DataCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLCON"].ToString());

        public void OpenConnection()
        {
            if (DataCon.State == ConnectionState.Open)
            {
                return;
            }
            DataCon.Open();
        }

        public void DisposeConn()
        {
            if (DataCon.State == ConnectionState.Open)
            {
                DataCon.Close();
                DataCon.Dispose();
            }
        }

        public SqlConnection OpenConnection(SqlConnection DbConn)
        {
            if (DbConn.State == System.Data.ConnectionState.Open)
            {
                return DbConn;
            }
            DbConn.ConnectionString = ConfigurationManager.ConnectionStrings["SQLCON"].ToString();
            DbConn.Open();
            return DbConn;
        }

        public void CloseConnection(SqlConnection DbConn)
        {
            if (DbConn.State == ConnectionState.Open)
            {
                DbConn.Close();
            }
            DbConn.Dispose();
        }

        public DataSet GetData(string sqlQuery)
        {
            DataSet DS = new DataSet();
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(sqlQuery, DataCon);
                DA.Fill(DS);
                return DS;
            }
            catch (System.Exception ex)
            {

            }
            return DS;
        }


        public DataTable GetArticle(string sqlArticleId)
        {
            DataTable dtArticle = new DataTable();
            using (SqlCommand _cmd = new SqlCommand("GetArticle_Sp", DataCon))
            {
                _cmd.CommandType = CommandType.StoredProcedure;

                _cmd.Parameters.Add(new SqlParameter("@ArticleId", SqlDbType.Int));
                _cmd.Parameters["@ArticleId"].Value = sqlArticleId;

                SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                _dap.Fill(dtArticle);
            }
            return dtArticle;
        }


        public DataTable GetQuestion(string sqlQuestionId)
        {
            DataTable dtQuestion = new DataTable();
            using (SqlCommand _cmd = new SqlCommand("GetQuestion_Sp", DataCon))
            {
                _cmd.CommandType = CommandType.StoredProcedure;

                _cmd.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int));
                _cmd.Parameters["@QuestionId"].Value = sqlQuestionId;

                SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                _dap.Fill(dtQuestion);
            }
            return dtQuestion;
        }
    }
}