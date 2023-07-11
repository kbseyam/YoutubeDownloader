using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader {
    internal class YoutubeMedia {

        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "formats")]
        public List<Format> Formats { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "webpage_url")]
        public string URL { get; set; }

        [JsonProperty(PropertyName = "ext")]
        public string Ext { get; set; }

        [JsonProperty(PropertyName = "channel")]
        public string ChannelName { get; set; }

        [JsonProperty(PropertyName = "requested_formats")]
        public List<Format> BestFormats { get; set; }

        [JsonProperty(PropertyName = "duration_string")]
        public string Duration { get; set; }

        public static YoutubeMedia FetchMediaInfo(string url) {
            string jsonString = Utils.RunCommand($"yt-dlp.exe --dump-json {url}");
            if (string.IsNullOrEmpty(jsonString)) {
                throw new Exception();
            } else {
                return JsonConvert.DeserializeObject<YoutubeMedia>(jsonString);
            }
        }

        public List<Format> GetFormats() {
            return Format.DeleteSimilarFormats(Formats);
        }
    }
}
