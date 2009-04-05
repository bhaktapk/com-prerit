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
                               CopyrightBeginYear = WebsiteInfo.DomainRegistrationYear,
                               CopyrightEndYear = DateTime.Today.Year,
                               SiteName = WebsiteInfo.SiteName,
                               ValidationUri = Request.Url,
                               WebsiteAuthor = WebsiteInfo.Author
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

        public static string GetSeoFriendlyName<T>() where T : Controller
        {
            const char separator = '-';

            string s = GetControllerNameWithoutSuffix<T>();

            bool firstLetterFound = false;

            var word = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {
                char letter = s[i];

                if (char.IsLetterOrDigit(letter))
                {
                    if (char.IsUpper(letter))
                    {
                        if (firstLetterFound)
                        {
                            word.Add(separator);
                        }

                        word.Add(char.ToLowerInvariant(letter));
                    }
                    else
                    {
                        word.Add(letter);
                    }

                    firstLetterFound = true;
                }
            }

            return new string(word.ToArray());
        }

        #endregion
    }
}