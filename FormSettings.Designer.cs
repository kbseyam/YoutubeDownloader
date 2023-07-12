namespace YoutubeDownloader {
    partial class FormSettings {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            BtnSave = new Button();
            label1 = new Label();
            ChkVideoID = new CheckBox();
            ChkVideoExtension = new CheckBox();
            ChkVideoCodec = new CheckBox();
            ChkResolution = new CheckBox();
            ChkFps = new CheckBox();
            ChkDynamicRange = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            ChkNumOfChannels = new CheckBox();
            ChkAsr = new CheckBox();
            ChkAudioCodec = new CheckBox();
            ChkAudioID = new CheckBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // BtnSave
            // 
            BtnSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSave.Location = new Point(687, 391);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(101, 47);
            BtnSave.TabIndex = 0;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(162, 21);
            label1.TabIndex = 3;
            label1.Text = "Show in video format:";
            // 
            // ChkVideoID
            // 
            ChkVideoID.AutoSize = true;
            ChkVideoID.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVideoID.Location = new Point(29, 33);
            ChkVideoID.Name = "ChkVideoID";
            ChkVideoID.Size = new Size(94, 24);
            ChkVideoID.TabIndex = 4;
            ChkVideoID.Text = "Format ID";
            ChkVideoID.UseVisualStyleBackColor = true;
            // 
            // ChkVideoExtension
            // 
            ChkVideoExtension.AutoSize = true;
            ChkVideoExtension.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVideoExtension.Location = new Point(29, 63);
            ChkVideoExtension.Name = "ChkVideoExtension";
            ChkVideoExtension.Size = new Size(91, 24);
            ChkVideoExtension.TabIndex = 5;
            ChkVideoExtension.Text = "Extension";
            ChkVideoExtension.UseVisualStyleBackColor = true;
            // 
            // ChkVideoCodec
            // 
            ChkVideoCodec.AutoSize = true;
            ChkVideoCodec.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkVideoCodec.Location = new Point(29, 93);
            ChkVideoCodec.Name = "ChkVideoCodec";
            ChkVideoCodec.Size = new Size(70, 24);
            ChkVideoCodec.TabIndex = 6;
            ChkVideoCodec.Text = "Codec";
            ChkVideoCodec.UseVisualStyleBackColor = true;
            // 
            // ChkResolution
            // 
            ChkResolution.AutoSize = true;
            ChkResolution.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkResolution.Location = new Point(29, 123);
            ChkResolution.Name = "ChkResolution";
            ChkResolution.Size = new Size(98, 24);
            ChkResolution.TabIndex = 7;
            ChkResolution.Text = "Resolution";
            ChkResolution.UseVisualStyleBackColor = true;
            // 
            // ChkFps
            // 
            ChkFps.AutoSize = true;
            ChkFps.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkFps.Location = new Point(29, 153);
            ChkFps.Name = "ChkFps";
            ChkFps.Size = new Size(51, 24);
            ChkFps.TabIndex = 8;
            ChkFps.Text = "FPS";
            ChkFps.UseVisualStyleBackColor = true;
            // 
            // ChkDynamicRange
            // 
            ChkDynamicRange.AutoSize = true;
            ChkDynamicRange.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkDynamicRange.Location = new Point(29, 183);
            ChkDynamicRange.Name = "ChkDynamicRange";
            ChkDynamicRange.Size = new Size(128, 24);
            ChkDynamicRange.TabIndex = 9;
            ChkDynamicRange.Text = "Dynamic range";
            ChkDynamicRange.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Location = new Point(208, 9);
            label2.Name = "label2";
            label2.Size = new Size(1, 213);
            label2.TabIndex = 10;
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Location = new Point(440, 9);
            label3.Name = "label3";
            label3.Size = new Size(1, 213);
            label3.TabIndex = 18;
            // 
            // ChkNumOfChannels
            // 
            ChkNumOfChannels.AutoSize = true;
            ChkNumOfChannels.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkNumOfChannels.Location = new Point(261, 123);
            ChkNumOfChannels.Name = "ChkNumOfChannels";
            ChkNumOfChannels.Size = new Size(161, 24);
            ChkNumOfChannels.TabIndex = 16;
            ChkNumOfChannels.Text = "Number of channels";
            ChkNumOfChannels.UseVisualStyleBackColor = true;
            // 
            // ChkAsr
            // 
            ChkAsr.AutoSize = true;
            ChkAsr.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkAsr.Location = new Point(261, 93);
            ChkAsr.Name = "ChkAsr";
            ChkAsr.Size = new Size(55, 24);
            ChkAsr.TabIndex = 15;
            ChkAsr.Text = "ASR";
            ChkAsr.UseVisualStyleBackColor = true;
            // 
            // ChkAudioCodec
            // 
            ChkAudioCodec.AutoSize = true;
            ChkAudioCodec.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkAudioCodec.Location = new Point(261, 63);
            ChkAudioCodec.Name = "ChkAudioCodec";
            ChkAudioCodec.Size = new Size(70, 24);
            ChkAudioCodec.TabIndex = 14;
            ChkAudioCodec.Text = "Codec";
            ChkAudioCodec.UseVisualStyleBackColor = true;
            // 
            // ChkAudioID
            // 
            ChkAudioID.AutoSize = true;
            ChkAudioID.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChkAudioID.Location = new Point(261, 33);
            ChkAudioID.Name = "ChkAudioID";
            ChkAudioID.Size = new Size(94, 24);
            ChkAudioID.TabIndex = 12;
            ChkAudioID.Text = "Format ID";
            ChkAudioID.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(244, 9);
            label4.Name = "label4";
            label4.Size = new Size(163, 21);
            label4.TabIndex = 11;
            label4.Text = "Show in audio format:";
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(ChkNumOfChannels);
            Controls.Add(ChkAsr);
            Controls.Add(ChkAudioCodec);
            Controls.Add(ChkAudioID);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(ChkDynamicRange);
            Controls.Add(ChkFps);
            Controls.Add(ChkResolution);
            Controls.Add(ChkVideoCodec);
            Controls.Add(ChkVideoExtension);
            Controls.Add(ChkVideoID);
            Controls.Add(label1);
            Controls.Add(BtnSave);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormSettings";
            Text = "Settings - Youtube downloader";
            Load += FormSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnSave;
        private Label label1;
        private CheckBox ChkVideoID;
        private CheckBox ChkVideoExtension;
        private CheckBox ChkVideoCodec;
        private CheckBox ChkResolution;
        private CheckBox ChkFps;
        private CheckBox ChkDynamicRange;
        private Label label2;
        private Label label3;
        private CheckBox ChkNumOfChannels;
        private CheckBox ChkAsr;
        private CheckBox ChkAudioCodec;
        private CheckBox ChkAudioID;
        private Label label4;
    }
}