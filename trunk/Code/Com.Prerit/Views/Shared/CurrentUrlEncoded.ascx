<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Com.Prerit.Models.Shared.CurrentUrlEncodedModel>" %>

<%= Url.Encode(Model.CurrentUrlEncoded) %>