<%@ Page Language="C#" MasterPageFile="~/MasterPageNoGoog.master" AutoEventWireup="true" Inherits="UserProfile" Codebehind="UserProfile.aspx.cs" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:Panel ID="pnlMyQuesAns" runat="server" Width="100%">


    <table border="0" cellpadding="0" cellspacing="0" style="width: 70%;font-size:16px">
        <tr align="center" style="width: 100%">
            <td align="right" valign="middle" style="width: 33%">
                <asp:LinkButton ID="lnlMyQuestions" Font-Names="Calibri" runat="server" Font-Bold="True"
                    PostBackUrl="~/QuestionAnswers.aspx?Type=Questions" Font-Underline="true">My 
                        Questions</asp:LinkButton>
            </td>
            <td align="center" valign="middle" style="width: 33%">
                &nbsp;&nbsp;<asp:LinkButton ID="lnkMyAnswers" Font-Names="Calibri" runat="server"
                    Font-Bold="True" PostBackUrl="~/QuestionAnswers.aspx?Type=Answers" Font-Underline="true">My 
                            Answers</asp:LinkButton>
            </td>
             <td align="left" valign="middle" style="width: 33%">
                <asp:LinkButton ID="lnkArticles" Font-Names="Calibri" runat="server"
                    Font-Bold="True" PostBackUrl="~/QuestionAnswers.aspx?Type=Articles" Font-Underline="true">My 
                            Articles</asp:LinkButton>
            </td>
        </tr>
    </table>


