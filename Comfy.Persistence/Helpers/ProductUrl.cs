using Humanizer;
using NickBuhro.Translit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comfy.Persistence.Helpers
{
    public static class ProductUrl
    {
        public static string Create(string productName, int productCode)
        {
            string url = productName
                    .ToLower()
                    .Replace(" ", "-")
                    .Replace(".", "-")
                    .Replace("/", "-")
                    .Replace("\\", "-")
                    .Replace("(", "-")
                    .Replace(")", "-")
                    .Replace("ґ", "г")
                    .Replace("є", "е")
                    .Replace("і", "и")
                    .Replace("ї", "йи")
                    .Replace("[", "-")
                    .Replace("]", "-")
                    .Replace("{", "-")
                    .Replace("}", "-")
                    .Replace("?", "-")
                    .Replace("!", "-")
                    .Replace("----", "-")
                    .Replace("---", "-")
                    .Replace("--", "-")
                    .Dasherize();
            url = Transliteration.CyrillicToLatin(url)
                .Replace("`", "");

            if (url.Last() != '-') url += "-";
            url += $"{productCode}";

            return url;
        }

        public static string GetUrlQuery(Dictionary<string, List<string>> dict)
        {
            var sbuilder = new StringBuilder(10);
            foreach (var x in dict)
            {
                foreach (var value in x.Value)
                {
                    sbuilder.Append($"{x.Key}={value}&");
                }
            }
            if (sbuilder.Length > 0)
            {
                sbuilder.Remove(sbuilder.Length - 1, 1);
            }
            return sbuilder.ToString();
        }

        public static bool TryRemoveEmptyAndDuplicatesFromQuery(string? query, out Dictionary<string, List<string>> queryDictionary)
        {
            queryDictionary = new Dictionary<string, List<string>>();
            bool queryChanged = false;
            if (String.IsNullOrEmpty(query) == false)
            {
                string[] pairs = query.Split('&');
                foreach (string pair in pairs)
                {
                    string[] parts = pair.Split('=');
                    if (queryDictionary.TryGetValue(parts[0], out List<string>? values))
                    {
                        if (String.IsNullOrEmpty(parts[0]))
                        {
                            queryDictionary.Remove(parts[0]);
                            queryChanged = true;
                            continue;
                        }
                        if (String.IsNullOrEmpty(parts[1]))
                        {
                            queryDictionary.Remove(parts[1]);
                            queryChanged = true;
                            continue;
                        }
                        if (values.Contains(parts[1]))
                        {
                            values.Remove(parts[1]);
                            if (queryDictionary.TryGetValue(parts[0], out values))
                            {
                                if (values.Count <= 0)
                                {
                                    queryDictionary.Remove(parts[0]);
                                }
                            }
                            queryChanged = true;
                        }
                        else
                        {
                            values.Add(parts[1]);
                        }
                    }
                    else
                    {
                        queryDictionary.Add(parts[0], new List<string>() { parts[1] });
                    }
                }
            }
            return queryChanged;
        }
    }
}
