using System.Diagnostics;

namespace YoutubeDownloader {
    public partial class FormMain : Form {
        private bool invokeInProgress = false;
        private bool stopInvoking = false;
        private delegate void SafeCallDelgate_INT(int value);
        private delegate void SafeCallDelgate_ComboBox_Index(ComboBox comboBox, int index);
        private delegate void SafeCallDelgate_STRING(Control control, string text);

        private YoutubeMedia? media;
        private DownloadPreferences? downloadPreferences;

        public FormMain() {
            InitializeComponent();
        }

        private void SetSlecetedIndexSafe(ComboBox comboBox, int index) {
            if (comboBox.InvokeRequired) {
                if (!stopInvoking) {
                    invokeInProgress = true;
                    SafeCallDelgate_ComboBox_Index d = new(SetSlecetedIndexSafe);
                    comboBox.Invoke(d, new object[] { comboBox, index });
                    invokeInProgress = false;
                }

            } else {
                comboBox.SelectedIndex = index;
            }
        }

        private void RefreshDownloadPercentage(int percentage) {
            if (ProgressBarDownload.InvokeRequired) {
                if (!stopInvoking) {
                    invokeInProgress = true;
                    SafeCallDelgate_INT d = new(RefreshDownloadPercentage);
                    ProgressBarDownload.Invoke(d, new object[] { percentage });
                    invokeInProgress = false;
                }

            } else {
                ProgressBarDownload.Value = percentage;
                if (percentage == 0) {
                    SetTextSafe(LbPercentage, string.Empty);
                } else {
                    SetTextSafe(LbPercentage, $"{percentage}%");
                }

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

        private void VisiblePreferencesControls(bool visible) {
            RbVideoAudio.Visible = visible;
            RbVideo.Visible = visible;
            RbAudio.Visible = visible;
            RbCustom.Visible = visible;
            ChkAudio.Visible = visible;
            ChkVideo.Visible = visible;
            CbVideo.Visible = visible;
            CbAudio.Visible = visible;
        }

        private void VisibleDownloadControls(bool visible) {
            LbPercentage.Visible = visible;
            LbDownloadStatus.Visible = visible;
            ProgressBarDownload.Visible = visible;
            BtnDownload.Visible = visible;
        }

        private void VisibleVideoInfoControls(bool visible) {
            LbTagChannelName.Visible = visible;
            LbVideoTitle.Visible = visible;
            LbChannelName.Visible = visible;
            LbTagTitle.Visible = visible;
        }

        private void EnablePreferencesControls(bool enabled) {
            RbVideoAudio.Enabled = enabled;
            RbVideo.Enabled = enabled;
            RbAudio.Enabled = enabled;
            RbCustom.Enabled = enabled;
            ChkAudio.Enabled = enabled;
            ChkVideo.Enabled = enabled;
        }

        private void RefreshFetchingControls(bool isFetching) {
            PBFetchRuning.Enabled = isFetching;
            PBFetchRuning.Visible = isFetching;
            lbFetchingInfo.Visible = isFetching;
        }

        private void RefreshPreferencesControls() {
            VisiblePreferencesControls(media != null);
            EnablePreferencesControls(media != null);
            RbVideoAudio.Checked = true;
            ChkAudio.Checked = false;
            ChkVideo.Checked = false;

            if (media != null) {
                // CbVideo
                CbVideo.DisplayMember = Format.VIDEO_FORMAT_STRING;
                CbVideo.DataSource = media.Formats.Where((item) => item.FormatType == Format.Type.VIDEO).ToList();
                SetSlecetedIndexSafe(CbVideo, 0);

                // CbAudio
                CbAudio.DataSource = media.Formats.Where((item) => item.FormatType == Format.Type.AUDIO).ToList();
                CbAudio.DisplayMember = Format.AUDIO_FORMAT_STRING;
                SetSlecetedIndexSafe(CbAudio, 0);
            } else {
                // CbVideo
                CbVideo.DataSource = null;
                CbVideo.DisplayMember = string.Empty;

                // CbAudio
                CbAudio.DataSource = null;
                CbAudio.DisplayMember = string.Empty;
            }



        }

        private void ResetDownloadControls() {
            VisibleDownloadControls(media != null);
            SetTextSafe(LbDownloadStatus, string.Empty);
            RefreshDownloadPercentage(0);
            SaveFileDialog1.Reset();
            BtnDownload.Enabled = media != null;
        }

        private void RefreshVideoInfoControls() {
            VisibleVideoInfoControls(media != null);

            if (media != null) {
                SetTextSafe(LbVideoTitle, $"{media.Title}     ({media.DurationString})");
                SetTextSafe(LbChannelName, media.ChannelName);
            } else {
                SetTextSafe(LbVideoTitle, string.Empty);
                SetTextSafe(LbChannelName, string.Empty);
            }
        }

        private void RefreshControls(bool isFetching = false) {
            RefreshFetchingControls(isFetching);
            RefreshVideoInfoControls();
            RefreshPreferencesControls();
            ResetDownloadControls();
        }

        private void UpdateLbDownloadStatus(string output) {

            if (output == Utils.COMMAND_FINISH) {
                SetTextSafe(LbDownloadStatus, "Done");
                return;
            }

            if (!output.StartsWith("[download] Destination")) {
                return;
            }

            if (downloadPreferences?.videoFormat != null) {
                if (downloadPreferences.audioFormat == null) {
                    SetTextSafe(LbDownloadStatus, "Downloading video");
                    return;
                } else if (output.EndsWith($"{downloadPreferences.videoFormat.FormatID}.{downloadPreferences.videoFormat.Ext}")) {
                    SetTextSafe(LbDownloadStatus, "Downloading video");
                    return;
                }
            }
            if (downloadPreferences?.audioFormat != null) {
                if (downloadPreferences.videoFormat == null) {
                    SetTextSafe(LbDownloadStatus, "Downloading audio");
                    return;
                } else if (output.EndsWith($"{downloadPreferences.audioFormat.FormatID}.{downloadPreferences.audioFormat.Ext}")) {
                    SetTextSafe(LbDownloadStatus, "Downloading audio");
                    return;
                }
            }
        }

        private void OnDownloadCommandOutput(string output) {
            if (output == null) {
                return;
            }

            UpdateLbDownloadStatus(output);

            if (output.StartsWith("[download]")) {
                int indexOfPercentage = output.IndexOf('%');
                if (indexOfPercentage == -1) { return; }

                bool hasPercentage = float.TryParse(output.AsSpan(11, indexOfPercentage - 11), out float percentage);
                if (hasPercentage) {
                    if (percentage > 100) {
                        percentage = 100;
                    }
                    RefreshDownloadPercentage((int)percentage);
                }
            }

        }

        private RadioButton GetCheakedRbFormat() {
            if (RbVideoAudio.Checked) {
                return RbVideoAudio;
            }
            if (RbVideo.Checked) {
                return RbVideo;
            }
            if (RbAudio.Checked) {
                return RbAudio;
            }

            return RbCustom;
        }

        private Format? GetSelectedVideoFormat() {
            if (media == null) {
                return null;
            }
            if (RbCustom.Checked && ChkVideo.Checked) {
                return CbVideo.SelectedItem as Format;
            }
            if (RbVideo.Checked || RbVideoAudio.Checked) {
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
            if (RbAudio.Checked || RbVideoAudio.Checked) {
                return media.BestFormats[1];
            }

            return null;
        }

        private void FormMain_Shown(object sender, EventArgs e) {
            Utils.CheckRequirements();
        }

        private async void BtnFetch_Click(object sender, EventArgs e) {
            media = null;
            RefreshControls(true);

            await Task.Run(() => media = YoutubeMedia.FetchMediaInfo(TbURL.Text));

            RefreshControls(false);

            if (media == null) {
                MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDownload_Click(object sender, EventArgs e) {
            ResetDownloadControls();

            if (media == null) {
                MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            downloadPreferences = new DownloadPreferences(GetSelectedVideoFormat(), GetSelectedAudioFormat());
            SaveFileDialog1.Filter = $"{downloadPreferences.FileExt}|*.{downloadPreferences.FileExt}";
            SaveFileDialog1.FileName = Utils.GetValidFileName(media.Title);

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK) {
                downloadPreferences.path = SaveFileDialog1.FileName;
                SetTextSafe(LbDownloadStatus, "Starting download");

                await Task.Run(() => {
                    if (media != null && downloadPreferences != null) {
                        media.Download(downloadPreferences,
                        OnDownloadCommandOutput);
                    } else {
                        MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }

        }

        private void ChkVideo_CheckedChanged(object sender, EventArgs e) {
            CbVideo.Enabled = ChkVideo.Checked;
        }

        private void ChkAudio_CheckedChanged(object sender, EventArgs e) {
            CbAudio.Enabled = ChkAudio.Checked;
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

        private void PBSettings_Click(object sender, EventArgs e) {
            RadioButton cheakedRadioButton = GetCheakedRbFormat();
            bool chkVideoIsCheacked = ChkVideo.Checked;
            bool chkAudioIsCheacked = ChkAudio.Checked;
            int videoFormatIndex = CbVideo.SelectedIndex;
            int audioFormatIndex = CbAudio.SelectedIndex;

            FormSettings formSettings = new();
            formSettings.ShowDialog();

            RefreshControls();
            cheakedRadioButton.Checked = true;
            ChkVideo.Checked = chkVideoIsCheacked;
            ChkAudio.Checked = chkAudioIsCheacked;
            CbVideo.SelectedIndex = videoFormatIndex;
            CbAudio.SelectedIndex = audioFormatIndex;
        }

        private void RbVideoAudio_MouseHover(object sender, EventArgs e) {
            if (media != null) {
                new ToolTip().SetToolTip(RbVideoAudio, $"Video: {media.BestFormats[0].VideoFormatString}\n" +
                    $"Audio: {media.BestFormats[1].AudioFormatString}");
            }
        }

        private void RbVideo_MouseHover(object sender, EventArgs e) {
            if (media != null) {
                new ToolTip().SetToolTip(RbVideo, media.BestFormats[0].VideoFormatString);
            }
        }

        private void RbAudio_MouseHover(object sender, EventArgs e) {
            if (media != null) {
                new ToolTip().SetToolTip(RbAudio, media.BestFormats[1].AudioFormatString);
            }
        }
    }
}