<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">No Albums</asp:Content>

<asp:Content ContentPlaceHolderID="metaTagContent" runat="server">
    <meta name="description" content="There are no albums at this time" />
    <meta name="keywords" content="no albums" />
</asp:Content>

<asp:Content ContentPlaceHolderID="linkContent" runat="server">
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.Albums.AllAlbums())).AbsoluteUri %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>No Photo Albums</span></h1>
    <p>
        There are no albums at this time.
    </p>
</asp:Content>
