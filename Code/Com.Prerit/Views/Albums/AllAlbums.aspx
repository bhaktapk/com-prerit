<%@ Import Namespace="Com.Prerit.Helpers.Albums" %>
<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<AllAlbumsModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">All Albums</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="View all of the albums" />
    <meta name="keywords" content="albums, all" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Albums.AllAlbums())).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <% Html.RenderAlbumsByYearPartial(); %>
</asp:Content>