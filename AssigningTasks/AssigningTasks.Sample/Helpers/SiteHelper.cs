using System;
using Microsoft.AspNetCore.Http;

namespace AssigningTasks.Sample.Helpers
{
    public class SiteHelper
    {
        public static string GetBaseUrl(HttpRequest context)
        {
            return $"{context.Scheme}://{context.Host}{context.PathBase}";
        }

        public static string GetCurrentUrl(HttpRequest context)
        {
            return $"{context.Scheme}://{context.Host}{context.PathBase}{context.Path}";
        }
    }
}
