<%@ Import Namespace="Com.Prerit.Helpers.Albums" %>
<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<AlbumsByYearModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server"><%= Model.Year %> Albums</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="View the <%= Model.Year %> albums" />
    <meta name="keywords" content="albums, <%= Model.Year %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Albums.AlbumsByYear(Model.Year))).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <% Html.RenderAlbumsByYearPartial(); %>
</asp:Content>