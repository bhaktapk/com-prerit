<%@ Import Namespace="Com.Prerit.Helpers.Albums" %>
<%@ Import Namespace="Com.Prerit.Models.Albums" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AlbumsByYearModel>" %>

<h1><span><%= Model.Year %> Albums</span></h1>
<div class="albums">
    <% Html.RenderAlbumPortraitPartial(); %>
</div>