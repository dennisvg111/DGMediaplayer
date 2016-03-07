using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3Player1
{
    public partial class FixFileNaming : Form
    {
        private string[] _files;
        private string[] _newFiles;
        private Mediaplayer _mediaPlayer;
        private MediaInfo _mediaInfo;
        private string _path;
        private string _infoFile;
        private readonly string[] _videoExtensions ={ ".asf", ".wma", ".wmv", ".wm", ".avi", "mpg", ".mpeg", ".m1v", ".mp2", ".mpe", ".wmz", ".wms", ".mov", ".mp4", ".gif" };
        private readonly string[] _audioExtensions = { ".mp3", ".m3u", ".mpa", ".mid", ".midi", ".rmi", ".cda", ".m4v" };
        private readonly string[] _textExtensions = { ".txt" };

        public FixFileNaming(Mediaplayer mediaplayer, MediaInfo mediaInfo, string path)
        {
            InitializeComponent();
            _mediaPlayer = mediaplayer;
            _mediaInfo = mediaInfo;
            _path = path;
            lbPath.Text = "Path: " + _path;
            Reload();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _files.Length; i++)
            {
                //MessageBox.Show(_files[i] + "\r\n" + _newFiles[i]);
                if (_newFiles[i] != _files[i])
                {
                    try
                    {
                        File.Move(_files[i], _newFiles[i]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An error has occured while renaming \r\n " + _files[i] + " to \r\n" +
                                        _newFiles[i] + ", please check the new fileNames");
                    }
                }
            }
            _mediaPlayer.ReloadMedia();
            _mediaInfo.ReloadMedia(_newFiles);
            tboxToReplace.Text = "";
            tboxReplaceWith.Text = "";
            Reload();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            if (CheckPath(_path))
            {
                _files = Directory.GetFiles(_path);
                if (_files != null)
                {
                    for (int i = 0; i < _files.Length; i++)
                    {
                        if (FileContainsCorrectExtension(_files[i], _textExtensions))
                        {
                            if (string.IsNullOrEmpty(_infoFile))
                            {
                                _infoFile = _files[i];
                            }
                            else
                            {
                                _infoFile = "Error";
                            }
                        }
                        //FileContainsCorrectExtension(_files[i]);
                        if (!FileContainsCorrectExtension(_files[i], _audioExtensions) &&
                            !FileContainsCorrectExtension(_files[i], _videoExtensions))
                        {
                            _files = _files.Where(w => w != _files[i]).ToArray();
                            i--;
                        }
                    }
                    if (_files.Length <= 0)
                    {
                        string[] subfolders = Directory.GetDirectories(_path, "*", SearchOption.AllDirectories);
                        _files = (from subfolder in subfolders
                            from file in Directory.GetFiles(subfolder)
                            where
                                FileContainsCorrectExtension(file, _audioExtensions) ||
                                FileContainsCorrectExtension(file, _videoExtensions)
                            select file.Replace(@"\", @"/")).ToArray();
                    }
                }
                DirectoryInfo parentLocation = Directory.GetParent(_path);
                if (parentLocation != null && (string.IsNullOrEmpty(_infoFile) || !File.Exists(_infoFile)))
                {
                    string[] filesInParentLocation = Directory.GetFiles(parentLocation.Parent.FullName);
                    foreach (string file in filesInParentLocation)
                    {
                        if (FileContainsCorrectExtension(file, _textExtensions))
                        {
                            if (string.IsNullOrEmpty(_infoFile))
                            {
                                _infoFile = file;
                            }
                            else
                            {
                                _infoFile = "Error";
                            }
                        }
                    }
                }
                if (File.Exists(_path + "info.txt"))
                {
                    _infoFile = _path + "info.txt";
                }
                else if (parentLocation != null && parentLocation.Parent != null &&
                         File.Exists(parentLocation.Parent.FullName + "/info.txt"))
                {
                    _infoFile = parentLocation.Parent.FullName + "/info.txt";
                }
            }
            lboxOldFiles.Items.Clear();
            if (_files != null)
            {
                foreach (string file in _files)
                {
                    lboxOldFiles.Items.Add(file.Replace(_path, ""));
                }
            }
            if (!string.IsNullOrEmpty(_infoFile) && _infoFile != "Error")
            {
                lboxOldFiles.Items.Add(_infoFile.Replace(_path, ""));
            }
            ReloadNewFiles();
        }

        private void ReloadNewFiles()
        {
            if (_files != null)
            {
                string newFile;
                string prefix = "";
                string mark = "";
                string extension = "";
                string customToString = "";
                int maxLength = Convert.ToInt32(nudLength.Value);
                _newFiles = new string[_files.Length];
                _files.CopyTo(_newFiles, 0);
                if (maxLength > 0)
                {
                    foreach (string file in _files)
                    {
                        //MessageBox.Show(GetFileNameWithoutExtras(new FileInfo(file).Name));
                        if (string.IsNullOrEmpty(GetFileNameWithoutExtras(new FileInfo(file).Name)))
                        {
                            //MessageBox.Show(new FileInfo(file).Name);
                            if (FileContainsCorrectExtension(file, _videoExtensions) &&
                                RemoveMarkFromString(RemoveExtensionsFromFile(new FileInfo(file).Name)
                                    .Replace(_path, "")).Length >
                                maxLength)
                            {
                                //MessageBox.Show(new FileInfo(file).Name);
                                int tempLength =
                                    Math.Round(
                                        Convert.ToDecimal(
                                            RemoveMarkFromString(
                                                RemoveExtensionsFromFile(new FileInfo(file).Name).Replace(_path, ""))
                                                .Replace(".",
                                                    System.Globalization.CultureInfo.CurrentCulture.NumberFormat
                                                        .NumberDecimalSeparator))).ToString().Length;
                                if (tempLength > maxLength)
                                {
                                    maxLength = tempLength;
                                }
                            }
                        }
                    }
                    if (maxLength != nudLength.Value)
                    {
                        nudLength.Value = maxLength;
                    }
                    for (int i = 0; i < maxLength; i++)
                    {
                        customToString += "0";
                    }
                    //MessageBox.Show(customToString);
                }
                lboxNewFiles.Items.Clear();
                for (int i = 0; i < _newFiles.Length; i++)
                {
                    newFile = _newFiles[i];
                    extension = GetExtensionsFromFile(newFile);
                    newFile = RemoveExtensionsFromFile(newFile);
                    newFile = newFile.Replace(@"\", @"/").Replace(_path.Replace(@"\", @"/"), "");
                    mark = GetMarkFromString(newFile);
                    newFile = RemoveMarkFromString(newFile);
                    if (!string.IsNullOrEmpty(tboxToReplace.Text))
                    {
                        newFile = Replace(newFile, tboxToReplace.Text, tboxReplaceWith.Text,
                            StringComparison.OrdinalIgnoreCase);
                    }
                    //MessageBox.Show(newFile +" contains "+ _path);
                    prefix = "";
                    if (new FileInfo(_files[i]).Directory.Name != new DirectoryInfo(_path).Name)
                    {
                        //MessageBox.Show(new FileInfo(_files[i]).Directory.Name + "\r\n" + new DirectoryInfo(_path).Name);
                        prefix = new FileInfo(_files[i]).Directory.Name + @"/";
                        //MessageBox.Show(newFile);
                        newFile = newFile.Replace(new FileInfo(_files[i]).Directory.Name + @"/", "");
                        //MessageBox.Show(newFile);
                    }
                    newFile = newFile.Replace(@"/", "");
                    if (chboxIncludeDirectory.Checked && !newFile.Contains(new DirectoryInfo(_path).Name))
                    {
                        prefix = prefix + new DirectoryInfo(_path).Name + "_";
                        //MessageBox.Show(prefix);
                    }
                    if (maxLength > 0 && string.IsNullOrEmpty(GetFileNameWithoutExtras(newFile)))
                    {
                        newFile = Convert.ToDecimal(newFile.Replace(@"/", "")
                            .Replace(".",
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat
                                    .NumberDecimalSeparator))
                            .ToString(customToString + ".##############################").Replace(
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat
                                    .NumberDecimalSeparator, ".");
                    }
                    //MessageBox.Show("path: "+_path+"\r\nprefix: "+prefix+"\r\nnewfile: "+newFile+"\r\nmark: "+mark+"\r\nextension: "+extension);
                    _newFiles[i] = _path + prefix + newFile + mark + extension;
                    //MessageBox.Show(i +": "+_newFiles[i]);
                    lboxNewFiles.Items.Add(_newFiles[i].Replace(_path, ""));
                }
                if (!string.IsNullOrEmpty(_infoFile) && _infoFile != "Error")
                {
                    lboxNewFiles.Items.Add(_infoFile.Replace(_path, ""));
                }
            }
        }

        private bool CheckPath(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

        private string DirectoryName(string file)
        {
            if (File.Exists(file))
            {
                return new FileInfo(file).DirectoryName;
            }
            return "";
        }

        private bool FileContainsCorrectExtension(string file, string[] extensions)
        {
            if (!CheckPath(file))
            {
                return false;
            }
            FileInfo fileInfo = new FileInfo(file);
            string extension = fileInfo.Extension;
            return extensions.Contains(extension);
        }
        private string GetFileNameWithoutExtras(string file)
        {
            string newFile = RemoveExtensionsFromFile(file);
            newFile = RemoveMarkFromString(newFile);
            //MessageBox.Show(file + "\r\n" + RemoveExtensionsFromFile(file));
            return
                Regex.Replace(newFile.Replace(_path, ""), @"[\d]", "").Replace(".", "").Replace(",", "").Replace("-", "");
        }
        private string GetExtensionsFromFile(string stringWithExtension)
        {
            string extension = "";
            if (File.Exists(stringWithExtension))
            {
                FileInfo fileInfo = new FileInfo(stringWithExtension);
                extension = fileInfo.Extension;
            }
            return extension;
        }
        private string RemoveExtensionsFromFile(string stringWithExtension)
        {
            string stringWithoutExtension = stringWithExtension;
            if (File.Exists(stringWithExtension))
            {
                FileInfo fileInfo = new FileInfo(stringWithExtension);
                string extension = fileInfo.Extension;
                stringWithoutExtension = stringWithExtension.Substring(0, stringWithExtension.LastIndexOf(extension)) + stringWithExtension.Substring(stringWithExtension.LastIndexOf(extension) + extension.Length);
            }
            else if (stringWithExtension.Contains("."))
            {
                stringWithoutExtension = stringWithExtension.Remove(stringWithExtension.IndexOf("."));
            }
            return stringWithoutExtension;
        }
        private string GetMarkFromString(string file)
        {
            string mark = "";
            int startPosition = 0;
            int endPosition = 0;
            if (file.LastIndexOf("-") == file.Length - "-".Length)
            {
                startPosition = file.LastIndexOf("-");
                endPosition = file.LastIndexOf("-") + "-".Length;
                string tempfile = file.Remove(file.LastIndexOf("-"));
                tempfile = Regex.Replace(tempfile, @"[\d]", "").Replace(".", "");
                if (tempfile.Length > 0 && tempfile.LastIndexOf("-") == tempfile.Length - "-".Length)
                {
                    startPosition = file.LastIndexOf("-", file.LastIndexOf("-") - 1);
                }
            }
            if (startPosition > 0 && endPosition > 0)
            {
                mark = file.Substring(startPosition, endPosition - startPosition);
            }
            return mark;
        }
        private string RemoveMarkFromString(string file)
        {
            if (file.LastIndexOf("-") == file.Length - "-".Length)
            {
                file = file.Remove(file.LastIndexOf("-"));
            }
            string tempfile = file;
            tempfile = Regex.Replace(tempfile, @"[\d]", "").Replace(".", "");
            if (tempfile.Length > 0 && tempfile.LastIndexOf("-") == tempfile.Length - "-".Length)
            {
                file = file.Remove(file.LastIndexOf("-"));
            }
            return file;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Reload();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (nudLength == ActiveControl)
            {
                //Reload();
            }
        }

        private void FixFileNaming_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void FixFileNaming_Activated(object sender, EventArgs e)
        {
            Reload();
        }

        private void nudLength_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void tboxToReplace_TextChanged(object sender, EventArgs e)
        {
            Reload();
        }

        private void tboxReplaceWith_TextChanged(object sender, EventArgs e)
        {
            Reload();
        }
        public static string Replace(string str, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder renameBuilder = new StringBuilder();

            int previousIndex = 0;
            int index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                renameBuilder.Append(str.Substring(previousIndex, index - previousIndex));
                renameBuilder.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            renameBuilder.Append(str.Substring(previousIndex));

            return renameBuilder.ToString();
        }

        private void lboxOldFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            lboxNewFiles.SelectedIndex = lboxOldFiles.SelectedIndex;
            if (tboxToReplace.Text == "" && File.Exists(_files[lboxOldFiles.SelectedIndex]))
            {
                tboxToReplace.Text = new FileInfo(_files[lboxOldFiles.SelectedIndex]).Name.Replace(new FileInfo(_files[lboxOldFiles.SelectedIndex]).Extension, "");
                tboxReplaceWith.Text = tboxToReplace.Text;
            }
        }

        private void lboxNewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            lboxOldFiles.SelectedIndex = lboxNewFiles.SelectedIndex;
        }
    }
}