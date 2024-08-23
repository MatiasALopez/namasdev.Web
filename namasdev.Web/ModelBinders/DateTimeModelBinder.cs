﻿using System;
using System.Globalization;
using System.Web.Mvc;

namespace namasdev.Web.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null 
                || String.IsNullOrWhiteSpace(value.AttemptedValue) 
                || value.AttemptedValue.Length < 8)
            {
                return bindingContext.ModelType.IsGenericType
                    ? (DateTime?)null
                    : default(DateTime);
            }

            DateTime dateTime;
            if (!DateTime.TryParse(value.AttemptedValue.Split(' ')[0], CultureInfo.CurrentUICulture, DateTimeStyles.None, out dateTime))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Fecha/Hora no válida.");
                return default(DateTime);
            }

            return dateTime;
        }
    }
}