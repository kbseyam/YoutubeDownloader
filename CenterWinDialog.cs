using System.Runtime.InteropServices;
using System.Text;

namespace YoutubeDownloader {
    internal partial class CenterWinDialog : IDisposable {
        private int mTries = 0;
        private Form mOwner;

        public CenterWinDialog(Form owner) {
            mOwner = owner;
            owner.BeginInvoke(new MethodInvoker(FindDialog));
        }

        private void FindDialog() {
            if (mTries < 0) return;
            EnumThreadWndProc callback = new(CheckWindow);
            if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero)) {
                if (++mTries < 10) mOwner.BeginInvoke(new MethodInvoker(FindDialog));
            }
        }
        private bool CheckWindow(IntPtr hWnd, IntPtr lp) {
            StringBuilder sb = new(260);
            _ = GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() != "#32770") return true;

            Rectangle frmRect = new(mOwner.Location, mOwner.Size);
            GetWindowRect(hWnd, out RECT dlgRect);
            MoveWindow(hWnd,
                frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                dlgRect.Right - dlgRect.Left,
                dlgRect.Bottom - dlgRect.Top, true);
            return false;
        }
        public void Dispose() {
            mTries = -1;
        }

        // P/Invoke declarations
        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
        [LibraryImport("kernel32.dll")]
        private static partial int GetCurrentThreadId();
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetWindowRect(IntPtr hWnd, out RECT rc);
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, [MarshalAs(UnmanagedType.Bool)] bool repaint);
        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
    }
}
