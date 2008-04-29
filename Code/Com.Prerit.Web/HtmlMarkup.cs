namespace Com.Prerit.Web
{
    public class HtmlMarkup : StringEnum<HtmlMarkup>
    {
        #region Fields

        private static readonly HtmlMarkup _accessKey = new HtmlMarkup("accesskey");

        private static readonly HtmlMarkup _class = new HtmlMarkup("class");

        private static readonly HtmlMarkup _onClick = new HtmlMarkup("onclick");

        #endregion

        #region Properties

        public static HtmlMarkup AccessKey
        {
            get { return _accessKey; }
        }

        public static HtmlMarkup Class
        {
            get { return _class; }
        }

        public static HtmlMarkup OnClick
        {
            get { return _onClick; }
        }

        #endregion

        #region Constructors

        protected HtmlMarkup(string name)
            : base(name)
        {
        }

        #endregion
    }
}