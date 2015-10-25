<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ForgotPassword" Codebehind="ForgotPassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <table style="width: 700px; height: 120px;padding-top:40px;">
        <tr><td align="center" colspan="3"><asp:Label ID="lblForgotPasswordMsg" runat="server" Visible="false" Font-Bold="true" Font-Names="Calibri" Font-Size="16px" ForeColor="Red" /></td></tr>
              
        <tr valign="top">
            <td align="right" style="font-family: Calibri;">
                Enter EMail Id</td>
            <td style="width: 353px">
                <asp:TextBox ID="txtEMail" runat="server" Width="330px" BorderStyle="Groove"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RequiredFieldValidator3" runat="server" 
                           ControlToValidate="txtEMail" ErrorMessage="EMail Required" 
                           ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                           ValidationGroup="1" Font-Names="Calibri"></asp:RegularExpressionValidator>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="1" ControlToValidate="txtEMail" runat="server" ErrorMessage="*" />
                
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" ValidationGroup="1" 
                     BackColor="#4fa4d5" BorderStyle="None"  ForeColor="White" Text="Submit" />
            </td>
        </tr>
    </table>



</asp:Content>

