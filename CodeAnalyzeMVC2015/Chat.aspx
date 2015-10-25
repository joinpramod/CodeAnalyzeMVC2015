<%@ Page Language="C#" AutoEventWireup="true" Inherits="Chat" Codebehind="Chat.aspx.cs" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Code Analyze, Discussion Room for code issues</title>
    
    <link href="StyleSheet.css" type="text/css" rel="stylesheet" />
    <meta name="robots" content="noindex" />
</head>
<body onunload="Leave()">
    <form id="form1" runat="server">



       <input id="hdnRoomID" type="hidden" name="hdnRoomID" runat="server"/>
       <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePartialRendering="True" EnablePageMethods="True">
            <Scripts>
                <asp:ScriptReference Path="scripts.js" />
            </Scripts>
        </asp:ScriptManager> 
        
        


    
    <table style="width: 100%; height: 450px;"><tr><td align="center">
       <table style="width: 800px; height: 439px;">
           <tr align="center">
               <td colspan="2">

                        <asp:LinkButton id="btnChat" Runat="server" PostBackUrl="~/Articles.aspx" Font-Underline="false"
                                Text="Home" Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" 
                                BorderStyle="None" BackColor="#4fa4d5" ForeColor="White" 
                                Width="70px"></asp:LinkButton>
                        <asp:Image ID="Image1" runat="server" Height="26px" ImageUrl="~/Images/logo.JPG" 
                            Width="33px" />
                        &nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Font-Size="Large" Text="Code Analyze,  discussion room for code issues."></asp:Label>



                        &nbsp;<asp:Label ID="Label2" runat="server" Text="Login to join discussion." Font-Bold="True" 
                                    Font-Names="Calibri" Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label>  
                <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="14px" 
                            ForeColor="#FF3300"></asp:Label>
                </td>
               <td rowspan="3" align="center">
                   Techies<br />
                            <asp:ListBox runat="server" ID="lstMembers" Enabled="false" 
                        Height="387px" Width="100px"></asp:ListBox>
                    
                </td>
           </tr>
           <tr align="center">
               <td colspan="2">
                         <asp:textbox runat="server" TextMode="MultiLine" id="txt" BorderStyle="Groove"
                             Width="873px" ReadOnly="True" Height="325px" />
               </td>
           </tr>
           <tr align="center">
               <td>250 Charactors max at a time. 
                   <asp:TextBox id="txtMsg" Runat="server" TextMode="MultiLine" Width="638px" BorderStyle="Groove"
                       Height="60px"></asp:TextBox>
                   </td>
               <td>
                   <input id="btn"  onclick="button_clicked()" type="button" value="SEND" 
                                style="font-family: Calibri; font-weight: bold; font-size: large;background-color:#4fa4d5;color:White"/>
                </td>
           </tr>
           <tr align="center">
           <td colspan="3">
           
            </td>
           </tr>
       </table>
      </td></tr></table>

                    
    </form>
</body>
</html>
