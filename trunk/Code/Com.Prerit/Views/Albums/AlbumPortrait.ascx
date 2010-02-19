<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AlbumPortraitModel>" %>

<p>
    <a href="" title=""><img alt="" height="" width="" src="" />
    <br />
    <span><%= Model.Album.Title %> (<%= Model.AlbumItemCount != null ? Model.AlbumItemCount.ToString() : "?" %>)</span></a>
</p>