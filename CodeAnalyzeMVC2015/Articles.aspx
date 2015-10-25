<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="true"
    AutoEventWireup="true" Inherits="Articles" Codebehind="Articles.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <table style="width: 100%">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>

                        <td style="font-family: Calibri; font-weight: bold;" align="right">
                            <asp:TextBox ID="txtQuestionTitle" BorderStyle="Groove" runat="server" Width="80%"></asp:TextBox>
                        </td>
                        <%--<td style="font-family: Calibri; font-weight: bold;">
                            Type
                        </td>--%>
                        <td align="left" width="28%" style="font-family: Calibri; font-weight: bold;">
                            <%--<asp:DropDownList ID="ddType" runat="server" DataTextField="QuestionType" Font-Size="14px"
                                Font-Names="Calibri" AppendDataBoundItems="true" DataValueField="QuestionTypeId">
                                <asp:ListItem Selected="True" Text="---Select---" Value="---Select---" />
                            </asp:DropDownList>--%>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddType"
                                ErrorMessage="*" ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>--%>
                            <asp:Button ID="btnSearch" runat="server" Font-Names="Calibri" OnClick="BtnSearch_Click"
                                Text="Search"  BackColor="#4fa4d5" BorderStyle="None" ForeColor="White" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GVQuestions" runat="server" BorderStyle="None" OnPageIndexChanging="GVQuestions_PageIndexChanging"
                    AutoGenerateColumns="False" CellPadding="3" Width="99%" CellSpacing="5" GridLines="None" PageSize="10" 
                    PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Right" PagerStyle-Font-Bold="true"
                    AllowPaging="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Question ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="LblQuestionId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ArticleID") %>'
                                    Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Most Recent Articles"
                        HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium"
                            ItemStyle-Width="75%" HeaderStyle-Font-Names="Calibri">
                            <ItemTemplate>

                                <section id="ccr-blog">
				                        <article>
					                        <%--<figure class="blog-thumbnails">--%>
					                                <%--<img src="Images/WriteArticle.png" alt="Article Image"> --%>
                                                    <asp:Image ID="imgArrow" runat="server" ImageUrl="~/Images/WriteArticle.png" Height="10%" Width="10%" />                       	                        
					                      <%--  </figure>--%>                                             
					                        <div class="blog-text">
						                        <%--<h1><a style="font-weight:bold" href="<%# String.Format("va.aspx/{0}/{1}", Eval("ArticleID"), Eval("ArticleTitle").ToString().Replace(" ", "-")) %>"><%# DataBinder.Eval(Container.DataItem, "ArticleTitle")%></a></h1>--%>
						                        <h1><a style="font-weight:bold" href="<%# String.Format("va.aspx?QId={0}&QT={1}", Eval("ArticleID"), Eval("ArticleTitle").ToString().Replace(" ", "-")) %>"><%# DataBinder.Eval(Container.DataItem, "ArticleTitle")%></a></h1>
                                                
                                                <%-- <h1><a style="font-weight:bold" href="<%va.aspx?QId=" + Eval("ArticleID") + "&QT=" + Eval("ArticleTitle").ToString().Replace(" ", "-")) %>">
                                                 <%# DataBinder.Eval(Container.DataItem, "ArticleTitle")%></a></h1>--%>
                                                <%--<asp:LinkButton Font-Bold="true" Font-Size="Large" ID="btnArticleTitle" runat="server" CommandName="Question"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ArticleTitle")%>' />--%>
                                                
                                                <p>
                                                    <asp:Label ID="lblDetails" Text='<%# DataBinder.Eval(Container.DataItem, "ArticleDetails")%>' runat="server" Font-Size="16px" />
						                        </p>
						                        <div>		
                                                        
							                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-thumbs-o-up"></i> <%# DataBinder.Eval(Container.DataItem,"ThumbsUp") %>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-thumbs-o-down"></i> <%# DataBinder.Eval(Container.DataItem,"ThumbsDown") %>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image ID="ImageButton1" ToolTip="Views" runat="server" Height="4%" ImageUrl="~/Images/eye.png" 
                                                    Width="4%" /><%# DataBinder.Eval(Container.DataItem,"Views") %>			
							                        <%--<span class="read-more"><a href="<%# String.Format("va.aspx/{0}/{1}", Eval("ArticleID"), Eval("ArticleTitle").ToString().Replace(" ", "-")) %>">Read More</a></span>--%>
                                                    <span class="read-more"><a href="<%# String.Format("va.aspx?QId={0}&QT={1}", Eval("ArticleID"), Eval("ArticleTitle").ToString().Replace(" ", "-")) %>">Read More</a></span>
						                        </div>
				                        </div> 			
				                        </article>
			                        </section>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

           </td>
        </tr>      

    </table>

<table style="width:95%;font-family:Calibri;font-size:16px">

        
        <tr>
            <td>
                <asp:GridView ID="gvQuestionsAns" runat="server" 
                   
                    AutoGenerateColumns="False" CellPadding="3" Width="100%" CellSpacing="5"
                    GridLines="None" ShowHeader="false">
                    
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
                                <asp:Image ID="imgArrow" runat="server" ImageUrl="~/Images/questions.png" Height="4%"
                                    Width="4%" />
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("Soln.aspx/{0}/{1}", Eval("QuestionID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>'
                                    Font-Names="Calibri">  <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>--%>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>'
                                    Font-Names="Calibri">  <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>
                                <%--<asp:LinkButton ID="btnQuestionTitle" runat="server" PostBackUrl='<%# String.Format("va.aspx/{0}/{1}", Eval("ArticleID"), Eval("QuestionTitle").ToString().Replace(" ", "-")) %>' 
                                        Text='<%# DataBinder.Eval(Container.DataItem, "QuestionTitle")%>' />--%>

                             <%-- <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# String.Format("Soln.aspx?QId={0}&QT={1}", Eval("QuestionID"), Eval("QuestionTitle")) %>'
                                    onclick="javascript:w= window.open(this.href,'','');return false;" Font-Names="Calibri">
                             <%# DataBinder.Eval(Container.DataItem,"QuestionTitle") %></asp:HyperLink>--%>


                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   
                </asp:GridView>
            </td>
        </tr>
    </table>



</asp:Content>

