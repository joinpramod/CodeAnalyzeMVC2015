using System;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Utilities
/// </summary>
 namespace CodeAnalyzeMVC2015
{
public static class Utilities
{
    static string Target = "";

    public static string ExpandUrls(string inputText)
    {
        string pattern = @"[""'=]?(http://|ftp://|https://|www\.|ftp\.[\w]+)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";
        System.Text.RegularExpressions.RegexOptions options = RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase;
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern, options);
        MatchEvaluator MatchEval = new MatchEvaluator(ExpandUrlsRegExEvaluator);
        return Regex.Replace(inputText, pattern, MatchEval);
    }

    private static string ExpandUrlsRegExEvaluator(System.Text.RegularExpressions.Match M)
    {

        string Href = M.Value;
        if (Href.StartsWith("=") || Href.StartsWith("'") || Href.StartsWith("\""))
            return Href;

        string Text = Href;
        if (Href.IndexOf("://") < 0)
        {
            if (Href.StartsWith("www."))
                Href = "http://" + Href;
            else if (Href.StartsWith("ftp"))
                Href = "ftp://" + Href;
            else if (Href.IndexOf("@") > -1)
                Href = "mailto:" + Href;
        }

        string Targ = !string.IsNullOrEmpty(Target) ? " target='" + Target + "'" : "";
        return "<a href='" + Href + "'" + Targ + ">" + Text + "</a>";

    }

    public static string GetUserIP()
    {
        string strIP = String.Empty;
        HttpRequest httpReq = HttpContext.Current.Request;

        //test for non-standard proxy server designations of client's IP
        if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
        {
            strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
        }
        else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
        //test for host address reported by the server
        else if
        (
            //if exists
            (httpReq.UserHostAddress.Length != 0)
            &&
            //and if not localhost IPV6 or localhost name
            ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
        )
        {
            strIP = httpReq.UserHostAddress;
        }
        return strIP;
    }


}
}