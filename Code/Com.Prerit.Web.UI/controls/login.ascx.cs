using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Com.Prerit.Web;

public partial class controls_login : UserControl
{
    #region Methods

    private bool IsLoginPage()
    {
        return Request.Path.ToLowerInvariant().IndexOf("/security/login/") != -1;
    }

    private bool IsValidReturnUrl()
    {
        bool result = false;

        string returnUrl = Request.QueryString["ReturnUrl"];

        if (!string.IsNullOrEmpty(returnUrl))
        {
            if (VirtualPathUtility.IsAbsolute(returnUrl) || VirtualPathUtility.IsAppRelative(returnUrl))
            {
                result = true;
            }
            else
            {
                Trace.Write("information", string.Format("The ReturnUrl '{0}' is not a valid Uri. The referrer was {1}.", returnUrl, Request.UrlReferrer));
            }
        }

        return result;
    }

    protected void LoginButton_Click(object sender, EventArgs args)
    {
        if (Page.IsValid)
        {
            if (Membership.ValidateUser(userNameInputText.Value, passwordInputText.Value))
            {
                MembershipUser user = Membership.GetUser(userNameInputText.Value);

                FormsAuthentication.SetAuthCookie(user.UserName, true);

                if (IsValidReturnUrl())
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, true);
                }
                else if (!IsLoginPage())
                {
                    Response.Redirect(Request.Url.PathAndQuery, false);
                }
                else
                {
                    Response.Redirect(FormsAuthentication.DefaultUrl, false);
                }
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