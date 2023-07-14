using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader {
    internal static class CenteredMessageBox {
        public static DialogResult Show(Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) {
            using (new CenterWinDialog(form)) {
                return MessageBox.Show(text, caption, buttons, icon);
            }
        }
    }
}
