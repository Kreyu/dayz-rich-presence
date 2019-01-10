using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using TinyJson;

namespace DZRichPresenceClient.Misc
{
    public static class GitHubApi
    {
        public static Version GetLatestReleaseVersion()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpWebRequest request = WebRequest.Create("https://api.github.com/repos/Kreyu/dayz-rich-presence/releases/latest") as HttpWebRequest;

            request.Method = "GET";
            request.Accept = "application/vnd.github.v3+json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
            request.ServicePoint.Expect100Continue = false;

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        ReleaseMetadata metadata = reader.ReadToEnd().FromJson<ReleaseMetadata>();
                        string version = Regex.Replace(metadata.TagName, "[^0-9.]", "");

                        return new Version(version);
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        private class ReleaseMetadata
        {
            [DataMember(Name="tag_name")]
            public string TagName { get; set; }
        }
    }
}
