﻿using System;
using System.Linq;
using System.Web.Mvc;

using Castle.Components.Validator;

namespace Com.Prerit.Web
{
    public static class ModelStateDictionaryExtensions
    {
        #region Methods

        public static void AddModelErrors(this ModelStateDictionary modelState, ErrorSummary errorSummary)
        {
            if (errorSummary == null)
            {
                throw new ArgumentNullException("errorSummary");
            }

            var errorInfos = from property in errorSummary.InvalidProperties
                             from message in errorSummary.GetErrorsForProperty(property)
                             select new
                                        {
                                            PropertyName = property,
                                            ErrorMessage = message
                                        };

            foreach (var errorInfo in errorInfos)
            {
                modelState.AddModelError(errorInfo.PropertyName, errorInfo.ErrorMessage);
            }
        }

        #endregion
    }
}