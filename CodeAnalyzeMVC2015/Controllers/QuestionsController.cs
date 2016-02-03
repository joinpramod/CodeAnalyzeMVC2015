using CodeAnalyzeMVC2015.Models;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Controllers
{
    //[RoutePrefix("Questions")]
    public class QuestionsController : BaseController
    {

        public ActionResult UpVote(string Id, string RId, string Title)
        {
            return null;
        }

        public ActionResult DownVote(string Id, string RId, string Title)
        {
            return null;
        }

    }
}