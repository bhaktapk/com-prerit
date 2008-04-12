using Framework;

namespace Prerit.Com.Web
{
    public sealed class HtmlMarkup : StringEnum<HtmlMarkup>
    {
        public static readonly HtmlMarkup AccessKey = new HtmlMarkup("accesskey");

        public static readonly HtmlMarkup Class = new HtmlMarkup("class");

        public static readonly HtmlMarkup OnClick = new HtmlMarkup("onclick");

        private HtmlMarkup(string name)
            : base(name)
        {
        }
    }
    
}