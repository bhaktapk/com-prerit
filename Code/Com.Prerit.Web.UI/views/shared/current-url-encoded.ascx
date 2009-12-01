<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Com.Prerit.Web.Models.Shared.CurrentUrlEncodedModel>" %>

<%= Url.Encode(Model.CurrentUrlEncoded) %>