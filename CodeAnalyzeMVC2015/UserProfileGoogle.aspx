<%@ Page Language="C#" MasterPageFile="~/MasterPageFB.master" AutoEventWireup="true" Inherits="UserProfile" Codebehind="UserProfileGoogle.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


        <script>
            function GetHashValue() {
                var hashVal = window.location.hash.substring(1);
                if (hashVal) {
                    var url = "http://codeanalyze.com/UserProfile.aspx?hgytytyhgfh=sadfsfdsfweADA345EF4Zsdfasdasdsdasde34&google=true&" + hashVal + "";
                    window.location = url;
                }
            }
        </script>

     
    <script type="text/javascript">
        //UserProfileGoogle
        function OpenGoogleLoginPopup() {
            var url = "https://accounts.google.com/o/oauth2/auth?";
            url += "scope=email%20profile&";
            url += "redirect_uri=http://codeanalyze.com/UserProfileGoogle.aspx&"
            url += "response_type=token&"
            url += "client_id=882773719808-khnohb473csa52jmlc477mgjn9rnrpb6.apps.googleusercontent.com";
            window.location = url;
        }
    </script>

   

    <table width="600px" style="font-size:16px">

        <tr valign="top" align="center">
        <td style="width:200px"></td>
            <td align="center">
                    <b> Click</b> 
                    </td>
                    <td style="width:200px; height:5px"><a id="A1" onclick="OpenGoogleLoginPopup();" > <img src="Google.png" height="25px" ></image> </a>
            </td>
          <td>
           <b> to sign in with Google</b> 
          </td>
            </tr> 
    </table>
       
       <br />
        <br />
    <br />
       <br />
       

    


</asp:Content>
