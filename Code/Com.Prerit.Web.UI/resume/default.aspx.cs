using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class resume_default : Page
{
    #region Methods

    protected void Page_Load(object sender, EventArgs args)
    {
        HtmlLink printCss = new HtmlLink();
        HtmlLink screenCss = new HtmlLink();

        printCss.Href = "~/resume/default_aspx_print.css";
        printCss.Attributes.Add("media", "print");
        printCss.Attributes.Add("rel", "stylesheet");
        printCss.Attributes.Add("type", "text/css");

        screenCss.Href = "~/resume/default_aspx_screen.css";
        screenCss.Attributes.Add("media", "projection, screen, tv");
        screenCss.Attributes.Add("rel", "stylesheet");
        screenCss.Attributes.Add("type", "text/css");

        Header.Controls.Add(printCss);
        Header.Controls.Add(screenCss);

        const string pdfNodeUrl = "~/resume/resume_of_prerit_bhakta.pdf";
        const string wordNodeUrl = "~/resume/resume_of_prerit_bhakta.doc";
        const string xmlNodeUrl = "~/resume/resume_of_prerit_bhakta.xml";

        SiteMapNode pdfResumeNode = SiteMap.Provider.FindSiteMapNode(pdfNodeUrl);
        SiteMapNode wordResumeNode = SiteMap.Provider.FindSiteMapNode(wordNodeUrl);
        SiteMapNode xmlResumeNode = SiteMap.Provider.FindSiteMapNode(xmlNodeUrl);

        if (pdfResumeNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", pdfNodeUrl));
        }

        if (wordResumeNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", wordNodeUrl));
        }

        if (xmlResumeNode == null)
        {
            throw new Exception(string.Format("Can't find site map node '{0}'", xmlNodeUrl));
        }

        pdfLink.HRef = pdfResumeNode.Url;
        pdfLink.Title = pdfResumeNode.Description;

        wordLink.HRef = wordResumeNode.Url;
        wordLink.Title = wordResumeNode.Description;

        xmlLink.HRef = xmlResumeNode.Url;
        xmlLink.Title = xmlResumeNode.Description;
    }

    #endregion
}