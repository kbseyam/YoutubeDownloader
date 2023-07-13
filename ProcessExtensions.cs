using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace YoutubeDownloader {
    internal static partial class ProcessExtensions {
        [Flags]
        private enum ThreadAccess : int {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [LibraryImport("kernel32.dll")]
        private static partial IntPtr OpenThread(ThreadAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwThreadId);
        [LibraryImport("kernel32.dll")]
        private static partial uint SuspendThread(IntPtr hThread);
        [LibraryImport("kernel32.dll")]
        private static partial int ResumeThread(IntPtr hThread);

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

        public static void Suspend(this Process process) {
            foreach (ProcessThread thread in process.Threads) {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero) {
                    break;
                }
                SuspendThread(pOpenThread);
            }
        }
        public static void Resume(this Process process) {
            foreach (ProcessThread thread in process.Threads) {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero) {
                    break;
                }
                ResumeThread(pOpenThread);
            }
        }
    }
}
