<%@ Page Language="C#" MasterPageFile="~/MasterPageNoGoog.master" ValidateRequest="true" EnableViewState="true"
    AutoEventWireup="true" Inherits="AskQuestions" Codebehind="AskQuestions.aspx.cs" %>



<%@ Register Namespace="CodeAnalyzeMVC2015" TagPrefix="custom" Assembly="CodeAnalyzeMVC2015" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateText() {
            var varcont = '<%=EditorAskQuestion.ClientID %>' + '_ctl02_ctl00';
            var varTitle = '<%=txtTitle.ClientID %>';
            var VarType = '<%=ddType.ClientID %>';
            var VarhfUserEMail = '<%=hfUserEMail.ClientID %>';

            var content = document.getElementById(varcont).contentWindow.document.body.innerHTML;
            var title = document.getElementById(varTitle).value;
            var type = document.getElementById(VarType).value ;
            var VarEMail = document.getElementById(VarhfUserEMail).value;

            if (VarEMail != "") {
                if (content == "<br>" && title == "" && type == "---Select---") {
                    alert("Please enter question title, type and details");
                    return false;
                }
                else if (content != "<br>" && title == "" && type == "---Select---") {
                    alert("Please enter question title and type");
                    return false;
                }
                else if (content == "<br>" && title != "" && type == "---Select---") {
                    alert("Please enter question type and details");
                    return false;
                }
                else if (content == "<br>" && title == "" && type != "---Select---") {
                    alert("Please enter question title and content");
                    return false;
                }
                else if (content == "<br>" && title != "" && type != "---Select---") {
                    alert("Please enter question title and content");
                    return false;
                }
                else if (content != "<br>" && title != "" && type == "---Select---") {
                    alert("Please enter question title and content");
                    return false;
                }
                else if (content != "<br>" && title == "" && type != "---Select---") {
                    alert("Please enter question title and content");
                    return false;
                }
                else
                    return true;
            }
            else {
                alert("Please login to post");
                return false;
            }
        }

</script>

    <AjaxToolKit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"/>


    <table width="100%" style=" font-size:18px; font-family:Calibri;word-wrap: normal; word-break: break-all;">
     
        <tr style="width: 100%">
            <td align="center" width="90%" style="padding-left:20%">
                <table style="width:100%">
                    <tr>
                        <td style="font-family: Calibri; font-weight: bold;">
                            <asp:TextBox ID="txtQuestionTitle" runat="server" BorderStyle="Groove" Width="70%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="searchQue" ControlToValidate="txtQuestionTitle"
                                    runat="server" ErrorMessage="*" />
                            <asp:Button ID="btnSearch" runat="server" Font-Names="Calibri" OnClick="BtnSearch_Click"
                            BorderStyle="None" BackColor="#F5E8AA" ForeColor="White" Font-Size="Medium" ValidationGroup="searchQue"
                                Text="Search" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%">
            <td width="100%">
                <asp:GridView ID="GVQuestions" runat="server" OnPageIndexChanging="GVQuestions_PageIndexChanging"
                    AllowPaging="True" OnRowCommand="GVQuestions_RowCommand" AutoGenerateColumns="False"
                    CellPadding="3" Width="100%" PageSize="5">
                    <PagerSettings NextPageText="Next" PreviousPageText="Prev" />
                    <Columns>
                        <asp:TemplateField HeaderText="Question ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="LblQuestionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QuestionID") %>'
                                    Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question Title" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="75%" HeaderStyle-Font-Names="Calibri">
                            <ItemTemplate>
                                <%--    <asp:LinkButton ID="LblQuestion"  OnDataBinding="LblQuestion_DataBinding" CommandName="Question" Width="500px" runat="server" 
                             Font-Names="Calibri" Text="<%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %>"
                             OnClientClick="window.open('Soln.aspx?ID=<%# Eval("QuestionID") %>','','');return false;"></asp:LinkButton> --%>
                                <asp:HyperLink ID="HyperLink1" Font-Names="Calibri" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle")) %>'
                                    onclick="javascript:w= window.open(this.href,'','');return false;">
                             <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--   <asp:TemplateField HeaderText="Posted Date" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Font-Names="Calibri">
                        <ItemTemplate>
                             <asp:Label ID="LblAskedDate" runat="server" Font-Names="Calibri" Text='<%# DataBinder.Eval(Container.DataItem,"AskedDateTime") %>'></asp:Label>      
                        </ItemTemplate>                                
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Delete" Visible="false" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="5%" HeaderStyle-Font-Names="Calibri">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" CommandName="Deletes" Text="Delete" runat="server"
                                    Font-Names="Calibri"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle Font-Bold="False" Font-Size="Small" HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>

    </table>
    
    <asp:HiddenField ID="hfUserEMail" runat="server" />
</asp:Content>
