<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="true"
    AutoEventWireup="true" Inherits="UnAnswered" Codebehind="UnAnswered.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <table style="width:100%;font-family:Calibri;font-size:16px;word-wrap: normal; word-break: break-all;">

        <tr>
            <td>
                <table style="width: 90%">
                    <tr>
                        <td align="right" style="font-family: Calibri; font-weight: bold;">
                            <asp:TextBox ID="txtQuestionTitle" runat="server" BorderStyle="Groove" Width="90%"></asp:TextBox>
                        </td>
                        <%--<td style="font-family: Calibri;">
                            &nbsp;&nbsp;&nbsp;&nbsp;Type
                        </td>--%>
                        <%--<td align="left" width="200px" style="font-family: Calibri; font-weight: bold;">
                            <asp:DropDownList ID="ddType" runat="server" DataTextField="QuestionType" Font-Size="14px"
                                Font-Names="Calibri" AppendDataBoundItems="true" DataValueField="QuestionTypeId">
                                <asp:ListItem Selected="True" Text="---Select---" Value="---Select---" />
                            </asp:DropDownList>
                         </td>--%>
                         <td align="left">
                            <asp:Button ID="btnSearch" BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" runat="server" Font-Names="Calibri" OnClick="BtnSearch_Click"
                                Text="Search" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GVQuestions" runat="server" OnPageIndexChanging="GVQuestions_PageIndexChanging"
                    AllowPaging="True" PagerSettings-Position="TopAndBottom"
                    AutoGenerateColumns="False" CellPadding="3" Width="100%" PageSize="500" CellSpacing="5"
                    GridLines="None" ShowHeader="false">
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
                                <asp:Image ID="imgArrow" runat="server" ImageUrl="~/Images/questions.png" Height="20px"
                                    Width="20px" />

                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>'
                                    Font-Names="Calibri" >  <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>

                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("Soln.aspx/{0}/{1}", Eval("QuestionID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>'
                                    Font-Names="Calibri">  <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>--%>

                                <%--<asp:LinkButton ID="btnQuestionTitle" runat="server" PostBackUrl='<%# String.Format("va.aspx/{0}/{1}", Eval("ArticleID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>' 
                                        Text='<%# DataBinder.Eval(Container.DataItem, "QuestionTitle")%>' />--%>

                             <%-- <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle")) %>'
                                    onclick="javascript:w= window.open(this.href,'','');return false;" Font-Names="Calibri">
                             <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>--%>


                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle Font-Bold="False" Font-Size="Small" HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
