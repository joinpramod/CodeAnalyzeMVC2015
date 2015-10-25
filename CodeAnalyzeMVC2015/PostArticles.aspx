<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="true"
    AutoEventWireup="true" Inherits="PostArticles" Codebehind="PostArticles.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">

    function ValidateText() {
        var VarhfUserEMail = '<%=hfUserEMail.ClientID %>';
        var VarEMail = document.getElementById(VarhfUserEMail).value;
        var Upload_File = document.getElementById('<%= fileArticleWordFile.ClientID %>');
        if (VarEMail != "") {
            if (Upload_File.value == "") {
                alert("Please select your article word file to post");
                return false;
            }
            else {
                if (Upload_File.value.indexOf("docx") < 0 && Upload_File.value.indexOf("doc") < 0 && Upload_File.value.indexOf("DOCX") < 0 && Upload_File.value.indexOf("DOC") < 0) {
                    alert('Invalid Format, please select only word doc.');
                    return false;
                }
                else {
                    return true;
                }
            }

        }
        else {
            alert("Please login to post");
            return false;
        }
    }

</script>


    <%--<table style="width: 650px; font-size:16px" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>--%>
                <table align="left" style="width: 100%; font-size:16px" cellpadding="2"
        cellspacing="2" >
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblAck" runat="server" Font-Names="Calibri" ForeColor="#FF3300"  Text=""
                                Visible="False" Width="100%"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <b style="font-family: Calibri;" />Post Article
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" align="center" style="font-family: Calibri;">
                            Download article word template from 
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="true" ForeColor="Blue"
                                NavigateUrl="~/ArticleTemplate.docx">here</asp:HyperLink>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" align="center" style="font-family: Calibri;">
                            You can either submit the article below or email attachment to
                            <a href="mailto:articles@codeanalyze.com" 
                                style="color: blue; text-decoration: underline; text-underline: single;">
                            articles@codeanalyze.com</a></td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" align="left" style="font-family: Calibri;">
                            Please submit a word document for the article.
                            <br />Please attach the source code in a zip file. Do not include .exe files in the source code else it will bounce back 
                            and not be submitted.
                            <br />Please differentiate the code for understandability. 
                            <br />Size of the attachment should not be more than 3mb
                            <br />Can submit any supporting youtube video link URL, this video need not be of yours
                            <br />It will take no more than 2 days to process your article and to move it online. Status will be emailed to you.
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" align="left" style="font-family: Calibri;">
                            &nbsp;</td>
                    </tr>
                    
                      <tr>
                        <td style="font-family: Calibri;height:40px" align="center" >
                            Upload article word file: <span style="font-family:Calibri; font-size:16px;font-weight:bold;color:Red">*</span>
                        </td>
                        <td>
                            <asp:FileUpload ID="fileArticleWordFile"  BorderStyle="None" Width="70%" runat="server" />
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                         runat="server" ControlToValidate="fileArticleWordFile" 
                                         ErrorMessage="Word files only" ForeColor="Red"
                                         ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx)$"
                                         ValidationGroup="1" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfeFileUpload" ValidationGroup="1"  ErrorMessage="*" runat="server" ControlToValidate="fileArticleWordFile" />--%>

                        </td>
                    </tr>
                     <tr>
                        <td style="font-family: Calibri;height:40px;" align="center" >
                            Upload source code zip file if any
                        </td>
                        <td>
                            <asp:FileUpload ID="fileArticleSourceCode" BorderStyle="None" Width="70%" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td style="font-family: Calibri;height:40px" align="center" >
                           YouTube link if any
                        </td>
                        <td>
                            <asp:TextBox ID="txtYouTubeLink" Width="70%" BorderStyle="Groove" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return ValidateText()"  OnClick="PreviewButton_Click"
                                Font-Names="Calibri" Font-Size="Large" Font-Bold="True"
                                BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" 
                                />
                        </td>
                    </tr>
                </table>
          <%--  </td>
        </tr>
    </table>--%>
    <asp:HiddenField ID="hfUserEMail" runat="server" />
</asp:Content>
