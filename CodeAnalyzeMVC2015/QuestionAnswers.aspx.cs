using CodeAnalyzeMVC2015;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class QuestionAnswers : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Users)(Session["User"]);
            //Session["Type"] = Request.QueryString["Type"];
            if (Request.QueryString["Type"].ToString() == "Questions")
                BindQuestions("SELECT * FROM VwQuestions WHERE AskedUser = " + user.UserId);

            else if (Request.QueryString["Type"].ToString() == "Answers")
                BindAnswers("SELECT * FROM VwSolutions WHERE RepliedUser = " + user.UserId);

            else if (Request.QueryString["Type"].ToString() == "Articles")
                BindArticles("SELECT * FROM VwArticles WHERE UserId = " + user.UserId);
        }


        private void BindQuestions(string sqlQuery)
        {
            DataTable DTQuestionsAnswersTbl = new DataTable();
            DataRow DRQuestionsAnswersData = null;
            DataSet DSQuestionsAnswersDetails = new DataSet();
            int i = 0;
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DSQuestionsAnswersDetails = connManager.GetData(sqlQuery);
            connManager.DisposeConn();
            if (DSQuestionsAnswersDetails.Tables[0].Rows.Count > 0)
            {
                DTQuestionsAnswersTbl.Columns.Add(new DataColumn("QuestionID", typeof(string)));
                DTQuestionsAnswersTbl.Columns.Add(new DataColumn("QuestionTitle", typeof(string)));
                DTQuestionsAnswersTbl.Columns.Add(new DataColumn("AskedDateTime", typeof(string)));
                DTQuestionsAnswersTbl.Columns.Add(new DataColumn("RepliedDate", typeof(string)));
                int tempFor1 = DSQuestionsAnswersDetails.Tables[0].Rows.Count;
                for (i = 0; i < tempFor1; i++)
                {
                    DRQuestionsAnswersData = DTQuestionsAnswersTbl.NewRow();
                    DRQuestionsAnswersData[0] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionID"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionID"]);
                    DRQuestionsAnswersData[1] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionTitle"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionTitle"]);
                    DRQuestionsAnswersData[2] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["AskedDateTime"]) == null ? "" : Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["AskedDateTime"]);
                    DRQuestionsAnswersData[3] = "";
                    DTQuestionsAnswersTbl.Rows.Add(DRQuestionsAnswersData);
                }
                GVQuestionsAnswers.DataSource = DTQuestionsAnswersTbl;
                GVQuestionsAnswers.DataBind();
                GVQuestionsAnswers.Visible = true;
                GVQuestionsAnswers.Columns[3].Visible = false;
            }
            else
            {
                GVQuestionsAnswers.Visible = false;
            }

        }


        private void BindAnswers(string sqlQuery)
        {
            try
            {
                DataTable DTQuestionsAnswersTbl = new DataTable();
                DataRow DRQuestionsAnswersData = null;
                DataSet DSQuestionsAnswersDetails = new DataSet();
                int i = 0;
                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();
                DSQuestionsAnswersDetails = connManager.GetData(sqlQuery);
                connManager.DisposeConn();
                if (DSQuestionsAnswersDetails.Tables[0].Rows.Count > 0)
                {
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("QuestionID", typeof(string)));
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("QuestionTitle", typeof(string)));
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("AskedDateTime", typeof(string)));
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("RepliedDate", typeof(string)));
                    int tempFor1 = DSQuestionsAnswersDetails.Tables[0].Rows.Count;
                    for (i = 0; i < tempFor1; i++)
                    {
                        DRQuestionsAnswersData = DTQuestionsAnswersTbl.NewRow();
                        DRQuestionsAnswersData[0] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionID"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionID"]);
                        DRQuestionsAnswersData[1] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionTitle"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["QuestionTitle"]);
                        DRQuestionsAnswersData[2] = "";
                        DRQuestionsAnswersData[3] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["RepliedDate"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["RepliedDate"]);
                        DTQuestionsAnswersTbl.Rows.Add(DRQuestionsAnswersData);
                    }
                    GVQuestionsAnswers.DataSource = DTQuestionsAnswersTbl;
                    GVQuestionsAnswers.DataBind();
                    GVQuestionsAnswers.Visible = true;
                    GVQuestionsAnswers.Columns[2].Visible = false;
                }
                else
                {
                    GVQuestionsAnswers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void BindArticles(string sqlQuery)
        {
            try
            {
                DataTable DTQuestionsAnswersTbl = new DataTable();
                DataRow DRQuestionsAnswersData = null;
                DataSet DSQuestionsAnswersDetails = new DataSet();
                int i = 0;
                ConnManager connManager = new ConnManager();
                connManager.OpenConnection();
                DSQuestionsAnswersDetails = connManager.GetData(sqlQuery);
                connManager.DisposeConn();
                if (DSQuestionsAnswersDetails.Tables[0].Rows.Count > 0)
                {
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("QuestionID", typeof(string)));
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("QuestionTitle", typeof(string)));
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("AskedDateTime", typeof(string)));
                    DTQuestionsAnswersTbl.Columns.Add(new DataColumn("RepliedDate", typeof(string)));
                    int tempFor1 = DSQuestionsAnswersDetails.Tables[0].Rows.Count;
                    for (i = 0; i < tempFor1; i++)
                    {
                        DRQuestionsAnswersData = DTQuestionsAnswersTbl.NewRow();
                        DRQuestionsAnswersData[0] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["ArticleID"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["ArticleID"]);
                        DRQuestionsAnswersData[1] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["ArticleTitle"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["ArticleTitle"]);
                        DRQuestionsAnswersData[2] = "";
                        DRQuestionsAnswersData[3] = System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["InsertedDate"]) == null ? "" : System.Convert.ToString(DSQuestionsAnswersDetails.Tables[0].Rows[i]["InsertedDate"]);
                        DTQuestionsAnswersTbl.Rows.Add(DRQuestionsAnswersData);
                    }
                    GVQuestionsAnswers.DataSource = DTQuestionsAnswersTbl;
                    GVQuestionsAnswers.DataBind();
                    GVQuestionsAnswers.Visible = true;
                    GVQuestionsAnswers.Columns[2].Visible = false;
                }
                else
                {
                    GVQuestionsAnswers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void GVQuestionsAnswers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVQuestionsAnswers.PageIndex = e.NewPageIndex;
            if (Session["Type"].ToString() == "Questions")
                BindQuestions("SELECT * FROM VwQuestions WHERE AskedUser = " + user.UserId);

            else if (Session["Type"].ToString() == "Answers")
                BindAnswers("SELECT * FROM VwSolutions WHERE ReplyId = " + user.UserId);
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Session["Type"].ToString() == "Questions")
                BindQuestions("SELECT * FROM VwQuestions WHERE QuestionTitle like '%" + txtSearch.Text + "%'");

            else if (Session["Type"].ToString() == "Answers")
                BindAnswers("SELECT * FROM VwSolutions WHERE QuestionTitle like '%" + txtSearch.Text + "%'");
        }


        protected void GVQuestionsAnswers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Request.QueryString["Type"].ToString() == "Articles")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label LblQuestionId = (Label)(row.FindControl("LblQuestionId"));
                Response.Redirect("VP.aspx?QId=" + LblQuestionId.Text + "");
            }

            else
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label LblQuestionId = (Label)(row.FindControl("LblQuestionId"));
                Response.Redirect("Soln.aspx?QId=" + LblQuestionId.Text + "");
            }
        }





    }
