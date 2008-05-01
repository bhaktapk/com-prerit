using System;
using System.Globalization;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Com.Prerit.Web;

public partial class account_create_default : Page
{
    #region Methods

    protected void AnswerQuestionButton_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (IsAnswerCorrect())
            {
                createAccountViews.ActiveViewIndex++;
            }
            else
            {
                answerQuestionFailureMessage.Visible = true;
            }
        }
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

            MembershipUser user = Membership.CreateUser(userNameInputText.Value, passwordInputText.Value, emailInputText.Value, null, null, true, out status);

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    FormsAuthentication.SetAuthCookie(user.UserName, true);

                    createAccountViews.ActiveViewIndex++;

                    break;
                default:
                    createAccountFailureMessage.Visible = true;

                    switch (status)
                    {
                        case MembershipCreateStatus.DuplicateUserName:
                            createAccountFailureMessageViews.ActiveViewIndex = (int) CreateAccountFailureMessageView.DuplicateUserName;
                            break;
                        case MembershipCreateStatus.DuplicateEmail:
                            createAccountFailureMessageViews.ActiveViewIndex = (int) CreateAccountFailureMessageView.DuplicateEmail;
                            break;
                        case MembershipCreateStatus.InvalidPassword:
                            createAccountFailureMessageViews.ActiveViewIndex = (int) CreateAccountFailureMessageView.InvalidPassword;
                            break;
                        case MembershipCreateStatus.InvalidEmail:
                            createAccountFailureMessageViews.ActiveViewIndex = (int) CreateAccountFailureMessageView.InvalidEmail;
                            break;
                        case MembershipCreateStatus.InvalidUserName:
                            createAccountFailureMessageViews.ActiveViewIndex = (int) CreateAccountFailureMessageView.InvalidUserName;
                            break;
                        default:
                            createAccountFailureMessageViews.ActiveViewIndex = (int) CreateAccountFailureMessageView.Unknown;
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

    private bool IsAnswerCorrect()
    {
        string wifesName = WebConfigurationManager.AppSettings["wifes-name"];

        if (string.IsNullOrEmpty(wifesName))
        {
            throw new Exception("The app setting 'wifes-name' has not been configured");
        }

        return string.Compare(wifesName, wifesNameInputText.Value, true, CultureInfo.InvariantCulture) == 0;
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

    protected void WifesNameCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = wifesNameRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            wifesNameLabel.CssClass = CssClassSelector.FormError;
            wifesNameInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    #endregion

    #region Nested Type: CreateAccountFailureMessageView

    protected enum CreateAccountFailureMessageView
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