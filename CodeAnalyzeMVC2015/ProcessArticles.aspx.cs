using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Web;
using CodeAnalyzeMVC2015;

public partial class ProcessArticles : System.Web.UI.Page
    {

        public string Email_address = "";
        public string firstName = "";
        public string LastName = "";
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindQuestionType("Select * from QuestionType");
                BindUserEmail("Select * from Users");
                LinkButton btnUserProfile = (LinkButton)this.Master.FindControl("btnUserProfile");
                btnUserProfile.ForeColor = Color.Yellow;
            }
        }

        private void BindQuestionType(string strQuery)
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
                    ddType.DataSource = DSQuestions;
                    ddType.DataBind();
                }
            }

        }

        private void BindUserEmail(string strQuery)
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
                    ddUserEmail.DataSource = DSQuestions;
                    ddUserEmail.DataBind();
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            user = (Users)Session["User"];


            lblUserRegMsg.Visible = false;
            try
            {

                if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
                {
                    if (fileUploadWordFile.HasFile || fileUploadSourceFile.HasFile || chkSkipSave.Checked)
                    {
                        string targetFolder = HttpContext.Current.Server.MapPath("~/Articles/");
                        string targetPath = Path.Combine(targetFolder, fileUploadWordFile.FileName);


                        if (fileUploadWordFile.HasFile && !chkSkipSave.Checked)
                            fileUploadWordFile.SaveAs(targetPath);

                        targetPath = Path.Combine(targetFolder, fileUploadSourceFile.FileName);

                        if (fileUploadSourceFile.HasFile && !chkSkipSave.Checked)
                            fileUploadSourceFile.SaveAs(targetPath);


                        user = new Users();
                        CodeArticles article = new CodeArticles();
                        ConnManager con = new ConnManager();
                        double dblArticleID = 0;
                        SqlConnection LclConn = new SqlConnection();
                        SqlTransaction SetTransaction = null;
                        bool IsinTransaction = false;
                        if (LclConn.State != ConnectionState.Open)
                        {
                            article.SetConnection = article.OpenConnection(LclConn);
                            SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                            IsinTransaction = true;
                        }
                        else
                        {
                            article.SetConnection = LclConn;
                        }

                        article.ArticleTitle = txtTitle.Text.Trim();
                        article.ArticleType = int.Parse(ddType.SelectedValue);
                        article.UserId = int.Parse(ddUserEmail.SelectedValue);
                        article.SourceFile = fileUploadSourceFile.FileName;
                        article.WordFile = fileUploadWordFile.FileName;
                        article.YouTubeURL = txtYoutTube.Text;
                        article.ArticleDetails = txtDetails.Text;

                    if (chkIsDisplay.Checked)
                        article.IsDisplay = 0;
                    else
                        article.IsDisplay = 1;

                    int[] myy = new int[8] { 15431, 14362, 21334, 25432, 13234, 18332, 9344, 3453, 6555, 7643, 2343, 1243, 5644, 1234, 2342, 3276,1752, 2845, 1945, 986, 5898, 9123 };
                    Random ran = new Random();
                    int mynum = myy[ran.Next(0, myy.Length)];
                    article.Views = mynum;

                    int[] myy2 = new int[38] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35 };
                    Random ran2 = new Random();
                    int mynum2 = myy2[ran2.Next(0, myy2.Length)];
                    article.ThumbsUp = mynum2;


                    article.OptionID = 1;
                        article.CreatedDateTime = DateTime.Now;


                        bool result = article.CreateArticles(ref dblArticleID, SetTransaction);
                        if (IsinTransaction && result)
                        {
                            SetTransaction.Commit();
                        }
                        else
                        {
                            SetTransaction.Rollback();
                        }
                        article.CloseConnection(LclConn);

                        lblUserRegMsg.Visible = true;

                        lblUserRegMsg.Text = "Article saved successfully";
                    }

                }
                else
                {
                    Response.Redirect("Topics.aspx");
                }
            }

            catch (Exception ex)
            {
                lblUserRegMsg.Visible = true;
                lblUserRegMsg.Text = "There was an exception, please try again.";
                txtTitle.Text = "";


            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProcessArticles.aspx");
        }

    }
