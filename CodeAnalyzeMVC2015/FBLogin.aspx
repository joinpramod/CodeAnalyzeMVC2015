<%@ Page Language="C#" AutoEventWireup="true" Inherits="FBLogin" Codebehind="FBLogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

         <asp:ScriptManager ID="ScriptManagerMain"
            runat="server"
            EnablePageMethods="true" 
            ScriptMode="Release" 
            LoadScriptsBeforeUI="true" />

        <script type="text/javascript">
            function GetValue() {
                debugger;
                document.getElementById('hfName').innerText = "asd1";
                document.getElementById('hfEmail').innerText = "asd2";
                PageMethods.FacebookLogin();
            }
        </script>


        <script>
            // Load the SDK Asynchronously
            (function (d) {
                var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
                if (d.getElementById(id)) { return; }
                js = d.createElement('script'); js.id = id; js.async = true;
                js.src = "//connect.facebook.net/en_US/all.js";
                ref.parentNode.insertBefore(js, ref);
            } (document));

            // Init the SDK upon load
            window.fbAsyncInit = function () {
                FB.init({
                    appId: '848792638492356', // Write your own application id
                    channelUrl: '//' + window.location.hostname + '/channel', // Path to your Channel File
                    scope: 'id,name,gender,user_birthday,email',
                    status: true, // check login status
                    cookie: true, // enable cookies to allow the server to access the session
                    version: 'v2.2',
                    xfbml: true  // parse XFBML
                });
                // listen for and handle auth.statusChange events
                FB.Event.subscribe('auth.statusChange', function (response) {
                    debugger;
                    if (response.authResponse) {
                        // user has auth'd your app and is logged into Facebook
                        var uid = "http://graph.facebook.com/" + response.authResponse.userID + "/picture";
                        FB.api('/me', function (me) {
                            if (me.name) {

                                // document.getElementById('auth-displayname').innerHTML = me.name;
                                // document.getElementById('Email').innerHTML = me.email;

                                document.getElementById('hfName').innerText = me.name;
                                document.getElementById('hfEmail').innerText = me.email;

                            }
                        })
                        document.getElementById('auth-loggedout').style.display = 'none';
                        //document.getElementById('auth-loggedin').style.display = 'block';


                    } else {
                        // user has not auth'd your app, or is not logged into Facebook
                        document.getElementById('auth-loggedout').style.display = 'block';
                        //document.getElementById('auth-loggedin').style.display = 'none';
                    }
                });

                $("#auth-logoutlink").click(function () { FB.logout(function () { window.location.reload(); }); });
            }
        </script>

        <script type="text/javascript">
            function OpenGoogleLoginPopup() {
                var url = "https://accounts.google.com/o/oauth2/auth?";
                url += "scope=https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email&";
                url += "state=%2Fprofile&"
                url += "redirect_uri=<%=Return_url %>&"
                url += "response_type=token&"
                url += "client_id=<%=Client_ID %>";
                window.location = url;
            }
        </script>




        <!-- Place this tag in your head or just before your close body tag. -->
        <script src="https://apis.google.com/js/platform.js" async defer></script>

        <div style="height: 500px; margin-left: 23px;">
       
            <div id="auth-status">
                <div id="auth-loggedout">                   
                    
                    <div class="fb-login-button" autologoutlink="false" scope="email,user_checkins" style="width:150px;height:40px">
                        Login with Facebook</div>                     

                </div>

                <a href="#" id="A1" onclick="OpenGoogleLoginPopup();" name="butrequest1"> <image src="SignInWithGoogle.png" width="135px" height="25px"></image></a>

                <%--<a href="#" id="A2" onclick="OpenGoogleLoginPopup();" name="butrequest2"><image src="googlelogo.jpg" width="34px" height="34px"></image></a>
                <a href="#" id="A3" onclick="OpenGoogleLoginPopup();" name="butrequest3"><image src="facebooklogo.png" width="35px" height="35px"></image></a>
                <a href="#" id="A4" onclick="OpenGoogleLoginPopup();" name="butrequest4"><image src="linkedinlogo.jpg" width="35px" height="35px"></image></a>--%>

                <div class="g-follow" data-annotation="bubble" data-height="20" data-href="//plus.google.com/u/0/110573606115895321599" data-rel="publisher"></div>


                <%--<div id="auth-loggedin" style="display: none">
                    Hi, <span id="auth-displayname"></span>(<a href="#" id="auth-logoutlink">logout</a>)
                    <br />
                    Email: <span id="Email"></span><br/> Ammar's Birthday <span id="BD"></span><br/>
                    Gender :<span id="Gender"></span><br/>
                    <br />
                    Profile Image:
                    <img id="profileImg" />
                </div>--%>


                <asp:HiddenField runat="server" ID="hfName" />
                <asp:HiddenField runat="server" ID="hfEmail" />
                <asp:Button ID="jc" Text="jc" runat="server" OnClientClick="GetValue()" />

            </div>
    </div>


    <div>
    
    </div>
    </form>
</body>
</html>
