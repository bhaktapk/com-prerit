<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LoggedInStatusModel>" %>

<h2><span>Logged In As</span></h2>
<p>
    <%= Model.EmailAddress %>
</p>
<h2><span>Log Out</span></h2>
<p>
    You can <a href="<%= Url.Action(MVC.Accounts.LogOut(Request.Url.PathAndQuery)) %>" title="Log out">log out</a> at any time.
</p>
