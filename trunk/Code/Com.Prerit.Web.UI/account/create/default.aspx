<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="account_create_default" %>

<%-- TODO: add ajax functionality --%>
<%-- TODO: add personal question --%>
<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <h1><span>Create an Account</span></h1>
    <p id="successMessage" class="successMessage" visible="false" runat="server">
        Your account has been created.
    </p>
    <p id="failureMessage" class="failureMessage" visible="false" runat="server">
        <asp:MultiView ID="failureMessageViews" runat="server">
            <asp:View runat="server">
                Username already exists. Please enter a different user name.
            </asp:View>
            <asp:View runat="server">
                A username for that e-mail address already exists. Please enter a different e-mail address.
            </asp:View>
            <asp:View runat="server">
                The password provided is invalid. Please enter a valid password value.
            </asp:View>
            <asp:View runat="server">
                The e-mail address provided is invalid. Please check the value and try again.
            </asp:View>
            <asp:View runat="server">
                The user name provided is invalid. Please check the value and try again.
            </asp:View>
            <asp:View runat="server">
                Your account could not be created.
            </asp:View>
        </asp:MultiView>
    </p>
    <asp:ValidationSummary ID="validationSummary" CssClass="validationSummary" EnableClientScript="false" HeaderText="Form Entry Errors"
        ForeColor="" ValidationGroup="createAccount" runat="server" />
    <asp:Panel DefaultButton="createAccountButton" runat="server">
        <fieldset>
            <asp:Label ID="userNameLabel" AssociatedControlID="userNameInputText" runat="server">User Name:</asp:Label>
            <input id="userNameInputText" maxlength="20" size="40" type="text" runat="server" />
            <asp:RequiredFieldValidator ID="userNameRequiredFieldValidator" ControlToValidate="userNameInputText" Display="None" EnableClientScript="false"
                ErrorMessage="User Name is required" ValidationGroup="createAccount" runat="server" />
            <asp:CustomValidator ID="userNameCustomValidator" ControlToValidate="userNameInputText" Display="None" EnableClientScript="false"
                OnServerValidate="UserNameCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="createAccount" runat="server" />
            <br />
            <asp:Label ID="passwordLabel" AssociatedControlID="passwordInputText" runat="server">Password (Min. <%= Membership.MinRequiredPasswordLength %> characters):</asp:Label>
            <input id="passwordInputText" maxlength="20" size="40" type="password" runat="server" />
            <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" ControlToValidate="passwordInputText" Display="None" EnableClientScript="false"
                ErrorMessage="Password is required" ValidationGroup="createAccount" runat="server" />
            <asp:CustomValidator ID="passwordCustomValidator" ControlToValidate="passwordInputText" Display="None" EnableClientScript="false"
                OnServerValidate="PasswordCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="createAccount" runat="server" />
            <br />
            <asp:Label ID="confirmPasswordLabel" AssociatedControlID="confirmPasswordInputText" runat="server">Confirm Password:</asp:Label>
            <input id="confirmPasswordInputText" maxlength="20" size="40" type="password" runat="server" />
            <asp:RequiredFieldValidator ID="confirmPasswordRequiredFieldValidator" ControlToValidate="confirmPasswordInputText" Display="None"
                EnableClientScript="false" ErrorMessage="Confirm Password is required" ValidationGroup="createAccount" runat="server" />
            <asp:CompareValidator ID="confirmPasswordCompareValidator" ControlToCompare="passwordInputText" ControlToValidate="confirmPasswordInputText"
                Display="None" EnableClientScript="false" ErrorMessage="Confirm Password must match Password" Operator="Equal" Type="String"
                ValidationGroup="createAccount" runat="server" />
            <asp:CustomValidator ID="confirmPasswordCustomValidator" ControlToValidate="confirmPasswordInputText" Display="None" EnableClientScript="false"
                OnServerValidate="ConfirmPasswordCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="createAccount"
                runat="server" />
            <br />
            <asp:Label ID="emailLabel" AssociatedControlID="emailInputText" runat="server">E-mail:</asp:Label>
            <input id="emailInputText" maxlength="40" size="40" type="text" runat="server" />
            <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" ControlToValidate="emailInputText" Display="None" EnableClientScript="false"
                ErrorMessage="E-mail is required" ValidationGroup="createAccount" runat="server" />
            <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" ControlToValidate="emailInputText" Display="None" EnableClientScript="false"
                ErrorMessage="E-mail is not a valid value" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="createAccount"
                runat="server" />
            <asp:CustomValidator ID="emailCustomValidator" ControlToValidate="emailInputText" Display="None" EnableClientScript="false"
                OnServerValidate="EmailCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="createAccount" runat="server" />
            <br />
            <asp:Button ID="createAccountButton" CssClass="button" OnClick="CreateAccountButton_Click" Text="Create Account" ValidationGroup="createAccount"
                runat="server" />
        </fieldset>
    </asp:Panel>
</asp:Content>
