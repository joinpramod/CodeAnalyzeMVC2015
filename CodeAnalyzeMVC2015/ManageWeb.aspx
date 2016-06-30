<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageWeb.aspx.cs" Inherits="CodeAnalyzeMVC2015.ManageWeb" %>

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
    
        <table style="width:100%;" cellpadding="4" cellspacing="4">
            <tr>
                <td style="font-size: large; font-weight: 700;">Admin</td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Manage.aspx">Manage DB</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="ManageContent.aspx">Manage Content</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="ProcessArticles.aspx">Process Articles</asp:HyperLink>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
