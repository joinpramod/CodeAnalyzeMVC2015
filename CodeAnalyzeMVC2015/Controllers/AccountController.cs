using System.Web.Mvc;
using CodeAnalyzeMVC2015.Models;
using System.Data;
using System.Collections.Generic;
using CodeAnalyzeMVC2015.AppCode;
using System;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace CodeAnalyzeMVC2015.Controllers
{

    public class AccountController : BaseController
    {
        private Users user = new Users();

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult ForgotPassword(string txtEMailId)
        {
            if (!string.IsNullOrEmpty(txtEMailId))
            {
                ConnManager con = new ConnManager();
                DataSet dsUser = con.GetData("Select * from Users where Email = '" + txtEMailId + "'");
                con.DisposeConn();
                if (dsUser.Tables[0].Rows.Count <= 0)
                {
                    ViewBag.Ack = "No such EMail Id exists";
                }
                if (!string.IsNullOrEmpty(dsUser.Tables[0].Rows[0]["Password"].ToString()))
                {
                    Mail mail = new Mail();
                    mail.IsBodyHtml = true;
                    string EMailBody = System.IO.File.ReadAllText(Server.MapPath("EMailBody.txt"));

                    mail.Body = string.Format(EMailBody, "Your CodeAnalyze account password is " + dsUser.Tables[0].Rows[0]["Password"].ToString());


                    mail.FromAdd = "admin@codeanalyze.com";
                    mail.Subject = "Code Analyze account password";
                    mail.ToAdd = dsUser.Tables[0].Rows[0]["EMail"].ToString();
                    mail.SendMail();

                    ViewBag.Ack = "Password mailed successfully";
                }
                else
                {

                    ViewBag.Ack = "You have created your profile thorugh one of the social sites. Please use the same channel to login. Google Or Facebook Or LinkedIn";
                }
            }
            return View();
        }


        public ActionResult ProcessLogin(string txtEMailId, string txtPassword)
        {

            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DataTable DSUserList = new DataTable();
            DSUserList = connManager.GetDataTable("select * from users where email = '" + txtEMailId + "' and Password = '" + txtPassword + "'");

            if (DSUserList.Rows.Count == 0)
            {
                ViewBag.lblAck = "Invalid login credentials, please try again";
                return View("../Account/Login");
            }
            else
            {
                Users user = new Users();
                user.UserId = double.Parse(DSUserList.Rows[0]["UserId"].ToString());
                user.FirstName = DSUserList.Rows[0]["FirstName"].ToString();
                user.LastName = DSUserList.Rows[0]["LastName"].ToString();
                user.Email = DSUserList.Rows[0]["EMail"].ToString();
                user.Address = DSUserList.Rows[0]["Address"].ToString();
                user.ImageURL = DSUserList.Rows[0]["ImageURL"].ToString();
                user.Details = DSUserList.Rows[0]["Details"].ToString();
                Session["User"] = user;
                Session["user.Email"] = user.Email;
                ViewBag.UserEmail = user.Email;

                List<ArticleModel> articles = new List<ArticleModel>();
                articles = connManager.GetArticles("Select * from VwArticles order by articleId desc");
                connManager.DisposeConn();

                PagingInfo info = new PagingInfo();
                info.SortField = " ";
                info.SortDirection = " ";
                info.PageSize = 10;
                info.PageCount = Convert.ToInt32(Math.Ceiling((double)(articles.Count / info.PageSize)));
                info.CurrentPageIndex = 0;
                var query = articles.OrderBy(c => c.ArticleID).Take(info.PageSize);
                ViewBag.PagingInfo = info;
                return View("../Articles/Index", query.ToList());

            }
        }


        public ActionResult Users()
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                if (Request.Form["Edit"] != null)
                {
                    return View(user);
                }
                else if (Request.Form["ChangePassword"] != null)
                {
                    return View("../Account/ChangePassword", user);
                }
                return null;
            }
            else
            {
                user = new Users();
                return View(user);
            }

        }

        [ReCaptcha]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateEditUser(Users user, HttpPostedFileBase fileUserPhoto)
        {
            //AddEdit user
            if (Request.Form["Cancel"] == null)
            {
                if (ModelState.IsValid)
                {
                    if (fileUserPhoto.ContentLength > 1048576)
                    {
                        ViewBag.Ack = "Image file size should be less than 1 mb";
                        //return;
                    }
                    else
                    {
                        try
                        {
                            ConnManager con = new ConnManager();
                            DataSet dsUser = con.GetData("Select * from Users where Email = '" + user.Email + "'");
                            con.DisposeConn();
                            if (dsUser.Tables[0].Rows.Count > 0)
                            {
                                if (Session["User"] == null)
                                {
                                    ViewBag.Ack = "EMail id already exists. If you have forgotten password, please click forgot password link on the Sign In page.";
                                    //return;
                                }
                                user.UserId = double.Parse(dsUser.Tables[0].Rows[0]["UserId"].ToString());
                            }


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

                            if (fileUserPhoto.FileName != "")
                            {
                                try
                                {
                                    string fileName = System.IO.Path.GetFileName(fileUserPhoto.FileName);

                                    user.ImageURL = "~/Images/" + fileName;
                                    if (!System.IO.File.Exists(Server.MapPath("~\\Images\\") + fileName))
                                    {
                                        fileUserPhoto.SaveAs(Server.MapPath("~\\Images\\") + fileName);
                                    }
                                    else
                                    {

                                        string userImageFileName = Server.MapPath("~\\Images\\1") + fileName;
                                        while (!System.IO.File.Exists(userImageFileName))
                                        {
                                            userImageFileName = userImageFileName.Replace("~\\Images\\1", "~\\Images\\11");

                                        }
                                        fileUserPhoto.SaveAs(userImageFileName);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ViewBag.Ack = "Please try again";
                                }
                            }

                            if (Session["User"] == null)
                            {
                                user.OptionID = 1;
                                user.CreatedDateTime = DateTime.Now;
                            }
                            else //if (HFMode.Value == "Edit")
                            {
                                user.OptionID = 5;
                                user.ModifiedDateTime = DateTime.Now;
                                dblUserID = user.UserId;
                            }

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



                            if (Session["User"] == null)
                            {
                                ViewBag.Ack = "User Registered Successfully. Please login.";
                                SendNewUserRegEMail();
                                SendEMail(user.Email, user.FirstName, user.LastName);
                            }
                            else
                                ViewBag.Ack = "User Updated Successfully.";
                        }
                        catch
                        {

                        }
                    }
                    Session["User"] = user;
                    return View("ViewUser", user);
                }
                else
                {
                    ViewBag.Ack = ModelState["ReCaptcha"].Errors[0].ErrorMessage;
                    return View("Users", user);
                }
            }
            else
            {

                if (Session["User"] != null)
                {
                    user = (Users)Session["User"];
                    return View("../Account/ViewUser", user);
                }
                else
                {
                    Response.Redirect("../Articles/Index");

                }
                return null;
            }
        }

        private void SendNewUserRegEMail()
        {
            try
            {
                Mail mail = new Mail();
                string EMailBody = System.IO.File.ReadAllText(Server.MapPath("EMailBody.txt"));
                mail.Body = string.Format(EMailBody, "Welcome to CodeAnalyze. We appreciate your time for posting code that help many.");
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "Welcome to CodeAnalyze - Blogger Rewards";
                mail.ToAdd = user.Email;
                mail.IsBodyHtml = true;
                mail.SendMail();
            }
            catch
            {


            }
        }

        private void SendEMail(string Email_address, string firstName, string LastName)
        {
            try
            {
                Mail mail = new Mail();
                mail.Body = "new user email id " + Email_address + " name " + firstName;
                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "New User registered";
                mail.ToAdd = "admin@codeanalyze.com";
                mail.IsBodyHtml = true;
                mail.SendMail();
            }
            catch
            {


            }
        }

        public ActionResult ViewUser(Users user)
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];

                if (Request.Url.ToString().Contains("localhost"))
                {
                    if (user.ImageURL != null && !user.ImageURL.Contains("/CodeAnalyzeMVC2015/"))
                    {
                        user.ImageURL = "/CodeAnalyzeMVC2015/" + user.ImageURL.Replace("~/", "");
                    }
                }
                else //if (user.ImageURL != null && !user.ImageURL.Contains("/codeanalyze.com/"))
                {
                    user.ImageURL = user.ImageURL.Replace("~", "");
                }

            }
            return View(user);
        }

        public ActionResult ChangePassword(string txtNewPassword)
        {

            if (Request.Form["Cancel"] == null)
            {
                if (Session["User"] != null && !string.IsNullOrEmpty(txtNewPassword))
                {
                    user = (Users)Session["User"];
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
                    user.Password = txtNewPassword;
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
                    //lblUserRegMsg.Visible = true;
                    ViewBag.Ack = "Password changed successfully";
                    //txtPassword.Text = "";
                    //txtConfirmPassword.Text = "";

                    ViewBag.OldPassword = user.Password;
                }
                return View(user);
            }
            else
            {
                if (Session["User"] != null)
                {
                    user = (Users)Session["User"];
                }
                return View("../Account/ViewUser", user);
            }
        }

        public ActionResult MyQues()
        {
            List<QuestionModel> questions = new List<QuestionModel>();
            questions = GetMyQues(questions);
            PagingInfo info = new PagingInfo();
            info.SortField = " ";
            info.SortDirection = " ";
            info.PageSize = 20;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(questions.Count() / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = questions.OrderBy(c => c.QuestionID).Take(info.PageSize);
            ViewBag.PagingInfo = info;
            return View(query.ToList());
        }

        private List<QuestionModel> GetMyQues(List<QuestionModel> questions)
        {
            user = (Users)Session["User"];
            if (ModelState.IsValid)
            {
                string strSQL = string.Empty;
                strSQL = "SELECT * FROM VwQuestions WHERE AskedUser = " + user.UserId;
                ConnManager connManager = new ConnManager();
                questions = connManager.GetQuestions(strSQL);
            }
            return questions;
        }

        [HttpPost]
        public ActionResult MyQues(PagingInfo info)
        {
            if (Request.Form["Cancel"] == null)
            {
                List<QuestionModel> questions = new List<QuestionModel>();
                questions = GetMyQues(questions);

                IQueryable<QuestionModel> query = questions.AsQueryable();
                query = query.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize);
                ViewBag.PagingInfo = info;
                List<QuestionModel> model = query.ToList();
                return View(model);
            }
            else
            {
                if (Session["User"] != null)
                {
                    user = (Users)Session["User"];
                }
                return View("../Account/ViewUser", user);

            }
        }



        public ActionResult MyAns()
        {

            List<VwSolutionsModel> solns = new List<VwSolutionsModel>();
            solns = GetMyAns(solns);
            PagingInfo info = new PagingInfo();
            info.SortField = " ";
            info.SortDirection = " ";
            info.PageSize = 20;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(solns.Count() / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = solns.OrderBy(c => c.QuestionID).Take(info.PageSize);
            ViewBag.PagingInfo = info;
            return View(query.ToList());

        }

        private List<VwSolutionsModel> GetMyAns(List<VwSolutionsModel> solns)
        {
            user = (Users)Session["User"];
            if (ModelState.IsValid)
            {
                string strSQL = string.Empty;
                strSQL = "SELECT * FROM VwSolutions WHERE RepliedUser = " + user.UserId;
                ConnManager connManager = new ConnManager();
                solns = connManager.GetSolns(strSQL);
            }
            return solns;
        }

        [HttpPost]
        public ActionResult MyAns(PagingInfo info)
        {
            if (Request.Form["Cancel"] == null)
            {
                List<VwSolutionsModel> questions = new List<VwSolutionsModel>();
                questions = GetMyAns(questions);

                IQueryable<VwSolutionsModel> query = questions.AsQueryable();
                query = query.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize);
                ViewBag.PagingInfo = info;
                List<VwSolutionsModel> model = query.ToList();
                return View(model);
            }
            else
            {
                if (Session["User"] != null)
                {
                    user = (Users)Session["User"];
                }
                return View("../Account/ViewUser", user);

            }
        }




        public ActionResult MyArts()
        {

            List<ArticleModel> solns = new List<ArticleModel>();
            solns = GetMyArts(solns);
            PagingInfo info = new PagingInfo();
            info.SortField = " ";
            info.SortDirection = " ";
            info.PageSize = 20;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(solns.Count() / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = solns.OrderBy(c => c.ArticleID).Take(info.PageSize);
            ViewBag.PagingInfo = info;
            return View(query.ToList());

        }

        private List<ArticleModel> GetMyArts(List<ArticleModel> solns)
        {
            user = (Users)Session["User"];
            if (ModelState.IsValid)
            {
                string strSQL = string.Empty;
                strSQL = "SELECT * FROM VwArticles WHERE UserId = " + user.UserId;
                ConnManager connManager = new ConnManager();
                solns = connManager.GetArticles(strSQL);
            }
            return solns;
        }

        [HttpPost]
        public ActionResult MyArts(PagingInfo info)
        {
            if (Request.Form["Cancel"] == null)
            {
                List<ArticleModel> arts = new List<ArticleModel>();
                arts = GetMyArts(arts);

                IQueryable<ArticleModel> query = arts.AsQueryable();
                query = query.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize);
                ViewBag.PagingInfo = info;
                List<ArticleModel> model = query.ToList();
                return View(model);
            }
            else
            {
                if (Session["User"] != null)
                {
                    user = (Users)Session["User"];
                }
                return View("../Account/ViewUser", user);

            }
        }



    }
}