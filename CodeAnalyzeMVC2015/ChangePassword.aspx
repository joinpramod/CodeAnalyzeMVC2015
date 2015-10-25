<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ChangePassword" Codebehind="ChangePassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%"><tr><td align="center" >

<table style="font-size:16px" width="70%">




<tr>
                <td align="center">
                    <asp:Label ID="lblUserRegMsg" runat="server" Font-Bold="true" 
                        Font-Names="Calibri" Font-Size="16px" ForeColor="Red" Visible="false" />
                </td>
            </tr>
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
                                 <%-- <AjaxToolKit:PasswordStrength TargetControlID="txtPassword" 
                                                MinimumNumericCharacters="1" MinimumUpperCaseCharacters="1"  StrengthIndicatorType="BarIndicator"
                                                PreferredPasswordLength="8"  ID="PasswordStrength1" runat="server">
                                                </AjaxToolKit:PasswordStrength>  --%>                 
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
                        <tr><td>&nbsp</td></tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" BackColor="#4fa4d5" 
                                    BorderStyle="None" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" 
                                    ForeColor="White" OnClick="btnSubmit_Click" Text="Change Password" 
                                    ValidationGroup="1" />
                            </td>
                           
                        </tr>
   </table>



   </td></tr></table>

</asp:Content>

