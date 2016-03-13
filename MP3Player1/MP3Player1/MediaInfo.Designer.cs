namespace MP3Player1
{
    partial class MediaInfo
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.tboxDescription = new System.Windows.Forms.TextBox();
            this.lbFilesCount = new System.Windows.Forms.Label();
            this.lbPath = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.btnDuration = new System.Windows.Forms.Button();
            this.lbSize = new System.Windows.Forms.Label();
            this.btnFixFiles = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(75, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(33, 13);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Title: ";
            // 
            // tboxDescription
            // 
            this.tboxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxDescription.BackColor = System.Drawing.SystemColors.Window;
            this.tboxDescription.Location = new System.Drawing.Point(13, 30);
            this.tboxDescription.Multiline = true;
            this.tboxDescription.Name = "tboxDescription";
            this.tboxDescription.ReadOnly = true;
            this.tboxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tboxDescription.Size = new System.Drawing.Size(299, 205);
            this.tboxDescription.TabIndex = 1;
            this.tboxDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboxDescription_KeyDown);
            // 
            // lbFilesCount
            // 
            this.lbFilesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFilesCount.AutoSize = true;
            this.lbFilesCount.Location = new System.Drawing.Point(10, 273);
            this.lbFilesCount.Name = "lbFilesCount";
            this.lbFilesCount.Size = new System.Drawing.Size(34, 13);
            this.lbFilesCount.TabIndex = 2;
            this.lbFilesCount.Text = "Files: ";
            // 
            // lbPath
            // 
            this.lbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPath.AutoSize = true;
            this.lbPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPath.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbPath.Location = new System.Drawing.Point(10, 260);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(35, 13);
            this.lbPath.TabIndex = 4;
            this.lbPath.Text = "Path: ";
            this.lbPath.Click += new System.EventHandler(this.lbPath_Click);
            // 
            // lbVersion
            // 
            this.lbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(10, 299);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(95, 13);
            this.lbVersion.TabIndex = 5;
            this.lbVersion.Text = "Assembly Version: ";
            // 
            // btnDuration
            // 
            this.btnDuration.Location = new System.Drawing.Point(13, 4);
            this.btnDuration.Name = "btnDuration";
            this.btnDuration.Size = new System.Drawing.Size(56, 23);
            this.btnDuration.TabIndex = 6;
            this.btnDuration.Text = "Duration";
            this.btnDuration.UseVisualStyleBackColor = true;
            this.btnDuration.Click += new System.EventHandler(this.btnDuration_Click);
            // 
            // lbSize
            // 
            this.lbSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSize.AutoSize = true;
            this.lbSize.Location = new System.Drawing.Point(10, 286);
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(33, 13);
            this.lbSize.TabIndex = 7;
            this.lbSize.Text = "Size: ";
            // 
            // btnFixFiles
            // 
            this.btnFixFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFixFiles.Location = new System.Drawing.Point(209, 273);
            this.btnFixFiles.Name = "btnFixFiles";
            this.btnFixFiles.Size = new System.Drawing.Size(103, 23);
            this.btnFixFiles.TabIndex = 8;
            this.btnFixFiles.Text = "Fix naming of files";
            this.btnFixFiles.UseVisualStyleBackColor = true;
            this.btnFixFiles.Click += new System.EventHandler(this.btnFixFiles_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(13, 234);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save info";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.BackgroundImage = global::DGMediaplayer.Properties.Resources.Download_blue;
            this.btnDownload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(284, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(27, 24);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDownload_MouseDown);
            // 
            // MediaInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(324, 321);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFixFiles);
            this.Controls.Add(this.lbSize);
            this.Controls.Add(this.btnDuration);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.lbFilesCount);
            this.Controls.Add(this.tboxDescription);
            this.Controls.Add(this.lbTitle);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 360);
            this.Name = "MediaInfo";
            this.Text = "MediaInfo";
            this.Activated += new System.EventHandler(this.MediaInfo_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MediaInfo_FormClosing);
            this.Load += new System.EventHandler(this.MediaInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.TextBox tboxDescription;
        private System.Windows.Forms.Label lbFilesCount;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Button btnDuration;
        private System.Windows.Forms.Label lbSize;
        private System.Windows.Forms.Button btnFixFiles;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDownload;
    }
}