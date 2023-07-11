using static System.Windows.Forms.DataFormats;
using System.Diagnostics;

namespace YoutubeDownloader {
    public partial class FormMain : Form {
        private bool invokeInProgress = false;
        private bool stopInvoking = false;
        private delegate void SafeCallDelgate_INT(int value);
        private delegate void SafeCallDelgate_STRING(Control control, string text);

        private YoutubeMedia media;
        private DownloadPreferences downloadPreferences;

        public FormMain() {
            InitializeComponent();
        }

        private void FormMain_Shown(object sender, EventArgs e) {
            Utils.CheckRequirements();
        }

        private void VisibleControlsForFetch(bool visible) {
            RbAudio.Visible = visible;
            RbCustom.Visible = visible;
            RbVideo.Visible = visible;
            RbVideoAudio.Visible = visible;
            LbChannelName.Visible = visible;
            Label2.Visible = visible;
            Label3.Visible = visible;
            LbVideoTitle.Visible = visible;
            LbPercentage.Visible = visible;
            LbStatus.Visible = visible;
            ProgressBarDownload.Visible = visible;
            ChkAudio.Visible = visible;
            ChkVideo.Visible = visible;
            BtnDownload.Visible = visible;
            CbAudio.Visible = visible;
            CbVideo.Visible = visible;
        }

        private async void BtnFetch_Click(object sender, EventArgs e) {
            VisibleControlsForFetch(false);
            pictureBox1.Enabled = true;
            pictureBox1.Visible = true;
            lbFetchingInfo.Visible = true;

            if (await Task.Run(() => FetchInfo())) {
                VisibleControlsForFetch(true);
            } else {
                VisibleControlsForFetch(false);
                pictureBox1.Enabled = false;
                pictureBox1.Visible = false;
                lbFetchingInfo.Visible = false;
                MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
            lbFetchingInfo.Visible = false;
        }

        private bool FetchInfo() {
            try {
                if (!Utils.IsYoutubeVideoUrl(TbURL.Text)) {
                    throw new Exception();
                }
                media = YoutubeMedia.FetchMediaInfo(TbURL.Text);
                RefreshControls(true);

                return true;
            } catch {
                media = null;
                RefreshControls(false);
                return false;
            }
        }

        private void RefreshControls(bool hasMedia) {
            RbVideoAudio.Enabled = hasMedia;
            RbVideo.Enabled = hasMedia;
            RbAudio.Enabled = hasMedia;
            RbCustom.Enabled = hasMedia;
            BtnDownload.Enabled = hasMedia;

            if (hasMedia) {
                LbVideoTitle.Text = media.Title;
                LbChannelName.Text = media.ChannelName;
            } else {
                LbVideoTitle.Text = string.Empty;
                LbChannelName.Text = string.Empty;
            }

            RefreshCbVideo();
            RefreshCbAudio();
        }

        private void RefreshCbVideo() {
            if (media != null) {
                CbVideo.DisplayMember = Format.VIDEO_FORMAT_STRING;
                CbVideo.DataSource = media.GetFormats().Where((item) => item.FormatType == Format.Type.VIDEO).ToList();
                CbVideo.SelectedIndex = 0;
            } else { CbVideo.DataSource = null; CbVideo.DisplayMember = null; }

        }

        private void RefreshCbAudio() {
            if (media != null) {
                CbAudio.DataSource = media.GetFormats().Where((item) => item.FormatType == Format.Type.AUDIO).ToList();
                CbAudio.DisplayMember = Format.AUDIO_FORMAT_STRING;
                CbAudio.SelectedIndex = 0;
            } else { CbAudio.DataSource = null; CbAudio.DisplayMember = null; }

        }

        private void ChkVideo_CheckedChanged(object sender, EventArgs e) {
            CbVideo.Enabled = ChkVideo.Checked;
        }

        private void ChkAudio_CheckedChanged(object sender, EventArgs e) {
            CbAudio.Enabled = ChkAudio.Checked;
        }

        private void ResetDownloadSControls() {
            SetTextSafe(LbStatus, string.Empty);
            SetTextSafe(LbPercentage, string.Empty);
            SetProgressBarValue(0);
            SaveFileDialog1.Reset();
        }

        private void BtnDownload_Click(object sender, EventArgs e) {
            ResetDownloadSControls();

            downloadPreferences = new DownloadPreferences(media, GetSelectedVideoFormat(), GetSelectedAudioFormat());
            SaveFileDialog1.Filter = $"{downloadPreferences.FileExt}|*.{downloadPreferences.FileExt}";
            SaveFileDialog1.FileName = Utils.GetValidFileName(media.Title);

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK) {
                downloadPreferences.path = SaveFileDialog1.FileName;
                SetTextSafe(LbStatus, "Starting download");

                Thread thread = new Thread(new ThreadStart(Download));
                thread.Start();
            }

        }

