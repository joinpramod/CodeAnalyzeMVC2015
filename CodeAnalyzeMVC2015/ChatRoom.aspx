<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ChatRoom" Codebehind="ChatRoom.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




	<table width="100%">
        <tr>
            <td align="center">
                <br />
                <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="Medium" Text="Select Technology:"></asp:Label>
                <br />
	                <asp:ListBox id="lstRooms" runat="server" Font-Names="Calibri" 
                                Font-Size="Medium" Height="139px" Width="10%">
                            <asp:ListItem Value="1" Selected="True">General</asp:ListItem>
								        <asp:ListItem Value="2">.Net</asp:ListItem>
								        <asp:ListItem Value="3">Java</asp:ListItem>
                                        <asp:ListItem Value="4">PHP</asp:ListItem>
								        <asp:ListItem Value="5">SQL</asp:ListItem>
                                        <asp:ListItem Value="6">Android</asp:ListItem>
								        <asp:ListItem Value="7">iPhone</asp:ListItem>
                                </asp:ListBox>
                <br />
                <br />
                <asp:Button id="btnChat" Runat="server" OnClick="btnJoinChat_Click" 
                                Text="Join" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" 
                                BorderStyle="None" BackColor="#4fa4d5" ForeColor="White"></asp:Button>
                               <%-- <asp:Button id="Button1" Runat="server" OnClientClick="javascript:window.open('chat.aspx','MsgWindow', 'width=1000px, height=500px');" 
                                Text="Join" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" 
                                BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" 
                                Width="70px"></asp:Button>--%>
                <br />
            </td>
        </tr>
    </table>


</asp:Content>

