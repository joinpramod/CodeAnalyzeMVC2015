<%@ Control Language="C#" AutoEventWireup="true" Inherits="Solutions" Codebehind="Solutions.ascx.cs" %>
<%@ Register Namespace="CodeAnalyzeMVC2015" TagPrefix="custom" Assembly="CodeAnalyzeMVC2015" %>




<script language="javascript" type="text/javascript">

    function ValidateText() {
        var varcont = '<%=SolutionEditor.ClientID %>' + '_ctl02_ctl00';
        var VarhfUserEMail = '<%=hfUserEMail.ClientID %>';

        var content = document.getElementById(varcont).contentWindow.document.body.innerHTML;
        var VarEMail = document.getElementById(VarhfUserEMail).value;

        if (VarEMail != "") {
            if (content == "<br>") {
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
    <AjaxToolKit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true" />

<asp:Panel ID="pnlSolution" runat="server" Width="100%">
    
    <table style="width: 100%;word-wrap: normal; word-break: break-all;">
        <tr>
            <td colspan="3" align="left">
                <table  style="width:50%;">
                    <tr>
                        <td style="width:20%;">
                            <asp:Image ID="imgAskedUser" runat="server" Height="50px" Width="50px" />
                        </td>
                        <td align="left">
                            <asp:Label ID="lblAskedUser" Width="80%" runat="server" Font-Bold="False" Font-Names="Calibri"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="width:100%" colspan="2">
                <%--<span class="style25">Question Title:</span>--%>
                <asp:Label ID="lblQuestionTitle" runat="server" Font-Names="Calibri" Font-Size="20px" 
                    ForeColor="Black" Width="80%"></asp:Label>
            </td>          
        </tr>
                 <tr>
          <td align="right" colspan="2" style="padding-right:2%">
              <asp:Image ID="ImageButton1" ToolTip="Views" runat="server" Height="30px" ImageUrl="~/Images/eye.png" 
                    Width="30px" /><asp:Label ID="lblViews" runat="server" Font-Names="Calibri" Font-Size="14px" Font-Bold="true"
                    ForeColor="Black"></asp:Label>
                    </td>
         
        </tr>
        <tr>
            <td width="100%" colspan="2">
                <%--<span class="style25">Details:</span>--%>
                <div id="divQuestionDetails" runat="server" style="height: auto; width: 100%; font-size: 16px;font-weight:normal" />
            </td>
        </tr>

    </table>


    <br />

    <asp:Panel ID="pnlReplies" runat="server">
        <table runat="server" id="tblReplies" style="font-size:16px" cellpadding="0" cellspacing="0">
            <%--<tr>
            <td align="center">
                <asp:Label ID="lblSessionTimeOut" Visible="false" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Large" ForeColor="Red"></asp:Label>
            </td>
        </tr> --%>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Answers" Font-Bold="True" Font-Names="Calibri"
                        Font-Size="X-Large" ForeColor="#009933"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
  
  <br />
  <br />
  <br />
  <br />
    <table style="width: 100%;font-size:16px" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblAck" runat="server" Font-Names="Calibri" ForeColor="Red"
                                Visible="False" Width="100%"></asp:Label><br />
                        </td>
                    </tr>
                    <tr >
                        <td colspan="2" style="background-color:#F5E8AA">
                            <b style="font-family: Calibri;" />Post your answer<span style="font-family:Calibri; font-size:16px;font-weight:bold;color:Red">*</span>
                        </td>
                    </tr>
                      <tr>
                        <td colspan="2" align="left">
                            Place your code between <b>#codestart</b> and <b>#codeend</b> tags mandatorily. If this not followed, code will not be properly highlighted.<br />
                            For ex 
                            <pre>
                            #codestart
                                //your code here
                            #codeend
                            </pre>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel runat="server" ID="pnlUser" Visible="false">
                                <table style="height: 40px; width: 90%;">
                                    <tr>
                                        <td>
                                            <b style="font-family: Calibri;">E-Mail:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEMail" runat="server" BorderStyle="Groove" Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <%--<asp:RegularExpressionValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEMail"
                                                ErrorMessage="EMail Required" Font-Names="Calibri" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="1" Font-Bold="False"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="1" ControlToValidate="txtEMail"
                                                runat="server" ErrorMessage="*" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <%--<td>
                            <b>Reply:</b><span style="font-family:Calibri; font-size:16px;font-weight:bold;color:Red">*</span>
                        </td>--%>
                        <td colspan="2">
                            <custom:CustomEditor ID="SolutionEditor" InitialCleanUp="true" NoScript="true" runat="server" Height="300px"
                                Width="100%" />

                          <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="SolutionEditor" ErrorMessage="*" 
                        ValidationGroup="1" Font-Names="Calibri"></asp:RequiredFieldValidator>--%>
                        <br />
                            <%--Firefox is best suited for inline images.--%>

                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return ValidateText()" OnClick="PreviewButton_Click"
                                Font-Names="Calibri" Font-Size="Large" Font-Bold="True" 
                                 BorderStyle="None" BackColor="#F5E8AA" ForeColor="White"
                                ValidationGroup="1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--<custom:CustomEditor ID="customEditor2" runat="server" Style="display:none" />--%>
        <asp:HiddenField ID="hfUserEMail" runat="server" />
</asp:Panel>
