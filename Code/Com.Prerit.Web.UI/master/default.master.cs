using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Com.Prerit.Web;

using Framework.Web;

public partial class master_default : MasterPage
{
    #region Methods

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
        SetMenuLinks();

        siteNameLink.InnerText = WebsiteInfo.SiteName;

        validateCssLink.HRef = HttpUtility.HtmlEncode(W3OrgLink.GetCssValidatorUrl(Request.Url.AbsoluteUri));

        validateXhtmlLink.HRef = HttpUtility.HtmlEncode(W3OrgLink.GetXhtmlValidatorUrl(Request.Url.AbsoluteUri));
    }

    protected void SetLiterals()
    {
        copyrightLiteral.Text = string.Format(copyrightLiteral.Text, DateTime.Today.Year, WebsiteInfo.Author);
    }

    protected void SetMenuLink(HtmlAnchor menuLink)
    {
        SiteMapNode node = SiteMap.Provider.FindSiteMapNode(menuLink.HRef);

        if (node == null)
        {
            throw new Exception(string.Format("No sitemap node exists for {0}.", ResolveUrl(menuLink.HRef)));
        }

        menuLink.Attributes[HtmlMarkup.AccessKey] = char.ToLowerInvariant(node.Title[0]).ToString();

        menuLink.InnerHtml = string.Format(menuLink.InnerHtml, node.Title[0], node.Title.Substring(1));

        menuLink.Title = node.Description;

        if (SiteMap.CurrentNode.Url.ToLowerInvariant().StartsWith(node.Url.Substring(0, node.Url.LastIndexOf('/') + 1).ToLowerInvariant()))
        {
            menuLink.Attributes[HtmlMarkup.Class] = CssClassSelector.Active;
        }
    }

    private void SetMenuLinks()
    {
        SetMenuLink(aboutLink);
        SetMenuLink(contactLink);
        SetMenuLink(photosLink);
        SetMenuLink(resumeLink);
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

    #endregion
}