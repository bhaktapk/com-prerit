using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Com.Prerit.Web.Controllers
{
    public abstract class ApplicationController<TModelBase> : Controller
    {
        #region Methods

        public static string GetControllerNameWithoutSuffix<TController>() where TController : Controller
        {
            const string suffix = "controller";

            string controllerName = typeof(TController).Name;

            if (!controllerName.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception(string.Format("Controller name '{0}' does not contain the suffix '{1}'", controllerName, suffix));
            }

            return controllerName.Remove(controllerName.Length - suffix.Length);
        }

        public static string GetSeoControllerName<TController>() where TController : Controller
        {
            const char separator = '-';

            string controllerName = GetControllerNameWithoutSuffix<TController>();

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

        public abstract TDerivedModel UpdateModelBase<TDerivedModel>(TDerivedModel model) where TDerivedModel : TModelBase;

        #endregion
    }
}