namespace YoutubeDownloader {
    internal class DownloadUtils {
        public static void Download(DownloadPreferences? downloadPreferences, Utils.EventOutputCommand eventOutput) {
            if (downloadPreferences == null) { return; }


            Utils.RunCommand($"yt-dlp.exe -f {downloadPreferences.FormatString} -o {downloadPreferences.media.ID}.%(ext)s {downloadPreferences.media.URL}", eventOutput);

            if (downloadPreferences.path != null) {
                if (File.Exists(downloadPreferences.path)) {
                    File.Delete(downloadPreferences.path);
                }

                if (File.Exists($"{downloadPreferences.media.ID}.{downloadPreferences.FileExt}")) {
                    File.Move($"{downloadPreferences.media.ID}.{downloadPreferences.FileExt}", downloadPreferences.path);
                }
            }

        }

    }
}
