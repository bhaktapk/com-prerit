using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Prerit.Com.Web;

public partial class controls_login : UserControl
{
    #region Methods

    protected void LoginButton_Click(object sender, EventArgs args)
    {
        if (Page.IsValid)
        {
            if (Membership.ValidateUser(userNameInputText.Value, passwordInputText.Value))
            {
                MembershipUser user = Membership.GetUser(userNameInputText.Value);

                FormsAuthentication.SetAuthCookie(user.UserName, true);

                Response.Redirect(Request.Url.PathAndQuery, false);
            }
            else
            {
                failureMessage.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (userNameInputText != null && string.IsNullOrEmpty(Page.Form.DefaultFocus))
        {
            Page.Form.DefaultFocus = userNameInputText.ClientID;
        }
    }

    protected void PasswordCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = passwordRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            passwordLabel.CssClass = CssClassSelector.FormError;
            passwordInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    protected void UserNameCustomValidator_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = userNameRequiredFieldValidator.IsValid;

        if (!args.IsValid)
        {
            userNameLabel.CssClass = CssClassSelector.FormError;
            userNameInputText.Attributes[HtmlMarkup.Class] = CssClassSelector.FormError;
        }
    }

    #endregion
}