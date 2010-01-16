<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<LoginModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Login</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="Log in to get access to the photo albums" />
    <meta name="keywords" content="login" />
    <link rel="canonical" href="<%= Url.Action(MVC.Accounts.Login()) %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
</asp:Content>