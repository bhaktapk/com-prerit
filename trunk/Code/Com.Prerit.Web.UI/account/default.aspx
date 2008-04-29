<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="account_default" %>

<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="Server">
    <h1><span>Available Account Options</span></h1>
    <p>
        Currently, there is only one thing you can do. You can <a id="loginLink" href="~/account/login/" title="" runat="server">
        login</a> if you have an account. In the future, you'll be able to do the following:
    </p>
    <ul>
        <li>Create an Account</li>
        <li>Change Your Password</li>
        <li>Recover Your Password</li>
    </ul>
</asp:Content>