</asp:Panel>
   
    <asp:Panel ID="pnlRegister" runat="server" Width="100%">
    <%--<div style="padding-left:60px">
            <asp:Panel runat="server" Visible="false" ID="fbggli" >
        <table width="80%" style="font-size:16px">
        <tr><td align="right" style="width:180px">
                 <a href="UserProfileFB.aspx"><img src="Images/SignInWithFacebook.png" height="25px" /> </a>&nbsp;
        </td>
        <td align="center" style="width:180px">
                 <a href="UserProfileGoogle.aspx"><img src="Images/SignInWithGoogle.png" height="25px" /> </a>&nbsp;
        </td>
        <td  align="left" style="width:180px">
                <a href="Linkedin.aspx"><img src="Images/SignInWithLinkedIn.jpg"  height="25px" /> </a>&nbsp;
        </td>
        </tr>
        </table>
        </asp:Panel>
        </div>--%>
        <asp:Panel ID="pnlSocial" runat="server">
                                    <table width="100%">
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
                                    </table>
                                    </asp:Panel> 
                                    <br />
        <table style="font-size:16px" width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="lblUserRegMsg" runat="server" Font-Bold="true" 
                        Font-Names="Calibri" Font-Size="16px" ForeColor="Red" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 5px;border:1px solid black;">
                    <table style="height: 250px; width: 80%;">
                    <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <b>First Name:</b>
                            </td>
                            <td align="left" style="padding-left:10px">
                                <asp:TextBox ID="txFirsttName" runat="server" BorderStyle="Groove" 
                                    Width="55%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txFirsttName" ErrorMessage="*" Font-Names="Calibri" 
                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <b>Last Name</b>
                            </td>
                            <td align="left" style="padding-left:10px">
                                <asp:TextBox ID="txtLastName" runat="server" BorderStyle="Groove" Width="55%"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtLastName" ErrorMessage="Last Name Required" 
                        ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <b>E-Mail:</b>
                            </td>
                            <td align="left" style="padding-left:10px">
                                <asp:TextBox ID="txtEMail" runat="server" BorderStyle="Groove" Width="75%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="txtEMail" ErrorMessage="*" ValidationGroup="1" />
                                <asp:RegularExpressionValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtEMail" ErrorMessage="*" Font-Names="Calibri" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    ValidationGroup="1"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <strong>Address</strong>:
                            </td>
                            <td align="left" style="padding-left:10px">
                                <asp:TextBox ID="txtAddress" runat="server" BorderStyle="Groove" 
                                    Font-Names="Calibri" Font-Size="14px" Height="67px" TextMode="MultiLine" 
                                    Width="75%"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                               ControlToValidate="txtAddress" ErrorMessage="Address Required" 
                               Font-Names="Calibri" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>

                        <asp:Panel runat="server" ID="pnlPassword">
                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <strong>Password</strong>:
                            </td>
                            <td align="left" style="padding-left:10px">
                                 <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Groove" 
                                                    TextMode="Password" Width="55%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                    ControlToValidate="txtPassword" ErrorMessage="*" Font-Names="Calibri" 
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                  <%--<AjaxToolKit:PasswordStrength TargetControlID="txtPassword" 
                                                MinimumNumericCharacters="1" MinimumUpperCaseCharacters="1"  StrengthIndicatorType="BarIndicator"
                                                PreferredPasswordLength="8"  ID="PasswordStrength1" runat="server">
                                                </AjaxToolKit:PasswordStrength>    --%>               
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <strong>Confirm Password</strong>:
                            </td>
                            <td align="left" style="padding-left:10px">
                                  <asp:TextBox ID="txtConfirmPassword" runat="server" BorderStyle="Groove" 
                                                    TextMode="Password" Width="55%"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                    ControlToValidate="txtConfirmPassword" ErrorMessage="*" Font-Names="Calibri" 
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <br />
                                  <asp:CompareValidator id="comparePasswords" 
                                                      runat="server"  ValidationGroup="1"
                                                      ControlToCompare="txtPassword"
                                                      ControlToValidate="txtConfirmPassword"
                                                      ErrorMessage="Passwords mismatch"
                                                      Display="Dynamic" />                
                            </td>
                        </tr>
                        </asp:Panel>


                        <%--<tr>
                            <td align="left" colspan="2">
                                
                                <asp:Panel ID="pnlChangePassword" runat="server">
                                    <asp:LinkButton ID="lnkChangePassword" runat="server" OnClick="changePassword" Font-Underline="true"
                                         ForeColor="Blue" Text="Change Password"></asp:LinkButton>
                                </asp:Panel>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" style="font-family: Calibri;">
                                <b>Upload Photo:</b>
                            </td>
                            <td align="left" style="padding-left:10px">
                                <asp:FileUpload ID="FileUpload1" runat="server" BorderStyle="Groove" 
                                    Width="70%" />  
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                         runat="server" ControlToValidate="FileUpload1" 
                                         ErrorMessage="Image files only" ForeColor="Red"                                         
                                         ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$"
                                         ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    <%--<asp:RequiredFieldValidator ID="rfeFileUpload" ValidationGroup="1"  ErrorMessage="Photo Required" runat="server" ControlToValidate="FileUpload1" />--%>
                       
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" style="font-family: Calibri;">
                                Brief details on your experience, work and expertiece
                                <br />
                                <asp:TextBox ID="txtDetails" runat="server" BorderStyle="Groove" 
                                    Font-Names="Calibri" Height="80px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td colspan="2" align="center">
                        <%--<Capcha:CaptchaControl ID="CaptchaControl1" runat="server" Visible="false"
                        EnableViewState="False" />--%>
                        
                          <asp:Image ID="imgcaptcha" AlternateText="Captcha" runat="server" />

                        </td></tr>
                        <tr><td colspan="2" align="center">
                        
                        <asp:TextBox ID="txtCapcha" runat="server" BorderStyle="Groove" 
                                                     Width="25%"></asp:TextBox>
                        </td></tr>

                        <tr><td colspan="2">&nbsp;</td></tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" BackColor="#4fa4d5" 
                                    BorderStyle="None" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" 
                                    ForeColor="White" OnClick="btnSubmit_Click" Text="Submit" 
                                    ValidationGroup="1" />
                            </td>
                            <%--<td align="center">
                                <asp:Button ID="btnCancel" runat="server" BackColor="#4fa4d5" 
                                    BorderStyle="None" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" 
                                    ForeColor="White"  OnClick="btnCancel_Click" Text="Cancel" 
                                    />
                            </td>--%>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>



    
    <br />
    <asp:Panel ID="pnlProfile" runat="server" Width="100%">
        <%--<table style="width: 78%; font-size:16px">
            <tr align="center">
                <td align="center">--%>
                    <table style="height: 230px; width: 100%;font-size:16px" cellpadding="4" cellspacing="4">
                        <tr>
                            <td rowspan="8" valign="top">
                                <asp:Image ID="imgProfile" runat="server" Height="140px" Width="100%" />
                            </td>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>First Name:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblFirstName" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>Last Name:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblLastName" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>EMail:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblEMail" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 25%; font-family: Calibri;" valign="top">
                                <b>Address:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblAddress" runat="server" Font-Names="Calibri" Width="350px"></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>Articles Posted:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblArticles" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>Questions Posted:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblQuestions" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>Replies Posted:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblAnswers" runat="server" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 25%; font-family: Calibri;">
                                <b>Details:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblDetails" runat="server"  Font-Names="Calibri"
                                    Width="75%" Font-Bold="False" Text="asdjakh asdgiusd oaisdh adig  aoisd uaoid aoidu haiusd hai sdoiad oais dgoaid oai dhaid oa gdoua gdoadoiu asd"></asp:Label>
                            </td>
                        </tr><tr><td>&nbsp;</td></tr>
                        <tr>
                            <td align="left" style="padding-left:20%" colspan="3" valign="top">
                                <asp:Button ID="btnEditProfile" runat="server" Font-Bold="True"  BackColor="#4fa4d5" 
                                    BorderStyle="None" ForeColor="White"
                                Font-Size="16px" Font-Names="Calibri" Height="25px" Text="Edit Profile"
                                    Width="15%" OnClick="btnEditProfile_Click" />
                                 <asp:Button ID="btnCancelChangePassword" runat="server" 
                                            OnClick="CancelChangePassword_Click" Text="Change Password"
                                        Font-Bold="True"  BackColor="#4fa4d5" 
                                    BorderStyle="None" ForeColor="White"
                                Font-Size="16px" Font-Names="Calibri" Height="25px"  />
                            </td>
                        </tr>
                    </table>
             <%--   </td>
            </tr>
        </table>--%>
    </asp:Panel>
    <asp:HiddenField ID="HFMode" runat="server" />

       <asp:HiddenField runat="server" ID="hfName" />
       <asp:HiddenField runat="server" ID="hfUserEMail" />

</asp:Content>
