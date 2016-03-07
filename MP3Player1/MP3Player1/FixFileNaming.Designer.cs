namespace MP3Player1
{
    partial class FixFileNaming
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
            this.lboxOldFiles = new System.Windows.Forms.ListBox();
            this.lboxNewFiles = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.chboxIncludeDirectory = new System.Windows.Forms.CheckBox();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.gboxSettings = new System.Windows.Forms.GroupBox();
            this.tboxReplaceWith = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tboxToReplace = new System.Windows.Forms.TextBox();
            this.lbPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.gboxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lboxOldFiles
            // 
            this.lboxOldFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lboxOldFiles.FormattingEnabled = true;
            this.lboxOldFiles.HorizontalScrollbar = true;
            this.lboxOldFiles.Location = new System.Drawing.Point(13, 13);
            this.lboxOldFiles.Name = "lboxOldFiles";
            this.lboxOldFiles.Size = new System.Drawing.Size(272, 160);
            this.lboxOldFiles.TabIndex = 0;
            this.lboxOldFiles.SelectedIndexChanged += new System.EventHandler(this.lboxOldFiles_SelectedIndexChanged);
            // 
            // lboxNewFiles
            // 
            this.lboxNewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxNewFiles.FormattingEnabled = true;
            this.lboxNewFiles.HorizontalScrollbar = true;
            this.lboxNewFiles.Location = new System.Drawing.Point(291, 12);
            this.lboxNewFiles.Name = "lboxNewFiles";
            this.lboxNewFiles.Size = new System.Drawing.Size(272, 160);
            this.lboxNewFiles.TabIndex = 1;
            this.lboxNewFiles.SelectedIndexChanged += new System.EventHandler(this.lboxNewFiles_SelectedIndexChanged);
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.Location = new System.Drawing.Point(488, 178);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 2;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // chboxIncludeDirectory
            // 
            this.chboxIncludeDirectory.AutoSize = true;
            this.chboxIncludeDirectory.Location = new System.Drawing.Point(6, 19);
            this.chboxIncludeDirectory.Name = "chboxIncludeDirectory";
            this.chboxIncludeDirectory.Size = new System.Drawing.Size(156, 17);
            this.chboxIncludeDirectory.TabIndex = 3;
            this.chboxIncludeDirectory.Text = "Include foldername in name";
            this.chboxIncludeDirectory.UseVisualStyleBackColor = true;
            this.chboxIncludeDirectory.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(6, 42);
            this.nudLength.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(120, 20);
            this.nudLength.TabIndex = 4;
            this.nudLength.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.nudLength.Click += new System.EventHandler(this.nudLength_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Length of numbered files";
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReload.Location = new System.Drawing.Point(13, 178);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 7;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // gboxSettings
            // 
            this.gboxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxSettings.Controls.Add(this.tboxReplaceWith);
            this.gboxSettings.Controls.Add(this.label3);
            this.gboxSettings.Controls.Add(this.label2);
            this.gboxSettings.Controls.Add(this.tboxToReplace);
            this.gboxSettings.Controls.Add(this.chboxIncludeDirectory);
            this.gboxSettings.Controls.Add(this.nudLength);
            this.gboxSettings.Controls.Add(this.label1);
            this.gboxSettings.Location = new System.Drawing.Point(13, 207);
            this.gboxSettings.Name = "gboxSettings";
            this.gboxSettings.Size = new System.Drawing.Size(550, 102);
            this.gboxSettings.TabIndex = 8;
            this.gboxSettings.TabStop = false;
            this.gboxSettings.Text = "Settings";
            // 
            // tboxReplaceWith
            // 
            this.tboxReplaceWith.Location = new System.Drawing.Point(293, 68);
            this.tboxReplaceWith.Name = "tboxReplaceWith";
            this.tboxReplaceWith.Size = new System.Drawing.Size(196, 20);
            this.tboxReplaceWith.TabIndex = 9;
            this.tboxReplaceWith.TextChanged += new System.EventHandler(this.tboxReplaceWith_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "with";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Replace";
            // 
            // tboxToReplace
            // 
            this.tboxToReplace.Location = new System.Drawing.Point(59, 68);
            this.tboxToReplace.Name = "tboxToReplace";
            this.tboxToReplace.Size = new System.Drawing.Size(196, 20);
            this.tboxToReplace.TabIndex = 6;
            this.tboxToReplace.TextChanged += new System.EventHandler(this.tboxToReplace_TextChanged);
            // 
            // lbPath
            // 
            this.lbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPath.Location = new System.Drawing.Point(94, 183);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(389, 13);
            this.lbPath.TabIndex = 9;
            this.lbPath.Text = "Path: ";
            // 
            // FixFileNaming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 321);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.gboxSettings);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.lboxNewFiles);
            this.Controls.Add(this.lboxOldFiles);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(591, 360);
            this.Name = "FixFileNaming";
            this.Text = "FixFileNaming";
            this.Activated += new System.EventHandler(this.FixFileNaming_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FixFileNaming_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.gboxSettings.ResumeLayout(false);
            this.gboxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lboxOldFiles;
        private System.Windows.Forms.ListBox lboxNewFiles;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.CheckBox chboxIncludeDirectory;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.GroupBox gboxSettings;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.TextBox tboxReplaceWith;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tboxToReplace;
    }
}