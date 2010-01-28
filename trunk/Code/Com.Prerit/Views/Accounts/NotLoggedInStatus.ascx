<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NotLoggedInStatusModel>" %>

<h2><span>Log In</span></h2>
<p>
    Try <a href="<%= Url.Action(MVC.Accounts.LogIn(Request.Url.PathAndQuery)) %>" title="Log In">logging in</a> and you <em>may</em> get to to view
    the photos! You'll need a Google account. Sound confusing? Just try it!
</p>
