<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageContent.aspx.cs" Inherits="CodeAnalyzeMVC2015.ManageContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 100%;border:solid;border-width:1px;border-color:black">
            <tr>
                <td colspan="2"><strong>Manage</strong></td>
            </tr>
            <tr>
                <td>Server:</td>
                <td><asp:TextBox ID="txtServer" runat="server" Width="191px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Username:&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="txtUsername" runat="server" Width="162px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Width="163px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Path:</td>
                <td>
                    <asp:TextBox ID="txtPath" runat="server" Width="466px"></asp:TextBox>
&nbsp;<asp:Button ID="btnShowFiles" runat="server" OnClick="btnShowFiles_Click" Text="Show Files" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>Download</strong></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="false">
<Columns>
    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="100" />
    <asp:BoundField DataField="Size" HeaderText="Size (KB)" DataFormatString="{0:N2}"
        ItemStyle-Width="100" />
    <asp:BoundField DataField="Date" HeaderText="Created Date" ItemStyle-Width="100" />
    <asp:TemplateField>
        <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
            CommandArgument='<%# Eval("Name") %>'></asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>
</asp:GridView></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="2"><strong>Upload</strong></td>
            </tr>
            <tr>
                <td>Select File:</td>
                <td>&nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                &nbsp;<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
