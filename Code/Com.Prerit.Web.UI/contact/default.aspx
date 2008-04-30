<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="contact_default" %>

<asp:Content ID="mainbar" ContentPlaceHolderID="mainbarPlaceHolder" runat="server">
    <h1><span>Email</span></h1>
    <p>
        Emails will generally be read during normal business hours in the Central Time Zone. Please consider these times when anticipating
        your reply.
    </p>
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <p id="successMessage" class="successMessage" visible="false" runat="server">
                Your message has been sent.
            </p>
            <asp:ValidationSummary ID="validationSummary" CssClass="validationSummary" EnableClientScript="false" HeaderText="Form Entry Errors"
                ForeColor="" ValidationGroup="contact" runat="server" />
            <asp:Panel DefaultButton="sendMessageButton" runat="server">
                <fieldset>
                    <asp:Label ID="nameLabel" AssociatedControlID="nameInputText" runat="server">Name:</asp:Label>
                    <input id="nameInputText" maxlength="40" size="40" type="text" runat="server" />
                    <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" ControlToValidate="nameInputText" Display="None" EnableClientScript="false"
                        ErrorMessage="Name is required" ValidationGroup="contact" runat="server" />
                    <asp:CustomValidator ID="nameCustomValidator" ControlToValidate="nameInputText" Display="None" EnableClientScript="false"
                        OnServerValidate="NameCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="contact" runat="server" />
                    <br />
                    <asp:Label ID="emailLabel" AssociatedControlID="emailInputText" runat="server">E-mail:</asp:Label>
                    <input id="emailInputText" maxlength="40" size="40" type="text" runat="server" />
                    <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" ControlToValidate="emailInputText" Display="None" EnableClientScript="false"
                        ErrorMessage="E-mail is required" ValidationGroup="contact" runat="server" />
                    <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" ControlToValidate="emailInputText" Display="None" EnableClientScript="false"
                        ErrorMessage="E-mail is not a valid value" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="contact"
                        runat="server" />
                    <asp:CustomValidator ID="emailCustomValidator" ControlToValidate="emailInputText" Display="None" EnableClientScript="false"
                        OnServerValidate="EmailCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="contact" runat="server" />
                    <br />
                    <asp:Label ID="messageLabel" AssociatedControlID="messageTextarea" runat="server">Message:</asp:Label>
                    <textarea id="messageTextarea" cols="40" rows="10" onkeypress="/* HACK: stop firefox from submitting when hitting enter */ if (event.stopPropagation) event.stopPropagation(); else event.cancelBubble = true;"
                        runat="server"></textarea>
                    <asp:RequiredFieldValidator ID="messageRequiredFieldValidator" ControlToValidate="messageTextarea" Display="None" EnableClientScript="false"
                        ErrorMessage="Message is required" ValidationGroup="contact" runat="server" />
                    <asp:CustomValidator ID="messageCustomValidator" ControlToValidate="messageTextarea" Display="None" EnableClientScript="false"
                        OnServerValidate="MessageCustomValidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="contact" runat="server" />
                    <br />
                    <asp:Button ID="sendMessageButton" CssClass="button" OnClick="SendMessageButton_Click" Text="Send Message" ValidationGroup="contact"
                        runat="server" />
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
