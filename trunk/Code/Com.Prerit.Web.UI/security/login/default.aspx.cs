using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class security_login_default : Page
{
    #region Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        SetLinks();
    }

    private void SetLinks()
    {
        SiteMapNode contactNode = SiteMap.Provider.FindSiteMapNode(contactLink.HRef);

        if (contactLink == null)
        {
            throw new ConfigurationErrorsException(string.Format("Can't find site map node '{0}'", contactLink.HRef));
        }

        contactLink.Title = contactNode.Description;
    }

    #endregion
}