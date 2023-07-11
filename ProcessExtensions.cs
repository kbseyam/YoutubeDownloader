using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader {
    internal static class ProcessExtensions {
        public static List<Process> GetChildProcesses(this Process process) {
            List<Process> children = new List<Process>();
            if (process != null) {
                ManagementObjectSearcher mos =
                new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessID={process.Id}");

                foreach (ManagementObject mo in mos.Get()) {
                    children.Add(Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])));
                }
            }

            return children;
        }
    }
}
