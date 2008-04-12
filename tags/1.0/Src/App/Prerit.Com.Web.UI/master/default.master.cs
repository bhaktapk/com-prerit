using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Framework.Web;
using Prerit.Com.Web;

public partial class master_default : MasterPage
{
	protected const string MenuLinkID = "menuLink";

	protected void MenuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs args)
	{
		switch (args.Item.ItemType)
		{
			case ListItemType.AlternatingItem:
			case ListItemType.Item:
				HtmlAnchor menuLink = args.Item.FindControl(MenuLinkID) as HtmlAnchor;

				SiteMapNode node = args.Item.DataItem as SiteMapNode;

				if (menuLink == null)
				{
					throw new ConfigurationErrorsException(string.Format("Cannot find control {0}.", MenuLinkID));
				}

				if (node == null)
				{
					throw new ConfigurationErrorsException(string.Format("The dataItem is not of type {0}.", typeof(SiteMapNode)));
				}

				menuLink.Attributes[HtmlMarkup.AccessKey] = char.ToLowerInvariant(node.Title[0]).ToString();

				menuLink.HRef = node.Url;

				menuLink.InnerHtml = string.Format(menuLink.InnerHtml, node.Title[0], node.Title.Substring(1));

				menuLink.Title = node.Description;

				if (SiteMap.CurrentNode == null)
				{
					throw new ConfigurationErrorsException(string.Format("No sitemap node exists for {0}.", Request.Url));
				}

				//TODO: cleanup
				if (SiteMap.CurrentNode.Url.ToLowerInvariant().StartsWith(node.Url.Substring(0, node.Url.LastIndexOf('/') + 1).ToLowerInvariant()))
				{
					menuLink.Attributes[HtmlMarkup.Class] = CssClassSelector.Active;
				}

				break;
		}
	}

	protected override void OnInit(EventArgs args)
	{
		base.OnInit(args);

		SetPageTitle();

		SetMetaTags();

		SetLiterals();

		SetLinks();

		if (SiteMap.RootNode != null)
		{
			siteNameLink.HRef = SiteMap.RootNode.Url;

			siteNameLink.Title = SiteMap.RootNode.Description;
		}
	}

	protected void SetLinks()
	{
		siteNameLink.InnerText = WebsiteInfo.SiteName;

		validateCssLink.HRef = HttpUtility.HtmlEncode(W3OrgLink.GetCssValidatorUrl(Request.Url.AbsoluteUri));

		validateXhtmlLink.HRef = HttpUtility.HtmlEncode(W3OrgLink.GetXhtmlValidatorUrl(Request.Url.AbsoluteUri));
	}

	protected void SetLiterals()
	{
		copyrightLiteral.Text = string.Format(copyrightLiteral.Text, DateTime.Today.Year, WebsiteInfo.Author);
	}

	protected void SetMetaTags()
	{
		authorMeta.Content = WebsiteInfo.Author;

		contentLanguageMeta.Content = CultureInfo.CurrentCulture.Name.ToLowerInvariant();

		contentTypeMeta.Content = string.Format(contentTypeMeta.Content, Response.ContentType, Response.Charset);

		copyrightMeta.Content = string.Format(copyrightMeta.Content, DateTime.Today.Year, WebsiteInfo.Author);

		if (SiteMap.CurrentNode != null)
		{
			descriptionMeta.Content = SiteMap.CurrentNode.Description;

			keywordsMeta.Content = SiteMap.CurrentNode[AspNetSiteMapMarkup.Keywords];
		}
	}

	protected void SetPageTitle()
	{
		TrySetPageTitleFromSiteMap();

		Page.Title = WebsiteInfo.FormatPageTitle(Page.Title);
	}

	protected void TrySetPageTitleFromSiteMap()
	{
		if (string.IsNullOrEmpty(Page.Title) && SiteMap.CurrentNode != null)
		{
			Page.Title = SiteMap.CurrentNode.Title;
		}
	}
}