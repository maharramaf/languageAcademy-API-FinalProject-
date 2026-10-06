using System.Text.RegularExpressions;

namespace Domain.Common
{
    public static class MapEmbed
    {
        public static string Normalize(string? value)
        {
            var raw = (value ?? string.Empty).Trim();
            if (raw.Length == 0) return string.Empty;

            var src = raw;
            var iframe = Regex.Match(raw, "src\\s*=\\s*[\"'](?<src>[^\"']+)[\"']", RegexOptions.IgnoreCase);
            if (iframe.Success)
                src = iframe.Groups["src"].Value.Trim();

            src = src.Replace("&amp;", "&", StringComparison.OrdinalIgnoreCase);
            if (src.Length is 0 or > 2000) return string.Empty;
            if (!Uri.TryCreate(src, UriKind.Absolute, out var uri)) return string.Empty;
            if (!string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            var host = uri.Host.ToLowerInvariant();
            var google = host == "google.com"
                || host.EndsWith(".google.com", StringComparison.Ordinal)
                || Regex.IsMatch(host, @"^(www|maps)\.google\.[a-z]{2,10}$");
            if (!google) return string.Empty;

            var path = uri.AbsolutePath.ToLowerInvariant();
            if (!path.Contains("/maps/embed", StringComparison.Ordinal))
                return string.Empty;

            return src;
        }
    }
}
