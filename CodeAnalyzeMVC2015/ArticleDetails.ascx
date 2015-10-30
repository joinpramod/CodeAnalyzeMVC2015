<%@ Control Language="C#" AutoEventWireup="true" Inherits="ArticleDetails" Codebehind="ArticleDetails.ascx.cs" %>


   <script language="javascript" type="text/javascript">

       function ValidateText() {
           var varcont = '<%=txtReply.ClientID %>';
           var VarhfUserEMail = '<%=hfUserEMail.ClientID %>';

           var content = document.getElementById(varcont).value;
           var VarEMail = document.getElementById(VarhfUserEMail).value;

           if (VarEMail != "") {
               if (content == "<br>" || content == "") {
                   alert("Please enter details"); 
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

<style>

.element, .outer-container {
width: 100%;
height: 1000px;
}

.outer-container 
{
position: relative;
overflow: hidden;
}

.inner-container {
position: absolute;
overflow-y: scroll;
width: 100%;
}

.inner-container::-webkit-scrollbar {
display: none;
}

</style>
    <table style="width: 100%;font-family:Calibri">
        <tr>
            <td width="100%">
                <asp:Label ID="lblArticleTitle" runat="server" Font-Names="Calibri" Font-Size="24px"
                    ForeColor="Black" Width="90%"></asp:Label>
                    </td>

                    </tr>
        <tr>
            <td  width="100%"  align="left">

                <table style="font-size: 14px">
                    <tr>
                        <td align="right" style="width: 60px">
                            <asp:Image ID="ImageButton1" ToolTip="Views" runat="server" Height="30px" ImageUrl="~/Images/eye.png"
                                Width="30px" />
                        </td>
                        <td align="left" style="width: 60px">
                            <asp:Label ID="lblViews" runat="server" Font-Names="Calibri" ForeColor="Black"></asp:Label>
                        </td>
                        <td align="right" style="width: 60px">
                            <asp:ImageButton ID="btnThumbsUp" runat="server" Height="25px" ImageUrl="~/ThumpsUp.png"
                                Width="25px" OnClick="btnThumbsUp_Click" />
                        </td>
                        <td align="left" style="width: 60px">
                            <asp:Label ID="lblThumbsUp" runat="server" Font-Bold="False" Font-Names="Calibri"></asp:Label>
                        </td>
                        <td align="right" style="width: 60px">
                            <asp:ImageButton ID="btnThumbsDown" runat="server" Height="25px" ImageUrl="~/ThumpsDown.png"
                                Width="25px" OnClick="btnThumbsDown_Click" />
                        </td>
                        <td align="left" style="width: 60px">
                            <asp:Label ID="lblThumbsDown" runat="server" Font-Names="Calibri"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr><td align="left">
        <asp:LinkButton Font-Bold="true" ID="lnkBtnSourceCode" OnClick="lnkBtnSourceCode_Click" runat="server" Font-Underline="true"
        ForeColor="Blue"  Text="Download Source Code" Font-Size="Medium"/></td></tr>
        </table>


        <%--<tr> 
            <td width="100%">--%>
            <div class="outer-container">
            <div class="inner-container">
          
                <div id="divContent" class="element"  runat="server" />
                <%--<asp:Literal runat="server" ID="litHtml" Text='<%#Eval("html") %>' />--%>

             
            </div>
            </div>
            <%--</td>
        </tr>--%>



        <table  style="width: 100%;font-family:Calibri">
        <tr>
            <td>
        <br />
        <asp:Panel ID="pnlVideo" runat="server">
            <section id="sidebar-video-post">
				<div class="ccr-gallery-ttile">
					<span></span> 
					<p><strong>Video Post</strong></p>
				</div> 
				<div class="sidebar-video">
				<iframe width="340" height="195" id="iframeVideo" runat="server" src="" frameborder="0" allowfullscreen></iframe>
				</div>
			</section>      
        </asp:Panel>        
        </td>
        </tr>


        <tr >
            <td width="100%" >
                    <br />
                        <table style="width:100%;font-size:16px">
                            <tr>
                                <td style="width:25%" align="center">
                                    <asp:Image ID="imgAskedUser" runat="server" />
                                </td>
                                <td align="left" valign="top" style="width:75%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" valign="top" style="width:35px;height:20px">
                                                <asp:Label ID="lblAskedUser" runat="server" Font-Names="Calibri"
                                                    Width="250px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  align="left" valign="top">
                                                <asp:Label ID="lblAskedUserDetails" runat="server" Font-Names="Calibri" 
                                                    Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlReplies" runat="server">
        <table runat="server" id="tblReplies" style="width:100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Comments" Font-Bold="True" Font-Names="Calibri"
                        Font-Size="X-Large" ForeColor="#009933"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblAck" runat="server" Font-Names="Calibri" ForeColor="Red"
                                Visible="False" Width="100%"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b style="font-family: Calibri;" />Post your comments
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                       <%-- <td>
                            <b>Comment:</b><span style="font-family:Calibri; font-size:16px;font-weight:bold;color:Red">*</span>
                        </td>--%>
                        <td>
                            <asp:TextBox ID="txtReply" BorderStyle="Groove" runat="server" TextMode="MultiLine" Height="100px" Width="100%" />
                             <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ArticleComment" ControlToValidate="txtReply"
                                    runat="server" ErrorMessage="*" />--%>
                        <br />

                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="PreviewButton_Click" 
                            BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" OnClientClick="return ValidateText()"
                                Font-Names="Calibri" Font-Size="Large" Font-Bold="True"
                                ValidationGroup="ArticleComment" />
                        </td>
                    </tr>
                </table>
        <asp:HiddenField ID="hfUserEMail" runat="server" />
         <asp:HiddenField ID="hfSourceFile" runat="server" />
