using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MP3Player1;

namespace DGMediaplayer
{
    public partial class Download : Form
    {
        private string _path;
        private List<Label> _labels = new List<Label>();
        private List<ProgressBar> _progressBars = new List<ProgressBar>();
        private List<WebClient> _webClients = new List<WebClient>();
        private List<string> _files = new List<string>();
        private List<DateTime> _startTimes = new List<DateTime>();
        private List<bool> _finished = new List<bool>();
        private List<string> _paths = new List<string>();
        private List<string> _URIs = new List<string>(); 
        private List<ContextMenuStrip> _contextMenuStrips = new List<ContextMenuStrip>();
        private List<ToolStripItem> _cancelItems = new List<ToolStripItem>();
        private List<ToolStripItem> _copyItems = new List<ToolStripItem>();
        private List<ToolStripItem> _restartItems = new List<ToolStripItem>();
        private List<ToolStripItem> _removeItems = new List<ToolStripItem>();
        private readonly Mediaplayer _parentform;

        public Download(string path, Mediaplayer mediaplayer)
        {
            InitializeComponent();
            this._path = path;
            this._parentform = mediaplayer;
            mediaplayer.SaveDownloader(this);
        }

        public void UpdatePath(string path)
        {
            this._path = path;
        }

        void Download_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            DownloadFile();
            tboxFrom.SelectAll();
        }
        void ProgressBarRightClick(object sender, EventArgs e)
        {
            int index = _progressBars.IndexOf((ProgressBar)sender);
            if (index >= 0)
            {
                _cancelItems[index].Visible = _webClients[index].IsBusy;
                _restartItems[index].Visible = !_webClients[index].IsBusy;
                _removeItems[index].Visible = !_webClients[index].IsBusy;
                _contextMenuStrips[index].Show(MousePosition);
            }
        }
        void contexMenu_CancelItemClicked(object sender, EventArgs e)
        {
            int index = _cancelItems.IndexOf((ToolStripItem)sender);
            if (index >= 0)
            {
                if (_webClients[index].IsBusy)
                {
                    _webClients[index].CancelAsync();
                }
            }
        }

        void contexMenu_RestartItemClicked(object sender, EventArgs e)
        {
            int index = _restartItems.IndexOf((ToolStripItem)sender);
            if (index >= 0)
            {
                if (!_webClients[index].IsBusy)
                {
                    ModifyProgressBarColor.SetState(_progressBars[index], 1);
                    _startTimes[index] = DateTime.Now;
                    using (_webClients[index])
                    {
                        _webClients[index].DownloadProgressChanged += wc_DownloadProgressChanged;
                        _webClients[index].DownloadFileCompleted += wc_DownloadCompleted;
                        _webClients[index].DownloadFileAsync(new System.Uri(_URIs[index]), _paths[index] + _files[index]);
                    }
                    _finished[index] = false;
                    _parentform.DisAllowClosing();
                }
            }
        }

        private void contexMenu_RemoveItemClicked(object sender, EventArgs e)
        {
            int index = _removeItems.IndexOf((ToolStripItem) sender);
            if (index >= 0)
            {
                if (!_webClients[index].IsBusy)
                {
                    _labels.RemoveAt(index);
                    _progressBars.RemoveAt(index);
                    _webClients.RemoveAt(index);
                    _files.RemoveAt(index);
                    _startTimes.RemoveAt(index);
                    _finished.RemoveAt(index);
                    _paths.RemoveAt(index);
                    _URIs.RemoveAt(index);
                    _contextMenuStrips.RemoveAt(index);
                    _cancelItems.RemoveAt(index);
                    _copyItems.RemoveAt(index);
                    _restartItems.RemoveAt(index);
                    _removeItems.RemoveAt(index);
                    panelDownloads.Controls.Clear();
                    panelDownloads.AutoScroll = false;
                    for (int i = 0; i < _progressBars.Count; i++)
                    {
                        _labels[i].Top = 23 * (i*2);
                        _progressBars[i].Top = 23 * ((i * 2) + 1);
                        panelDownloads.Controls.Add(_labels[i]);
                        panelDownloads.Controls.Add(_progressBars[i]);
                    }
                    panelDownloads.AutoScroll = true;
                }
            }
        }

