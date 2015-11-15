﻿using CodeAnalyzeMVC2015.AppCode;
using CodeAnalyzeMVC2015.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    public class HomeController : BaseController
    {
        private Users user = new Users();

        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            // ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact(string txtEMail, string txtSuggestion)
        {
            //ViewBag.Message = "Your contact page.";
            if (!string.IsNullOrEmpty(txtEMail) && !string.IsNullOrEmpty(txtSuggestion))
            {

                //double dblQuestionID = 0;
                //ClsSuggestion suggestion = new ClsSuggestion();
                //SqlConnection LclConn = new SqlConnection();
                //SqlTransaction SetTransaction = null;
                //bool IsinTransaction = false;
                //if (LclConn.State != ConnectionState.Open)
                //{
                //    suggestion.SetConnection = suggestion.OpenConnection(LclConn);
                //    SetTransaction = LclConn.BeginTransaction(IsolationLevel.ReadCommitted);
                //    IsinTransaction = true;
                //}
                //else
                //{
                //    suggestion.SetConnection = LclConn;
                //}
                //suggestion.OptionID = 1;
                //suggestion.Suggestion = txtSuggestion;
                //suggestion.CreatedDate = DateTime.Now;

                //if (Session["User"] != null)
                //    suggestion.CreatedUser = user.UserId;
                //else
                //{
                //    double dblUser = 0;
                //    suggestion.CreatedUser = dblUser;
                //}

                //bool result = suggestion.CreateSuggestion(ref dblQuestionID, SetTransaction);

              //  if (IsinTransaction && result)
                //{
                  //  SetTransaction.Commit();
                    Mail mail = new Mail();
                    mail.Body = txtSuggestion;
                    if (Session["User"] != null)
                        mail.FromAdd = "admin@codeanalyze.com";
                    else
                        mail.FromAdd = txtEMail;
                    mail.Subject = "Suggestion";
                    mail.ToAdd = "admin@codeanalyze.com";

                    mail.SendMail();
                //}
                // else
                // {
                //     SetTransaction.Rollback();
                //  }
                // suggestion.CloseConnection(LclConn);

                // lblSuggestion.Visible = true;
                ViewBag.Ack = "Thank you very much.";


            }
            return View();
        }

        public ActionResult Rewards()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PostingGuidelines()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Disclaimer()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

       

    }
}