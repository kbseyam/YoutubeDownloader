using Newtonsoft.Json;
using System.ComponentModel;

namespace YoutubeDownloader {
    internal sealed class Format {

        public enum Type {
            VIDEO, AUDIO, UNKNOWN
        }

        [JsonProperty(PropertyName = "format_id")]
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

        [JsonProperty(PropertyName = "audio_channels")]
        public int? NumberOfAudioChannels { get; private set; }

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
                string formatString = string.Empty;
                if (Properties.Settings.Default.ShowVideoID) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? $"ID: {FormatID}" : $" | ID: {FormatID}";
                }
                if (Properties.Settings.Default.ShowResolution) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? Resolution : $" | {Resolution}";
                }
                if (Properties.Settings.Default.ShowFps) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? $"FPS: {Fps}" : $" | FPS: {Fps}";
                }
                if (Properties.Settings.Default.ShowDynamicRange) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? DynamicRange : $" | {DynamicRange}";
                }
                if (Properties.Settings.Default.ShowVideoFileExtension) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? Ext : $" | {Ext}";
                }
                if (Properties.Settings.Default.ShowVideoCodec) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? $"Codec: {Vcodec}" : $" | Codec: {Vcodec}";
                }
                return formatString;
            }
        }

        public const string AUDIO_FORMAT_STRING = "AudioFormatString";

        public string AudioFormatString {
            get {
                string formatString = string.Empty;
                if (Properties.Settings.Default.ShowAudioID) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? $"ID: {FormatID}" : $" | ID: {FormatID}";
                }
                if (Properties.Settings.Default.ShowASR) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? $"ASR: {Asr / 1000}kHz" : $" | ASR: {Asr / 1000}kHz";
                }
                if (Properties.Settings.Default.ShowNumberOfAudioChannels) {
                    if (NumberOfAudioChannels == 1) {
                        formatString += (string.IsNullOrEmpty(formatString)) ? $"{NumberOfAudioChannels} Channel" : $" | {NumberOfAudioChannels} Channel";
                    } else {
                        formatString += (string.IsNullOrEmpty(formatString)) ? $"{NumberOfAudioChannels} Channels" : $" | {NumberOfAudioChannels} Channels";
                    }
                }
                if (Properties.Settings.Default.ShowAudioCodec) {
                    formatString += (string.IsNullOrEmpty(formatString)) ? $"Codec: {Acodec}" : $" | Codec: {Acodec}";
                }

                return formatString;
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
