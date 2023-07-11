using System.Diagnostics;
using System.Management;

namespace YoutubeDownloader {
    internal static class ProcessExtensions {
        public static List<Process> GetChildProcesses(this Process process) {
            List<Process> children = new();
            if (process != null) {
                ManagementObjectSearcher mos =
                    new($"Select * From Win32_Process Where ParentProcessID={process.Id}");

                children.AddRange(from ManagementObject mo in mos.Get()
                                  select Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])));
            }

            return children;
        }
    }
}
