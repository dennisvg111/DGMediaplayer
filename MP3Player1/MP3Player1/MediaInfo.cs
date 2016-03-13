using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MP3Player1
{
    public partial class MediaInfo : Form
    {
        private readonly string[] _videoExtensions =
        {".asf", ".wma", ".wmv", ".wm", ".avi", "mpg", ".mpeg", ".m1v", ".mp2", ".mpe", ".wmz", ".wms", ".mov", ".mp4", ".gif"};
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public string Title { get; set; }
        public string DescriptionFile { get; set; }
        public string Path { get; set; }
        public List<string> FilesList { get; set; }
        MediaDuration _mediaDuration;
        private long _duration = 0;
        private FixFileNaming _fixFileNaming;
        private Mediaplayer parentform;
        private bool update = true;
        public MediaInfo()
        {
            InitializeComponent();
            _mediaDuration = new MediaDuration(FilesList, "_", this);
            update = true;
        }
        public MediaInfo(string title, string descriptionFile, string path, string[] filesList)
        {
            InitializeComponent();
            Title = title;
            DescriptionFile = descriptionFile;
            Path = path;
            FilesList = new List<string>();
            FilesList.AddRange(filesList);
            _mediaDuration = new MediaDuration(FilesList, path, this);
            update = true;
        }
        public MediaInfo(string title, string path, string[] filesList)
        {
            InitializeComponent();
            Title = title;
            Path = path;
            FilesList = new List<string>();
            FilesList.AddRange(filesList);
            _mediaDuration = new MediaDuration(FilesList, path, this);
            update = true;
        }

        private void UpdateForm()
        {
            tboxDescription.AutoSize = true;
            if (!string.IsNullOrEmpty(Title))
            {
                lbTitle.Text = "Title: " + Title;
            }
            else
            {
                lbTitle.Text = "No title found";
            }
            if (!string.IsNullOrEmpty(DescriptionFile))
            {
                tboxDescription.Text = File.ReadAllText(DescriptionFile);
            }
            else
            {
                tboxDescription.Text = "No description found";
            }
            tboxDescription.ReadOnly = string.IsNullOrEmpty(Path);
            btnSave.Enabled = !string.IsNullOrEmpty(Path);
            if (FilesList != null)
            {
                lbFilesCount.Text = "Files: " + FilesList.Count;
                long totalFileSize = 0;
                foreach (string file in FilesList)
                {
                    totalFileSize += GetFileSize(file);
                }
                lbSize.Text = SizeSuffix(totalFileSize);
            }
            else
            {
                lbFilesCount.Text = "Files: 0";
                lbSize.Text = "No files found";
            }
            if (!string.IsNullOrEmpty(Path))
            {
                lbPath.Text = "Path: " + Path;
            }
            else
            {
                lbPath.Text = "No path found";
            }
            lbVersion.Text = "Assembly: " + Assembly.GetExecutingAssembly().GetName().Name + ", Version: " +
                             Assembly.GetExecutingAssembly().GetName().Version;
            if (!string.IsNullOrEmpty(Path) && Directory.Exists(Path))
            {
                lbPath.Font = new Font(lbPath.Font.FontFamily, lbPath.Font.Size, FontStyle.Underline);
                lbPath.Cursor = Cursors.Hand;
                lbPath.ForeColor = Color.FromKnownColor(KnownColor.HotTrack);
            }
            else
            {
                lbPath.Font = new Font(lbPath.Font.FontFamily, lbPath.Font.Size);
                lbPath.Cursor = DefaultCursor;
                lbPath.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }
            tboxDescription.Select(tboxDescription.Text.Length,0);
            Size tempSize = this.Size;
            this.AutoSize = false;
            this.Size = tempSize;
            MinimumSize = this.Size;
            btnDuration.Enabled = FilesList != null && FilesList.Count != 0;
            btnFixFiles.Enabled = !string.IsNullOrEmpty(Path);
            btnDownload.Enabled = !string.IsNullOrEmpty(Path);
            update = false;
        }

        private void MediaInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            if (parentform != null)
            {
                parentform.PreventClosing();
            }
            e.Cancel = true; // this cancels the close event.
        }

        private void btnDuration_Click(object sender, EventArgs e)
        {
            if (FilesList != null && FilesList.Count > 0)
            {
                TimeSpan duration = TimeSpan.FromSeconds(_duration);
                if (_duration > 0 && _duration < 3600)
                {
                    MessageBox.Show("Duration: " + duration.Minutes + "m " +
                                    duration.Seconds + "s");
                }
                else if (_duration > 0)
                {
                    MessageBox.Show("Duration: " + Math.Floor(duration.TotalHours) + "h " + duration.Minutes + "m " + duration.Seconds + "s");
                }
                else
                {
                    _mediaDuration.StartPosition = FormStartPosition.CenterScreen;
                    _mediaDuration.Show();
                    _mediaDuration.CalculateTotalDuration();
                }
            }
        }

        public void CreateNewDurationScanner()
        {
            _mediaDuration = new MediaDuration(FilesList, Path, this);
        }

        public void StoreDuration(long duration)
        {
            _duration = duration;
        }
        public string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public long GetFileSize(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    return new FileInfo(file).Length;
                }
            }
            catch (Exception)
            {
                //file not found
            }
            return 0;
        }

        private void lbPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Path) && Directory.Exists(Path))
            {
                if (FilesList != null && FilesList.Count != 0)
                {
                    if (parentform != null && !string.IsNullOrEmpty(parentform.CurrentMediaString) &&
                        System.IO.File.Exists(parentform.CurrentMediaString))
                    {
                        string filePath = @parentform.CurrentMediaString.Replace(@"/", @"\");
                        if (!File.Exists(filePath))
                        {
                            return;
                        }
                        string argument = "/select, \"" + filePath + "\"";

                        Process.Start("explorer.exe", argument);
                    }
                    else
                    {
                        Process.Start(Path);
                    }
                }
                else
                {
                    Process.Start(Path);
                }
            }
        }

        private void btnFixFiles_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                if (_fixFileNaming == null)
                {
                    _fixFileNaming = new FixFileNaming(parentform, this, Path);
                }
                _fixFileNaming.StartPosition = FormStartPosition.Manual;
                _fixFileNaming.Location = new Point(this.Left, this.Top);
                _fixFileNaming.Show();
            }
        }

        private void tboxDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.SuppressKeyPress = true;
                (sender as TextBox).SelectAll();
            }
        }

        public void ReloadMedia(string[] files)
        {
            if (FilesList != null)
            {
                FilesList.Clear();
                if (files != null)
                {
                    FilesList.AddRange(files);
                }
            }
        }

        public void SaveParentForm(Mediaplayer mediaplayer)
        {
            parentform = mediaplayer;
        }

        private void MediaInfo_Activated(object sender, EventArgs e)
        {
            if (_fixFileNaming != null && _fixFileNaming.Visible)
            {
                _fixFileNaming.WindowState = FormWindowState.Normal;
                _fixFileNaming.Focus();
            }
            if (parentform.GetDownload() != null && parentform.GetDownload().Visible)
            {
                parentform.GetDownload().WindowState = FormWindowState.Normal;
                parentform.GetDownload().Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DescriptionFile) && File.Exists(DescriptionFile))
            {
                File.WriteAllText(DescriptionFile, tboxDescription.Text);
            }
            else
            {
                DescriptionFile = Path + "info.txt";
                File.WriteAllText(DescriptionFile, tboxDescription.Text);
            }
        }

        private void MediaInfo_Load(object sender, EventArgs e)
        {
            if (update)
            {
                UpdateForm();
            }
        }

        private void btnDownload_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (parentform.GetDownload() == null)
                {
                    parentform.SetDownload(new DGMediaplayer.Download(Path, parentform));
                    parentform.GetDownload().Location = new Point(this.Left, this.Top);
                    parentform.GetDownload().Size = this.MinimumSize;
                    parentform.GetDownload().MinimumSize = this.MinimumSize;
                }
                parentform.GetDownload().SetPath(Path);
                parentform.GetDownload().StartPosition = FormStartPosition.Manual;
                parentform.GetDownload().Show();
            }
            if (e.Button == MouseButtons.Right && parentform.GetDownload() != null)
            {
                ContextMenuStrip downloadContextMenuStrip = new ContextMenuStrip();
                ToolStripItem cancelAll = downloadContextMenuStrip.Items.Add("Cancel all downloads");
                cancelAll.Click += contexMenu_CancelAllClicked;
                ToolStripItem restartAll = downloadContextMenuStrip.Items.Add("Restart all downloads");
                restartAll.Click += contexMenu_RestartAllClicked;
                downloadContextMenuStrip.Show(MousePosition);
            }
        }

        void contexMenu_CancelAllClicked(object sender, EventArgs e)
        {
            parentform.GetDownload().CancelAll();
        }

        void contexMenu_RestartAllClicked(object sender, EventArgs e)
        {
            parentform.GetDownload().RestartAll();
        }
    }
}
