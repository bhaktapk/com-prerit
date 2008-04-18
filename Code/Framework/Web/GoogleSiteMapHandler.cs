using System;
using System.Configuration;
using System.Net.Mime;
using System.Web;
using System.Xml;

namespace Framework.Web
{
    public class GoogleSiteMapHandler : IHttpHandler
    {
        #region Properties

        public bool IsReusable
        {
            get { return true; }
        }

        #endregion

        #region Methods

        private Uri GetBaseUrl(HttpContext context)
        {
            return new Uri(context.Request.Url.Scheme + Uri.SchemeDelimiter + context.Request.Url.Authority);
        }

        public void ProcessRequest(HttpContext context)
        {
            if (SiteMap.RootNode == null)
            {
                throw new ConfigurationErrorsException("A sitemap must be configured.");
            }

            context.Response.ContentType = MediaTypeNames.Text.Xml;

            RenderGoogleSiteMap(context);
        }

        private void RenderGoogleSiteMap(HttpContext context)
        {
            using (XmlTextWriter writer = new XmlTextWriter(context.Response.OutputStream, context.Response.ContentEncoding))
            {
                SetXmlTextWriterProperties(writer);

                writer.WriteStartDocument();

                writer.WriteStartElement(GoogleSiteMapMarkup.Urlset);

                WriteUrlSetAttributes(writer);

                WriteUrlElement(GetBaseUrl(context), SiteMap.RootNode, writer);

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
        }

        private void SetXmlTextWriterProperties(XmlTextWriter writer)
        {
            writer.Formatting = Formatting.Indented;

            writer.IndentChar = '\t';

            writer.Indentation = 1;
        }

        private void TryWriteElement(string elementName, string elementValue, XmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(elementValue))
            {
                writer.WriteElementString(elementName, elementValue);
            }
        }

        private void WriteChangeFrequencyElement(SiteMapNode node, XmlTextWriter writer)
        {
            TryWriteElement(GoogleSiteMapMarkup.Changefreq, node[AspNetSiteMapMarkup.ChangeFrequency], writer);
        }

        private void WriteLastModifiedElement(SiteMapNode node, XmlTextWriter writer)
        {
            TryWriteElement(GoogleSiteMapMarkup.Lastmod, node[AspNetSiteMapMarkup.LastModified], writer);
        }

        private void WriteLocElement(Uri baseUrl, SiteMapNode node, XmlTextWriter writer)
        {
            Uri absoluteUri = new Uri(baseUrl, node.Url);

            writer.WriteElementString(GoogleSiteMapMarkup.Loc, absoluteUri.AbsoluteUri);
        }

        private void WritePriorityElement(SiteMapNode node, XmlTextWriter writer)
        {
            TryWriteElement(GoogleSiteMapMarkup.Priority, node[AspNetSiteMapMarkup.Priority], writer);
        }

        private void WriteUrlElement(Uri baseUrl, SiteMapNode node, XmlTextWriter writer)
        {
            writer.WriteStartElement(GoogleSiteMapMarkup.Url);

            WriteLocElement(baseUrl, node, writer);

            WriteLastModifiedElement(node, writer);

            WriteChangeFrequencyElement(node, writer);

            WritePriorityElement(node, writer);

            writer.WriteEndElement();

            foreach (SiteMapNode subNode in node.ChildNodes)
            {
                WriteUrlElement(baseUrl, subNode, writer);
            }
        }

        private void WriteUrlSetAttributes(XmlTextWriter writer)
        {
            writer.WriteAttributeString(GoogleSiteMapMarkup.Xmlns, GoogleSiteMapMarkup.XmlnsValue);

            writer.WriteAttributeString(GoogleSiteMapMarkup.Xmlns, GoogleSiteMapMarkup.Xsi, null, GoogleSiteMapMarkup.XsiValue);

            writer.WriteAttributeString(GoogleSiteMapMarkup.Xsi, GoogleSiteMapMarkup.SchemaLocation, null, GoogleSiteMapMarkup.SchemaLocationValue);
        }

        #endregion
    }
}