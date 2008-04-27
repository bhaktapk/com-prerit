using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Com.Prerit.Web;

public partial class about_default : Page
{
    #region Methods

    private void AddGoogleVerificationMetaTag()
    {
        HtmlMeta htmlMeta = new HtmlMeta();

        htmlMeta.Content = WebsiteInfo.GoogleVerificationMetaTagContent;

        htmlMeta.Name = WebsiteInfo.GoogleVerificationMetaTagName;

        Header.Controls.Add(htmlMeta);
    }

    protected void Page_Load(object sender, EventArgs args)
    {
        AddGoogleVerificationMetaTag();

        SetLinks();
    }

    private void SetLinks()
    {
        SiteMapNode resumeNode = SiteMap.Provider.FindSiteMapNode(resumeLink.HRef);

        if (resumeNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", resumeLink.HRef));
        }

        resumeLink.Title = resumeNode.Description;
    }

    #endregion
}