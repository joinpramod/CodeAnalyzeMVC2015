using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CodeAnalyzeMVC2015;

public partial class Topics : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            // this.Title = "c#, java, php, javascript and many more";
            // if (!IsPostBack)
            // {
            //BindQuestions("Select top 200 * from Question order by AskedDateTime desc");
            // BindQuestions("Select top 125 * from Question where isoriginal = 'Y' and askeduser = 1 order by questionid");  
            //BindQuestionType("Select * from QuestionType");
            BindQuestions("Select top 200 * from Question order by questionid");

            //HtmlMeta metaDescription = new HtmlMeta();
            //metaDescription.Name = "description";
            //metaDescription.Content = "CodeAnalyze is a simple coding forum to get code of frequently used logics and functionalities and discuss coding issues. Users will also get rewarded(Amazon gift cards) for posting as it actually helps others and save their time.";
            //Page.Header.Controls.Add(metaDescription);
            //HtmlMeta metaKeywords = new HtmlMeta();
            //metaKeywords.Name = "keywords";
            //metaKeywords.Content = "code, c#, java, php, asp.net, swings, jquery, ajax, sharepoint, javascript, sql, css, n many more";
            //Page.Header.Controls.Add(metaKeywords);

            LinkButton lnkBtnTopics = (LinkButton)this.Master.FindControl("btnTopics");
            //lnkBtnTopics.Font.Size = 24;
            lnkBtnTopics.ForeColor = Color.Yellow;

            // }
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


        //protected void BtnSearch_Click(object sender, EventArgs e)
        //{
        //    if (ddType.SelectedIndex != 0)
        //        BindQuestions("Select top(200) * from Question  where QuestionTypeId = " + ddType.SelectedValue + " and IsOriginal Is NULL  order by AskedDateTime desc");
        //    else
        //        BindQuestions("Select top(200) * from Question where IsOriginal  Is NULL order by AskedDateTime desc");       

        //    if(GVQuestions.DataSource!=null)
        //    GVQuestions.Visible = true;
        //}



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

        //protected void GVQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GVQuestions.PageIndex = e.NewPageIndex;
        //    if(ddType.SelectedIndex != 0)
        //        BindQuestions("Select top(200) * from Question  where IsOriginal Is NULL and QuestionTypeId = " + ddType.SelectedValue + " order by AskedDateTime desc");
        //    else
        //        BindQuestions("Select top(200) * from Question where IsOriginal Is NULL order by AskedDateTime desc");
        //}
    }


