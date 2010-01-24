<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NotLoggedInStatusModel>" %>

<h2><span>Log In</span></h2>
<p>
    Try <a href="<%= Url.Action(MVC.Accounts.LogIn(Request.Url.PathAndQuery)) %>" title="Login">logging in</a> to view the photos! Don't worry about not
    having an account with this site. You don't need one.
</p>