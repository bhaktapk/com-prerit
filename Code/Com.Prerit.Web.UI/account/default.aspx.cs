using System;
using System.Web;
using System.Web.UI;

public partial class account_default : Page
{
    #region Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        SetLinks();
    }

    private void SetLinks()
    {
        SiteMapNode loginNode = SiteMap.Provider.FindSiteMapNode(loginLink.HRef);
        SiteMapNode createAccountNode = SiteMap.Provider.FindSiteMapNode(createAccountLink.HRef);
        SiteMapNode contactNode = SiteMap.Provider.FindSiteMapNode(contactLink.HRef);

        if (loginNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", loginLink.HRef));
        }

        if (createAccountNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", createAccountLink.HRef));
        }

        if (contactNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", contactLink.HRef));
        }

        loginLink.Title = loginNode.Description;
        createAccountLink.Title = createAccountNode.Description;
        contactLink.Title = contactNode.Description;
    }

    #endregion
}