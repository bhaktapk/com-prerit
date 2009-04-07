using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Com.Prerit.Web.Models.Shared;

namespace Com.Prerit.Web.Controllers
{
    public abstract class DefaultMasterController : Controller
    {
        #region Methods

        public T CreateBaseModel<T>() where T : DefaultMasterModel, new()
        {
            {
                return new T
                           {
                               ContentEncoding = Response.ContentEncoding.WebName,
                               ContentType = Response.ContentType,
                               CopyrightBeginYear = WebsiteInfo.DomainRegistrationYear,
                               CopyrightEndYear = DateTime.Today.Year,
                               SiteName = WebsiteInfo.SiteName,
                               ValidationUri = Request.Url
                           };
            }
        }

        public static string GetControllerNameWithoutSuffix<T>() where T : Controller
        {
            const string suffix = "controller";

            string controllerName = typeof(T).Name;

            if (!controllerName.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception(string.Format("Controller name '{0}' does not contain the suffix '{1}'", controllerName, suffix));
            }

            return controllerName.Remove(controllerName.Length - suffix.Length);
        }

        public static string GetSeoControllerName<T>() where T : Controller
        {
            const char separator = '-';

            string controllerName = GetControllerNameWithoutSuffix<T>();

            bool firstLetterFound = false;

            var seoChars = new List<char>();

            foreach (char character in controllerName)
            {
                if (char.IsLetterOrDigit(character))
                {
                    if (char.IsUpper(character))
                    {
                        if (firstLetterFound)
                        {
                            seoChars.Add(separator);
                        }

                        seoChars.Add(char.ToLowerInvariant(character));
                    }
                    else
                    {
                        seoChars.Add(character);
                    }

                    firstLetterFound = true;
                }
            }

            return new string(seoChars.ToArray());
        }

        #endregion
    }
}