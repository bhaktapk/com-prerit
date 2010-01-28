<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">OpenID Information</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="OpenID Information" />
    <meta name="keywords" content="OpenID, information, xrds location" />
    <meta http-equiv="X-XRDS-Location" content="<%= new Uri(Request.Url, Url.Action(MVC.OpenId.Xrds())).AbsoluteUri %>" />
    <link rel="canonical" href="<%= Url.Action(MVC.OpenId.Index()) %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>OpenID Information</span></h1>
    <h2><span>OpenID.Realm</span></h2>
    <p>
        <a href="<%= Url.Action(MVC.OpenId.Index()) %>" title=""><%= new Uri(Request.Url, Url.Action(MVC.OpenId.Index())).AbsoluteUri%></a>
    </p>
    <h2><span>Required SREG</span></h2>
    <ul>
        <li>
            Email
        </li>
    </ul>
    <h2><span>XRDS Location</span></h2>
    <p>
        <a href="<%= Url.Action(MVC.OpenId.Xrds()) %>" title=""><%= new Uri(Request.Url, Url.Action(MVC.OpenId.Xrds())).AbsoluteUri %></a>
    </p>
    <h3><span>OpenID.Return_To</span></h3>
    <ul>
        <li>
            <%= new Uri(Request.Url, Url.Action(MVC.OpenId.Respond())).AbsoluteUri %>
        </li>
    </ul>
</asp:Content>
