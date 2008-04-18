using System;
using System.Diagnostics;
using System.Web;

namespace Framework.Web
{
    public static class VirtualPathUtilityLinq
    {
        #region Methods

        public static bool IsPathVirtual(string path)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    //TODO: use regular expressions
                    result = VirtualPathUtility.IsAppRelative(path) || VirtualPathUtility.IsAbsolute(path);
                }
                catch (Exception e)
                {
                    //swallow exception because an "Is" method should never throw an exception
                    Trace.TraceInformation(e.ToString());
                }
            }

            return result;
        }

        #endregion
    }
}