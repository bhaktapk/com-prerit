<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AlbumPhotoModel>" %>

<p>
    <a rel="photoLink" href="<%= Url.Action(MVC.Albums.WebOptimized(Model.Album.Year, Model.Album.Slug, Model.PhotoItem)) %>"
    title="Image <%= Model.PhotoItem %> of <%= Model.Album.PhotoCount %> of album &quot;<%= Model.Album.Title %> (<%= Model.Album.Year %>)&quot;"><img
    alt="Image <%= Model.PhotoItem %> of <%= Model.Album.PhotoCount %> of album &quot;<%= Model.Album.Title %> (<%= Model.Album.Year %>)&quot;"
    src="<%= Url.Action(MVC.Albums.Thumbnail(Model.Album.Year, Model.Album.Slug, Model.PhotoItem)) %>" onload="photoOnLoad(this);" /></a>
</p>