        void contexMenu_CopyItemClicked(object sender, EventArgs e)
        {
            int index = _copyItems.IndexOf((ToolStripItem)sender);
            if (index >= 0)
            {
                Clipboard.SetText(_URIs[index]);
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int index = _webClients.IndexOf((WebClient) sender);
            if (index >= 0)
            {
                _progressBars[index].Value = e.ProgressPercentage;
                TimeSpan timeElapsed = DateTime.Now - _startTimes[index];
                long timeRemaining = (long) ((100 - e.ProgressPercentage)*timeElapsed.TotalSeconds/e.ProgressPercentage);
                if (timeRemaining > 0)
                {
                    //Console.WriteLine(timeRemaining.ToString());
                    int estimateOffset = (int) ((100 - e.ProgressPercentage)*0.8);
                    if (estimateOffset <= 0)
                    {
                        estimateOffset = 1;
                    }
                    //timeRemaining = (timeRemaining/estimateOffset)*estimateOffset;
                    //Console.WriteLine(timeRemaining.ToString() + " estimated with " + estimateOffset);
                    string timeRemainingFormatted = string.Format("{0}m, {1:00}s", timeRemaining/60, timeRemaining%60);
                    _labels[index].Text = _files[index] + ": " + timeRemainingFormatted;
                    _labels[index].AutoSize = true;
                }
                else
                {
                    _labels[index].Text = _files[index];
                }
            }
        }

        void wc_DownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            int index = _webClients.IndexOf((WebClient)sender);
            if (index >= 0)
            {
                //progressBars[index].Enabled = false;
                _finished[index] = true;
                if (e.Error == null && !e.Cancelled)
                {
                    _labels[index].Text = "✔ " + _files[index];
                    ModifyProgressBarColor.SetState(_progressBars[index], 3);
                }
                else
                {
                    _labels[index].Text = "✖ " + _files[index];
                    _progressBars[index].Value = _progressBars[index].Maximum;
                    File.Delete(_paths[index] + _files[index]);
                    ModifyProgressBarColor.SetState(_progressBars[index], 2);
                }
                bool allFinished = true;
                foreach (bool finish in _finished)
                {
                    if (!finish)
                    {
                        allFinished = false;
                    }
                }
                if (allFinished)
                {
                    downloadFinishedNotification.Visible = true;
                    downloadFinishedNotification.ShowBalloonTip(2000);
                    _parentform.ReloadMedia();
                    _parentform.AllowClosing();
                }
            }
        }

