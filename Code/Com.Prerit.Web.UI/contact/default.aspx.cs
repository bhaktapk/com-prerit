using System;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

using Prerit.Com.Web;

public partial class contact_default : Page
{
    #region Methods

    private void ClearForm()
    {
        emailInputText.Value = null;
        nameInputText.Value = null;
        messageTextarea.Value = null;
    }

    protected void EmailCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = emailRequiredFieldValidator.IsValid && emailRegularExpressionValidator.IsValid;

        if (!args.IsValid)
        {
            emailLabel.CssClass = CssClassSelector.FormError;
            emailInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void MessageCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = messageRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            messageLabel.CssClass = CssClassSelector.FormError;
            messageTextarea.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void NameCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = nameRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            nameLabel.CssClass = CssClassSelector.FormError;
            nameInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void Page_Load(object sender, EventArgs args)
    {
        SetDefaultFocus();
    }

    protected void SendMessageButton_Click(object sender, EventArgs args)
    {
        if (IsValid)
        {
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = WebsiteInfo.SmtpHost;

            using (
                MailMessage message =
                    new MailMessage(emailInputText.Value,
                                    WebsiteInfo.AuthorEmailAddress,
                                    WebsiteInfo.GetContactEmailSubject(nameInputText.Value),
                                    messageTextarea.Value))
            {
                message.IsBodyHtml = false;

                smtpClient.Send(message);
            }

            successMessage.Visible = true;

            ClearForm();
        }
    }

    private void SetDefaultFocus()
    {
        if (string.IsNullOrEmpty(User.Identity.Name))
        {
            Form.DefaultFocus = nameInputText.ClientID;
        }
        else
        {
            nameInputText.Value = User.Identity.Name;

            Form.DefaultFocus = emailInputText.ClientID;
        }
    }

    #endregion
}