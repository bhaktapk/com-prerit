<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LoggedInStatusModel>" %>

<h2><span>Welcome Back</span></h2>
<p>
    You're already logged in. <a href="<%= Url.Action(MVC.Accounts.Logout(Request.Url.PathAndQuery)) %>" title="Logout">Log out</a> if you want to
    log in with another account.
</p>