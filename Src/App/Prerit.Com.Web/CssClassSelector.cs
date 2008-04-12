using Framework;

namespace Prerit.Com.Web
{
    public sealed class CssClassSelector : StringEnum<CssClassSelector>
    {
        public static readonly CssClassSelector Active = new CssClassSelector("active");

        public static readonly CssClassSelector FormError = new CssClassSelector("formError");

        private CssClassSelector(string name)
            : base(name)
        {
        }
    }
    
}