        private void Download() {
            DownloadUtils.Download(downloadPreferences,
                OnDownloadCommandOutput);
        }

        private void UpdateLbStatus(string output) {

            if (output == Utils.COMMAND_FINISH) {
                SetTextSafe(LbStatus, "Done");
                return;
            }

            if (!output.StartsWith("[download] Destination")) {
                return;
            }

            if (downloadPreferences.videoFormat != null) {
                if (downloadPreferences.audioFormat == null) {
                    SetTextSafe(LbStatus, "Downloading video");
                    return;
                } else if (output.EndsWith($"{downloadPreferences.videoFormat.FormatID}.{downloadPreferences.videoFormat.Ext}")) {
                    SetTextSafe(LbStatus, "Downloading video");
                    return;
                }
            }
            if (downloadPreferences.audioFormat != null) {
                if (downloadPreferences.videoFormat == null) {
                    SetTextSafe(LbStatus, "Downloading audio");
                    return;
                } else if (output.EndsWith($"{downloadPreferences.audioFormat.FormatID}.{downloadPreferences.audioFormat.Ext}")) {
                    SetTextSafe(LbStatus, "Downloading audio");
                    return;
                }
            }
        }

        private void OnDownloadCommandOutput(string output) {
            if (output == null) {
                return;
            }

            UpdateLbStatus(output);

            if (output.StartsWith("[download]")) {
                int indexOfPercentage = output.IndexOf('%');
                if (indexOfPercentage == -1) { return; }
                float.TryParse(output.Substring(11, indexOfPercentage - 11), out float percentage);
                if (percentage > 100) {
                    percentage = 100;
                }
                SetProgressBarValue((int)percentage);
                SetTextSafe(LbPercentage, ((int)percentage).ToString() + '%');

            }

        }

        private void SetProgressBarValue(int value) {
            if (ProgressBarDownload.InvokeRequired) {
                if (!stopInvoking) {
                    invokeInProgress = true;
                    SafeCallDelgate_INT d = new SafeCallDelgate_INT(SetProgressBarValue);
                    ProgressBarDownload.Invoke(d, new object[] { value });
                    invokeInProgress = false;
                }

            } else {
                ProgressBarDownload.Value = value;
            }
        }

        private void SetTextSafe(Control control, string text) {
            if (control.InvokeRequired) {
                if (!stopInvoking) {
                    invokeInProgress = true;
                    SafeCallDelgate_STRING d = new SafeCallDelgate_STRING(SetTextSafe);
                    control.Invoke(d, new object[] { control, text });
                    invokeInProgress = false;
                }

            } else {
                control.Text = text;
            }
        }

        private Format GetSelectedVideoFormat() {
            if (RbCustom.Checked && ChkVideo.Checked) {
                return CbVideo.SelectedItem as Format;
            }
            if (RbVideo.Checked || RbVideoAudio.Checked) {
                return media.BestFormats[0];
            }

            return null;
        }

        private Format GetSelectedAudioFormat() {
            if (RbCustom.Checked && ChkAudio.Checked) {
                return CbAudio.SelectedItem as Format;
            }
            if (RbAudio.Checked || RbVideoAudio.Checked) {
                return media.BestFormats[1];
            }

            return null;
        }

        private void RbCustom_CheckedChanged(object sender, EventArgs e) {
            ChkVideo.Enabled = RbCustom.Checked;
            ChkVideo.Checked = RbCustom.Checked;
            ChkAudio.Enabled = RbCustom.Checked;
            ChkAudio.Checked = RbCustom.Checked;
        }

        private async void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (Utils.Process != null) {
                if (!Utils.Process.HasExited) {
                    List<Process> childProcesses = Utils.Process.GetChildProcesses().Find(p => p.ProcessName == "yt-dlp").GetChildProcesses();

                    foreach (Process childProcess in childProcesses) {
                        childProcess.Kill();
                        childProcess.WaitForExit();
                        childProcess.Dispose();
                    }
                    Utils.RemoveFilesStartsWith(media.ID);
                }
            }

            if (invokeInProgress) {
                e.Cancel = true;
                stopInvoking = true;
                await Task.Factory.StartNew(() => {
                    while (invokeInProgress) ;
                });
                Close();
            }
        }

        private void TbURL_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                BtnFetch.PerformClick();
            }

        }
    }
}