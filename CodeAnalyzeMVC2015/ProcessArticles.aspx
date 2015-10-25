<%@ Page Language="C#" MasterPageFile="~/MasterPageNoGoog.master" AutoEventWireup="true" Inherits="ProcessArticles" Codebehind="ProcessArticles.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


        <asp:Label ID="lblUserRegMsg" runat="server" Visible="false" Font-Bold="true" Font-Names="Calibri" Font-Size="16px" ForeColor="Red" />
        <table style="height: 250px; width: 700px;">
            <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    <b>Title:</b>
                </td>
                <td align="left" >
                    <asp:TextBox ID="txtTitle" runat="server" BorderStyle="Groove" Width="480px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    Details:</td>
                <td align="left" >
                    <asp:TextBox ID="txtDetails" runat="server" BorderStyle="Groove" Width="480px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    <b>User E-Mail:</b>
                </td>
                <td align="left" >
                     <asp:DropDownList ID="ddUserEmail" runat="server" DataTextField="EMail" Font-Size="14px"
                        Font-Names="Calibri" AppendDataBoundItems="true" DataValueField="UserId">
                        <asp:ListItem Selected="True" Text="---Select---" Value="---Select---" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    <b>Question Type:</b>
                </td>
                <td align="left" >
                     <asp:DropDownList ID="ddType" runat="server" DataTextField="QuestionType" Font-Size="14px"
                                Font-Names="Calibri" AppendDataBoundItems="true" DataValueField="QuestionTypeId">
                                <asp:ListItem Selected="True" Text="---Select---" Value="---Select---" />
                            </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    <b>Upload Html File: </b>
                </td>
                <td align="left">
                    <asp:FileUpload ID="fileUploadWordFile" runat="server" BorderStyle="Groove" 
                        Width="223px" />
                </td>
            </tr>
            <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    <b>Upload Source File: </b>
                </td>
                <td align="left">
                    <asp:FileUpload ID="fileUploadSourceFile" runat="server" BorderStyle="Groove" 
                        Width="223px" />
                </td>
            </tr>
             <tr>
                <td align="left"  style="font-family: Calibri; width: 123px;">
                    <b>You Tube URL: </b>
                </td>
                <td align="left">
                     <asp:TextBox ID="txtYoutTube" runat="server" BorderStyle="Groove" Width="480px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="padding-left: 160px" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Font-Bold="True" Font-Size="16px" Font-Names="Calibri" OnClick="btnSubmit_Click"
                        Text="Submit" ValidationGroup="1" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Font-Bold="True" Font-Size="16px" Font-Names="Calibri"  OnClick="btnCancel_Click"
                        Text="Cancel" />
                </td>
            </tr>
        </table>


       <asp:HiddenField runat="server" ID="hfName" />
       <asp:HiddenField runat="server" ID="hfUserEMail" />

</asp:Content>
