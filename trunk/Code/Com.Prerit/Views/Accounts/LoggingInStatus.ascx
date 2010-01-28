<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LoggingInStatusModel>" %>

<h2><span>Why OpenID?</span></h2>
<p>
    It is a single username and password that allows you to log in to any site with OpenID.
</p>
<p>
    It works on thousands of websites.
</p>
<p>
    It's an open standard.
</p>
<p>
    <a href="http://openid.net/get-an-openid/what-is-openid/" title="What is OpenID?">Learn more.</a>
</p>
<h2><span>For Programmers</span></h2>
<p>
    Want to know more about the OpenID values used on this site? <a href="<%= Url.Action(MVC.OpenId.Index()) %>" title="OpenID Information">Look
    no more!</a>
</p>