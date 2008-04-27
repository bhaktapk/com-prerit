using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class security_default : Page
{
    #region Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        SetLinks();
    }

    private void SetLinks()
    {
        SiteMapNode loginNode = SiteMap.Provider.FindSiteMapNode(loginLink.HRef);

        if (loginLink == null)
        {
            throw new ConfigurationErrorsException(string.Format("Can't find site map node '{0}'", loginLink.HRef));
        }

        loginLink.Title = loginNode.Description;
    }

    #endregion
}