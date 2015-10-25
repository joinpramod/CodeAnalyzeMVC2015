using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace CodeAnalyzeMVC2015
{
    public class oAuthLinkedIn : oAuthBase2
    {
        public enum Method { GET, POST, PUT, DELETE };
        public const string REQUEST_TOKEN = "https://api.linkedin.com/uas/oauth/requestToken";
        public const string AUTHORIZE = "https://api.linkedin.com/uas/oauth/authorize";
        public const string ACCESS_TOKEN = "https://api.linkedin.com/uas/oauth/accessToken";

        private string _consumerKey = "";
        private string _consumerSecret = "";
        private string _token = "";
        private string _tokenSecret = "";
        private bool _IsTimeStamp = false;

        #region Properties
        public string ConsumerKey
        {
            get
            {
                if (_consumerKey.Length == 0)
                {
                    _consumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                }
                return _consumerKey;
            }
            set { _consumerKey = value; }
        }

        public string ConsumerSecret
        {
            get
            {
                if (_consumerSecret.Length == 0)
                {
                    _consumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                }
                return _consumerSecret;
            }
            set { _consumerSecret = value; }
        }

        public string Token { get { return _token; } set { _token = value; } }
        public string TokenSecret { get { return _tokenSecret; } set { _tokenSecret = value; } }

        public bool IsTimeStamp
        {
            get
            {
                return _IsTimeStamp;
            }
            set { _IsTimeStamp = value; }
        }
        #endregion

        /// <summary>
        /// Get the link to Twitter's authorization page for this application.
        /// </summary>
        /// <returns>The url with a valid request token, or a null string.</returns>
        public string AuthorizationLinkGet()
        {
            string ret = null;
            string response = oAuthWebRequest(Method.POST, REQUEST_TOKEN, String.Empty);
            if (response != null)
            {
                if (response.Length > 0)
                {
                    //response contains token and token secret.  We only need the token.
                    //oauth_token=36d1871d-5315-499f-a256-7231fdb6a1e0&oauth_token_secret=34a6cb8e-4279-4a0b-b840-085234678ab4&oauth_callback_confirmed=true
                    NameValueCollection qs = HttpUtility.ParseQueryString(response);
                    if (qs["oauth_token"] != null)
                    {
                        this.Token = qs["oauth_token"];
                        this.TokenSecret = qs["oauth_token_secret"];
                        ret = AUTHORIZE + "?oauth_token=" + this.Token;
                    }
                }
            }
            else
            {
                IsTimeStamp = true;
                response = oAuthWebRequest(Method.POST, REQUEST_TOKEN, String.Empty);
                if (response.Length > 0)
                {
                    //response contains token and token secret.  We only need the token.
                    //oauth_token=36d1871d-5315-499f-a256-7231fdb6a1e0&oauth_token_secret=34a6cb8e-4279-4a0b-b840-085234678ab4&oauth_callback_confirmed=true
                    NameValueCollection qs = HttpUtility.ParseQueryString(response);
                    if (qs["oauth_token"] != null)
                    {
                        this.Token = qs["oauth_token"];
                        this.TokenSecret = qs["oauth_token_secret"];
                        ret = AUTHORIZE + "?oauth_token=" + this.Token;
                    }
                }
            }
            return ret;
        }


        /// <summary>
        /// Exchange the request token for an access token.
        /// </summary>
        /// <param name="authToken">The oauth_token is supplied by Twitter's authorization page following the callback.</param>
        public void AccessTokenGet(string authToken)
        {
            this.Token = authToken;

            string response = oAuthWebRequest(Method.POST, ACCESS_TOKEN, string.Empty);

            if (response.Length > 0)
            {
                //Store the Token and Token Secret
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    this.Token = qs["oauth_token"];
                }
                if (qs["oauth_token_secret"] != null)
                {
                    this.TokenSecret = qs["oauth_token_secret"];
                }
            }
        }

        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="method">GET or POST</param>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post (querystring format)</param>
        /// <returns>The web server response.</returns>
        public string oAuthWebRequest(Method method, string url, string postData)
        {
            string outUrl = "";
            string querystring = "";
            string ret = "";


            //Setup postData for signing.
            //Add the postData to the querystring.
            if (method == Method.POST)
            {
                if (postData.Length > 0)
                {
                    //Decode the parameters and re-encode using the oAuth UrlEncode method.
                    NameValueCollection qs = HttpUtility.ParseQueryString(postData);
                    postData = "";
                    foreach (string key in qs.AllKeys)
                    {
                        if (postData.Length > 0)
                        {
                            postData += "&";
                        }
                        qs[key] = HttpUtility.UrlDecode(qs[key]);
                        qs[key] = this.UrlEncode(qs[key]);
                        postData += key + "=" + qs[key];

                    }
                    if (url.IndexOf("?") > 0)
                    {
                        url += "&";
                    }
                    else
                    {
                        url += "?";
                    }
                    url += postData;
                }
            }

            Uri uri = new Uri(url);

            string nonce = this.GenerateNonce();

            string timeStamp = this.GenerateTimeStamp(IsTimeStamp);

            //Generate Signature
            string sig = this.GenerateSignature(uri,
                this.ConsumerKey,
                this.ConsumerSecret,
                this.Token,
                this.TokenSecret,
                method.ToString(),
                timeStamp,
                nonce,
                out outUrl,
                out querystring);


            querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);

            //Convert the querystring to postData
            if (method == Method.POST)
            {
                postData = querystring;
                querystring = "";
            }

            if (querystring.Length > 0)
            {
                outUrl += "?";
            }

            if (method == Method.POST || method == Method.GET)
                ret = WebRequest(method, outUrl + querystring, postData);
            //else if (method == Method.PUT)
            //ret = WebRequestWithPut(outUrl + querystring, postData);
            return ret;
        }


        /// <summary>
        /// WebRequestWithPut
        /// </summary>
        /// <param name="method">WebRequestWithPut</param>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string APIWebRequest(string method, string url, string postData)
        {
            Uri uri = new Uri(url);
            string nonce = this.GenerateNonce();
            string timeStamp = this.GenerateTimeStamp(IsTimeStamp);

            string outUrl, querystring;

            //Generate Signature
            string sig = this.GenerateSignature(uri,
                this.ConsumerKey,
                this.ConsumerSecret,
                this.Token,
                this.TokenSecret,
                method,
                timeStamp,
                nonce,
                out outUrl,
                out querystring);

            //querystring += "&oauth_signature=" + HttpUtility.UrlEncode(sig);
            //NameValueCollection qs = HttpUtility.ParseQueryString(querystring);

            //string finalGetUrl = outUrl + "?" + querystring;

            HttpWebRequest webRequest = null;

            //webRequest = System.Net.WebRequest.Create(finalGetUrl) as HttpWebRequest;
            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            //webRequest.ContentType = "text/xml";
            webRequest.Method = method;
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            webRequest.AllowWriteStreamBuffering = true;

            webRequest.PreAuthenticate = true;
            webRequest.ServicePoint.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            webRequest.Headers.Add("Authorization", "OAuth realm=\"http://api.linkedin.com/\",oauth_consumer_key=\"" + this.ConsumerKey + "\",oauth_token=\"" + this.Token + "\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"" + HttpUtility.UrlEncode(sig) + "\",oauth_timestamp=\"" + timeStamp + "\",oauth_nonce=\"" + nonce + "\",oauth_verifier=\"" + this.Verifier + "\", oauth_version=\"1.0\"");
            //webRequest.Headers.Add("Authorization", "OAuth oauth_nonce=\"" + nonce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timeStamp + "\", oauth_consumer_key=\"" + this.ConsumerKey + "\", oauth_token=\"" + this.Token + "\", oauth_signature=\"" + HttpUtility.UrlEncode(sig) + "\", oauth_version=\"1.0\"");
            if (postData != null)
            {
                byte[] fileToSend = Encoding.UTF8.GetBytes(postData);
                webRequest.ContentLength = fileToSend.Length;

                Stream reqStream = webRequest.GetRequestStream();

                reqStream.Write(fileToSend, 0, fileToSend.Length);
                reqStream.Close();
            }

            string returned = WebResponseGet(webRequest);

            return returned;
        }


        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Method method, string url, string postData)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            //webRequest.UserAgent  = "Identify your application please.";
            //webRequest.Timeout = 20000;

            if (method == Method.POST)
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";

                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    requestWriter.Close();
                    requestWriter = null;
                }
            }

            responseData = WebResponseGet(webRequest);

            webRequest = null;

            return responseData;

        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (WebException exc)
            {
                if (!IsTimeStamp)
                {
                    return null;
                }
                ////WebException exc
                //exc.ToString();
                //HttpContext.Current.Response.Write(exc.Response.Headers.ToString());
                //using (WebResponse response = exc.Response)
                //{
                //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                //    using (Stream data = response.GetResponseStream())
                //    {
                //        string text = new StreamReader(data).ReadToEnd();
                //        responseData = text;
                //        HttpContext.Current.Response.Write(GenerateTimeStamp() + "<br/><br/>" + text + "<br/><br/>");
                //    }
                //}

                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            //finally
            //{
            //    webRequest.GetResponse().GetResponseStream().Close();
            //    responseReader.Close();
            //    responseReader = null;
            //}

            return responseData;
        }
    }
}