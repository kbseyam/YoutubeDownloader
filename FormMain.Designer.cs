namespace YoutubeDownloader {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            LbTagURL = new Label();
            TbURL = new TextBox();
            BtnFetch = new Button();
            CbVideo = new ComboBox();
            CbAudio = new ComboBox();
            BtnDownload = new Button();
            ChkVideo = new CheckBox();
            ChkAudio = new CheckBox();
            ProgressBarDownload = new ProgressBar();
            LbDownloadStatus = new Label();
            SaveFileDialog1 = new SaveFileDialog();
            LbPercentage = new Label();
            LbVideoTitle = new Label();
            LbTagTitle = new Label();
            LbTagChannelName = new Label();
            LbChannelName = new Label();
            RbVideoAudio = new RadioButton();
            RbVideo = new RadioButton();
            RbAudio = new RadioButton();
            RbCustom = new RadioButton();
            PBFetchRuning = new PictureBox();
            lbFetchingInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)PBFetchRuning).BeginInit();
            SuspendLayout();
            // 
            // LbTagURL
            // 
            LbTagURL.AutoSize = true;
            LbTagURL.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LbTagURL.Location = new Point(9, 13);
            LbTagURL.Margin = new Padding(4, 0, 4, 0);
            LbTagURL.Name = "LbTagURL";
            LbTagURL.Size = new Size(46, 20);
            LbTagURL.TabIndex = 0;
            LbTagURL.Text = "URL:";
            // 
            // TbURL
            // 
            TbURL.AutoCompleteCustomSource.AddRange(new string[] { "https://www.youtube.com/watch?v=" });
            TbURL.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TbURL.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TbURL.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TbURL.Location = new Point(90, 10);
            TbURL.Margin = new Padding(4, 3, 4, 3);
            TbURL.MaxLength = 100;
            TbURL.Name = "TbURL";
            TbURL.Size = new Size(638, 26);
            TbURL.TabIndex = 1;
            TbURL.KeyDown += TbURL_KeyDown;
            // 
            // BtnFetch
            // 
            BtnFetch.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnFetch.Location = new Point(617, 52);
            BtnFetch.Margin = new Padding(4, 3, 4, 3);
            BtnFetch.Name = "BtnFetch";
            BtnFetch.Size = new Size(111, 40);
            BtnFetch.TabIndex = 2;
            BtnFetch.Text = "Fetch";
            BtnFetch.UseVisualStyleBackColor = true;
            BtnFetch.Click += BtnFetch_Click;
            // 
            // CbVideo
            // 
            CbVideo.DropDownStyle = ComboBoxStyle.DropDownList;
            CbVideo.Enabled = false;
            CbVideo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CbVideo.FormattingEnabled = true;
            CbVideo.Location = new Point(90, 188);
            CbVideo.Margin = new Padding(4, 3, 4, 3);
            CbVideo.Name = "CbVideo";
            CbVideo.Size = new Size(638, 28);
            CbVideo.TabIndex = 8;
            CbVideo.Visible = false;
            // 
            // CbAudio
            // 
            CbAudio.DropDownStyle = ComboBoxStyle.DropDownList;
            CbAudio.Enabled = false;
            CbAudio.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CbAudio.FormattingEnabled = true;
            CbAudio.Location = new Point(90, 232);
            CbAudio.Margin = new Padding(4, 3, 4, 3);
            CbAudio.Name = "CbAudio";
            CbAudio.Size = new Size(638, 28);
            CbAudio.TabIndex = 10;
            CbAudio.Visible = false;
            // 
            // BtnDownload
            // 
            BtnDownload.Enabled = false;
            BtnDownload.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnDownload.Location = new Point(617, 284);
            BtnDownload.Margin = new Padding(4, 3, 4, 3);
            BtnDownload.Name = "BtnDownload";
            BtnDownload.Size = new Size(111, 40);
            BtnDownload.TabIndex = 11;
            BtnDownload.Text = "Download";
            BtnDownload.UseVisualStyleBackColor = true;
            BtnDownload.Visible = false;
            BtnDownload.Click += BtnDownload_Click;
            // 
            // ChkVideo
            // 
            ChkVideo.AutoSize = true;
            ChkVideo.Enabled = false;
            ChkVideo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVideo.Location = new Point(9, 190);
            ChkVideo.Margin = new Padding(4, 3, 4, 3);
            ChkVideo.Name = "ChkVideo";
            ChkVideo.Size = new Size(73, 24);
            ChkVideo.TabIndex = 7;
            ChkVideo.Text = "Video:";
            ChkVideo.UseVisualStyleBackColor = true;
            ChkVideo.Visible = false;
            ChkVideo.CheckedChanged += ChkVideo_CheckedChanged;
            // 
            // ChkAudio
            // 
            ChkAudio.AutoSize = true;
            ChkAudio.Enabled = false;
            ChkAudio.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ChkAudio.Location = new Point(9, 234);
            ChkAudio.Margin = new Padding(4, 3, 4, 3);
            ChkAudio.Name = "ChkAudio";
            ChkAudio.Size = new Size(73, 24);
            ChkAudio.TabIndex = 9;
            ChkAudio.Text = "Audio:";
            ChkAudio.UseVisualStyleBackColor = true;
            ChkAudio.Visible = false;
            ChkAudio.CheckedChanged += ChkAudio_CheckedChanged;
            // 
            // ProgressBarDownload
            // 
            ProgressBarDownload.Location = new Point(11, 357);
            ProgressBarDownload.Margin = new Padding(4, 3, 4, 3);
            ProgressBarDownload.Name = "ProgressBarDownload";
            ProgressBarDownload.Size = new Size(719, 27);
            ProgressBarDownload.Style = ProgressBarStyle.Continuous;
            ProgressBarDownload.TabIndex = 10;
            ProgressBarDownload.Visible = false;
            // 
            // LbDownloadStatus
            // 
            LbDownloadStatus.AutoSize = true;
            LbDownloadStatus.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LbDownloadStatus.Location = new Point(13, 336);
            LbDownloadStatus.Margin = new Padding(4, 0, 4, 0);
            LbDownloadStatus.Name = "LbDownloadStatus";
            LbDownloadStatus.Size = new Size(0, 18);
            LbDownloadStatus.TabIndex = 11;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.AddExtension = false;
            // 
            // LbPercentage
            // 
            LbPercentage.AutoSize = true;
            LbPercentage.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            LbPercentage.Location = new Point(370, 392);
            LbPercentage.Margin = new Padding(4, 0, 4, 0);
            LbPercentage.Name = "LbPercentage";
            LbPercentage.Size = new Size(0, 25);
            LbPercentage.TabIndex = 12;
            // 
            // LbVideoTitle
            // 
            LbVideoTitle.AutoEllipsis = true;
            LbVideoTitle.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LbVideoTitle.Location = new Point(90, 76);
            LbVideoTitle.Margin = new Padding(4, 0, 4, 0);
            LbVideoTitle.Name = "LbVideoTitle";
            LbVideoTitle.Size = new Size(519, 40);
            LbVideoTitle.TabIndex = 13;
            LbVideoTitle.Visible = false;
            // 
            // LbTagTitle
            // 
            LbTagTitle.AutoSize = true;
            LbTagTitle.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LbTagTitle.Location = new Point(9, 76);
            LbTagTitle.Margin = new Padding(4, 0, 4, 0);
            LbTagTitle.Name = "LbTagTitle";
            LbTagTitle.Size = new Size(36, 16);
            LbTagTitle.TabIndex = 14;
            LbTagTitle.Text = "Title:";
            LbTagTitle.Visible = false;
            // 
            // LbTagChannelName
            // 
            LbTagChannelName.AutoSize = true;
            LbTagChannelName.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LbTagChannelName.Location = new Point(9, 54);
            LbTagChannelName.Margin = new Padding(4, 0, 4, 0);
            LbTagChannelName.Name = "LbTagChannelName";
            LbTagChannelName.Size = new Size(59, 16);
            LbTagChannelName.TabIndex = 15;
            LbTagChannelName.Text = "Channel:";
            LbTagChannelName.Visible = false;
            // 
            // LbChannelName
            // 
            LbChannelName.AutoEllipsis = true;
            LbChannelName.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LbChannelName.Location = new Point(90, 54);
            LbChannelName.Margin = new Padding(4, 0, 4, 0);
            LbChannelName.Name = "LbChannelName";
            LbChannelName.Size = new Size(519, 18);
            LbChannelName.TabIndex = 16;
            LbChannelName.Visible = false;
            // 
            // RbVideoAudio
            // 
            RbVideoAudio.AutoSize = true;
            RbVideoAudio.Checked = true;
            RbVideoAudio.Enabled = false;
            RbVideoAudio.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RbVideoAudio.Location = new Point(110, 149);
            RbVideoAudio.Margin = new Padding(4, 3, 4, 3);
            RbVideoAudio.Name = "RbVideoAudio";
            RbVideoAudio.Size = new Size(155, 24);
            RbVideoAudio.TabIndex = 3;
            RbVideoAudio.TabStop = true;
            RbVideoAudio.Text = "Best Video+Audio";
            RbVideoAudio.UseVisualStyleBackColor = true;
            RbVideoAudio.Visible = false;
            // 
            // RbVideo
            // 
            RbVideo.AutoSize = true;
            RbVideo.Enabled = false;
            RbVideo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RbVideo.Location = new Point(318, 149);
            RbVideo.Margin = new Padding(4, 3, 4, 3);
            RbVideo.Name = "RbVideo";
            RbVideo.Size = new Size(105, 24);
            RbVideo.TabIndex = 4;
            RbVideo.Text = "Best Video";
            RbVideo.UseVisualStyleBackColor = true;
            RbVideo.Visible = false;
            // 
            // RbAudio
            // 
            RbAudio.AutoSize = true;
            RbAudio.Enabled = false;
            RbAudio.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RbAudio.Location = new Point(476, 149);
            RbAudio.Margin = new Padding(4, 3, 4, 3);
            RbAudio.Name = "RbAudio";
            RbAudio.Size = new Size(105, 24);
            RbAudio.TabIndex = 5;
            RbAudio.Text = "Best Audio";
            RbAudio.UseVisualStyleBackColor = true;
            RbAudio.Visible = false;
            // 
            // RbCustom
            // 
            RbCustom.AutoSize = true;
            RbCustom.Enabled = false;
            RbCustom.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            RbCustom.Location = new Point(634, 149);
            RbCustom.Margin = new Padding(4, 3, 4, 3);
            RbCustom.Name = "RbCustom";
            RbCustom.Size = new Size(82, 24);
            RbCustom.TabIndex = 6;
            RbCustom.Text = "Custom";
            RbCustom.UseVisualStyleBackColor = true;
            RbCustom.Visible = false;
            RbCustom.CheckedChanged += RbCustom_CheckedChanged;
            // 
            // PBFetchRuning
            // 
            PBFetchRuning.BackColor = Color.Transparent;
            PBFetchRuning.BackgroundImageLayout = ImageLayout.None;
            PBFetchRuning.Enabled = false;
            PBFetchRuning.Image = (Image)resources.GetObject("PBFetchRuning.Image");
            PBFetchRuning.Location = new Point(304, 161);
            PBFetchRuning.Margin = new Padding(4, 3, 4, 3);
            PBFetchRuning.Name = "PBFetchRuning";
            PBFetchRuning.Size = new Size(132, 132);
            PBFetchRuning.SizeMode = PictureBoxSizeMode.CenterImage;
            PBFetchRuning.TabIndex = 21;
            PBFetchRuning.TabStop = false;
            PBFetchRuning.Visible = false;
            // 
            // lbFetchingInfo
            // 
            lbFetchingInfo.AutoSize = true;
            lbFetchingInfo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbFetchingInfo.Location = new Point(299, 304);
            lbFetchingInfo.Margin = new Padding(4, 0, 4, 0);
            lbFetchingInfo.Name = "lbFetchingInfo";
            lbFetchingInfo.Size = new Size(142, 20);
            lbFetchingInfo.TabIndex = 22;
            lbFetchingInfo.Text = "Fetching video info";
            lbFetchingInfo.Visible = false;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(741, 455);
            Controls.Add(lbFetchingInfo);
            Controls.Add(PBFetchRuning);
            Controls.Add(RbCustom);
            Controls.Add(RbAudio);
            Controls.Add(RbVideo);
            Controls.Add(RbVideoAudio);
            Controls.Add(LbChannelName);
            Controls.Add(LbTagChannelName);
            Controls.Add(LbTagTitle);
            Controls.Add(LbVideoTitle);
            Controls.Add(LbPercentage);
            Controls.Add(LbDownloadStatus);
            Controls.Add(ProgressBarDownload);
            Controls.Add(ChkAudio);
            Controls.Add(ChkVideo);
            Controls.Add(BtnDownload);
            Controls.Add(CbAudio);
            Controls.Add(CbVideo);
            Controls.Add(BtnFetch);
            Controls.Add(TbURL);
            Controls.Add(LbTagURL);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FormMain";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Youtube downloader";
            FormClosing += FormMain_FormClosing;
            Shown += FormMain_Shown;
            ((System.ComponentModel.ISupportInitialize)PBFetchRuning).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LbTagURL;
        private TextBox TbURL;
        private Button BtnFetch;
        private ComboBox CbVideo;
        private ComboBox CbAudio;
        private Button BtnDownload;
        private CheckBox ChkVideo;
        private CheckBox ChkAudio;
        private ProgressBar ProgressBarDownload;
        private Label LbDownloadStatus;
        private SaveFileDialog SaveFileDialog1;
        private Label LbPercentage;
        private Label LbVideoTitle;
        private Label LbTagTitle;
        private Label LbTagChannelName;
        private Label LbChannelName;
        private RadioButton RbVideoAudio;
        private RadioButton RbVideo;
        private RadioButton RbAudio;
        private RadioButton RbCustom;
        private PictureBox PBFetchRuning;
        private Label lbFetchingInfo;
    }
}