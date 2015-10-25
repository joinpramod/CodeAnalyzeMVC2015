<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="true" AutoEventWireup="true" Inherits="Topics" Codebehind="Topics.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <table style="width:100%;font-size:16px">

            <tr>
             <td>
                    <asp:GridView ID="GVQuestions" runat="server"  BorderStyle="None" OnRowCommand="GVQuestions_RowCommand" 
                        AutoGenerateColumns="False" CellPadding="3" Width="100%" CellSpacing="5" 
                        GridLines="None" ShowHeader="false">
                     
                        <Columns>
                            <asp:TemplateField HeaderText="Question ID" Visible="False">
                                <ItemTemplate> 
                                    <asp:Label ID="LblQuestionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"QuestionID") %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="75%"  HeaderStyle-Font-Names="Calibri">
                                <ItemTemplate>


                                     <asp:Image ID="imgArrow" runat="server" ImageUrl="~/Images/Info.png" Height="20px" Width="20px" />
                                     <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("Soln.aspx/{0}/{1}", Eval("QuestionID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>' 
                                     Font-Names="Calibri">
                                     <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>  --%>   
                                     
                                     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>' 
                                     Font-Names="Calibri">
                                     <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>  

                                    <%--       <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle")) %>' 
                                     onclick="javascript:w= window.open(this.href,'','');return false;" Font-Names="Calibri">
                                     <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>  --%>
                                                             
                                </ItemTemplate>                                
                            </asp:TemplateField>
                        </Columns>
                       
                    </asp:GridView>  
               </td>            
            </tr>
       </table>    
        

</asp:Content>

