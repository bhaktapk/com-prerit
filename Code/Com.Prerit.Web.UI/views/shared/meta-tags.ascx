<%@ Import Namespace="Com.Prerit.Web.Models.Shared" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MetaTagsModel>" %>

<meta http-equiv="content-language" content="<%= Model.Culture %>" />
<meta http-equiv="content-type" content="<%= Model.ContentType %>; charset=<%= Model.ContentEncoding %>" />
<meta name="author" content="Prerit Bhakta" />
<meta name="copyright" content="Copyright &copy; <%= Model.CurrentYear %> Prerit Bhakta" />