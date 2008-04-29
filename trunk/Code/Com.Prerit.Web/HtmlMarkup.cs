namespace Com.Prerit.Web
{
    public sealed class HtmlMarkup : StringEnum<HtmlMarkup>
    {
        #region Fields

        public static readonly HtmlMarkup AccessKey = new HtmlMarkup("accesskey");

        public static readonly HtmlMarkup Class = new HtmlMarkup("class");

        public static readonly HtmlMarkup OnClick = new HtmlMarkup("onclick");

        #endregion

        #region Constructors

        private HtmlMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}