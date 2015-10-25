using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CodeAnalyzeMVC2015;

public partial class UnAnswered : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //BindQuestions("Select top 200 * from Question order by AskedDateTime desc");
            //BindQuestions("Select top 200 * from Question where IsOriginal = 'Y' order by AskedDateTime desc");  
            // BindQuestions("Select * from VwUnResolved Where ReplyId is null");

            //BindQuestions("Select * from VwUnResolved Where QuestionId > 37861");  

            string strType = Request.QueryString["Type"];

            if (!string.IsNullOrEmpty(strType))
                BindQuestions("Select * from Question Where QuestionId > 37861 and QuestionTypeId = " + strType);
            else
                BindQuestions("Select * from Question Where QuestionId > 37861");

            // BindQuestionType("Select * from QuestionType");

            LinkButton btnUnanswered = (LinkButton)this.Master.FindControl("btnUnanswered");
            // btnUnanswered.Font.Size = 24;
            btnUnanswered.ForeColor = Color.Yellow;
            //HtmlMeta metaDescription = new HtmlMeta();
            //metaDescription.Name = "description";
            ////metaDescription.Content = "Code Analyze is coding forum for finding solution to various coding issues in any language, technology, software or hardware.";
            //metaDescription.Content = "Code Analyze is a simple coding forum to get code of frequently used logics and functionalities. Also users will get rewarded for posting.";
            //Page.Header.Controls.Add(metaDescription);
            //HtmlMeta metaKeywords = new HtmlMeta();
            //metaKeywords.Name = "keywords";
            //metaKeywords.Content = "code, c#, java, php, asp.net, swings, jquery, ajax, sharepoint, javascript, sql, css, n many more";
            //Page.Header.Controls.Add(metaKeywords);
            //}
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
                ClientScriptManager cr = Page.ClientScript;
                string scriptStr = "alert('No records exists.');";
                cr.RegisterStartupScript(this.GetType(), "test", scriptStr, true);
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
            string strSQL = "Select * from Question Where QuestionId > 37861 ";

            //if (ddType.SelectedIndex != 0)
            //{         
            //    strSQL += " and QuestionTypeId = " + ddType.SelectedValue + " ";
            //}
            if (!string.IsNullOrEmpty(txtQuestionTitle.Text))
            {
                strSQL += " and QuestionTitle like '%" + txtQuestionTitle.Text + "%' ";
            }
            strSQL += " order by AskedDateTime desc";

            BindQuestions(strSQL);
            // else
            //   BindQuestions("Select * from Question Where QuestionId > 37861");

            if (GVQuestions.DataSource != null)
                GVQuestions.Visible = true;
        }



        protected void GVQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Question")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label LblQuestionId = (Label)(row.FindControl("LblQuestionId"));
                //Response.Redirect("Soln.aspx?QId=" + Encryption.EncryptQueryString(LblQuestionId.Text) + "");
                LinkButton LblQT = (LinkButton)(row.FindControl("LblQuestion"));

                Response.Write("<script>");
                Response.Write("window.open('Soln.aspx?QId=" + LblQuestionId.Text + "&QT=" + LblQT.Text + "','_blank')");
                Response.Write("</script>");

            }
        }

        protected void GVQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVQuestions.PageIndex = e.NewPageIndex;
            //if(ddType.SelectedIndex != 0)
            //    //BindQuestions("Select * from VwUnResolved Where ReplyId is null and QuestionTypeId = " + ddType.SelectedValue + " order by AskedDateTime desc");
            //    BindQuestions("Select * from Question Where QuestionId > 37861 and QuestionTypeId = " + ddType.SelectedValue + "  order by AskedDateTime desc");

            //else
            BindQuestions("Select * from Question Where QuestionId > 37861");
            // BindQuestions("Select * from VwUnResolved Where ReplyId is null order by AskedDateTime desc");
        }
    }
