using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Net.Mail;
using CodeAnalyzeMVC2015;

public partial class PostArticles : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {

            user = (Users)Session["User"];

            if (!IsPostBack)
            {

                if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
                {
                    hfUserEMail.Value = user.Email;
                }
                else
                {
                    hfUserEMail.Value = "";
                }

                LinkButton lnkPostArticle = (LinkButton)this.Master.FindControl("lnkPostArticle");
                //lnkPostArticle.Font.Size = 21;
                lnkPostArticle.ForeColor = Color.Yellow;

            }
        }







        protected void PreviewButton_Click(object sender, EventArgs e)
        {

            if (Session["User"] == null)
            {
                lblAck.Visible = true;
                lblAck.Font.Bold = true;
                // lblAck.Text = "Please sign in to post your question.";
            }
            else
            {
                Mail mail = new Mail();
                mail.Body = "<a>New article from " + user.Email + ", file name is " + fileArticleWordFile.FileName + " Youtube URL - " + txtYouTubeLink.Text + " </a>";
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "New Article from " + user.Email;
                mail.ToAdd = "articles@codeanalyze.com";


                string strFileName = System.IO.Path.GetFileName(fileArticleWordFile.PostedFile.FileName);
                Attachment attachFile = new Attachment(fileArticleWordFile.PostedFile.InputStream, strFileName);
                mail.FileAttachment = attachFile;

                if (fileArticleSourceCode.HasFile)
                {
                    strFileName = System.IO.Path.GetFileName(fileArticleSourceCode.PostedFile.FileName);
                    attachFile = new Attachment(fileArticleSourceCode.PostedFile.InputStream, strFileName);
                    mail.SourceFileAttachment = attachFile;
                }

                mail.IsBodyHtml = true;

                if (user.Email != "admin@codeanalyze.com")
                {
                    mail.SendMail();
                }


                lblAck.Visible = true;
                lblAck.Text = "Article emailed successfully. We will get back to you if needed. Thanks much for your post. Appreciate it. ";
                lblAck.Visible = true;
            }

        }


    }
