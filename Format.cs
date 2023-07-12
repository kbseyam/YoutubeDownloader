using Newtonsoft.Json;
using System.ComponentModel;

namespace YoutubeDownloader {
    internal sealed class Format {

        public enum Type {
            VIDEO, AUDIO, UNKNOWN
        }

        [DefaultValue("")]
        [JsonProperty(PropertyName = "format_id", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string FormatID { get; private set; }

        [JsonProperty(PropertyName = "ext")]
        public string? Ext { get; private set; }

        [JsonProperty(PropertyName = "acodec")]
        public string? Acodec { get; private set; }

        [JsonProperty(PropertyName = "vcodec")]
        public string? Vcodec { get; private set; }

        [JsonProperty(PropertyName = "resolution")]
        public string? Resolution { get; private set; }

        [JsonProperty(PropertyName = "dynamic_range")]
        public string? DynamicRange { get; private set; }

        [JsonProperty(PropertyName = "fps")]
        public float? Fps { get; private set; }

        [JsonProperty(PropertyName = "asr")]
        public int? Asr { get; private set; }

        public Type FormatType {
            get {
                if (IsVideo()) {
                    return Type.VIDEO;
                } else if (IsAudio()) {
                    return Type.AUDIO;
                }
                return Type.UNKNOWN;
            }
        }

        [JsonConstructor]
        public Format(string formatID, string? ext, string? acodec, string? vcodec, string? resolution, string? dynamicRange, float? fps, int? asr) {
            FormatID = formatID;
            Ext = ext;
            Acodec = acodec;
            Vcodec = vcodec;
            Resolution = resolution;
            DynamicRange = dynamicRange;
            Fps = fps;
            Asr = asr;
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
