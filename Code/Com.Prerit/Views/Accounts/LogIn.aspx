<%@ Import Namespace="Com.Prerit.Core" %>
<%@ Import Namespace="Com.Prerit.Helpers.Shared" %>
<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<LogInModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Log Into Your Google Account Via OpenID</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="Log into your Google account via OpenID." />
    <meta name="keywords" content="log in, Google account, OpenID" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Accounts.LogIn(null))).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>Log Into Your Google Account Via OpenID</span></h1>
    <% Html.RenderValidationSummaryPartial(); %>
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