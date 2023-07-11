using Newtonsoft.Json;

namespace YoutubeDownloader {
    internal class YoutubeMedia {

        private readonly Guid guid_ = System.Guid.NewGuid();

        public string Guid {
            get {
                return guid_.ToString();
            }
        }

        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "formats")]
        private List<Format>? Formats_ { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "webpage_url")]
        public string URL { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "channel")]
        public string ChannelName { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "requested_formats")]
        private List<Format>? BestFormats_ { get; set; }

        [JsonProperty(PropertyName = "duration_string")]
        public string Duration { get; set; } = string.Empty;

        public static YoutubeMedia? FetchMediaInfo(string url) {
            string jsonString = Utils.RunCommand($"yt-dlp.exe --dump-json {url}");
            if (string.IsNullOrEmpty(jsonString)) {
                return null;
            } else {
                return JsonConvert.DeserializeObject<YoutubeMedia>(jsonString);
            }
        }

        public List<Format> Formats {
            get {
                if (Formats_ != null) {
                    return Format.DeleteSimilarFormats(Formats_);
                }

                return new List<Format>();
            }
        }

        public List<Format> BestFormats {
            get {
                if (BestFormats_ != null) {
                    return BestFormats_;
                }

                return new List<Format>();
            }
        }
    }
}