        private void tboxTo_TextChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = IsValid();
        }

        private bool IsValidText(string text)
        {
            return !string.IsNullOrEmpty(text) && text.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && !File.Exists(Path.Combine(this._path, text));
        }

        private bool IsValid()
        {
            Uri uri = null;
            if (!Uri.TryCreate(tboxFrom.Text, UriKind.Absolute, out uri) || null == uri)
            {
                return false;
            }
            return !string.IsNullOrEmpty(tboxTo.Text) && tboxTo.Text.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && !File.Exists(Path.Combine(this._path, tboxTo.Text));
        }

        private bool IsValidUri(string uriString)
        {
            Uri uri = null;
            return Uri.TryCreate(uriString, UriKind.Absolute, out uri) && null != uri;
        }

        private void tboxFrom_TextChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = IsValid();
            if (IsValidUri(tboxFrom.Text))
            {
                string extension = Path.GetExtension(tboxFrom.Text);
                if (extension.Length > 4)
                {
                    extension = "";
                }
                if (extension != "")
                {
                    tboxTo.Text = Path.GetFileNameWithoutExtension(tboxTo.Text);
                    tboxTo.Text += extension;
                }

                int x = -1;
                if (int.TryParse(Path.GetFileNameWithoutExtension(tboxTo.Text), out x))
                {
                    tboxTo.Text = tboxTo.Text.Replace(Path.GetFileNameWithoutExtension(tboxTo.Text), "");
                }
                if (Path.GetFileNameWithoutExtension(tboxTo.Text) == "")
                {
                    for (int i = 1; i < 9999; i++)
                    {
                        if (!File.Exists(this._path + i + extension) && extension != "")
                        {
                            tboxTo.Text = i + extension;
                            break;
                        }
                        if (!File.Exists(this._path + i + Path.GetExtension(tboxTo.Text)) && extension == "")
                        {
                            tboxTo.Text = i + Path.GetExtension(tboxTo.Text);
                            break;
                        }
                    }
                }
            }
        }

        private void tboxTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DownloadFile();
            }
        }

        private void tboxFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DownloadFile();
            }
        }

        private void DownloadFile()
        {
            string from = tboxFrom.Text;
            string to = this._path + tboxTo.Text;
            if (IsValid())
            {

                panelDownloads.AutoScroll = false;
                Label label = new Label();
                label.Text = tboxTo.Text;
                _files.Add(tboxTo.Text);
                _paths.Add(_path);
                _URIs.Add(tboxFrom.Text);
                _startTimes.Add(DateTime.Now);
                label.Top = 23 * (_labels.Count + _progressBars.Count);
                _labels.Add(label);
                panelDownloads.Controls.Add(label);
                ProgressBar progressBar = new ProgressBar();
                progressBar.Height = 13;
                progressBar.Width = 279;
                progressBar.Top = 23 * (_labels.Count + _progressBars.Count);
                progressBar.Click += ProgressBarRightClick;
                ContextMenuStrip contexMenu = new ContextMenuStrip();
                ToolStripItem cancelItem = contexMenu.Items.Add("Cancel");
                cancelItem.Click += contexMenu_CancelItemClicked;
                _cancelItems.Add(cancelItem);
                ToolStripItem restartItem = contexMenu.Items.Add("Restart");
                restartItem.Click += contexMenu_RestartItemClicked;
                _restartItems.Add(restartItem);
                ToolStripItem copyItem = contexMenu.Items.Add("Copy URL");
                copyItem.Click += contexMenu_CopyItemClicked;
                _copyItems.Add(copyItem);
                ToolStripItem removeItem = contexMenu.Items.Add("Remove from list");
                removeItem.Click += contexMenu_RemoveItemClicked;
                _removeItems.Add(removeItem);
                _contextMenuStrips.Add(contexMenu);
                _progressBars.Add(progressBar);
                panelDownloads.Controls.Add(progressBar);
                panelDownloads.AutoScroll = true;

                WebClient webClient = new WebClient();
                using (webClient)
                {
                    webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
                    webClient.DownloadFileCompleted += wc_DownloadCompleted;
                    webClient.DownloadFileAsync(new System.Uri(from), to);
                }
                _webClients.Add(webClient);
                string extension = Path.GetExtension(tboxTo.Text);
                tboxTo.Text = extension;
                _finished.Add(false);
                _parentform.DisAllowClosing();
                tboxFrom.Text = "";
                tboxFrom.Focus();
            }
        }

        public void SetPath(string path)
        {
            this._path = path;
        }

        public void CancelAll()
        {
            for (int i = 0; i < _webClients.Count; i++)
            {
                _webClients[i].CancelAsync();
            }
        }

        private void downloadFinishedNotification_BalloonTipClosed(object sender, EventArgs e)
        {
            downloadFinishedNotification.Visible = false;
        }

        private void downloadFinishedNotification_BalloonTipClicked(object sender, EventArgs e)
        {
            downloadFinishedNotification.Visible = false;
        }
    }
}

public static class ModifyProgressBarColor
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    public static void SetState(this ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }
}