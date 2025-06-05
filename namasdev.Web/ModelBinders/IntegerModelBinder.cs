using System;
using System.Globalization;
using System.Web.Mvc;

namespace namasdev.Web.ModelBinders
{
    public class IntegerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null
                || String.IsNullOrWhiteSpace(value.AttemptedValue))
            {
                return bindingContext.ModelType.IsGenericType
                    ? (int?)null
                    : default(int);
            }

            int valor;
            if (!int.TryParse(value.AttemptedValue, NumberStyles.Any, CultureInfo.CurrentUICulture, out valor))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Número no válido.");
                return default(int);
            }

            return valor;
        }
    }
}