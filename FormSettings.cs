using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeDownloader {
    public partial class FormSettings : Form {
        public FormSettings() {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e) {
            // Get audio settings
            ChkVideoID.Checked = Properties.Settings.Default.ShowVideoID;
            ChkVideoExtension.Checked = Properties.Settings.Default.ShowVideoFileExtension;
            ChkVideoCodec.Checked = Properties.Settings.Default.ShowVideoCodec;
            ChkResolution.Checked = Properties.Settings.Default.ShowResolution;
            ChkFps.Checked = Properties.Settings.Default.ShowFps;
            ChkDynamicRange.Checked = Properties.Settings.Default.ShowDynamicRange;

            // Get video settings
            ChkAudioID.Checked = Properties.Settings.Default.ShowAudioID;
            ChkAudioCodec.Checked = Properties.Settings.Default.ShowAudioCodec;
            ChkAsr.Checked = Properties.Settings.Default.ShowASR;
            ChkNumOfChannels.Checked = Properties.Settings.Default.ShowNumberOfAudioChannels;
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            // Set video settings
            Properties.Settings.Default.ShowVideoID = ChkVideoID.Checked;
            Properties.Settings.Default.ShowVideoFileExtension = ChkVideoExtension.Checked;
            Properties.Settings.Default.ShowVideoCodec = ChkVideoCodec.Checked;
            Properties.Settings.Default.ShowResolution = ChkResolution.Checked;
            Properties.Settings.Default.ShowFps = ChkFps.Checked;
            Properties.Settings.Default.ShowDynamicRange = ChkDynamicRange.Checked;

            // Set audio settings
            Properties.Settings.Default.ShowAudioID = ChkAudioID.Checked;
            Properties.Settings.Default.ShowAudioCodec = ChkAudioCodec.Checked;
            Properties.Settings.Default.ShowASR = ChkAsr.Checked;
            Properties.Settings.Default.ShowNumberOfAudioChannels = ChkNumOfChannels.Checked;

            Properties.Settings.Default.Save();
            Close();
        }
    }
}
