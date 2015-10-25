<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="true" AutoEventWireup="true" Inherits="VA" Codebehind="VA.aspx.cs" %>
<%@ Register TagPrefix="ViewArticle" TagName="Article" Src="~/ArticleDetails.ascx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
     <ViewArticle:Article ID="Solutions" runat="server"/>




</asp:Content>

