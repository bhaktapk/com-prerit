<%@ Import Namespace="Com.Prerit.Helpers.Albums" %>
<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<AlbumByYearAndSlugModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server"><%= Model.Album.Title %> (<%= Model.Album.Year %>)</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="View the album &quot;<%= Model.Album.Title %> (<%= Model.Album.Year %>)&quot;" />
    <meta name="keywords" content="album, <%= Model.Album.Title %>, <%= Model.Album.Year %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Albums.AlbumByYearAndSlug(Model.Album.Year, Model.Album.Slug))).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span><%= Model.Album.Title %> (<%= Model.Album.Year %>)</span></h1>
    <div class="photos">
        <% Html.RenderAlbumPhotosPartial(); %>
    </div>
</asp:Content>