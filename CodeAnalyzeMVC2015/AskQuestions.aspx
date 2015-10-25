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
                            BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" Font-Size="Medium" ValidationGroup="searchQue"
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
    <table style="width:100%; font-size:16px" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>
                <table align="left" style="width: 100%">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblAck" runat="server" Font-Names="Calibri" ForeColor="#FF3300"  Text=""
                                Visible="False" Width="95%"></asp:Label><br />
                            <asp:LinkButton ID="lnkSolution" ForeColor="Blue" Font-Underline="true" runat="server" Visible="false" Font-Names="Calibri"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            Post Your Question
                        </td>
                    </tr>
                   <%-- <asp:Panel ID="pnlEMail" runat="server" Visible="false">
                        <tr>
                            <td style="font-family: Calibri;" align="right">
                                EMail:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEMail" runat="server" Width="315px" Font-Names="Calibri"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEMail"
                                    ErrorMessage="EMail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="1" Font-Names="Calibri"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="1" ControlToValidate="txtEMail"
                                    runat="server" ErrorMessage="*" />
                            </td>
                        </tr>
                    </asp:Panel>--%>
                    <tr>
                        <td style="font-family: Calibri;" align="center">
                            Title:<span style="font-family:Calibri; font-size:16px;color:Red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" BorderStyle="Groove" runat="server" Height="45px" Font-Names="Calibri" TextMode="MultiLine"
                                Width="90%"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="Title Required" ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Calibri;" align="center">
                            Type:<span style="font-family:Calibri; font-size:16px;color:Red">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddType" runat="server" DataTextField="QuestionType" Font-Size="14px"
                                Font-Names="Calibri" AppendDataBoundItems="true" DataValueField="QuestionTypeId">
                                <asp:ListItem Selected="True" Text="---Select---" Value="---Select---" />
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="---Select---" runat="server" ControlToValidate="ddType" 
                                ErrorMessage="Type Required" ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Calibri;" align="center">
                            Details:<span style="font-family:Calibri; font-size:16px;color:Red">*</span>
                        </td>
                        <td>
                            <%--<cc:HtmlEditor ID="EditorAskQuestion"  runat="server" Height="300px"  ToggleMode="None"  Width="640px" />--%>
                            <%--<cc1:Editor ID="Editor1" runat="server" />--%>
                            <br />
                            <custom:CustomEditor  ID="EditorAskQuestion"  NoScript="true" 
                                runat="server" Height="300px" Width="89%" />


                            <%--<br />--%>
                        
                            <%--<sigma:RichTextBox ID="EditorAskQuestion"  runat="server" Height="500px"  Width="650px" />--%>
                              <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="EditorAskQuestion" ErrorMessage="*" 
                                ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>--%>

                       
                         <%--Firefox is best suited for inline images.--%>
                        </td>
                        <%--<td>
                            <asp:TextBox runat="server" ID="EditorAskQuestion" Font-Names="Calibri" Font-Size="16px"
                                BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" TextMode="MultiLine"
                                Height="200px" Width="640px" /><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="EditorAskQuestion" ErrorMessage="*Details" 
                                ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>
                            </td>           OnClientClick="sendValue()"  --%>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClientClick="return ValidateText()" OnClick="PreviewButton_Click"
                                Font-Names="Calibri" Font-Size="Large" Font-Bold="True"
                                BorderStyle="None" BackColor="#4fa4d5" ForeColor="White"
                                ValidationGroup="1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfUserEMail" runat="server" />
</asp:Content>
