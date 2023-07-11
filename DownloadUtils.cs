namespace YoutubeDownloader {
    internal class DownloadUtils {
        public static void Download(DownloadPreferences? downloadPreferences, Utils.EventOutputCommand eventOutput) {
            if (downloadPreferences == null) { return; }


            Utils.RunCommand($"yt-dlp.exe -f {downloadPreferences.FormatString} -o {downloadPreferences.media.Guid}.%(ext)s {downloadPreferences.media.URL}", eventOutput);

            FileInfo[] filesInfoStartWithGuid = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles($"{downloadPreferences.media.Guid}*");

            if (filesInfoStartWithGuid.Count() != 0)
            {
                if (downloadPreferences.path != null) {
                    if (File.Exists(downloadPreferences.path)) {
                        File.Delete(downloadPreferences.path);
                    }

                    string tempFilePath = filesInfoStartWithGuid[0].FullName;

                    if (File.Exists(tempFilePath)) {
                        File.Move(tempFilePath, downloadPreferences.path);
                    }
                }
            }
        }

    }
}
