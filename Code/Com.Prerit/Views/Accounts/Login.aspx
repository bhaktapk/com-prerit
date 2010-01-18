<%@ Import Namespace="Com.Prerit.Core" %>
<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Import Namespace="Com.Prerit.Models.OpenId" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<LoginModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Login</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="Log in to get access to the photo albums" />
    <meta name="keywords" content="login" />
    <link rel="canonical" href="<%= Url.Action(MVC.Accounts.Login(null)) %>" />
    <link rel="stylesheet" href="<%= Links.content.styles.openid_jquery.openid_jquery_css %>" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="<%= Links.scripts.openid_jquery.openid_jquery_js %>"></script>
    <script type="text/javascript">
        $(function() {
            openid.init("<%= CreateRequestModel.PropertyName.OpenIdIdentifier %>", "<%= CreateRequestModel.PropertyName.OpenIdUsername %>");
        });
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>Log In With OpenID</span></h1>
    <noscript>
        <p>
            OpenID is service that allows you to log-on to many different websites using a single indentity. <a href="http://openid.net/get-an-openid/what-is-openid/"
            title="What is OpenID?">Find out more</a> about OpenID and how to <a href="http://openid.net/get-an-openid/" title="Get an OpenID">get an OpenID
            enabled account</a>.
        </p>
    </noscript>
    <div id="openid_choice">
        <h2><span>Please Click Your Account Provider</span></h2>
        <div id="openid_btns">
        </div>
    </div>
    <% using (Html.BeginForm(MVC.OpenId.CreateRequest(), FormMethod.Post, new { id = "openid_form" })) { %>
        <%= Html.Hidden(CreateRequestModel.PropertyName.ReturnUrl, Model.ReturnUrl) %>
        <div id="openid_input_area">
            <fieldset>
                <%= Html.TextBox(CreateRequestModel.PropertyName.OpenIdIdentifier, "http://") %>
                <br />
                <input id="openid_submit" class="button" type="submit" value="Log In" />
            </fieldset>
        </div>
    <% } %>
</asp:Content>