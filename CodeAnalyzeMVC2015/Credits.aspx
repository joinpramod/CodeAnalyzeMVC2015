﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Credits" Codebehind="Credits.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
        
            <script type="text/javascript">
                $(document).ready(function () {
                    var divs = $('div[id^="content-"]').hide(),
                i = 0;
                    (function cycle() {
                        divs.eq(i).fadeIn(400)
                    .delay(5000)
                    .fadeOut(400, cycle);
                        i = ++i % divs.length;
                    })();
                });

        </script>

     
        <table style="font-family: Calibri; font-size: 16px;" width="100%">
            <tr>
                <td align="left">
                    <%--<p>
                        We believe that users who post articles, questions, solutions should be honored 
                        by <span style="color: Black">rewards </span>as a mark of our appreciation 
                        becuase it actually helps many users.
                    </p><br />--%>
                    <p>
                        <span>We have started with initial 5 levels of reward system and we believe to 
                        make it fully successful. We will be emailing you Amazon Gift Card instantly, user can reach us if 
                        they feel they deserve a reward but haven’t been considered yet. We will be validating your posts before emailing the rewards.
                        But having said that we do not expect great articles. Do take a look at <u><a href="PostingGuidelines.aspx" style="font-weight:bold;color:Blue;">Posting Guidelines</a></u> <br />
                    <p>
                        We hope to make this code blogging more happier by making bloggers earn some rewards along with blogging.<u><a href="Info.aspx" style="font-weight:bold;color:Blue;">More about us</a></u></p><br /> 
                    <p>
                        <%--<span style="color: #FF0000">Happy Coding!!</span>--%></p><br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <p>
                        <%--Users will be rewarded each time after reaching<strong> MARK</strong>--%></p>
                    <p>
                        <strong>MARK</strong> is
                        <br />
                            &nbsp;&nbsp; [5 articles] <b>OR</b><br />
                            &nbsp;&nbsp; [15 questions, answers including both] <b>OR</b><br />
                            &nbsp;&nbsp; [(10 questions, answers including both) + 2 articles]
                    </p>
                    <br />
                    <br />
                    <table cellpadding="5" cellspacing="5">
                        <tr>
                            <td width="103">
                                <p>
                                    <b>Levels</b></p>
                            </td>
                            <td>
                                <p>
                                    <b>Stars</b></p>
                            </td>
                            <td>
                                <p>
                                    <b>Rewards</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <p>
                                    Contributor</p>
                            </td>
                            <td align="left">
                                <p>
                                    <asp:Image ID="Image2" runat="server" Height="20px" 
                                        ImageUrl="~/Images/1star.png" Width="20px" />
                                </p>
                            </td>
                            <td align="left">
                                <p>
                                    Amazon GiftCard <strong>$5</strong> for 1st time reaching MARK
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    Expert</p>
                            </td>
                            <td align="left">
                                <p>
                                    <asp:Image ID="Image3" runat="server" Height="20px" 
                                        ImageUrl="~/Images/2stars.png" Width="40px" />
                                </p>
                            </td>
                            <td align="left">
                                <p>
                                    Amazon GiftCard <strong>$5</strong> for 2nd time reaching MARK&nbsp;
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="103">
                                <p>
                                    Master 
                                </p>
                            </td>
                            <td align="left">
                                <p>
                                    <asp:Image ID="Image4" runat="server" Height="20px" 
                                        ImageUrl="~/Images/3stars.png" Width="60px" />
                                </p>
                            </td>
                            <td align="left">
                                <p>
                                   Amazon GiftCard <strong>$10</strong> for 3rd time reaching MARK&nbsp;
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="103">
                                <p>
                                    Guru</p>
                            </td>
                            <td align="left">
                                <p>
                                    <asp:Image ID="Image5" runat="server" Height="20px" 
                                        ImageUrl="~/Images/4yellowstars.jpeg" Width="80px" />
                                </p>
                            </td>
                            <td align="left">
                                <p>
                                   Amazon GiftCard <strong>$10</strong> for 4th time reaching MARK&nbsp;
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="103">
                                <p>
                                    Scholar</p>
                            </td>
                            <td align="left">
                                <p>
                                    <asp:Image ID="Image6" runat="server" Height="20px" 
                                        ImageUrl="~/Images/5Stars.jpeg" Width="100px" />
                                </p>
                            </td>
                            <td align="left">
                                <p>
                                   Amazon GiftCard <strong>$20</strong> for 5th time reaching MARK&nbsp;
                                </p>
                            </td>
                        </tr>
                    <%--<tr>
                        <td  width="103">
                            &nbsp;</td>
                        <td >
                            &nbsp;</td>
                        <td  align="left">
                            <p>
                                When a user reach Scholar level he/she would have been rewarded with a total of 
                                $50 and would have a total of 50 questions OR answers and 40 articles</p>
                        </td>
                    </tr>--%>
                        <tr>
                            <td width="103">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                Amazon GiftCard <strong>$5</strong> for next everytime reaching MARK&nbsp;
                            </td>
                        </tr>
                    </table>
                    <p>
                    <br /><br />
                    <o:p>&nbsp;Amazon gift cards will be emailed to you and will be of Amazon of your 
                        respective country. So Giftcards will be in your country currency equal to above 
                        mentioned amount.</o:p></p>
                    <p>
                        <span style="color: #FF3300">For Ex<o:p> - Users from India will receive from 
                        Amazon India in Rupees. </o:p>
                        </span>
                    </p>
                    <p>
                        If this is not possible then we will email you of $ dollars by default.</p>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table class="solution" style="width:82%;">
                        <tr align="center" class="solution">
                            <td class="style21" style="font-family:Calibri;">
                                <asp:TextBox ID="txtFullName" runat="server" Visible="false" Width="230px"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Font-Names="Calibri" 
                                    onclick="btnSearch_Click" Text="Search" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Calibri" 
                        ForeColor="#009933" Text="Reward details of the users coming soon." 
                        Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GVHonours" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="3" 
                        OnPageIndexChanging="GVHonours_PageIndexChanging" 
                        PagerSettings-Position="TopAndBottom" PageSize="10" ShowHeaderWhenEmpty="true" 
                        Width="100%">
                        <PagerSettings NextPageText="Next" PreviousPageText="Prev" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Font-Names="Calibri" 
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Picture" 
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="profileImage" runat="server" Height="50px" 
                                        ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ImageURL") %>' Width="50px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Names="Calibri" 
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Full Name" 
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFullName" runat="server" Font-Names="Calibri" 
                                        Text='<%# DataBinder.Eval(Container.DataItem,"FullName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Names="Calibri" 
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" 
                                HeaderText="Total Valid Answers &amp; Articles" 
                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblReplies" runat="server" Font-Names="Calibri" 
                                        Text='<%# DataBinder.Eval(Container.DataItem,"Company") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Names="Calibri" 
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" 
                                HeaderText="Reward" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                             <%--<asp:Label ID="lblRewardDesc" runat="server" Font-Names="Calibri" Text='<%# DataBinder.Eval(Container.DataItem,"RewardDesc") %>'></asp:Label>--%>      
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Names="Calibri" 
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Reward Received Date" 
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                             <%--<asp:Label ID="lblRewardReceivedDate" runat="server" Font-Names="Calibri" Text='<%# DataBinder.Eval(Container.DataItem,"RewardReceivedDate") %>'></asp:Label>--%>      
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle Font-Bold="False" Font-Size="Small" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>


     <div style="width:200px;height:200px"">
     <img src="Images/amazon10.jpg" />
 
     </div>
    
        <br />
        <br />

        <div style="font-size:large;font-weight:bold;">
        Recent Users Rewarded
        </div>
        <div id="content-1" style="padding-left:10%">
             <table style="width:80%; font-size:16px; font-family: Calibri;">
            <tr>
                <%--<td style="width:15%" align="center">
                    <asp:Image ID="imgAskedUser" runat="server" Height="70px" Visible="false"
                        ImageUrl="~/Images/Vishwa.png" Width="70px" />
                </td>--%>
                <td align="left" valign="top" style="width:75%">
                    <table style="width: 100%;">
                        <tr>
                            <td align="left" valign="top" style="height:20px;font-weight:bold">
                                Deepak Shastri</td>
                        </tr>
                        <tr>
                            <td  align="left" valign="top" style="font-size:13px">
                                Java Developer, work on Java and Android application. India.</td>
                        </tr>
                        <tr>
                            <td  align="left" valign="top">
                                Didn't expect to get rewarded for code blogging.
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
        </div>
        <div id="content-2"  style="padding-left:10%">
            <table style="width:80%; font-size:16px; font-family: Calibri;">
                <tr>
                    <%--<td style="width:15%" align="center">
                        <asp:Image ID="Image1" runat="server" Height="70px" Visible="false"
                            ImageUrl="~/Images/Anoop.png" Width="70px" />
                    </td>--%>
                    <td align="left" valign="top" style="width:75%">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left" valign="top" style="height:20px;font-weight:bold">
                                    Anoop D </td>
                            </tr>
                            <tr>
                                <td  align="left" valign="top" style="font-size:13px">
                                    Work mainly on Iphone application. Crazy about apple and love to explore everything about it. India.</td>
                            </tr>
                            <tr>
                                <td  align="left" valign="top">
                                                
                                    It was really good to know that bloggers would be rewarded initially, so started 
                                    using this and then when I recevied the reward in the email it was great.   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
      

    
</asp:Content>

