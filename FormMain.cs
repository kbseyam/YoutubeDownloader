using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace YoutubeDownloader {
    public partial class FormMain : Form {
        private bool invokeInProgress = false;
        private bool stopInvoking = false;
        private delegate void SafeCallDelgate_INT(int value);
        private delegate void SafeCallDelgate_INT_2(ComboBox comboBox, int value);
        private delegate void SafeCallDelgate_STRING(Control control, string text);

        private YoutubeMedia? media;
        private DownloadPreferences? downloadPreferences;

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
                ResetDownloadControls();
                ResetPreferencesControls();
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
            if (!Utils.IsYoutubeVideoUrl(TbURL.Text)) {
                throw new Exception();
            }
            media = YoutubeMedia.FetchMediaInfo(TbURL.Text);
            RefreshControls();

            return media != null;
        }

        private void RefreshControls() {
            RbVideoAudio.Enabled = media != null;
            RbVideo.Enabled = media != null;
            RbAudio.Enabled = media != null;
            RbCustom.Enabled = media != null;
            BtnDownload.Enabled = media != null;

            if (media != null) {
                SetTextSafe(LbVideoTitle, $"{media.Title}     ({media.Duration})");
                SetTextSafe(LbChannelName, media.ChannelName);
            } else {
                SetTextSafe(LbVideoTitle, string.Empty);
                SetTextSafe(LbChannelName, string.Empty);
            }

            RefreshCbVideo();
            RefreshCbAudio();
        }

        private void RefreshCbVideo() {
            if (media != null) {
                CbVideo.DisplayMember = Format.VIDEO_FORMAT_STRING;
                CbVideo.DataSource = media.Formats.Where((item) => item.FormatType == Format.Type.VIDEO).ToList();
                SetSlecetedIndexSafe(CbVideo, 0);
            } else { CbVideo.DataSource = null; CbVideo.DisplayMember = string.Empty; }

        }

        private void RefreshCbAudio() {
            if (media != null) {
                CbAudio.DataSource = media.Formats.Where((item) => item.FormatType == Format.Type.AUDIO).ToList();
                CbAudio.DisplayMember = Format.AUDIO_FORMAT_STRING;
                SetSlecetedIndexSafe(CbAudio, 0);
            } else { CbAudio.DataSource = null; CbAudio.DisplayMember = string.Empty; }

        }

        private void ChkVideo_CheckedChanged(object sender, EventArgs e) {
            CbVideo.Enabled = ChkVideo.Checked;
        }

        private void ChkAudio_CheckedChanged(object sender, EventArgs e) {
            CbAudio.Enabled = ChkAudio.Checked;
        }

        private void ResetPreferencesControls() {
            RbVideoAudio.Checked = true;
            SetSlecetedIndexSafe(CbVideo, 0);
            SetSlecetedIndexSafe(CbAudio, 0);
            ChkAudio.Checked = false;
            ChkVideo.Checked = false;
        }

        private void ResetDownloadControls() {
            SetTextSafe(LbStatus, string.Empty);
            SetTextSafe(LbPercentage, string.Empty);
            SetProgressBarValue(0);
            SaveFileDialog1.Reset();
        }

        private void BtnDownload_Click(object sender, EventArgs e) {
            ResetDownloadControls();

            if (media == null) {
                MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            downloadPreferences = new DownloadPreferences(media, GetSelectedVideoFormat(), GetSelectedAudioFormat());
            SaveFileDialog1.Filter = $"{downloadPreferences.FileExt}|*.{downloadPreferences.FileExt}";
            SaveFileDialog1.FileName = Utils.GetValidFileName(media.Title);

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK) {
                downloadPreferences.path = SaveFileDialog1.FileName;
                SetTextSafe(LbStatus, "Starting download");

                Thread thread = new(new ThreadStart(Download));
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

            if (downloadPreferences?.videoFormat != null) {
                if (downloadPreferences.audioFormat == null) {
                    SetTextSafe(LbStatus, "Downloading video");
                    return;
                } else if (output.EndsWith($"{downloadPreferences.videoFormat.FormatID}.{downloadPreferences.videoFormat.Ext}")) {
                    SetTextSafe(LbStatus, "Downloading video");
                    return;
                }
            }
            if (downloadPreferences?.audioFormat != null) {
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
                bool hasPercentage = float.TryParse(output.AsSpan(11, indexOfPercentage - 11), out float percentage);

                if (hasPercentage) {
                    if (percentage > 100) {
                        percentage = 100;
                    }
                    SetProgressBarValue((int)percentage);
                    SetTextSafe(LbPercentage, ((int)percentage).ToString() + '%');
                }
            }

        }

        private void SetSlecetedIndexSafe(ComboBox comboBox, int index) {
            if (comboBox.InvokeRequired) {
                if (!stopInvoking) {
                    invokeInProgress = true;
                    SafeCallDelgate_INT_2 d = new(SetSlecetedIndexSafe);
                    comboBox.Invoke(d, new object[] { comboBox, index });
                    invokeInProgress = false;
                }

            } else {
                comboBox.SelectedIndex = index;
            }
        }

        private void SetProgressBarValue(int value) {
            if (ProgressBarDownload.InvokeRequired) {
                if (!stopInvoking) {
                    invokeInProgress = true;
                    SafeCallDelgate_INT d = new(SetProgressBarValue);
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
                    SafeCallDelgate_STRING d = new(SetTextSafe);
                    control.Invoke(d, new object[] { control, text });
                    invokeInProgress = false;
                }

            } else {
                control.Text = text;
            }
        }

        private Format? GetSelectedVideoFormat() {
            if (media == null) {
                return null;
            }
            if (RbCustom.Checked && ChkVideo.Checked) {
                return CbVideo.SelectedItem as Format;
            }
            if ((RbVideo.Checked || RbVideoAudio.Checked)) {
                return media.BestFormats[0];
            }

            return null;
        }

        private Format? GetSelectedAudioFormat() {
            if (media == null) {
                return null;
            }
            if (RbCustom.Checked && ChkAudio.Checked) {
                return CbAudio.SelectedItem as Format;
            }
            if ((RbAudio.Checked || RbVideoAudio.Checked)) {
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
                    Process? ytdlpProcess = Utils.Process.GetChildProcesses().Find(p => p.ProcessName == "yt-dlp");
                    if (ytdlpProcess != null) {
                        List<Process> childProcesses = ytdlpProcess.GetChildProcesses();

                        foreach (Process childProcess in childProcesses) {
                            childProcess.Kill();
                            childProcess.WaitForExit();
                            childProcess.Dispose();
                        }
                        if (media != null) {
                            Utils.RemoveFilesStartsWith(media.Guid);
                        }
                    }

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