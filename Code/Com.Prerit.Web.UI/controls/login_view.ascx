<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login_view.ascx.cs" Inherits="controls_login_view" %>
<asp:LoginView ID="loginView" runat="server">
    <AnonymousTemplate>
        <asp:Panel DefaultButton="loginButton" runat="server">
            <h2><span>Log In</span></h2>
            <p id="failureMessage" class="failureMessage" visible="false" runat="server">
                Incorrect user name or password.
            </p>
            <asp:ValidationSummary ID="validationSummary" CssClass="validationSummary" EnableClientScript="false" HeaderText="Form Entry Errors"
                ForeColor="" ValidationGroup="login" runat="server" />
            <fieldset>
                <asp:Label ID="userNameLabel" AssociatedControlID="userNameInputText" runat="server">User Name:</asp:Label>
                <input id="userNameInputText" type="text" runat="server" />
                <asp:RequiredFieldValidator ID="userNameRequiredFieldValidator" ControlToValidate="userNameInputText" Display="None" EnableClientScript="false"
                    ErrorMessage="User Name is required" ValidationGroup="login" runat="server" />
                <asp:CustomValidator ID="userNameCustomValidator" ControlToValidate="userNameInputText" Display="None" EnableClientScript="false"
                    OnServerValidate="UserNameCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="login" runat="server" />
                <br />
                <asp:Label ID="passwordLabel" AssociatedControlID="passwordInputText" runat="server">Password:</asp:Label>
                <input id="passwordInputText" type="password" value="Password" runat="server" />
                <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" ControlToValidate="passwordInputText" Display="None" EnableClientScript="false"
                    ErrorMessage="Password is required" ValidationGroup="login" runat="server" />
                <asp:CustomValidator ID="passwordCustomValidator" ControlToValidate="passwordInputText" Display="None" EnableClientScript="false"
                    OnServerValidate="PasswordCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="login" runat="server" />
                <br />
                <asp:Button ID="loginButton" CssClass="button" OnClick="LoginButton_Click" Text="Log In" ValidationGroup="login" runat="server" />
            </fieldset>
        </asp:Panel>
    </AnonymousTemplate>
    <LoggedInTemplate>
        <h2><span>Welcome Back</span></h2>
        <p>
            Hi <a id="userProfileLink" href="javascript:alert('Hold your horses. It is still under construction!');" title="User profile of {0}"
            runat="server"></a>, you can't do much right now but you will soon! If you want to login as someone else, please
            <asp:LoginStatus ID="loginStatus" LogoutText="logout" ToolTip="Logout" runat="server" />
            first.
        </p>
    </LoggedInTemplate>
</asp:LoginView>
