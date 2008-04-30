using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class account_login_default : Page
{
    #region Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        SetLinks();
    }

    private void SetLinks()
    {
        SiteMapNode contactNode = SiteMap.Provider.FindSiteMapNode(contactLink.HRef);
        SiteMapNode createAccountNode = SiteMap.Provider.FindSiteMapNode(createAccountLink.HRef);

        if (contactNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", contactLink.HRef));
        }

        if (createAccountNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", createAccountLink.HRef));
        }

        contactLink.Title = contactNode.Description;
        createAccountLink.Title = createAccountNode.Description;
    }

    #endregion
}