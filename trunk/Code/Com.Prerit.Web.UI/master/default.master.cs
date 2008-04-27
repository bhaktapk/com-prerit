using System;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Com.Prerit.Web;

using Framework.Web;

public partial class master_default : MasterPage
{
    #region Methods

    private string BuildAccessKeyLinkText(HtmlAnchor menuLink, SiteMapNode node)
    {
        Debug.Assert(menuLink != null);
        Debug.Assert(node != null);

        int accessKeyIndex = node.Title.IndexOf(node[SiteMapMarkup.AccessKey]);

        if (accessKeyIndex == -1)
        {
            throw new Exception(string.Format("Unable to find accesskey '{0}' for '{1}'", node[SiteMapMarkup.AccessKey], node.Url));
        }

        string preAccessKeyText = accessKeyIndex != 0 ? node.Title.Substring(0, accessKeyIndex) : null;
        string postAccessKeyText = accessKeyIndex != node.Title.Length - 1 ? node.Title.Substring(accessKeyIndex + 1) : null;

        return string.Format(menuLink.InnerHtml, preAccessKeyText, node[SiteMapMarkup.AccessKey], postAccessKeyText);
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
        Debug.Assert(menuLink != null);

        SiteMapNode node = SiteMap.Provider.FindSiteMapNode(menuLink.HRef);

        if (node == null)
        {
            throw new Exception(string.Format("No sitemap node exists for {0}.", ResolveUrl(menuLink.HRef)));
        }

        menuLink.Attributes[HtmlMarkup.AccessKey] = node[SiteMapMarkup.AccessKey];

        menuLink.InnerHtml = BuildAccessKeyLinkText(menuLink, node);

        menuLink.Title = node.Description;

        if (SiteMap.CurrentNode.Url.ToLowerInvariant().StartsWith(node.Url.Substring(0, node.Url.LastIndexOf('/') + 1).ToLowerInvariant()))
        {
            menuLink.Attributes[HtmlMarkup.Class] = CssClassSelector.Active;
        }
    }

    private void SetMenuLinks()
    {
        SetMenuLink(aboutLink);
        SetMenuLink(accountLink);
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