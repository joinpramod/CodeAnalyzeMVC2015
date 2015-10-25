using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using CodeAnalyzeMVC2015;

public partial class Articles : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            //adasfsdf
            // this.Title = "c#, java, php, javascript and many more";
            if (!IsPostBack)
            {
                BindArticles("Select * from VwArticles order by articleId desc");
                //   BindQuestions("Select top 100 * from Question Where QuestionId > 37861");
                //BindQuestionType("Select * from QuestionType");
                HtmlMeta metaDescription = new HtmlMeta();
                metaDescription.Name = "description";
                metaDescription.Content = "Get Amazon gift cards of your respective country for code blogging as appreciation. Try now.";
                Page.Header.Controls.Add(metaDescription);
                HtmlMeta metaKeywords = new HtmlMeta();
                metaKeywords.Name = "keywords";
                metaKeywords.Content = "Java, C#, PHP, Android, JQuery, XCode, XML, SQL, ASP.NET, HTML5 n many more";
                Page.Header.Controls.Add(metaKeywords);

                LinkButton lnkViewArticles = (LinkButton)this.Master.FindControl("lnkViewArticles");
                // lnkViewArticles.Font.Size = 21;
                lnkViewArticles.ForeColor = Color.Yellow;
            }
        }

        //private void BindQuestionType(string strQuery)
        //{
        //    ConnManager connManager = new ConnManager();
        //    connManager.OpenConnection();
        //    DataSet DSQuestions = new DataSet();
        //    DSQuestions = connManager.GetData(strQuery);
        //    connManager.DisposeConn();
        //    if (DSQuestions != null)
        //    {
        //        if (DSQuestions.Tables[0].Rows.Count > 0)
        //        {
        //            ddType.DataSource = DSQuestions;
        //            ddType.DataBind();
        //        }
        //    }
        //}

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string strSQL = "Select * from VwArticles Where ArticleId > 0 ";

            //if (ddType.SelectedIndex != 0)
            //{
            //    strSQL += " and ArticleType = " + ddType.SelectedValue + " ";
            //}
            if (!string.IsNullOrEmpty(txtQuestionTitle.Text))
            {
                strSQL += " and ArticleTitle like '%" + txtQuestionTitle.Text + "%' ";
            }
            strSQL += " order by InsertedDate desc";

            BindQuestions(strSQL);
            // else
            //   BindQuestions("Select * from Question Where QuestionId > 37861");

            if (GVQuestions.DataSource != null)
                GVQuestions.Visible = true;
        }


        private void BindArticles(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSQuestions = new DataSet();
            DSQuestions = connManager.GetData(strQuery);
            connManager.DisposeConn();
            if (DSQuestions != null)
            {
                if (DSQuestions.Tables[0].Rows.Count > 0)
                {
                    GVQuestions.DataSource = DSQuestions;
                    GVQuestions.DataBind();
                }
                else
                {
                    GVQuestions.DataSource = null;
                    GVQuestions.DataBind();
                }
            }
            else
            {
                GVQuestions.DataSource = null;
                GVQuestions.DataBind();
                //      ClientScriptManager cr = Page.ClientScript;
                //      string scriptStr = "alert('No records exists.');";
                //      cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
            }
        }

        private void BindQuestions(string strQuery)
        {
            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataSet DSQuestions = new DataSet();
            DSQuestions = connManager.GetData(strQuery);
            connManager.DisposeConn();
            if (DSQuestions != null)
            {
                if (DSQuestions.Tables[0].Rows.Count > 0)
                {
                    gvQuestionsAns.DataSource = DSQuestions;
                    gvQuestionsAns.DataBind();
                }
                else
                {
                    gvQuestionsAns.DataSource = null;
                    gvQuestionsAns.DataBind();
                }
            }
            else
            {
                gvQuestionsAns.DataSource = null;
                gvQuestionsAns.DataBind();
                //ClientScriptManager cr = Page.ClientScript;
                //string scriptStr = "alert('No records exists.');";
                //cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
            }
        }

        protected void GVQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Question")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label LblQuestionId = (Label)(row.FindControl("LblQuestionId"));
                //Response.Redirect("Soln.aspx?QId=" + Encryption.EncryptQueryString(LblQuestionId.Text) + "");
                LinkButton LblQT = (LinkButton)(row.FindControl("btnArticleTitle"));

                //Response.Write("<script>");
                //Response.Write("window.open('VA.aspx?QId=" + LblQuestionId.Text + "&QT=" + LblQT.Text + "','_blank')");
                Response.Redirect(String.Format("va.aspx?QId=" + LblQuestionId.Text + "&QT=" + LblQT.Text.ToString().Replace(" ", "-")));
                //Response.Redirect(String.Format("va.aspx/{0}/{1}", LblQuestionId.Text, LblQT.Text.ToString().Replace(" ", "-")));
                //Response.Write("</script>");

            }
        }


        protected void GVQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVQuestions.PageIndex = e.NewPageIndex;
            BindArticles("Select * from VwArticles order by articleId desc");
        }

    }
