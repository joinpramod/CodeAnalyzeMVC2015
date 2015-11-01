using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using CodeAnalyzeMVC2015.Models;

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

        public DataTable GetDataTable(string sqlQuery)
        {
            DataSet DS = new DataSet();
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(sqlQuery, DataCon);
                DA.Fill(DS);
            }
            catch (System.Exception ex)
            {

            }
            return DS.Tables[0];
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


        public List<ArticleModel> GetArticles(string sqlQuery)
        {
            OpenConnection();
            DataTable DSQuestions = new DataTable();
            DSQuestions = GetDataTable(sqlQuery);
            DisposeConn();

            List<ArticleModel> articles = new List<ArticleModel>();
            ArticleModel article;
            foreach (DataRow row in DSQuestions.Rows)
            {
                article = new ArticleModel();
                article.ArticleID = row["ArticleID"].ToString();
                article.ArticleTitle = row["ArticleTitle"].ToString();
                article.InsertedDate = row["InsertedDate"].ToString();
                article.ThumbsUp = row["ThumbsUp"].ToString();
                article.ThumbsDown = row["ThumbsDown"].ToString();
                article.Views = row["Views"].ToString();
                articles.Add(article);
            }
            return articles;
        }

        public List<QuestionModel> GetQuestions(string sqlQuery)
        {
            OpenConnection();
            DataTable DSQuestions = new DataTable();
            DSQuestions = GetDataTable(sqlQuery);
            DisposeConn();

            List<QuestionModel> questions = new List<QuestionModel>();
            QuestionModel question;
            foreach (DataRow row in DSQuestions.Rows)
            {
                question = new QuestionModel();
                question.QuestionID = row["QuestionID"].ToString();
                question.QuestionType = row["QuestionTypeID"].ToString();
                question.QuestionTitle = row["QuestionTitle"].ToString();
                question.Question = row["Question"].ToString();
                question.AskedUser = row["AskedUser"].ToString();
                question.AskedDateTime = row["AskedDateTime"].ToString();
                questions.Add(question);
            }
            return questions;
        }

        //public List<QuestionType> GetQuestionType(string sqlQuery)
        //{
        //    OpenConnection();
        //    DataTable DSQuestions = new DataTable();
        //    DSQuestions = GetData(sqlQuery);
        //    DisposeConn();

        //    List<QuestionType> types = new List<QuestionType>();
        //    QuestionType type;
        //    foreach (DataRow row in DSQuestions.Rows)
        //    {
        //        type = new QuestionType();
        //        type.TypeId = row["QuestionID"].ToString();
        //        type.TypeValue = row["QuestionTitle"].ToString();

        //        types.Add(type);
        //    }
        //    return types;
        //}

    }
}