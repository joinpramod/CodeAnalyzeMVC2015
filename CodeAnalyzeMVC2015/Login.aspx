<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoGoog.master" AutoEventWireup="true" Inherits="Login" Codebehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">

    function ValidateLogin() {

        var varUserId = '<%=txtEMailId.ClientID %>';
        var VarPwd = '<%=txtPassword.ClientID %>';


        var userName = document.getElementById(varUserId).value;
        var pwd = document.getElementById(VarPwd).value;


        if (userName != "" && pwd != "") {
            return true;
        }
        else {
            alert("Please enter username and password");
            return false;
        }
    }

</script>


<asp:Panel ID="pnlLogin" runat="server" Width="100%">

                    <table width="100%"><tr><td align="center" style="border:1px solid black;">

                                    <table style="font-size:16px" width="80%">
                                         <tr><td>&nbsp;</td></tr>
                                        <tr>
                                            
                                            <td style="padding-left:4%;" align="center">EMail:
                                            <asp:TextBox ID="txtEMailId" BorderStyle="Groove" runat="server" Text="" Width="34%"></asp:TextBox>
                                  
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td  align="center">Password:
                                            <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Groove" TextMode="Password" Width="25%"></asp:TextBox>
                                 
                                                    <asp:Button ID="btnLogin" BackColor="#4fa4d5" ForeColor="White" runat="server" Font-Names="Calibri"
                                                     Text="Login" BorderStyle="None"  OnClientClick="return ValidateLogin()" ValidationGroup="login" OnClick="btnLogin_Click"/>

                                            </td>
                                        </tr>
                                        <tr>
                                          
                                            <td  style="padding-left:56%;">
                                                
                                                <asp:LinkButton ID="lnkForgotPwd" runat="server" Font-Names="Calibri" OnClick="lnkForgotPwd_Click"
                                                    Font-Size="14px">Forgot Password</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr>
                                   <tr><td align="center"><table width="100%">
                                        <tr>
                                        <td align="center">
                                                  <asp:ImageButton ID="ImageButton2" runat="server" Height="22px"
                                                    ImageUrl="~/Images/SignInWithFacebook.png" PostBackUrl="~/UserProfileFB.aspx" 
                                                    ToolTip="Sign In With Facebook" Width="150px" />
                                            </td>
                                       <%-- </tr>
                                        <tr>--%>
                                        <td align="center">
                                                  <asp:ImageButton ID="ImageButton3" runat="server" Height="22px"
                                                    ImageUrl="~/Images/SignInWithGoogle.png" PostBackUrl="~/UserProfileGoogle.aspx"
                                                    ToolTip="Sign In With Google" Width="150px" />
                                              
                                            </td>

                                        <%--</tr>
                                        <tr>--%>
                                          <td align="center">
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="22px" Visible="false"
                                                    ImageUrl="~/Images/SignInWithLinkedIn.jpg" PostBackUrl="~/Linkedin.aspx"
                                                    ToolTip="Sign In With Linked Account" Width="150px" />
                                            </td>
                                        </tr>
                                        </table></td></tr>
                                         <tr><td>&nbsp;</td></tr> <tr><td>&nbsp;</td></tr>
                                    </table>


                                    </td></tr></table>

                                </asp:Panel>

</asp:Content>

