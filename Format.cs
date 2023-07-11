using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader {
    internal sealed class Format {

        [JsonProperty(PropertyName = "format_id")]
        public string FormatID { get; set; }

        [JsonProperty(PropertyName = "ext")]
        public string Ext { get; set; }

        [JsonProperty(PropertyName = "acodec")]
        public string Acodec { get; set; }

        [JsonProperty(PropertyName = "vcodec")]
        public string Vcodec { get; set; }

        [JsonProperty(PropertyName = "resolution")]
        public string Resolution { get; set; }

        [JsonProperty(PropertyName = "dynamic_range")]
        public string DynamicRange { get; set; }

        [JsonProperty(PropertyName = "fps")]
        public float? Fps { get; set; }

        [JsonProperty(PropertyName = "asr")]
        public int? Asr { get; set; }

        public Type FormatType {
            get {
                if (IsVideo()) {
                    return Type.VIDEO;
                } else if (IsAudio()) {
                    return Type.AUDIO;
                }
                return Type.UNKNOWN;
            }
            private set { FormatType = value; }
        }

        public enum Type {
            VIDEO, AUDIO, UNKNOWN
        }

        public const string VIDEO_FORMAT_STRING = "VideoFormatString";

        public string VideoFormatString {
            get {
                return $"{Resolution} | {Convert.ToInt16(Fps)} fps | {DynamicRange} | {Ext}";
            }
        }

        public const string AUDIO_FORMAT_STRING = "AudioFormatString";

        public string AudioFormatString {
            get {
                return $"{Acodec} | {Asr / 1000}kHz";
            }
        }

        public static List<Format> DeleteSimilarFormats(List<Format> formats) {
            for (int first = 0; first < formats.Count - 1; first++) {
                for (int second = first + 1; second < formats.Count; second++) {
                    if (formats[first].Equals(formats[second])) {
                        formats.RemoveAt(second);
                        second--;
                    }
                }
            }

            return formats;
        }

        private bool Equals(Format format) {
            return (Ext == format.Ext && Acodec == format.Acodec && Vcodec == format.Vcodec &&
                Resolution == format.Resolution && DynamicRange == format.DynamicRange &&
                 Fps == format.Fps && Asr == format.Asr);
        }

        private bool HasVideo() {
            return Vcodec != "none" && Vcodec != null;
        }

        private bool HasAudio() {
            return Acodec != "none" && Acodec != null;
        }

        private bool IsVideo() {
            return HasVideo() && !HasAudio();
        }

        private bool IsAudio() {
            return !HasVideo() && HasAudio();
        }
    }
}
