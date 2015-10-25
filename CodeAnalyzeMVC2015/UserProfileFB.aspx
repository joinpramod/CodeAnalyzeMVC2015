<%@ Page Language="C#" MasterPageFile="~/MasterPageFB.master" AutoEventWireup="true" Inherits="UserProfileFB" Codebehind="UserProfileFB.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script>

        //UserProfileFB
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
                scope: 'email,id,name,gender,user_birthday',
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                version: 'v2.2',
                xfbml: true  // parse XFBML
            });
            // listen for and handle auth.statusChange events
            FB.Event.subscribe('auth.statusChange', function (response) {
            //  debugger;
                if (response.authResponse) {
              //  debugger;
                    // user has auth'd your app and is logged into Facebook
                    var uid = "http://graph.facebook.com/" + response.authResponse.userID + "/picture";

                    FB.api('/me', function (me) {
                        if (me.name) {
                      // debugger;
                            var fbEMail = me.email;
                            var fbName = me.name;
                            var url = "http://codeanalyze.com/UserProfile.aspx?hgytytyhgfh=sadfsfdsfweADA345EF4Zsdfasdasdsdasde34&facebook=true&fbemail=" + fbEMail + "&fbname=" + fbName + "";
                            window.location = url;
                        }
                    })
                }
            });
        }
          
    </script>



    <table width="70%" style="height:300px">
        <tr><td style="height:20px">&nbsp;</td></tr>
        <tr align="top" style="width:100%; padding-top:10px" valign="top">
            <td align="left" style="width:100%;padding-left:200px;vertical-align:top;font-family:Calibri;font-size:18px;font-weight:bold">
            <b>
            Click
                <%--<div class="fb-login-button" autologoutlink="false" scope="email,user_checkins">
                    Login with Facebook
                </div>--%>

                <fb:login-button size="large" scope="public_profile,email"
                                 onlogin="require('./log').info('onlogin callback')">
                  Login with Facebook
                </fb:login-button>

            to login with Facebook
            </b>
            </td>
            </tr> 
            <%--<td align="center"><span id="span2" style="font-family:Calibri;font-size:18px;font-weight:bold">OR</span></td>--%>
            <%--<td align="center" style="width:100%;padding-left:20px">
                     <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" Enabled="false" 
                    ImageUrl="~/Images/GoogleGreyScale.png" 
                    ToolTip="Sign In With Google Account Disable" Width="180px" Visible="False" />
            </td>--%>
           <%-- <td align="center"><span id="span1" style="font-family:Calibri;font-size:18px;font-weight:bold">OR</span></td>--%>
        <%--    <td align="center" style="width:100%;padding-left:20px">
                  <asp:ImageButton ID="ImageButton1" runat="server" Height="25px"
                    ImageUrl="~/Images/LinkedInGreyScale.png" Enabled="false" 
                    ToolTip="Sign In With Linked Account Disabled" Width="180px" 
                      Visible="False" />
            </tr>--%>
            <%--<tr> <td align="center" colspan="5"><span id="span3" style="font-family:Calibri;font-size:18px;font-weight:bold">OR</span></td> </tr>--%>

    </table>
       
  
       

 
    

</asp:Content>
