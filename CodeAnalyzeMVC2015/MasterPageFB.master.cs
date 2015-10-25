using CodeAnalyzeMVC2015;
using System;
using System.IO;



public partial class MasterPageFB : System.Web.UI.MasterPage
    {
        Users user = new Users();

        protected void btnReferFriend_Click(object sender, EventArgs e)
        {
            string strFrom = " freinds";
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                if (!string.IsNullOrEmpty(user.FirstName))
                    strFrom = user.FirstName;
                else
                    strFrom = user.Email;
            }

            Mail mail = new Mail();

            string EMailBody = File.ReadAllText(Server.MapPath("EMailBody.txt"));

            string strCA = "<a id=HyperLink1 style=font-size: medium; font-weight: bold; color:White href=http://codeanalyze.com>CodeAnalyze</a>";

            mail.Body = string.Format(EMailBody, "You have been refered to " + strCA + " by one of your " + strFrom + ". Get Rewards Amazon Gift Cards for code blogging. Do take a look");


            mail.FromAdd = "admin@codeanalyze.com";
            mail.Subject = "Referred to CodeAnalyze http://codeanalyze.com -" + user.Email;
            mail.ToAdd = txtReferEMail.Text;
            mail.IsBodyHtml = true;
            mail.SendMail();

            txtReferEMail.Text = "Done";
        }

    }
