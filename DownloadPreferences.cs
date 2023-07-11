namespace YoutubeDownloader {
    internal class DownloadPreferences {

        public DownloadPreferences(YoutubeMedia media, Format? videoFormat, Format? audioFormat) {
            this.videoFormat = videoFormat;
            this.audioFormat = audioFormat;
            this.media = media;
        }

        public YoutubeMedia media;
        public Format? videoFormat;
        public Format? audioFormat;
        public string? path;

        public string? FileExt {
            get {
                if (videoFormat != null) {
                    return videoFormat.Ext;
                }
                if (audioFormat != null) {
                    return audioFormat.Ext;
                }

                return null;
            }
        }

        public string FormatString {
            get {
                string format = string.Empty;

                if (videoFormat != null) {
                    format += videoFormat.FormatID;
                    if (audioFormat != null) {
                        format += '+' + audioFormat.FormatID;
                    }
                } else {
                    if (audioFormat != null) {
                        format += audioFormat.FormatID;
                    }
                }

                return format;
            }
        }
    }
}
