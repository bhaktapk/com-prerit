<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">OpenID Information</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="General OpenID information such as the openid.realm and openid.return_to parameters are listed." />
    <meta name="keywords" content="OpenID, information, openid.realm, openid.return_to, openid.sreg.required, X-XRDS-Location" />
    <meta http-equiv="X-XRDS-Location" content="<%= new Uri(Request.Url, Url.Action(MVC.OpenId.Xrds())).AbsoluteUri %>" />
    <link rel="canonical" href="<%= new Uri(Request.Url, Url.Action(MVC.OpenId.Index())) %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>OpenID Information</span></h1>
    <h2><span>OpenID.Realm</span></h2>
    <p>
        <a href="<%= Url.Action(MVC.OpenId.Index()) %>" title=""><%= new Uri(Request.Url, Url.Action(MVC.OpenId.Index())).AbsoluteUri%></a>
    </p>
    <h2><span>OpenID.Return_To</span></h2>
    <p>
        <%= new Uri(Request.Url, Url.Action(MVC.OpenId.Respond())).AbsoluteUri %>
    </p>
    <h2><span>OpenID.SREG.Required</span></h2>
    <ul>
        <li>
            Email
        </li>
    </ul>
    <h2><span>X-XRDS-Location</span></h2>
    <p>
        <a href="<%= Url.Action(MVC.OpenId.Xrds()) %>" title=""><%= new Uri(Request.Url, Url.Action(MVC.OpenId.Xrds())).AbsoluteUri %></a>
    </p>
</asp:Content>
