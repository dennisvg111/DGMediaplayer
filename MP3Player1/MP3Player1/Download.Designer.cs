namespace DGMediaplayer
{
    partial class Download
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Download));
            this.tboxFrom = new System.Windows.Forms.TextBox();
            this.tboxTo = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panelDownloads = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.downloadFinishedNotification = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // tboxFrom
            // 
            this.tboxFrom.Location = new System.Drawing.Point(13, 13);
            this.tboxFrom.Name = "tboxFrom";
            this.tboxFrom.Size = new System.Drawing.Size(126, 20);
            this.tboxFrom.TabIndex = 0;
            this.tboxFrom.TextChanged += new System.EventHandler(this.tboxFrom_TextChanged);
            this.tboxFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboxFrom_KeyDown);
            // 
            // tboxTo
            // 
            this.tboxTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxTo.Location = new System.Drawing.Point(164, 13);
            this.tboxTo.Name = "tboxTo";
            this.tboxTo.Size = new System.Drawing.Size(103, 20);
            this.tboxTo.TabIndex = 1;
            this.tboxTo.TextChanged += new System.EventHandler(this.tboxTo_TextChanged);
            this.tboxTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboxTo_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(273, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(39, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panelDownloads
            // 
            this.panelDownloads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDownloads.AutoScroll = true;
            this.panelDownloads.Location = new System.Drawing.Point(13, 40);
            this.panelDownloads.Name = "panelDownloads";
            this.panelDownloads.Size = new System.Drawing.Size(299, 269);
            this.panelDownloads.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "to";
            // 
            // downloadFinishedNotification
            // 
            this.downloadFinishedNotification.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.downloadFinishedNotification.BalloonTipText = "All downloads finished";
            this.downloadFinishedNotification.BalloonTipTitle = "DGMediaplayer downloads";
            this.downloadFinishedNotification.Icon = ((System.Drawing.Icon)(resources.GetObject("downloadFinishedNotification.Icon")));
            this.downloadFinishedNotification.Text = "Mediaplayer";
            this.downloadFinishedNotification.BalloonTipClicked += new System.EventHandler(this.downloadFinishedNotification_BalloonTipClicked);
            this.downloadFinishedNotification.BalloonTipClosed += new System.EventHandler(this.downloadFinishedNotification_BalloonTipClosed);
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 321);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelDownloads);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tboxTo);
            this.Controls.Add(this.tboxFrom);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 360);
            this.Name = "Download";
            this.Text = "Download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Download_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxFrom;
        private System.Windows.Forms.TextBox tboxTo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panelDownloads;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon downloadFinishedNotification;
    }
}