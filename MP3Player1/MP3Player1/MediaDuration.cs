using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MP3Player1
{
    public partial class MediaDuration : Form
    {
        delegate void SetTextCallback(int progress, string file, string progressString);
        private List<string> _filesList;
        private long _totalDuration = 0;
        private readonly string[] _videoExtensions = { ".asf", ".wma", ".wmv", ".wm", ".avi", "mpg", ".mpeg", ".m1v", ".mp2", ".mpe", ".wmz", ".wms", ".mov", ".mp4", ".gif" };
        private readonly string[] _audioExtensions = { ".mp3", ".m3u", ".mpa", ".mid", ".midi", ".rmi", ".cda", ".m4v" };
        BackgroundWorker _backgroundWorker1 = new BackgroundWorker();
        private Form _parentForm;
        private string _path;
        public MediaDuration(List<string> filesList, string path, Form form)
        {
            InitializeComponent();
            _filesList = filesList;
            if (filesList != null && filesList.Count > 0)
            {
                pbarFiles.Minimum = 0;
                pbarFiles.Maximum = _filesList.Count;
                pbarFiles.Value = 0;
            }
            _path = path;
            _backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork); // This does the job ...
            _backgroundWorker1.WorkerSupportsCancellation = true; // This allows cancellation.
            _parentForm = form;
        }
        private bool StringContainsCorrectExtension(string file, string[] extensions)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file);
                //MessageBox.Show(fileInfo.Extension + " " + file);
                return extensions.Contains<string>(fileInfo.Extension.ToLower());
            }
            catch
            {
                MessageBox.Show("File " + file + " not found");
                return false;
            }
        }

        public void CalculateTotalDuration()
        {
            if (!_backgroundWorker1.IsBusy)
            {
                _backgroundWorker1.RunWorkerAsync();
                _backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    (s3, e3) =>
                    {
                        Hide(); //I is hide now
                        TimeSpan duration = TimeSpan.FromSeconds(_totalDuration);
                        if (_totalDuration > 0 && _totalDuration < 3600)
                        {
                            MessageBox.Show("Duration: " + duration.Minutes + "m " +
                                            duration.Seconds + "s");
                        }
                        else if (_totalDuration > 0)
                        {
                            MessageBox.Show("Duration: " + Math.Floor(duration.TotalHours) + "h " + duration.Minutes + "m " + duration.Seconds + "s");
                        }
                        if (_totalDuration > 0)
                        {
                            ((MediaInfo) _parentForm).StoreDuration(_totalDuration);
                        }
                        ((MediaInfo) _parentForm).CreateNewDurationScanner();
                    });
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_totalDuration > 0)
            {
                return;
            }
            _totalDuration = 0;
            if (_filesList == null || _filesList.Count <= 0)
            {
                return;
            }
            _totalDuration = 0;
            int progress = 0;
            try
            {
                foreach (string file in _filesList)
                {
                    progress++;
                    if (_backgroundWorker1.CancellationPending)
                    {
                        _totalDuration = 0;
                        break;
                    }
                    else
                    {
                        this.UpdateForm(progress, file.Replace(_path, ""), progress + "/" + _filesList.Count);
                        if (StringContainsCorrectExtension(file, _videoExtensions) ||
                            StringContainsCorrectExtension(file, _audioExtensions))
                        {
                            WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
                            IWMPMedia mediainfo = wmp.newMedia(file);
                            _totalDuration += Convert.ToInt64(mediainfo.duration);
                        }
                    }
                }
            }
            catch (Exception)
            {
                _totalDuration = 0;
                // fileslist changed while looping through it
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _backgroundWorker1.CancelAsync();
            Hide();
        }

        private void UpdateForm(int progress, string file, string progressString)
        {
            if (this.lbCurrent.InvokeRequired && this.lbProgress.InvokeRequired && this.pbarFiles.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateForm);
                this.Invoke(d, new object[] { progress, file, progressString });
            }
            else
            {
                this.pbarFiles.Value = progress;
                this.lbCurrent.Text = file;
                this.lbProgress.Text = progressString;
            }
        }
    }
}
