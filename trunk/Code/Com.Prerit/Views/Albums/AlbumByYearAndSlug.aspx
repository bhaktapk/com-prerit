<%@ Import Namespace="Com.Prerit.Helpers.Albums" %>
<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Import Namespace="Links" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<AlbumByYearAndSlugModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server"><%= Model.Album.Title %> (<%= Model.Album.Year %>)</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="View the album &quot;<%= Model.Album.Title %> (<%= Model.Album.Year %>)&quot;" />
    <meta name="keywords" content="album, <%= Model.Album.Title %>, <%= Model.Album.Year %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Albums.AlbumByYearAndSlug(Model.Album.Year, Model.Album.Slug))).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="scriptContent" runat="server">
    <script src="<%= scripts.albums.album_by_year_and_slug_js %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span><%= Model.Album.Title %> (<%= Model.Album.Year %>)</span></h1>
    <div class="photos">
        <img class="eagerload" src="<%= content.images.processing_notifier_gif %>" alt="Eager loading of background image" height="16" width="16" />
        <% Html.RenderAlbumPhotosPartial(); %>
    </div>
</asp:Content>