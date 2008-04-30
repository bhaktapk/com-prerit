<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login.ascx.cs" Inherits="controls_login" %>
<h2><span>Log In</span></h2>
<p id="failureMessage" class="failureMessage" visible="false" runat="server">
    <asp:MultiView ID="failureMessageViews" runat="server">
        <asp:View runat="server">
            Your account has not been approved yet.
        </asp:View>
        <asp:View runat="server">
            Your account has been locked out due to too many failed attempts.
        </asp:View>
        <asp:View runat="server">
            Incorrect user name or password.
        </asp:View>
    </asp:MultiView>
</p>
<asp:ValidationSummary ID="validationSummary" CssClass="validationSummary" EnableClientScript="false" HeaderText="Form Entry Errors"
    ForeColor="" ValidationGroup="login" runat="server" />
<asp:Panel DefaultButton="loginButton" runat="server">
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
