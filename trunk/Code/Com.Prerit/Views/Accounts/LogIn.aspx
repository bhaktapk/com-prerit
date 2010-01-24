<%@ Import Namespace="Com.Prerit.Core" %>
<%@ Import Namespace="Com.Prerit.Helpers.Shared" %>
<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Import Namespace="Com.Prerit.Models.OpenId" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<LogInModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Log In</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="Log in to get access to the photo albums" />
    <meta name="keywords" content="log in" />
    <link rel="canonical" href="<%= Url.Action(MVC.Accounts.LogIn(null)) %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>Log Into Your Google Account Via OpenID</span></h1>
    <p>
        You can log into this site with your Google account via <a href="http://openid.net/get-an-openid/what-is-openid/" title="What is OpenID?">OpenID</a>.
        This is a safe and revolutionary method that all websites will be incorporating. It's fast, easy, free and most importantly safe.
    </p>
    <p>
        If you are already logged into your <a href="https://www.google.com/accounts/" title="Google Account">Google Account</a> you won't have to log in again.
    </p>
    <% using (Html.BeginForm(MVC.OpenId.RequestAuth(Model.ReturnUrl), FormMethod.Post)) { %>
        <fieldset>
            <input class="button" type="submit" value="Log In" />
        </fieldset>
    <% } %>
</asp:Content>