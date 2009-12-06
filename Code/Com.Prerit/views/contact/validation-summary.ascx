<%@ Import Namespace="Com.Prerit.Helpers.Contact" %>
<%@ Import Namespace="Com.Prerit.Models.Contact" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IndexModel>" %>

<div class="validationSummary">
    <h2><span>Form Entry Errors</span></h2>
    <ul>
        <% Html.RepeatErrorMessages(error => { %>
            <li><%= error.ErrorMessage %></li>
        <% }); %>
    </ul>
</div>