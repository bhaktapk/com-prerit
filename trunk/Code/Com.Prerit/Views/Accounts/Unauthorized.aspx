<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<UnauthorizedModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Unauthorized Access</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="You are seeking a resource for which you are unauthorized." />
    <meta name="keywords" content="unauthorized" />
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Accounts.Unauthorized())).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>Unauthorized</span></h1>
    <p>
        You are seeking a resource for which you are unauthorized. Try <a href="<%= Url.Action(MVC.Accounts.LogIn(Model.ReturnUrl)) %>" title="Log In">
        logging in</a> with another account after you <a href="<%= Url.Action(MVC.Accounts.LogOut(Request.Url.PathAndQuery)) %>" title="Log out">log
        out</a>.
    </p>
</asp:Content>