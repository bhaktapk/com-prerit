<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login_view.ascx.cs" Inherits="controls_login_view" %>
<%@ Register TagPrefix="ctrl" TagName="login" Src="~/controls/login.ascx" %>
<asp:LoginView runat="server">
    <AnonymousTemplate>
        <ctrl:login runat="server" />
    </AnonymousTemplate>
    <LoggedInTemplate>
        <h2><span>Welcome Back</span></h2>
        <p>
            Hi <a href="javascript:alert('Hold your horses. It is still under construction!');" title="<%= string.Format("User profile of {0}", Page.User.Identity.Name) %>">
            <%= Page.User.Identity.Name %></a>, you can't do much right now but you will soon! If you want to login as someone else,
            please
            <asp:LoginStatus LogoutText="logout" ToolTip="Logout" runat="server" />
            first.
        </p>
    </LoggedInTemplate>
</asp:LoginView>
