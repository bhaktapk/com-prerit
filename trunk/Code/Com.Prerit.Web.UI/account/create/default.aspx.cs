using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Com.Prerit.Web;

public partial class account_create_default : Page
{
    #region Methods

    private void ClearForm()
    {
        userNameInputText.Value = null;
        passwordInputText.Value = null;
        confirmPasswordInputText.Value = null;
        emailInputText.Value = null;
    }

    protected void ConfirmPasswordCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = confirmPasswordRequiredFieldValidator.IsValid && confirmPasswordCompareValidator.IsValid;

        if (!confirmPasswordRequiredFieldValidator.IsValid)
        {
            confirmPasswordLabel.CssClass = CssClassSelector.FormError;
            confirmPasswordInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }

        if (!confirmPasswordCompareValidator.IsValid)
        {
            passwordLabel.CssClass = CssClassSelector.FormError;
            passwordInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;

            confirmPasswordLabel.CssClass = CssClassSelector.FormError;
            confirmPasswordInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void CreateAccountButton_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            MembershipCreateStatus status;

            Membership.CreateUser(userNameInputText.Value, passwordInputText.Value, emailInputText.Value, null, null, true, out status);

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    successMessage.Visible = true;

                    ClearForm();
                    break;
                default:
                    failureMessage.Visible = true;

                    switch (status)
                    {
                        case MembershipCreateStatus.DuplicateUserName:
                            failureMessageViews.ActiveViewIndex = (int) FailureMessageView.DuplicateUserName;
                            break;
                        case MembershipCreateStatus.DuplicateEmail:
                            failureMessageViews.ActiveViewIndex = (int) FailureMessageView.DuplicateEmail;
                            break;
                        case MembershipCreateStatus.InvalidPassword:
                            failureMessageViews.ActiveViewIndex = (int) FailureMessageView.InvalidPassword;
                            break;
                        case MembershipCreateStatus.InvalidEmail:
                            failureMessageViews.ActiveViewIndex = (int) FailureMessageView.InvalidEmail;
                            break;
                        case MembershipCreateStatus.InvalidUserName:
                            failureMessageViews.ActiveViewIndex = (int) FailureMessageView.InvalidUserName;
                            break;
                        default:
                            failureMessageViews.ActiveViewIndex = (int) FailureMessageView.Unknown;
                            Trace.Warn(string.Format("User could not be created due to the MembershipCreateStatus value of {0}", status));
                            break;
                    }
                    break;
            }
        }
    }

    protected void EmailCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = emailRequiredFieldValidator.IsValid && emailRegularExpressionValidator.IsValid;

        if (!args.IsValid)
        {
            emailLabel.CssClass = CssClassSelector.FormError;
            emailInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void PasswordCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = passwordRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            passwordLabel.CssClass = CssClassSelector.FormError;
            passwordInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void UserNameCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = userNameRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            userNameLabel.CssClass = CssClassSelector.FormError;
            userNameInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    #endregion

    #region Nested Type: FailureMessageView

    protected enum FailureMessageView
    {
        DuplicateUserName = 0,
        DuplicateEmail = 1,
        InvalidPassword = 2,
        InvalidEmail = 3,
        InvalidUserName = 4,
        Unknown = 5
    }

    #endregion
}