using System.Web;

namespace Framework.Web
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
                    string urlPath = context.Request.Path.ToLowerInvariant();

                    int defaultDocumentIndex = urlPath.IndexOf("/default.aspx");

                    if (defaultDocumentIndex != -1)
                    {
                        IncrementToIncludeTrailingSlash(ref defaultDocumentIndex);

                        node = base.FindSiteMapNode(urlPath.Substring(0, defaultDocumentIndex));

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