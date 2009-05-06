<%@ Import Namespace="Com.Prerit.Web.Models.Contact" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IndexModel>" %>

<div class="validationSummary">
    <h2><span>Form Entry Errors</span></h2>
    <ul>
        <%-- TODO: use repeater (e.g. http://haacked.com/archive/2008/05/03/code-based-repeater-for-asp.net-mvc.aspx) --%>
        <% foreach (var modelState in ViewData.ModelState.Values) { %>
            <% foreach (var error in modelState.Errors) { %>
                <li><%= error.ErrorMessage %></li>
            <% } %>
        <% } %>
    </ul>
</div>
