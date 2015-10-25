<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="true" AutoEventWireup="true" Inherits="Soln" Codebehind="Soln.aspx.cs" %>
<%@ Register TagPrefix="Sol" TagName="Solutions" Src="~/Solutions.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <link 
       href="css/prettify.css" 
        type="text/css" 
        rel="stylesheet"/>
      <script type="text/javascript" 
       src="js/prettify.js">
      </script>

      <div>
     <Sol:Solutions ID="Solutions" runat="server"/>
     </div>

    <script>
        prettyPrint();
    </script>
</asp:Content>

