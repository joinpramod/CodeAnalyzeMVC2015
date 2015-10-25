<%@ Page Language="C#" MasterPageFile="~/MasterPageNoGoog.master" AutoEventWireup="true" Inherits="QuestionAnswers" Codebehind="QuestionAnswers.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <table width="700px">
            <tr>     
                <td width="100%" align="center" style="height: 50px" valign="middle">
                    <span style="font-family: Calibri">Question Title :</span>
                    <asp:TextBox ID="txtSearch" runat="server" Width="500px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Calibri" 
                     BorderStyle="None" BackColor="#4fa4d5" ForeColor="White"
                        onclick="btnSearch_Click" />
                </td>
           </tr>
           <tr>     
               <td width="100%">
                    <asp:GridView ID="GVQuestionsAnswers" OnPageIndexChanging="GVQuestionsAnswers_PageIndexChanging" runat="server" 
                    AllowPaging="True" OnRowCommand="GVQuestionsAnswers_RowCommand" AutoGenerateColumns="False" CellPadding="3" Width="100%" PageSize="15">
                <PagerSettings NextPageText="Next" PreviousPageText="Prev" />
                <Columns>
                    <asp:TemplateField HeaderText="Question ID" Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="LblQuestionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QuestionID") %>'
                                Visible="False" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Question Title" ItemStyle-Width="55%" HeaderStyle-Font-Names="Calibri">
                        <ItemTemplate>
                             <asp:LinkButton ID="LblQuestion" CommandName="Question" runat="server" Font-Names="Calibri" Text='<%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %>'></asp:LinkButton>      
                        </ItemTemplate>                                
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DateTime" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Font-Names="Calibri">
                        <ItemTemplate>
                                        <asp:Label ID="LblAskedDateTime" runat="server"  Font-Names="Calibri" Text='<%# DataBinder.Eval(Container.DataItem,"AskedDateTime") %>'></asp:Label>      
                        </ItemTemplate>                                
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="DateTime" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Names="Calibri">
                        <ItemTemplate>
                                        <asp:Label ID="LblRepliedDate" runat="server"  Font-Names="Calibri" Text='<%# DataBinder.Eval(Container.DataItem,"RepliedDate") %>'></asp:Label>      
                        </ItemTemplate>                                
                    </asp:TemplateField>
                </Columns>
                <PagerStyle Font-Bold="False" Font-Size="Small" HorizontalAlign="Center" />
            </asp:GridView>  
               </td>
           </tr>
           <tr>
           <td align="center">
           <br/>
               <asp:Button ID="btnDone" runat="server" Text="Done" Font-Names="Calibri" 
                BorderStyle="None" BackColor="#4fa4d5" ForeColor="White"
                   onclick="btnDone_Click" />
           </td>
           </tr>
       </table>
</asp:Content>

