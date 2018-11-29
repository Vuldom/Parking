using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Extensions
{
    public static class ViewDataExtensions
    {
        public static (bool HasErrors, IHtmlContent Errors) GetModelStateErrors<T>(this ViewDataDictionary<T> dictionary, IJsonHelper helper)
        {
            var errors = dictionary.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
            return (errors.Length > 0, helper.Serialize(errors));
        }
    }
}
