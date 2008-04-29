namespace Com.Prerit.Web
{
    public sealed class CssClassSelector : StringEnum<CssClassSelector>
    {
        #region Fields

        public static readonly CssClassSelector Active = new CssClassSelector("active");

        public static readonly CssClassSelector FormError = new CssClassSelector("formError");

        #endregion

        #region Constructors

        private CssClassSelector(string name)
            : base(name)
        {
        }

        #endregion
    }
}