<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="account_default" %>

<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <h1><span>Available Account Options</span></h1>
    <p>
        Currently, there are a couple of things you can do. You can <a id="loginLink" href="~/account/login/" title="" runat="server">
        login</a> if you have an account or you can <a id="createAccountLink" href="~/account/create/" title="" runat="server">create
        an account</a> if you don't already have one. If you need to reset your password or cancel your account, you'll have to
        <a id="contactLink" href="~/contact/" title="" runat="server">contact</a> me so I can do it for you.
    </p>
</asp:Content>
