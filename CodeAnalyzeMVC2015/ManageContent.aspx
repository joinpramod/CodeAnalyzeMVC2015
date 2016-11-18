<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageContent.aspx.cs" Inherits="CodeAnalyzeMVC2015.ManageContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Code Analyze - Blogger Rewards</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/favicon.ico" type="image/ico" />

    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="Language" content="en-us" />
    <meta name="robots" content="noindex" />

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
                <td><asp:TextBox ID="txtServer" runat="server" Text="ftp://web-16.znetlive.in" Width="191px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Username:&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="txtUsername" runat="server" Text="ftpadmin" Width="162px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Text="" Width="163px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Path:</td>
                <td>
                    <asp:TextBox ID="txtPath" runat="server" Width="466px" Text="/ftpadmin/codeanalyze.com/wwwroot/"></asp:TextBox>
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
