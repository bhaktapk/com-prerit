using System;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Prerit.Com.Web;

public partial class controls_login_view : UserControl
{
    #region Constants

    protected const string FailureMessageID = "failureMessage";

    protected const string LoginButtonID = "loginButton";

    protected const string PasswordInputTextID = "passwordInputText";

    protected const string PasswordLabelID = "passwordLabel";

    protected const string PasswordRequiredFieldValidatorID = "passwordRequiredFieldValidator";

    protected const string UserNameInputTextID = "userNameInputText";

    protected const string UserNameRequiredFieldValidatorID = "userNameRequiredFieldValidator";

    protected const string UserProfileLinkID = "userProfileLink";

    #endregion

    #region Methods

    protected T FindControlFromLoginView<T>(string id) where T : Control
    {
        return loginView.FindControl(id) as T;
    }

    protected T GetControlFromLoginView<T>(string id) where T : Control
    {
        T control = FindControlFromLoginView<T>(id);

        if (control == null)
        {
            throw new ConfigurationErrorsException(string.Format("Can't find control '{0}'  in login view", id));
        }

        return control;
    }

    protected void LoginButton_Click(object sender, EventArgs args)
    {
        if (Page.IsValid)
        {
            HtmlInputText passwordInputText = GetControlFromLoginView<HtmlInputText>(PasswordInputTextID);

            HtmlInputText userNameInputText = GetControlFromLoginView<HtmlInputText>(UserNameInputTextID);

            if (Membership.ValidateUser(userNameInputText.Value, passwordInputText.Value))
            {
                MembershipUser user = Membership.GetUser(userNameInputText.Value);

                FormsAuthentication.SetAuthCookie(user.UserName, true);

                Response.Redirect(Request.Url.PathAndQuery, false);
            }
            else
            {
                HtmlGenericControl failureMessage = GetControlFromLoginView<HtmlGenericControl>(FailureMessageID);

                failureMessage.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs args)
    {
        HtmlInputText userNameInputText = FindControlFromLoginView<HtmlInputText>(UserNameInputTextID);

        if (userNameInputText != null && string.IsNullOrEmpty(Page.Form.DefaultFocus))
        {
            Page.Form.DefaultFocus = userNameInputText.ClientID;
        }

        HtmlAnchor userProfileLink = FindControlFromLoginView<HtmlAnchor>(UserProfileLinkID);

        if (userProfileLink != null)
        {
            userProfileLink.Title = string.Format(userProfileLink.Title, Page.User.Identity.Name);
            userProfileLink.InnerText = Page.User.Identity.Name;
        }
    }

    protected void PasswordCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        RequiredFieldValidator passwordRequiredFieldValidator = GetControlFromLoginView<RequiredFieldValidator>(PasswordRequiredFieldValidatorID);

        args.IsValid = passwordRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            Label passwordLabel = GetControlFromLoginView<Label>(PasswordLabelID);

            HtmlInputText passwordInputText = GetControlFromLoginView<HtmlInputText>(PasswordInputTextID);

            passwordLabel.CssClass = CssClassSelector.FormError;
            passwordInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void UserNameCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        RequiredFieldValidator userNameRequiredFieldValidator = GetControlFromLoginView<RequiredFieldValidator>(UserNameRequiredFieldValidatorID);

        args.IsValid = userNameRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            Label userNameLabel = GetControlFromLoginView<Label>("userNameLabel");

            HtmlInputText userNameInputText = GetControlFromLoginView<HtmlInputText>(UserNameInputTextID);

            userNameLabel.CssClass = CssClassSelector.FormError;
            userNameInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    #endregion
}