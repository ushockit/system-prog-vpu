using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class AppUtils
    {
        public string DefaultCultureName => "ru";
        public CultureInfo[] AvailableCultures => new CultureInfo[]
        {
            new CultureInfo("en"),
            new CultureInfo("ru"),
            new CultureInfo("de"),
        };
        public string[] AvailableLanguages => new string[] { "ru", "en", "de" };

        public void ChangeCulture(string cultureName)
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
        }
    }
}
