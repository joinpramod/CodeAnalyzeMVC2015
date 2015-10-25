<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Error" Codebehind="Error.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width:100%; height: 100px;">
    <tr>
        <td align="center" style="font-weight: bold; font-family: Calibri">
            <asp:Label ID="Label3" runat="server" 
                Text="Ooops!! There seem to be an error. Request you to try again."></asp:Label>
        </td>
    </tr>
</table>

</asp:Content>

