<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AlbumPortraitModel>" %>

<p>
    <a href="<%= Url.Action(MVC.Albums.AlbumByYearAndSlug(Model.Album.Year, Model.Album.Slug)) %>" title="Portrait of album '<%= Model.Album.Title %>'"><img
    alt="Portrait of album '<%= Model.Album.Title %>'" src="<%= Url.Action(MVC.Albums.Portrait(Model.Album.Year, Model.Album.Slug)) %>" />
    <br />
    <span><%= Model.Album.Title %> (<%= Model.Album.PhotoCount %>)</span></a>
</p>