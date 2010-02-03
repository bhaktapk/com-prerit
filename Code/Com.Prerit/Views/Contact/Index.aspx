<%@ Import Namespace="Com.Prerit.Controllers" %>
<%@ Import Namespace="Com.Prerit.Helpers.Shared" %>
<%@ Import Namespace="Com.Prerit.Models.Contact" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<IndexModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Contact Prerit Bhakta Via Email</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="Contact Prerit Bhakta via email." />
    <meta name="keywords" content="contact, Prerit Bhakta, email" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Contact.Index())).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainBarContent" runat="server">
    <h1><span>Contact Via Email</span></h1>
    <% Html.RenderValidationSummaryPartial(); %>
    <p>
        Emails will generally be read during normal business hours in the Central Time Zone. Please consider these times when anticipating your reply.
    </p>
    <% using (Html.BeginForm(MVC.Contact.SendEmail(), FormMethod.Post)) { %>
        <fieldset>
            <label for="<%= IndexModel.PropertyName.Name %>">Name:</label>
            <%= Html.TextBox(IndexModel.PropertyName.Name, null, new { maxlength = "40", size = "40" }) %>
            <br />
            <label for="<%= IndexModel.PropertyName.EmailAddress %>">E-mail:</label>
            <%= Html.TextBox(IndexModel.PropertyName.EmailAddress, null, new { maxlength = "40", size = "40" }) %>
            <br />
            <label for="<%= IndexModel.PropertyName.Message %>">Message:</label>
            <%= Html.TextArea(IndexModel.PropertyName.Message, null, 10, 40, null) %>
            <br />
            <input id="sendMessageButton" class="button" type="submit" value="Send Message" />
        </fieldset>
    <% } %>
</asp:Content>