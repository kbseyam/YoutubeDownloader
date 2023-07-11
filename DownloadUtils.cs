using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader {
    internal class DownloadUtils {
        public static void Download(DownloadPreferences downloadPreferences, Utils.EventOutputCommand eventOutput) {
            Utils.RunCommand($"yt-dlp.exe -f {downloadPreferences.FormatString} -o {downloadPreferences.media.ID}.%(ext)s {downloadPreferences.media.URL}", eventOutput);

            if (File.Exists(downloadPreferences.path)) {
                File.Delete(downloadPreferences.path);
            }

            if (File.Exists($"{downloadPreferences.media.ID}.{downloadPreferences.FileExt}")) {
                File.Move($"{downloadPreferences.media.ID}.{downloadPreferences.FileExt}", downloadPreferences.path);
            }
        }

    }
}
