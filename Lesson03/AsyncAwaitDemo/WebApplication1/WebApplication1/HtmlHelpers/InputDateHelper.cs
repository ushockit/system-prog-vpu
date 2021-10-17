using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.HtmlHelpers
{
    public static class InputDateHelper
    {
        public static IHtmlContent DateInput<T>(
            this IHtmlHelper<T> html, 
            string expression, 
            DateTime date, 
            object htmlAttributes)
        {
            string[] values = date.ToShortDateString().Split(".");
            string value = $"{values[2]}-{values[1]}-{values[0]}";

            return html.TextBox(expression, value, htmlAttributes);
        }
    }
}
