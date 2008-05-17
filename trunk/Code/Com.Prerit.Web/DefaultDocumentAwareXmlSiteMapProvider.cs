using System.Web;

namespace Com.Prerit.Web
{
    public class DefaultDocumentAwareXmlSiteMapProvider : XmlSiteMapProvider
    {
        #region Methods

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            SiteMapNode node = base.FindSiteMapNode(rawUrl);

            if (node == null)
            {
                HttpContext context = HttpContext.Current;

                if (context != null)
                {
                    rawUrl = rawUrl.ToLowerInvariant();

                    int defaultDocumentIndex = rawUrl.IndexOf("/default.aspx");

                    if (defaultDocumentIndex != -1)
                    {
                        IncrementToIncludeTrailingSlash(ref defaultDocumentIndex);

                        node = base.FindSiteMapNode(rawUrl.Substring(0, defaultDocumentIndex));

                        if (node != null && !node.IsAccessibleToUser(context))
                        {
                            node = null;
                        }
                    }
                }
            }

            return node;
        }

        private void IncrementToIncludeTrailingSlash(ref int defaultDocumentIndex)
        {
            defaultDocumentIndex++;
        }

        #endregion
    }
}