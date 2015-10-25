using CodeAnalyzeMVC2015;
using System;
using System.IO;



public partial class SendEMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Mail mail = new Mail();
                string EMailBody = File.ReadAllText(Server.MapPath("EMailBody.txt"));

                string var = "Hi Suraj,";

                var += "<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;Thanks for sending the article ASP.NET MVC Architecture Routing Pipeline. Your articles has been posted and is live. <br /> <br />&nbsp;&nbsp;&nbsp;&nbsp; You can take a look ";

                var += "<a href=http://codeanalyze.com/va.aspx?QId=36&QT=ASP.NET-MVC-Architecture-Routing--Pipeline>here</a>";

                var += "<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;We appreciate your time and value it a lot. We look forward more of these in future. We will also be sending you rewards email of Amazon Gift as the best possible appreciation from CodeAnalyze when you reach our MARK. Check <a href=http://codeanalyze.com/Credits.aspx>Rewards</a> for more details.";

                var += "<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;Please reach us any time for any concerns, we assure to resolve immediately";


                var += "<br /><br />";

                var += "Thanks<br />CodeAnalyze Team";

                mail.Body = string.Format(EMailBody, "" + var + "");

                //mail.Body = mail.Body + "We appreciate your time and value it a lot. We look forward more of like this in future. You will also be getting your rewards email of Amazon Gift as the best possible appreciation from CodeAnalyze.";

                mail.FromAdd = "admin@codeanalyze.com";
                mail.Subject = "CodeAnalyze - Article Posted";
                //  mail.ToAdd = "suraj.0241@gmail.com";
                mail.IsBodyHtml = true;
                mail.SendMail();
            }
            catch
            {


            }
        }



    }
