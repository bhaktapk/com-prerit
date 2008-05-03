<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="account_create_default" %>

<%@ MasterType VirtualPath="~/master/default.master" %>
<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <script type="text/javascript">
        var loginViewUpdatePanelClientID = '<%= Master.LoginViewUpdatePanelClientID %>';
        var step3HeaderID = '<%= Step3HeaderID %>';
    </script>
    <asp:ScriptManagerProxy runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/account/create/default.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <h1><span>Create an Account</span></h1>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:MultiView ID="createAccountViews" ActiveViewIndex="0" runat="server">
                <asp:View runat="server">
                    <h2><span>Step 1: Answer a Simple Question</span></h2>
                    <p>
                        If you want to create an account, you'll need to prove you know either me or my wife. It's altogether very simple. You'll
                        just need to answer the following question:
                    </p>
                    <p id="answerQuestionFailureMessage" class="failureMessage" visible="false" runat="server">
                        The answer you gave was incorrect.
                    </p>
                    <asp:ValidationSummary CssClass="validationSummary" EnableClientScript="false" HeaderText="Form Entry Errors" ForeColor=""
                        ValidationGroup="answerQuestion" runat="server" />
                    <asp:Panel DefaultButton="answerQuestionButton" runat="server">
                        <fieldset>
                            <asp:Label ID="wifesNameLabel" AssociatedControlID="wifesNameInputText" runat="server">What is my wife's first name?</asp:Label>
                            <input id="wifesNameInputText" maxlength="20" size="40" type="text" runat="server" />
                            <asp:RequiredFieldValidator ID="wifesNameRequiredFieldValidator" ControlToValidate="wifesNameInputText" Display="None" EnableClientScript="false"
                                ErrorMessage="Please answer the simple question" ValidationGroup="answerQuestion" runat="server" />
                            <asp:CustomValidator ID="wifesNameCustomValidator" ControlToValidate="wifesNameInputText" Display="None" EnableClientScript="false"
                                OnServerValidate="WifesNameCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="answerQuestion"
                                runat="server" />
                            <br />
                            <asp:Button ID="answerQuestionButton" CssClass="button" OnClick="AnswerQuestionButton_Click" Text="Submit Your Answer" ValidationGroup="answerQuestion"
                                runat="server" />
                        </fieldset>
                    </asp:Panel>
                </asp:View>
                <asp:View runat="server">
                    <h2><span>Step 2: Enter Account Information</span></h2>
                    <p id="createAccountFailureMessage" class="failureMessage" visible="false" runat="server">
                        <asp:MultiView ID="createAccountFailureMessageViews" runat="server">
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
                    <asp:ValidationSummary CssClass="validationSummary" EnableClientScript="false" HeaderText="Form Entry Errors" ForeColor=""
                        ValidationGroup="createAccount" runat="server" />
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
                </asp:View>
                <asp:View runat="server">
                    <h2 id="<%= Step3HeaderID %>"><span>Step 3: Congratulations!</span></h2>
                    <p>
                        Congratulations! You have successfully created an account. Don't worry about logging into the site, it's already been done
                        for you. The only thing left to do now is explore the content that has been under lock and key.
                    </p>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
