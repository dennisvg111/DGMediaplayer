namespace MP3Player1
{
    partial class Mediaplayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mediaplayer));
            this.lboxMedia = new System.Windows.Forms.ListBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.ArtistLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMarkFile = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.cboxSearch = new System.Windows.Forms.ComboBox();
            this.btnTop = new System.Windows.Forms.Button();
            this.cboxTag = new System.Windows.Forms.ComboBox();
            this.timerPreventClosing = new System.Windows.Forms.Timer(this.components);
            this.nIconMediaPlayer = new System.Windows.Forms.NotifyIcon(this.components);
            this.cMenuNIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPause = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNext = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMark = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.cBoxSorting = new System.Windows.Forms.ComboBox();
            this.cMenuNIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // lboxMedia
            // 
            this.lboxMedia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxMedia.FormattingEnabled = true;
            this.lboxMedia.HorizontalScrollbar = true;
            this.lboxMedia.Location = new System.Drawing.Point(12, 85);
            this.lboxMedia.Name = "lboxMedia";
            this.lboxMedia.Size = new System.Drawing.Size(300, 160);
            this.lboxMedia.TabIndex = 5;
            this.lboxMedia.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(65, 268);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(69, 13);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "Not available";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(12, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(59, 23);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ArtistLabel
            // 
            this.ArtistLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ArtistLabel.AutoSize = true;
            this.ArtistLabel.Location = new System.Drawing.Point(65, 251);
            this.ArtistLabel.Name = "ArtistLabel";
            this.ArtistLabel.Size = new System.Drawing.Size(69, 13);
            this.ArtistLabel.TabIndex = 9;
            this.ArtistLabel.Text = "Not available";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Artist:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Title:";
            // 
            // btnMarkFile
            // 
            this.btnMarkFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMarkFile.Location = new System.Drawing.Point(12, 286);
            this.btnMarkFile.Name = "btnMarkFile";
            this.btnMarkFile.Size = new System.Drawing.Size(134, 23);
            this.btnMarkFile.TabIndex = 16;
            this.btnMarkFile.Text = "Mark/Unmark current file";
            this.btnMarkFile.UseVisualStyleBackColor = true;
            this.btnMarkFile.Click += new System.EventHandler(this.btnMarkFile_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.Location = new System.Drawing.Point(289, 12);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(23, 23);
            this.btnInfo.TabIndex = 17;
            this.btnInfo.Text = "?";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // cboxSearch
            // 
            this.cboxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxSearch.FormattingEnabled = true;
            this.cboxSearch.Location = new System.Drawing.Point(77, 14);
            this.cboxSearch.Name = "cboxSearch";
            this.cboxSearch.Size = new System.Drawing.Size(141, 21);
            this.cboxSearch.TabIndex = 18;
            this.cboxSearch.DropDownClosed += new System.EventHandler(this.cboxSearch_DropDownClosed);
            this.cboxSearch.TextChanged += new System.EventHandler(this.cboxSearch_TextChanged);
            // 
            // btnTop
            // 
            this.btnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTop.Location = new System.Drawing.Point(255, 286);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(57, 23);
            this.btnTop.TabIndex = 19;
            this.btnTop.Text = "(Un)tag";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // cboxTag
            // 
            this.cboxTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxTag.FormattingEnabled = true;
            this.cboxTag.Items.AddRange(new object[] {
            "[top]"});
            this.cboxTag.Location = new System.Drawing.Point(152, 287);
            this.cboxTag.Name = "cboxTag";
            this.cboxTag.Size = new System.Drawing.Size(97, 21);
            this.cboxTag.TabIndex = 20;
            this.cboxTag.Text = "[top]";
            this.cboxTag.DropDownClosed += new System.EventHandler(this.cboxTag_DropDownClosed);
            // 
            // timerPreventClosing
            // 
            this.timerPreventClosing.Interval = 250;
            this.timerPreventClosing.Tick += new System.EventHandler(this.timerPreventClosing_Tick);
            // 
            // nIconMediaPlayer
            // 
            this.nIconMediaPlayer.ContextMenuStrip = this.cMenuNIcon;
            this.nIconMediaPlayer.Icon = ((System.Drawing.Icon)(resources.GetObject("nIconMediaPlayer.Icon")));
            this.nIconMediaPlayer.Text = "Mediaplayer";
            this.nIconMediaPlayer.DoubleClick += new System.EventHandler(this.nIconMediaPlayer_DoubleClick);
            // 
            // cMenuNIcon
            // 
            this.cMenuNIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPlay,
            this.tsmPause,
            this.tsmStop,
            this.tsmNext,
            this.tsmMark,
            this.tsmTag,
            this.tsmClose});
            this.cMenuNIcon.Name = "cMenuNIcon";
            this.cMenuNIcon.Size = new System.Drawing.Size(147, 158);
            // 
            // tsmPlay
            // 
            this.tsmPlay.Name = "tsmPlay";
            this.tsmPlay.Size = new System.Drawing.Size(146, 22);
            this.tsmPlay.Text = "Play";
            this.tsmPlay.Click += new System.EventHandler(this.tsmPlay_Click);
            // 
            // tsmPause
            // 
            this.tsmPause.Name = "tsmPause";
            this.tsmPause.Size = new System.Drawing.Size(146, 22);
            this.tsmPause.Text = "Pause";
            this.tsmPause.Click += new System.EventHandler(this.tsmPause_Click);
            // 
            // tsmStop
            // 
            this.tsmStop.Name = "tsmStop";
            this.tsmStop.Size = new System.Drawing.Size(146, 22);
            this.tsmStop.Text = "Stop";
            this.tsmStop.Click += new System.EventHandler(this.tsmStop_Click);
            // 
            // tsmNext
            // 
            this.tsmNext.Name = "tsmNext";
            this.tsmNext.Size = new System.Drawing.Size(146, 22);
            this.tsmNext.Text = "Next";
            this.tsmNext.Click += new System.EventHandler(this.tsmNext_Click);
            // 
            // tsmMark
            // 
            this.tsmMark.Name = "tsmMark";
            this.tsmMark.Size = new System.Drawing.Size(146, 22);
            this.tsmMark.Text = "Mark";
            this.tsmMark.Click += new System.EventHandler(this.tsmMark_Click);
            // 
            // tsmTag
            // 
            this.tsmTag.Name = "tsmTag";
            this.tsmTag.Size = new System.Drawing.Size(146, 22);
            this.tsmTag.Text = "(Un)Tag [top]";
            this.tsmTag.Click += new System.EventHandler(this.tsmTag_Click);
            // 
            // tsmClose
            // 
            this.tsmClose.Name = "tsmClose";
            this.tsmClose.Size = new System.Drawing.Size(146, 22);
            this.tsmClose.Text = "Force Quit";
            this.tsmClose.Click += new System.EventHandler(this.tsmClose_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.AccessibleDescription = "public";
            this.axWindowsMediaPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 39);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(300, 45);
            this.axWindowsMediaPlayer1.TabIndex = 21;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            // 
            // cBoxSorting
            // 
            this.cBoxSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxSorting.FormattingEnabled = true;
            this.cBoxSorting.Items.AddRange(new object[] {
            "Default",
            "Shuffle",
            "Newest"});
            this.cBoxSorting.Location = new System.Drawing.Point(224, 14);
            this.cBoxSorting.Name = "cBoxSorting";
            this.cBoxSorting.Size = new System.Drawing.Size(59, 21);
            this.cBoxSorting.TabIndex = 22;
            this.cBoxSorting.DropDownClosed += new System.EventHandler(this.cBoxSorting_DropDownClosed);
            // 
            // Mediaplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 321);
            this.Controls.Add(this.cBoxSorting);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.cboxTag);
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.cboxSearch);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnMarkFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ArtistLabel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.lboxMedia);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(290, 220);
            this.Name = "Mediaplayer";
            this.Text = "Mediaplayer";
            this.Activated += new System.EventHandler(this.Mediaplayer_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mediaplayer_FormClosing);
            this.Resize += new System.EventHandler(this.Mediaplayer_Resize);
            this.cMenuNIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lboxMedia;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label ArtistLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMarkFile;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.ComboBox cboxSearch;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.ComboBox cboxTag;
        private System.Windows.Forms.Timer timerPreventClosing;
        private System.Windows.Forms.NotifyIcon nIconMediaPlayer;
        private System.Windows.Forms.ContextMenuStrip cMenuNIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmPlay;
        private System.Windows.Forms.ToolStripMenuItem tsmPause;
        private System.Windows.Forms.ToolStripMenuItem tsmStop;
        private System.Windows.Forms.ToolStripMenuItem tsmMark;
        private System.Windows.Forms.ToolStripMenuItem tsmTag;
        private System.Windows.Forms.ToolStripMenuItem tsmClose;
        private System.Windows.Forms.ToolStripMenuItem tsmNext;
        public AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ComboBox cBoxSorting;
    }
}

