<%@ Import Namespace="Com.Prerit.Helpers.Shared" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="validationSummary">
    <h2><span>Form Entry Errors</span></h2>
    <ul>
        <% Html.RepeatErrorMessages(error => { %>
            <li><%= error.ErrorMessage %></li>
        <% }); %>
    </ul>
</div>