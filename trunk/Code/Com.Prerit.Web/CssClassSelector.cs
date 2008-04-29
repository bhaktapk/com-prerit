namespace Com.Prerit.Web
{
    public class CssClassSelector : StringEnum<CssClassSelector>
    {
        #region Fields

        private static readonly CssClassSelector _active = new CssClassSelector("active");

        private static readonly CssClassSelector _formError = new CssClassSelector("formError");

        #endregion

        #region Properties

        public static CssClassSelector Active
        {
            get { return _active; }
        }

        public static CssClassSelector FormError
        {
            get { return _formError; }
        }

        #endregion

        #region Constructors

        protected CssClassSelector(string name)
            : base(name)
        {
        }

        #endregion
    }
}