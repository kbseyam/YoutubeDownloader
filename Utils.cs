using System.Diagnostics;

namespace YoutubeDownloader {
    internal sealed class Utils {
        public const string COMMAND_FINISH = "the command is finish";

        public static Process? Process { get; set; }

        public static bool ExistsOnPath(string fileName) {
            return GetFullPath(fileName) != null;
        }

        private static string? GetFullPath(string fileName) {
            if (File.Exists(fileName)) {
                return Path.GetFullPath(fileName);
            }

            string? values = Environment.GetEnvironmentVariable("PATH");
            if (values != null) {
                foreach (string path in values.Split(Path.PathSeparator)) {
                    string fullPath = Path.Combine(path, fileName);
                    if (File.Exists(fullPath)) {
                        return fullPath;
                    }
                }
            }


            return null;
        }

        public static void CheckRequirements() {
            Check_ytdlp();
            Check_ffmpeg();
            Check_ffprobe();
        }

        private static void Check_ytdlp() {
            if (!ExistsOnPath("yt-dlp.exe")) {
                MessageBox.Show("yt-dlp.exe not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private static void Check_ffmpeg() {
            if (!ExistsOnPath("ffmpeg.exe")) {
                MessageBox.Show("ffmpeg not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private static void Check_ffprobe() {
            if (!ExistsOnPath("ffprobe.exe")) {
                MessageBox.Show("ffprobe not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public delegate void EventOutputCommand(string output);

        public static string RunCommand(string command) {
            ProcessStartInfo startInfo = new() {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process = new Process {
                StartInfo = startInfo,
            };
            Process.Start();

            string result = Process.StandardOutput.ReadToEnd();

            Process = null;
            return result;
        }

        public static string RunCommand(string command, EventOutputCommand eventOutput) {
            string output = string.Empty;

            ProcessStartInfo startInfo = new() {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                Arguments = $"/c {command} && echo {COMMAND_FINISH}",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process = new Process {
                StartInfo = startInfo
            };
            Process.OutputDataReceived += (sender, args) => {
                if (Process != null) {
                    if (!Process.HasExited) {
                        if (!string.IsNullOrEmpty(args.Data)) {
                            output += args.Data + '\n';
                            eventOutput(args.Data);
                        }
                    }
                }

            };
            Process.Start();
            Process.BeginOutputReadLine();
            Process.WaitForExit();

            Process = null;

            return output;
        }

        public static string GetValidFileName(string fileName) {
            fileName = fileName.Replace('?', '？');
            char[] chars = Path.GetInvalidFileNameChars();
            foreach (char c in chars) {
                fileName = fileName.Replace(c, '_');
            }

            return fileName;
        }

        public static bool IsOneCommand(string command) {
            return command.IndexOfAny(new char[] { '&', '|', '<', '>' }) == -1;
        }

        public static List<FileInfo> GetFilesStartWith(string startWith) {
            List<FileInfo> files = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles().ToList();

            for (int i = 0; i < files.Count; i++) {
                if (!files[i].Name.StartsWith(startWith)) {
                    files.RemoveAt(i);
                    i--;
                }
            }

            return files;
        }

        public static void RemoveFilesStartsWith(string startWith) {
            List<FileInfo> files = GetFilesStartWith(startWith);
            foreach (FileInfo file in files) {
                File.Delete(file.FullName);
            }
        }

        public static bool IsYoutubeVideoUrl(string url) {
            if (IsOneCommand(url)) {
                if ((url.StartsWith("https://www.youtube.com/watch?v=") ||
                    url.StartsWith("http://www.youtube.com/watch?v=")) && !url.EndsWith("=")) {
                    return true;
                }
            }

            return false;
        }
    }
}
