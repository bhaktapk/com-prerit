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
        return Request.Path.ToLowerInvariant().IndexOf("/account/login/") != -1;
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
                MembershipUser user = Membership.GetUser(userNameInputText.Value);

                failureMessage.Visible = true;

                if (!user.IsApproved)
                {
                    failureMessageViews.ActiveViewIndex = (int) FailureMessageView.NotApproved;
                }
                else if (user.IsLockedOut)
                {
                    failureMessageViews.ActiveViewIndex = (int) FailureMessageView.LockedOut;
                }
                else
                {
                    failureMessageViews.ActiveViewIndex = (int) FailureMessageView.InvalidUserNameOrPassword;
                }
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

    #region Nested Type: FailureMessageView

    protected enum FailureMessageView
    {
        NotApproved = 0,
        LockedOut = 1,
        InvalidUserNameOrPassword = 2
    }

    #endregion
}