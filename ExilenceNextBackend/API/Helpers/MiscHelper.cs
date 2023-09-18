namespace API.Helpers
{
    using System.Linq;

    public static class MiscHelper
    {
        public static string VersionFromUserAgent(string useragent)
        {
            var exilenceHeader = useragent.Split(' ').Where(s => s.Contains("exilence")).First();
            var version = exilenceHeader.Split('/').Last();
            return version;
        }
    }
}
