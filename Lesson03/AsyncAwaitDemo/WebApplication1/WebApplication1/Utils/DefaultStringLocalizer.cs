using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class DefaultStringLocalizer : IStringLocalizer
    {
        Dictionary<string, Dictionary<string, string>> resources;

        public DefaultStringLocalizer()
        {
            var enDict = new Dictionary<string, string>
            {
                { "WelcomeTitle", "Welcome!!!" }
            };

            var ruDict = new Dictionary<string, string>
            {
                { "WelcomeTitle", "Добро пожаловать!" }
            };

            resources = new Dictionary<string, Dictionary<string, string>>
            {
                { "en", enDict },
                { "ru", ruDict }
            };
        }

        public LocalizedString this[string name]
        {
            get
            {
                var currCulture = CultureInfo.CurrentCulture.Name;
                string word = string.Empty;
                if (resources.ContainsKey(currCulture) && resources[currCulture].ContainsKey(name))
                {
                    word = resources[currCulture][name];
                }
                return new LocalizedString(name, word);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}
