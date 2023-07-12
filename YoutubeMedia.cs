using Newtonsoft.Json;
using System.ComponentModel;

namespace YoutubeDownloader {
    internal class YoutubeMedia {

        public string Guid {
            get; private set;
        }

        [DefaultValue(0)]
        [JsonProperty(PropertyName = "id", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string ID { get; private set; }

        [DefaultValue("")]
        [JsonProperty(PropertyName = "webpage_url", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string URL { get; private set; }

        [DefaultValue("")]
        [JsonProperty(PropertyName = "channel", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string ChannelName { get; private set; }

        [DefaultValue("")]
        [JsonProperty(PropertyName = "title", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Title { get; private set; }

        [DefaultValue("")]
        [JsonProperty(PropertyName = "duration_string", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string DurationString { get; private set; }

        [JsonProperty(PropertyName = "formats")]
        public List<Format> Formats { get; private set; }

        [JsonProperty(PropertyName = "requested_formats")]
        public List<Format> BestFormats { get; private set; }

        [JsonConstructor]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private YoutubeMedia(string id, List<Format> formats, string title, string url, string channelName,
            List<Format> bestFormats, string durationString) {
            Guid = System.Guid.NewGuid().ToString();
            ID = id;
            URL = url;
            ChannelName = channelName;
            Title = title;
            DurationString = durationString;

            if (formats != null) {
                Formats = Format.DeleteSimilarFormats(formats);
            } else {
                Formats = new List<Format>();
            }

            if (bestFormats != null) {
                BestFormats = bestFormats;
            } else {
                BestFormats = new List<Format>();
            }
        }

        public static YoutubeMedia? FetchMediaInfo(string url) {
            if (Utils.IsYoutubeVideoUrl(url)) {
                string jsonString = Utils.RunCommand($"yt-dlp.exe --dump-json {url}");

                return JsonConvert.DeserializeObject<YoutubeMedia>(jsonString);
            }

            return null;
        }

        public void Download(DownloadPreferences downloadPreferences, Utils.EventOutputCommand eventOutput) {
            Utils.RunCommand($"yt-dlp.exe -f {downloadPreferences.FormatString} -o" +
                $" {Guid}.%(ext)s {URL}", eventOutput);

            FileInfo[] filesInfoStartWithVideoGuid = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles($"{Guid}*");

            if (filesInfoStartWithVideoGuid.Length != 0) {
                if (downloadPreferences.path != null) {
                    if (File.Exists(downloadPreferences.path)) {
                        File.Delete(downloadPreferences.path);
                    }

                    string tempFilePath = filesInfoStartWithVideoGuid[0].FullName;

                    if (File.Exists(tempFilePath)) {
                        File.Move(tempFilePath, downloadPreferences.path);
                    }
                }
            }
        }
    }
}
