using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace CodeAnalyzeMVC2015
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            System.Threading.Timer ChatRoomsCleanerTimer = new System.Threading.Timer(new TimerCallback(ChatEngine.CleanChatRooms), null, 1200000, 1200000);

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 45;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = new Exception();
            ex = Server.GetLastError();
            if (!ex.Message.Contains("This is an invalid script resource request"))
            {
                if (!ex.Message.Contains("File does not exist"))
                {
                    Mail mail = new Mail();
                    try
                    {
                        string strBody = "";

                        if (!string.IsNullOrEmpty(ex.Message))
                            strBody += "Message -- " + ex.Message + "<br /><br />";
                        if (!string.IsNullOrEmpty(ex.Source))
                            strBody += "Source -- " + ex.Source + "<br /><br />";
                        if (ex.TargetSite != null)
                            strBody += "TargetSite -- " + ex.TargetSite + "<br /><br />";
                        if (ex.Data != null)
                            strBody += "Data -- " + ex.Data + "<br /><br />";
                        if (ex.InnerException != null)
                            strBody += "InnerException -- " + ex.InnerException + "<br /><br />";
                        if (!string.IsNullOrEmpty(ex.Source))
                            strBody += "Source -- " + ex.Source + "<br /><br />";

                        try
                        {
                            strBody += "IP - " + Utilities.GetUserIP() + "<br /><br />";
                        }
                        catch
                        {

                        }

                        strBody += " Stack Trace -- " + ex.StackTrace + " <br /><br />";

                        try
                        {
                            if (ex.GetType().ToString() != null)
                                strBody += "Type -- " + ex.GetType().ToString() + "<br />";

                            if (ex.GetType().ToString() == "System.Web.HttpException")
                                Response.Redirect("/Home/Error");
                        }
                        catch
                        {

                        }
                        mail.Body = strBody;
                    }
                    catch
                    {
                        try
                        {
                            mail.Body = "IP - " + Utilities.GetUserIP() + "<br />" + ex.ToString();
                        }
                        catch
                        {
                            mail.Body = ex.ToString();
                        }
                    }
                    mail.FromAdd = "admin@codeanalyze.com";
                    mail.Subject = "Error";
                    mail.ToAdd = "admin@codeanalyze.com";
                    mail.IsBodyHtml = true;
                    mail.SendMail();
                    if (Request.Url.ToString().Contains("localhost"))
                        HttpContext.Current.Response.Redirect("/CodeAnalyzeMVC2015/Home/Error");
                    else
                        HttpContext.Current.Response.Redirect("/Home/Error");
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}