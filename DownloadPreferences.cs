using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

namespace YoutubeDownloader {
    internal class DownloadPreferences {

        public DownloadPreferences(YoutubeMedia media, Format videoFormat, Format audioFormat) {
            this.videoFormat = videoFormat;
            this.audioFormat = audioFormat;
            this.media = media;
        }

        public YoutubeMedia media;
        public Format videoFormat;
        public Format audioFormat;
        public string path;

        public string FileExt {
            get {
                if (videoFormat != null) {
                    return videoFormat.Ext;
                }

                return audioFormat.Ext;
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
