using System.Text;

namespace Comfy.Application.Common.Helpers;

public static class ProductUrl
{
    public static string GetUrlQuery(Dictionary<string, List<string>> dict)
    {
        var sBuilder = new StringBuilder(10);
        foreach (var x in dict)
        {
            foreach (var value in x.Value)
            {
                sBuilder.Append($"{x.Key}={value}&");
            }
        }
        if (sBuilder.Length > 0)
        {
            sBuilder.Remove(sBuilder.Length - 1, 1);
        }
        return sBuilder.ToString();
    }

    public static bool TryRemoveEmptyAndDuplicatesFromQuery(string? query, out Dictionary<string, List<string>> queryDictionary)
    {
        queryDictionary = new Dictionary<string, List<string>>();
        if (string.IsNullOrEmpty(query) || query.Contains('=') == false)
        {
            return false;
        }
        var queryChanged = false;
        var pairs = query.Split('&');
        foreach (var pair in pairs)
        {
            var parts = pair.Split('=');
            if (queryDictionary.TryGetValue(parts[0], out var values))
            {
                if (string.IsNullOrEmpty(parts[0]))
                {
                    queryDictionary.Remove(parts[0]);
                    queryChanged = true;
                    continue;
                }
                if (string.IsNullOrEmpty(parts[1]))
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
                queryDictionary.Add(parts[0], new List<string> { parts[1] });
            }
        }
        return queryChanged;
    }
}