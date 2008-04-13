using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Prerit.Com.Web;

public partial class about_default : Page
{
	protected void Page_Load(object sender, EventArgs args)
	{
		AddGoogleVerificationMetaTag();

		SetLinks();
	}

	private void SetLinks()
	{
		SiteMapNode resumeNode = SiteMap.Provider.FindSiteMapNodeFromKey(resumeLink.HRef);

		if (resumeNode == null)
		{
			throw new ConfigurationErrorsException(string.Format("Can't find site map node '{0}'", resumeLink.HRef));
		}

		resumeLink.Title = resumeNode.Description;
	}

	private void AddGoogleVerificationMetaTag()
	{
		HtmlMeta htmlMeta = new HtmlMeta();

		htmlMeta.Content = WebsiteInfo.GoogleVerificationMetaTagContent;

		htmlMeta.Name = WebsiteInfo.GoogleVerificationMetaTagName;

		Header.Controls.Add(htmlMeta);
	}
}