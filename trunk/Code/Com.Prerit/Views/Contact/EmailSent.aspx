<%@ Import Namespace="Com.Prerit.Helpers.Shared" %>
<%@ Import Namespace="Com.Prerit.Models.Contact" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<EmailSentModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Email To Prerit Bhakta Has Been Sent</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="An email to Prerit Bhakta has been sent." />
    <meta name="keywords" content="email sent, contact, Prerit Bhakta" />
</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Contact.EmailSent())).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainBarContent" runat="server">
    <h1><span>Email Sent</span></h1>
    <p>
        The following e-mail message has been sent.
    </p>
    <fieldset>
        Name:
        <%= Html.Encode(Model.Name) %>
        <br />
        E-mail:
        <%= Html.Encode(Model.EmailAddress) %>
        <br />
        <p><%= Html.HtmlizeLineBreaks(Html.Encode(Model.Message)) %></p>
    </fieldset>
</asp:Content>