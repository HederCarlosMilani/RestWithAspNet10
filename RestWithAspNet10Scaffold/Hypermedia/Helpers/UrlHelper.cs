using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNet10Scaffold.Hypermedia.Helpers;

public static class UrlHelper
{
    private static readonly object _lock = new();
    public static string GenerateUrl(this IUrlHelper urlHelper, string routeName, string path )
    {
        lock (_lock)
        {
            var url = urlHelper.Link(routeName, new { controller = path }) ?? string.Empty;
            
            return url.Replace("%2f", "/").ToLower().TrimEnd("/".ToCharArray());
        }
    }
}