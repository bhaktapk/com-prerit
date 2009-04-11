<%@ Import Namespace="Com.Prerit.Web.Models.Contact"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IndexModel>" %>

<div class="validationSummary">
    <h2><span>Form Entry Errors</span></h2>
    <ul>
        <% foreach (var modelState in ViewData.ModelState.Values) { %>
            <% foreach (var error in modelState.Errors) { %>
                <li><%= error.ErrorMessage %></li>
            <% } %>
        <% } %>
    </ul>
</div>
