<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="LinkedIn" Codebehind="LinkedIn.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <table width="700px" style="font-size:16px">
             <tr>
              <td align="center" valign="middle">
                  &nbsp;
                  </td>
             </tr>
             
             <tr>
              <td align="center" valign="middle">
                 <asp:LinkButton ID="hypAuthToken" Font-Underline="true" ForeColor="Blue" OnClick="GetToken" runat="server" Font-Names="Calibri" CausesValidation="false"></asp:LinkButton>
              </td>
             </tr>
             
             <tr>
              <td align="center" valign="middle">
                  &nbsp;</td>
             </tr>
             
             <tr>
              <td style="font-family: Calibri"  align="center" valign="bottom">
                  Enter LinkedIn Security Code :&nbsp;<asp:TextBox ID="txtoAuth_verifier" BorderStyle="Groove"
                      runat="server" Width="80px"></asp:TextBox>         
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="LI" ControlToValidate="txtoAuth_verifier" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>            
                  &nbsp;
                <asp:Button ID="btnGetAccessToken" runat="server" Text="Login" BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" 
                    onclick="btnGetAccessToken_Click" Font-Names="Calibri" ValidationGroup="LI" />
             </td>
             </tr>

            </table>
</asp:Content>

