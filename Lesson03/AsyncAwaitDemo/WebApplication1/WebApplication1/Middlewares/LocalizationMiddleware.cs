using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Utils;

namespace WebApplication1.Middlewares
{
    public class LocalizationMiddleware
    {
        readonly RequestDelegate _next;
        readonly AppUtils _utils;
        public LocalizationMiddleware(RequestDelegate next, AppUtils utils)
        {
            _next = next;
            _utils = utils;
        }
        public async Task Invoke(HttpContext context)
        {
            var queryLangParam = context.Request.Query["lang"].ToString();
            var lang = queryLangParam.Equals("") ? _utils.DefaultCultureName : queryLangParam;

            try
            {
                CultureInfo.CurrentCulture = new CultureInfo(lang);
                CultureInfo.CurrentUICulture = new CultureInfo(lang);
            }
            catch (CultureNotFoundException) { }
            await _next.Invoke(context);
        }
    }
}
