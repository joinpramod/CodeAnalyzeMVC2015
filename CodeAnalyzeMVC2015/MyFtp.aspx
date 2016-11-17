<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFtp.aspx.cs" Inherits="MyFtp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <asp:ScriptManager ID="scm" runat="server" EnablePageMethods="true"
        EnablePartialRendering="true" ScriptMode="Release">
    </asp:ScriptManager>
    <div>
    <style type="text/css">
        .setBoarder
        {
            border: 1px solid #CCCCCC;
            border-spacing: 5px;
        }

        .FormLeftSpace
        {
            width: 19%;
            float: left;
            clear: none;
            min-width: 19%;
        }

        .FormLeftColumn
        {
            width: 49%;
            float: left;
            clear: none;
        }
    </style>



    <div id="wrapper">
        <div id="main" class='wide'>
            <div id="content">
                <div>

                    <asp:MultiView runat="server" ID="mvFtp" ActiveViewIndex="0">
                        <asp:View runat="server" ID="vLogin">
                              <div  id="account_signin" style="width: 496px; margin: 0 auto 40px auto;">
                                <div id="sign_in_username_password" style="padding-top: 20px;">
                                    <table style="width: 100%;" align="center" border="0" cellpadding="3" cellspacing="0">
                                         <tr>
                                            <td style="width: 110px; text-align: right;">
                                                Server Name:
                                            </td>
                                            <td style="width: 262px">
                                                <asp:TextBox ID="txtServerName" runat="server" Height="25px" Width="263px"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvServerName" runat="server" ErrorMessage="Server Name is required!<br />"
                                                    ControlToValidate="txtServerName" Display="dynamic" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; text-align: right;">
                                                User Name:
                                            </td>
                                            <td style="width: 262px">
                                                <asp:TextBox ID="txtUserName" runat="server" Height="25px" Width="263px"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="User Name is required!"
                                                    ControlToValidate="txtUserName" Display="dynamic" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; text-align: right; padding-top: 10px;">
                                                Password:
                                            </td>
                                            <td style="width: 262px; text-align: left; padding-top: 10px;">
                                                <asp:TextBox ID="txtPassWord" runat="server" Font-Size="Smaller" Height="25px" TextMode="Password"
                                                    Width="263px"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is required!"
                                                    ControlToValidate="txtPassWord" Display="dynamic" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; height: 20px;" colspan="2" align="center">
                                                <div class="buttonHolder" style="text-align: center;">
                                                    <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click" CausesValidation="true"
                                                        Text="Login"  Width="110" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="vFtpMain">
                            <div id="dvFtp" style="margin: 0; padding: 0">
                                <div style="padding: 10px;" class="setBoarder">
                                    <div class="setBoarder" style="background-color: #F0FFFF">
                                        <b>Current Directory: </b>
                                        <asp:Label runat="server" ID="lblDirectory"></asp:Label>
                                    </div>
                                    <br />
                                    <div>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%; vertical-align: top;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="setBoarder" style="background-color: #F0FFFF">
                                                                <b>Folders: </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel runat="server" ID="pnlFolder" ScrollBars="Vertical" Height="300px">
                                                                    <br />
                                                                    <asp:GridView ID="gvFolder" CssClass="setBoarder" runat="server" AutoGenerateColumns="False"
                                                                        EnableModelValidation="True" ShowHeader="False" Width="100%" OnRowDataBound="gvFolder_RowDataBound"
                                                                        OnRowCommand="gvFolder_RowCommand" OnRowEditing="gvFolder_RowEditing" OnRowDeleting="gvFolder_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="imgFolder" runat="server" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkFolderName" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                        Text="Edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                                                                        ImageUrl="~/images/cross.png" ImageAlign="Middle" Text="Delete" OnClientClick="return confirm('Are you sure to delete?')" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="setBoarder" style="background-color: #F0FFFF">
                                                                <b>Files: </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel runat="server" ID="pnlFile" ScrollBars="Vertical" Height="300px">
                                                                    <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" CssClass="setBoarder"
                                                                        EnableModelValidation="True" ShowHeader="False" Width="100%" OnRowEditing="gvFile_RowEditing"
                                                                        OnRowDataBound="gvFile_RowDataBound" OnRowCommand="gvFile_RowCommand" OnRowDeleting="gvFile_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="imgFile" runat="server" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkFileName" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                        Text="Edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="LastModifiedDate">
                                                                                <ItemStyle Width="20%" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Size">
                                                                                <ItemStyle Width="15%" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgFileDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                                                                        ImageUrl="~/images/cross.png" ImageAlign="Middle" Text="Delete" OnClientClick="return confirm('Are you sure to delete?')" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="2%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="setBoarder" style="background-color: #F0FFFF">
                                        <b>Actions: </b>
                                        <br />
                                        <div>
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                            &nbsp;
                                            <asp:FileUpload ID="fuFtp" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnCreateDirectory" runat="server" Text="Create Directory" OnClick="btnCreateDirectory_Click"
                                                ValidationGroup="Dir" />
                                            <asp:TextBox ID="txtDirectory" runat="server" ValidationGroup="Dir"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDirectory" runat="server" ErrorMessage="Required"
                                                Display="Dynamic" ControlToValidate="txtDirectory" SetFocusOnError="true" ValidationGroup="Dir"></asp:RequiredFieldValidator>
                                            <%--<asp:Button ID="btnDeleteDir" runat="server" Text="Delete"  OnClientClick="return confirm('Are you sure to delete?')"  />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
