<%@ Import Namespace="Com.Prerit.Models.Accounts" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.master" Inherits="System.Web.Mvc.ViewPage<LoginModel>" %>

<asp:Content ContentPlaceHolderID="titleContent" runat="server">Login</asp:Content>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <meta name="description" content="Log in to get access to the photo albums" />
    <meta name="keywords" content="login" />
    <link rel="canonical" href="<%= Url.Action(MVC.Accounts.Login()) %>" />
    <link rel="stylesheet" href="<%= Links.content.styles.openid_jquery.openid_jquery_css %>" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="<%= Links.scripts.openid_jquery.openid_jquery_js %>"></script>
    <script type="text/javascript">
        $(function() {
            openid.init('openid_identifier');
        });
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="mainbarContent" runat="server">
    <h1><span>Log In With OpenID</span></h1>
    <noscript>
        <p>
            OpenID is service that allows you to log-on to many different websites using a single indentity. Find out <a href="http://openid.net/what/">
            more about OpenID</a> and <a href="http://openid.net/get/">how to get an OpenID enabled account</a>.
        </p>
    </noscript>
    <div id="openid_choice">
        <h2><span>Please Click Your Account Provider</span></h2>
        <div id="openid_btns">
        </div>
    </div>
    <form id="openid_form" action="" method="post">
        <div id="openid_input_area">
            <fieldset>
                <input id="openid_identifier" name="openid_identifier" type="text" value="http://" />
                <br />
                <input id="openid_submit" class="button" type="submit" value="Log In" />
            </fieldset>
        </div>
    </form>
</asp:Content>