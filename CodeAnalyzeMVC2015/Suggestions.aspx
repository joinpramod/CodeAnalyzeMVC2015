<%@ Page Language="C#" MasterPageFile="~/MasterPageNoGoog.master" AutoEventWireup="true" Inherits="Suggestions" Codebehind="Suggestions.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
        <div id="Content">
          <table style="border-color:Black;border-width:1px;width:100%" cellpadding="0" cellspacing="0">
          <tr><td>&nbsp;</td></tr>
          <tr align="left">
          <td align="left" style="padding-left:30px">
                <table width="100%">

            <tr><td align="center"><asp:Label ID="lblSuggestion" runat="server" Visible="false" Font-Bold="true" Font-Names="Calibri" Font-Size="16px" ForeColor="Red" /></td></tr>

            <asp:Panel ID="pnlEMail" runat="server">
            <tr>
                  <td align="left" style="font-family: Calibri;width:200px">
                       <b>EMail &nbsp;</b><%--</td>
                  <td>--%><asp:TextBox ID="txtEMail" runat="server" Width="318px" BorderStyle="Groove"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                           ControlToValidate="txtEMail" ErrorMessage="EMail Required" 
                           ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>
                  </td>
            </tr>
             </asp:Panel>
             
            <tr>
                   <td style="font-family: Calibri;padding-left:40px">
                       <%--<b>Suggestion :</b><br />--%>
                            <%--<cc:HtmlEditor ID="SolutionEditor" ToggleMode="None" runat="server" Height="300px" 
                                    Width="640px" />--%>
                            <asp:TextBox runat="server" ID="EditorAskQuestion" Font-Names="Calibri" Font-Size="16px"
                                BorderStyle="Groove" TextMode="MultiLine"
                                Height="200px" Width="600px" /><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="EditorAskQuestion" ErrorMessage="*Details" 
                                ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>
                        </td>
             </tr>
              <tr>
                    <td align="left" style="padding-left: 40%">
                        &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                        OnClick="PreviewButton_Click" BackColor="#4fa4d5" BorderStyle="None"  ForeColor="White"
                            Font-Bold="True" ValidationGroup="1" />
                    </td>
               </tr>
                 <tr><td align="right" style="font-family: Calibri; font-size: medium">
                     <table style="width:100%;">
                         <tr>
                             <td align="left">
                                 CodeAnalyze
                     <br />
                     New Jersey,
                     <br />
                     USA</td>
                             <td>
                                 &nbsp;</td>
                             <td align="left" width="150px">
                                 &nbsp;</td>
                         </tr>
                     </table>
                     </td></tr>
            </table>
          
          </td>
          </tr>
          </table>  
        
            
        </div>
</asp:Content>

