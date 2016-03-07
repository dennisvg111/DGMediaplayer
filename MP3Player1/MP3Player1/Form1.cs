using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WMPLib;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DGMediaplayer;

// 🔀 ▶ ⏯ ➡ 

namespace MP3Player1
{
    public partial class Mediaplayer : Form
    {
        private int _timing = 0;
        private const string CurrentMarkChar = "-";
        private const string TopMarkChar = "[top]";
        private bool _doplay = false;
        private readonly List<string> _currentFileEntries = new List<string>();
        private string _correctLocation;
        private string[] _fileEntries;
        private string _location;
        private int _currentMedia = 0;
        private bool _preventClosing = false;
        private Download _download;
        private bool _waitForClosing = false;
        private readonly string[] _videoExtensions =
        {
            ".asf", ".wma", ".wmv", ".wm", ".avi", "mpg", ".mpeg", ".m1v",
            ".mp2", ".mpe", ".wmz", ".wms", ".mov", ".mp4", ".gif"
        };

        private readonly string[] _audioExtensions = 
        {
            ".mp3", ".m3u", ".mpa", ".mid", ".midi", ".rmi", ".cda", ".m4v"
        };
        private readonly string[] _textExtensions = {".txt"};
        private bool _clickStarted = false;
        private bool _seriesContainsVideoMarkChar = false;
        private MediaInfo _mediaInfoForm;
        private int _seriesTiming = 0;
        private bool _changingLocation = false;
        private string oldSorting;

        public string CurrentMediaString { get { return _currentFileEntries[_currentMedia]; } }

        public Mediaplayer()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.settings.volume = 100;
            cBoxSorting.Text = "Default";
            
        }

        private bool CheckPath()
        {
            if (File.Exists(_location))
            {
                // This path is a file
                return false;
            }
            else if (Directory.Exists(_location))
            {
                // This path is a directory
                return true;
            }
            else
            {
                // This path doesn't exist
                return false;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (!string.IsNullOrEmpty(_correctLocation))
            {
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
                folderBrowserDialog1.SelectedPath = _correctLocation.Replace("/", @"\");
            }
            folderBrowserDialog1.Description = "Select folder containing media";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                oldSorting = "Default";
                _changingLocation = true;
                _seriesTiming = 0;
                _timing = 0;
                _seriesContainsVideoMarkChar = false;
                _mediaInfoForm = null;
                _location = folderBrowserDialog1.SelectedPath;
                _location = _location.Replace(@"\", "/");
                if (_location.Substring(_location.Length - 1, 1) != "/")
                {
                    _location = _location + "/";
                }
                if (CheckPath())
                {
                    string tempTextFile = "";
                    _correctLocation = _location;
                    _fileEntries = Directory.GetFiles(_location);
                    for (int i = 0; i < _fileEntries.Length; i++)
                    {
                        if (StringContainsCorrectExtension(_fileEntries[i], _textExtensions))
                        {
                            if (string.IsNullOrEmpty(tempTextFile))
                            {
                                tempTextFile = _fileEntries[i];
                            }
                            else
                            {
                                tempTextFile = "Error";
                            }
                        }
                        //stringContainsCorrectExtension(fileEntries[i]);
                        if (!StringContainsCorrectExtension(_fileEntries[i], _audioExtensions) &&
                            !StringContainsCorrectExtension(_fileEntries[i], _videoExtensions))
                        {
                            _fileEntries = _fileEntries.Where(w => w != _fileEntries[i]).ToArray();
                            i--;
                        }
                    }
                    if (_fileEntries.Length <= 0)
                    {
                        string[] subfolders = Directory.GetDirectories(_location, "*", SearchOption.AllDirectories);
                        _fileEntries = (from subfolder in subfolders
                            from file in Directory.GetFiles(subfolder)
                            where
                                StringContainsCorrectExtension(file, _audioExtensions) ||
                                StringContainsCorrectExtension(file, _videoExtensions)
                            select file.Replace(@"\", @"/")).ToArray();
                    }
                    FillCboxSearch();
                    lboxMedia.Items.Clear();
                    _currentMedia = 0;
                    _currentFileEntries.Clear();
                    List<string> tempfileEntries = _fileEntries.ToList();
                    tempfileEntries.Sort();
                    _fileEntries = tempfileEntries.ToArray();
                    //MessageBox.Show(_fileEntries[0]);
                    foreach (string fileEntry in _fileEntries)
                    {
                        if (fileEntry.IndexOf(cboxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            _currentFileEntries.Add(fileEntry);
                        }
                    }
                    SortSongs();
                    if (_currentFileEntries.Count > 0)
                    {
                       axWindowsMediaPlayer1.settings.autoStart = false;
                       axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];

                       axWindowsMediaPlayer1.settings.autoStart = true;
                        lboxMedia.SetSelected(_currentMedia, true);
                        UpdateLabels();
                    }
                    else
                    {
                        TitleLabel.Text = "No media files found.";
                    }
                    string mediaFolder = _location;
                    mediaFolder = mediaFolder.Remove(mediaFolder.Length - 1);
                    mediaFolder = mediaFolder.Remove(0, mediaFolder.LastIndexOf("/") + 1);
                    DirectoryInfo parentLocation = Directory.GetParent(_correctLocation);
                    if (parentLocation != null && (string.IsNullOrEmpty(tempTextFile) || !File.Exists(tempTextFile)))
                    {
                        string[] filesInParentLocation = Directory.GetFiles(parentLocation.Parent.FullName);
                        foreach (string file in filesInParentLocation)
                        {
                            if (StringContainsCorrectExtension(file, _textExtensions))
                            {
                                if (string.IsNullOrEmpty(tempTextFile))
                                {
                                    tempTextFile = file;
                                }
                                else
                                {
                                    tempTextFile = "Error";
                                }
                            }
                        }
                    }
                    if (File.Exists(_correctLocation + "info.txt"))
                    {
                        tempTextFile = _correctLocation + "info.txt";
                    }
                    else if (parentLocation != null && parentLocation.Parent != null &&
                             File.Exists(parentLocation.Parent.FullName + "/info.txt"))
                    {
                        tempTextFile = parentLocation.Parent.FullName + "/info.txt";
                    }
                    if (!string.IsNullOrEmpty(tempTextFile) &&
                        StringContainsCorrectExtension(tempTextFile, _textExtensions) &&
                        File.Exists(tempTextFile))
                    {
                        _mediaInfoForm = new MediaInfo(mediaFolder, tempTextFile, _correctLocation,
                            _fileEntries);
                        string timingString = File.ReadAllLines(tempTextFile).Last();
                        timingString = Regex.Replace(timingString, "[^0-9.]", "");
                        if (!string.IsNullOrEmpty(timingString) &&
                            timingString.Contains(".") && timingString.IndexOf(".") == timingString.LastIndexOf(".") &&
                            !string.IsNullOrEmpty(timingString.Substring(0, timingString.IndexOf("."))) && !string.IsNullOrEmpty(timingString.Substring(timingString.IndexOf(".") + 1)) && File.ReadAllLines(tempTextFile).Last().Contains(timingString))
                        {
                            if (timingString.Contains("."))
                            {
                                _seriesTiming = Convert.ToInt32(timingString.Substring(0, timingString.IndexOf(".")))*60 +
                                                Convert.ToInt32(timingString.Substring(timingString.IndexOf(".") + 1));
                            }
                            else
                            {
                                _seriesTiming = Convert.ToInt32(timingString);
                            }
                            if (_seriesContainsVideoMarkChar && _timing == 0 && _currentFileEntries[_currentMedia] != _fileEntries[0])
                            {
                                _timing = _seriesTiming;
                            }
                        }
                    }
                    else
                    {
                        _mediaInfoForm = new MediaInfo(mediaFolder, _correctLocation, _fileEntries);
                    }
                    nIconMediaPlayer.Text = "Mediaplayer: "+new DirectoryInfo(_location).Name;
                }
                //MessageBox.Show(axWindowsMediaPlayer1.URL.Replace("\\", "/") + _seriesContainsVideoMarkChar.ToString());
            }
            _changingLocation = false;
            if (_mediaInfoForm != null)
            {
                _mediaInfoForm.SaveParentForm(this);
            }
        }

        private void SortSongs()
        {
            _currentMedia = 0;
            if (cBoxSorting.Text=="Shuffle" && _currentFileEntries.Count > 0)
            {
                List<string> randomizerList = new List<string>();
                Random randomizer = new Random();
                while (_currentFileEntries.Count > 0)
                {
                    int randomInt = randomizer.Next(0, _currentFileEntries.Count);
                    randomizerList.Add(_currentFileEntries[randomInt]);
                    _currentFileEntries.RemoveAt(randomInt);
                }
                foreach (string randomFileName in randomizerList)
                {
                    _currentFileEntries.Add(randomFileName);
                }
                if (_currentFileEntries.Count == 1)
                {
                    WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
                            IWMPMedia mediainfo = wmp.newMedia(_currentFileEntries[_currentMedia]);
                            double duration = mediainfo.duration;
                            _timing = new Random().Next(0, Convert.ToInt32(duration));
                }
            }
            if (cBoxSorting.Text=="Default" && _currentFileEntries.Count > 0)
            {
                _currentFileEntries.Sort();
                for (int i = 0; i < _currentFileEntries.Count; i++)
                {
                    if (_currentFileEntries[i].Replace(@"/", @"\") ==axWindowsMediaPlayer1.URL.Replace(@"/", @"\"))
                    {
                        _currentMedia = i;
                    }
                }
            }
            if (cBoxSorting.Text == "Newest" && _currentFileEntries.Count > 0)
            {
                if (cBoxSorting.Text != oldSorting)
                {
                    _currentFileEntries.Sort(new NewestComparer());
                    _currentMedia = 0;
                }
                for (int i = 0; i < _currentFileEntries.Count; i++)
                {
                    if (_currentFileEntries[i].Replace(@"/", @"\") == axWindowsMediaPlayer1.URL.Replace(@"/", @"\"))
                    {
                        _currentMedia = i;
                    }
                }
            }
            lboxMedia.Items.Clear();
            _seriesContainsVideoMarkChar = false;
            for (int i = 0; i < _currentFileEntries.Count; i++)
            {
                lboxMedia.Items.Add(_currentFileEntries[i].Replace(_correctLocation, ""));
                if (_currentFileEntries[i].Contains(CurrentMarkChar))
                {
                    string fileWithoutExtensions = RemoveExtensionsFromString(_currentFileEntries[i]);
                    if (FileContainsMarkCharacter(_currentFileEntries[i], CurrentMarkChar, true))
                    {
                        //MessageBox.Show(axWindowsMediaPlayer1.URL);
                        _seriesContainsVideoMarkChar = true;
                        _currentMedia = i;
                        if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                        {
                            _timing = _seriesTiming;
                        }
                        else
                        {
                            _timing = 0;
                        }
                        if (
                            _currentFileEntries[i].LastIndexOf(CurrentMarkChar,
                                _currentFileEntries[i].LastIndexOf(CurrentMarkChar) - 1) != -1)
                        {
                            string timingString =
                                _currentFileEntries[i].Substring(
                                    _currentFileEntries[i].LastIndexOf(CurrentMarkChar,
                                        _currentFileEntries[i].LastIndexOf(CurrentMarkChar) - 1) + 1,
                                    _currentFileEntries[i].LastIndexOf(CurrentMarkChar) -
                                    (_currentFileEntries[i].LastIndexOf(CurrentMarkChar,
                                        _currentFileEntries[i].LastIndexOf(CurrentMarkChar) - 1) + 1));
                            int count = 0;
                            int j = 0;
                            while ((j = timingString.IndexOf(".", j)) != -1)
                            {
                                j += ".".Length + 1;
                                count++;
                            }
                            if (count == 0)
                            {
                                int y;
                                if (int.TryParse(timingString, out y))
                                {
                                    _timing = Convert.ToInt32(timingString);
                                }
                            }
                            if (count == 1)
                            {
                                string[] timingStrings = timingString.Split('.');
                                int y;
                                if (int.TryParse(timingStrings[0], out y) && int.TryParse(timingStrings[1], out y))
                                {
                                    _timing = Convert.ToInt32(timingStrings[0])*60 + Convert.ToInt32(timingStrings[1]);
                                }
                            }
                            if (count == 2)
                            {
                                string[] timingStrings = timingString.Split('.');
                                int y;
                                if (int.TryParse(timingStrings[0], out y) && int.TryParse(timingStrings[1], out y) &&
                                    int.TryParse(timingStrings[2], out y))
                                {
                                    _timing = ((Convert.ToInt32(timingStrings[0])*60) +
                                               Convert.ToInt32(timingStrings[1]))*60 + Convert.ToInt32(timingStrings[2]);
                                }
                            }
                        }
                    }
                }
            }
            if (_currentFileEntries.Count > 0 /*&&axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying*/&&
                _currentFileEntries[_currentMedia] !=axWindowsMediaPlayer1.URL &&
                _currentFileEntries[_currentMedia].Replace(@"/", @"\") !=axWindowsMediaPlayer1.URL &&
                _currentFileEntries[_currentMedia].Replace(@"\", @"/") !=axWindowsMediaPlayer1.URL)
            {
                lboxMedia.SetSelected(_currentMedia, true);
            }
            oldSorting = cBoxSorting.Text;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (lboxMedia.SelectedIndex >= 0 && lboxMedia.SelectedIndex < _currentFileEntries.Count)
            {
                _timing = 0;
                _currentMedia = lboxMedia.SelectedIndex;
                _seriesContainsVideoMarkChar = false;
               axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];
                if (StringContainsCorrectExtension(_currentFileEntries[_currentMedia], _videoExtensions))
                {
                    _clickStarted = true;
                    if (FileContainsMarkCharacter(_currentFileEntries[_currentMedia], CurrentMarkChar, true))
                    {
                        _seriesContainsVideoMarkChar = true;
                    }
                }
            }
        }

        private void NextMedia()
        {
            _currentMedia++;
            if (_currentMedia >= _currentFileEntries.Count)
            {
                _currentMedia = 0;
            }
            if (_currentMedia < _currentFileEntries.Count)
            {
                _timing = 0;
                _seriesContainsVideoMarkChar = false;
               axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];
                if (StringContainsCorrectExtension(_currentFileEntries[_currentMedia], _videoExtensions))
                {
                    _clickStarted = true;
                    if (FileContainsMarkCharacter(_currentFileEntries[_currentMedia], CurrentMarkChar, true))
                    {
                        _seriesContainsVideoMarkChar = true;
                    }
                }
                lboxMedia.SetSelected(_currentMedia, true);
            }
        }

        private string RemoveExtensionsFromString(string stringWithExtension)
        {
            string stringWithoutExtension = stringWithExtension;
            for (int i = 0; i < (_videoExtensions.Length + _audioExtensions.Length); i++)
            {
                if (i < _videoExtensions.Length)
                {
                    stringWithoutExtension = stringWithoutExtension.Replace(_videoExtensions[i], "");
                }
                else
                {
                    stringWithoutExtension =
                        stringWithoutExtension.Replace(_audioExtensions[i - _videoExtensions.Length], "");
                }
            }
            return stringWithoutExtension;
        }

        private bool StringContainsCorrectExtension(string file, string[] Extensions)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file);
                //MessageBox.Show(fileInfo.Extension + " " + file);
                if (Extensions.Contains<string>(fileInfo.Extension.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("File " + file + " not found");
                return false;
            }
        }

        private void UpdateLabels()
        {
            string title = RemoveExtensionsFromString(_currentFileEntries[_currentMedia]).Replace(_correctLocation, "");
                //.Replace(correctLocation, "").Replace(".mp3", "").Replace(".mp4", "");
            string artist = "not available";
            if (title.Contains(" - "))
            {
                int i = title.IndexOf(" - "); //for other results change to LastIndexOf
                string[] splitTitle = new string[] {title.Substring(0, i), title.Substring(i + 1)};
                title = splitTitle[1];
                artist = splitTitle[0];
                title = title.Remove(0, 2);
            }
            label1.Text = "Artist:";
            if (artist == "not available" &&
                StringContainsCorrectExtension(_currentFileEntries[_currentMedia], _videoExtensions))
            {
                artist = _location;
                artist = artist.Remove(artist.Length - 1);
                artist = artist.Remove(0, artist.LastIndexOf("/") + 1);
                label1.Text = "Series:";
            }
            TitleLabel.Text = title;
            ArtistLabel.Text = artist;
        }

        private bool FileContainsMarkCharacter(string file, string markChar, bool checkOnlyLast)
        {
            string fileWithoutExtensions = RemoveExtensionsFromString(file);
            if (checkOnlyLast)
            {
                if ((StringContainsCorrectExtension(file, _videoExtensions) ||
                     StringContainsCorrectExtension(file, _audioExtensions)) &&
                    fileWithoutExtensions.LastIndexOf(markChar) == fileWithoutExtensions.Length - markChar.Length)
                {
                    return true;
                }
                return false;
            }
            if ((StringContainsCorrectExtension(file, _videoExtensions) ||
                 StringContainsCorrectExtension(file, _audioExtensions)) &&
                fileWithoutExtensions.LastIndexOf(markChar) > file.LastIndexOf("/"))
            {
                return true;
            }
            return false;
        }

        private void btnMarkFile_Click(object sender, EventArgs e)
        {
            if (_currentFileEntries.Count > 0)
            {
                //bool fileRenameSucces = false;
                string oldFile = _currentFileEntries[_currentMedia].Replace(@"\", "/");
                if (!string.IsNullOrEmpty(oldFile))
                {
                    if (StringContainsCorrectExtension(oldFile, _videoExtensions) ||
                        StringContainsCorrectExtension(oldFile, _audioExtensions))
                    {
                        if (FileContainsMarkCharacter(oldFile, CurrentMarkChar, true)
                            &&
                            !(axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused ||
                             axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying))
                        {
                            _seriesContainsVideoMarkChar = false;
                        }
                        else if (!FileContainsMarkCharacter(oldFile, CurrentMarkChar, true))
                        {
                            _seriesContainsVideoMarkChar = true;
                        }
                        if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                        {
                            _timing = _seriesTiming;
                        }
                        else
                        {
                            _timing = 0;
                        }
                        string newFile = MarkFile(oldFile, CurrentMarkChar);
                        if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                            newFile != RemoveExtensionsFromString(oldFile))
                        {
                           axWindowsMediaPlayer1.URL = newFile;
                           axWindowsMediaPlayer1.Ctlcontrols.stop();
                            ChangeStringInFileLists(oldFile, newFile);
                            //MessageBox.Show("File succesfully marked/unmarked");
                        }
                        else if (newFile == "")
                        {
                            MessageBox.Show("Mark/Unmark only supported with video files");
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong. renaming of file:\r\n" + oldFile + "\r\nTo:\r\n" +
                                            newFile +
                                            "\r\nWent wrong");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not resolve file extension");
                    }
                }
                else
                {
                    MessageBox.Show("No file selected");
                }
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }
        private void btnTop_Click(object sender, EventArgs e)
        {
            //bool fileRenameSucces = false;
            string oldFile =axWindowsMediaPlayer1.URL.Replace(@"\", "/");
            if (!string.IsNullOrEmpty(oldFile))
            {
                if (StringContainsCorrectExtension(oldFile, _audioExtensions) ||
                    StringContainsCorrectExtension(oldFile, _videoExtensions))
                {
                    if (cboxTag.Text.Length > 0 && cboxTag.Text.Substring(0,1) == "[" && cboxTag.Text.Substring(cboxTag.Text.Length-1, 1) == "]")
                    {
                        _timing = 0;
                        if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                        {
                            _timing = _seriesTiming;
                        }
                        string newFile = MarkFile(oldFile, cboxTag.Text);
                        if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                            newFile != RemoveExtensionsFromString(oldFile))
                        {
                           axWindowsMediaPlayer1.URL = newFile;
                           axWindowsMediaPlayer1.Ctlcontrols.stop();
                            if (_timing != 0)
                            {
                               axWindowsMediaPlayer1.Ctlcontrols.play();
                            }
                            ChangeStringInFileLists(oldFile, newFile);
                            //MessageBox.Show("File succesfully marked/unmarked");
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong. renaming of file:\r\n" + oldFile + "\r\nTo:\r\n" +
                                            newFile +
                                            "\r\nWent wrong");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Make sure a tag is always enclosed in [brackets]");
                    }
                }
                else
                {
                    MessageBox.Show("Could not resolve file extension");
                }
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }

        private string MarkFile(string fileName, string mark)
        {
            string oldFile = fileName;
            string newFile = "";
            if (_currentFileEntries.Contains(fileName.Replace(@"\", @"/")) && !_changingLocation)
                //to prevent marking when changing location
            {
                if (FileContainsMarkCharacter(oldFile, CurrentMarkChar, true) &&
                    StringContainsCorrectExtension(oldFile, _videoExtensions) && mark == CurrentMarkChar)
                {
                    int markCharacterPosition = RemoveExtensionsFromString(oldFile).LastIndexOf(CurrentMarkChar);
                    if (oldFile.LastIndexOf(CurrentMarkChar, oldFile.LastIndexOf(CurrentMarkChar) - 1) != -1)
                    {
                        int x;
                        if (
                            int.TryParse(
                                oldFile.Substring(
                                    oldFile.LastIndexOf(CurrentMarkChar, oldFile.LastIndexOf(CurrentMarkChar) - 1) +
                                    CurrentMarkChar.Length,
                                    oldFile.LastIndexOf(CurrentMarkChar) -
                                    (oldFile.LastIndexOf(CurrentMarkChar, oldFile.LastIndexOf(CurrentMarkChar) - 1) +
                                     CurrentMarkChar.Length))
                                    .Replace(".",
                                        ""), out x))
                        {
                            markCharacterPosition = oldFile.LastIndexOf(CurrentMarkChar,
                                oldFile.LastIndexOf(CurrentMarkChar) - 1);
                            newFile = oldFile.Remove(markCharacterPosition,
                                (oldFile.LastIndexOf(CurrentMarkChar) - markCharacterPosition) + CurrentMarkChar.Length);
                        }
                        else
                        {
                            newFile = oldFile.Remove(markCharacterPosition, CurrentMarkChar.Length);
                        }
                    }
                    else
                    {
                        newFile = oldFile.Remove(markCharacterPosition, CurrentMarkChar.Length);
                    }
                    if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition > 0 && !string.IsNullOrEmpty(axWindowsMediaPlayer1.Ctlcontrols.currentPositionString))
                    {
                        newFile = newFile.Substring(0, RemoveExtensionsFromString(newFile).Length) + CurrentMarkChar +
                                 axWindowsMediaPlayer1.Ctlcontrols.currentPositionString.Replace(":", ".") +
                                  CurrentMarkChar + newFile.Substring(markCharacterPosition);
                        _timing = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                    }
                }
                else if (FileContainsMarkCharacter(oldFile, cboxTag.Text, false) &&
                         (StringContainsCorrectExtension(oldFile, _audioExtensions) || StringContainsCorrectExtension(oldFile, _videoExtensions)) && mark == cboxTag.Text)
                {
                    int markCharacterPosition = RemoveExtensionsFromString(oldFile).LastIndexOf(cboxTag.Text);
                    newFile = oldFile.Remove(markCharacterPosition, cboxTag.Text.Length);
                    if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying ||
                       axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                    {
                        _timing = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                    }
                }
                else
                {
                    if (StringContainsCorrectExtension(oldFile, _videoExtensions) && mark == CurrentMarkChar)
                    {
                        int markCharacterPosition = RemoveExtensionsFromString(oldFile).Length;
                        if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition > 0 && !string.IsNullOrEmpty(axWindowsMediaPlayer1.Ctlcontrols.currentPositionString))
                        {
                            newFile = oldFile.Substring(0, RemoveExtensionsFromString(oldFile).Length) +
                                      CurrentMarkChar +
                                     axWindowsMediaPlayer1.Ctlcontrols.currentPositionString.Replace(":", ".") +
                                      CurrentMarkChar + oldFile.Substring(markCharacterPosition);
                            _timing = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                        }
                        else
                        {
                            newFile = oldFile.Substring(0, RemoveExtensionsFromString(oldFile).Length) +
                                      CurrentMarkChar +
                                      oldFile.Substring(markCharacterPosition);
                        }
                    }
                    else if ((StringContainsCorrectExtension(oldFile, _audioExtensions) || StringContainsCorrectExtension(oldFile, _videoExtensions)) && mark == cboxTag.Text)
                    {
                        int markCharacterPosition = RemoveMarkFromString(RemoveExtensionsFromString(oldFile)).Length;
                        newFile = oldFile.Substring(0, markCharacterPosition) + cboxTag.Text +
                                  oldFile.Substring(markCharacterPosition);
                        //MessageBox.Show(newFile);
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying ||
                           axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            _timing = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                        }
                    }
                }
                //MessageBox.Show(newFile);
                if (newFile != "")
                {
                    try
                    {
                        File.Move(oldFile, newFile);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Renaming of file " + oldFile + " to " + newFile + " failed.\r\nReason: " +
                                        exc.Message);
                    }
                }
                else
                {
                    //MessageBox.Show("Mark/Unmark only supported with video files");
                }
            }
            return newFile;
        }

        private void ChangeStringInFileLists(string oldString, string newString)
        {
            for (int i = 0; i < _fileEntries.Length; i++)
            {
                if (_fileEntries[i] == oldString || _fileEntries[i].Replace(@"\", "/") == oldString ||
                    _fileEntries[i].Replace("/", @"\") == oldString)
                {
                    _fileEntries[i] = newString;
                }
            }
            for (int i = 0; i < _currentFileEntries.Count; i++)
            {
                if (_currentFileEntries[i] == oldString || _currentFileEntries[i].Replace(@"\", "/") == oldString ||
                    _currentFileEntries[i].Replace("/", @"\") == oldString)
                {
                    _currentFileEntries[i] = newString;
                }
            }
            lboxMedia.Items.Clear();
            foreach (string currentFileEntry in _currentFileEntries)
            {
                lboxMedia.Items.Add(currentFileEntry.Replace(_correctLocation, ""));
            }
            lboxMedia.SetSelected(_currentMedia, true);
            UpdateLabels();
            FillCboxSearch();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (_mediaInfoForm == null)
            {
                _mediaInfoForm = new MediaInfo();
                _mediaInfoForm.SaveParentForm(this);
            }
            //_mediaInfoForm.StartPosition = new Point(this.Left, this.Top);
            _mediaInfoForm.StartPosition = FormStartPosition.Manual;
            _mediaInfoForm.Location = new Point(this.Left, this.Top);
            _mediaInfoForm.Show();
        }

        private void Mediaplayer_Activated(object sender, EventArgs e)
        {
            if (_mediaInfoForm != null && _mediaInfoForm.Visible)
            {
                _mediaInfoForm.WindowState = FormWindowState.Normal;
                _mediaInfoForm.Focus();
            }
            if (cboxSearch.SelectionLength > 0)
            {
                this.BeginInvoke(new Action(() => { cboxSearch.Select(cboxSearch.Text.Length, 0); }));
            }
        }

        private void cboxSearch_TextChanged(object sender, EventArgs e)
        {
            if (_fileEntries != null)
            {
                lboxMedia.Items.Clear();
                _currentMedia = 0;
                _currentFileEntries.Clear();
                foreach (string fileEntry in _fileEntries)
                {
                    if (
                        fileEntry.Replace(_correctLocation, "")
                            .IndexOf(cboxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _currentFileEntries.Add(fileEntry);
                    }
                }
                oldSorting = "Default";
                SortSongs();
                if (_currentFileEntries.Count > 0)
                {
                    bool playing =axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying;
                    if (_currentFileEntries[_currentMedia] ==axWindowsMediaPlayer1.URL ||
                        _currentFileEntries[_currentMedia].Replace(@"/", @"\") ==axWindowsMediaPlayer1.URL ||
                        _currentFileEntries[_currentMedia].Replace(@"\", @"/") ==axWindowsMediaPlayer1.URL)
                    {
                        //MessageBox.Show("works");
                    }
                    else
                    {
                        //MessageBox.Show(currentFileEntries[currentSong] + "!=" +axWindowsMediaPlayer1.URL);
                       axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];
                    }
                    if (!playing)
                    {
                       axWindowsMediaPlayer1.Ctlcontrols.stop();
                    }
                    lboxMedia.SetSelected(_currentMedia, true);

                    //listBox1.SetSelected(currentSong, true);
                }
                else
                {
                    TitleLabel.Text = "No media files found.";
                }
            }
        }

        private void FillCboxSearch()
        {
            bool cboxWasAlreadyEmpty = cboxSearch.Items.Count == 0;
            cboxSearch.Items.Clear();
            cboxTag.Items.Clear();
            cboxTag.Items.Add(TopMarkChar);
            List<string> searchStringList = new List<string>();
            foreach (string file in _fileEntries)
            {
                for (int i = 0; i < file.Length; i++)
                {
                    if (file.IndexOf("[", i) >= i && file.IndexOf("]", i + 2) >= i + 2)
                    {
                        string searchString = file.Substring(file.IndexOf("[", i),
                            file.IndexOf("]", i + 2) - file.IndexOf("[", i) + 1);
                        searchStringList.Add(searchString);
                        i = file.IndexOf("]", i + 2);
                    }
                }
                if (file.Replace(_correctLocation, "").Contains(@"/") || file.Replace(_correctLocation, "").Contains(@"\"))
                {
                    string subDirectory = new FileInfo(file).DirectoryName.Replace(@"\", @"/").Replace(_correctLocation.Replace(@"\", @"/"), "");
                    if (!cboxSearch.Items.Contains(subDirectory))
                    {
                        cboxSearch.Items.Add(subDirectory);
                    }
                }
            }
            foreach (string searchString in SortAndFilterListByQuantity(searchStringList))
            {
                if (!cboxTag.Items.Contains(searchString))
                {
                    cboxTag.Items.Add(searchString);
                }
                cboxSearch.Items.Add(searchString);
            }
            if (cboxSearch.Items.Count == 0 && !cboxWasAlreadyEmpty)
            {
                cboxSearch.IntegralHeight = false; //resets dropdownheight
                cboxSearch.IntegralHeight = true;
            }
        }

        private void cboxSearch_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { cboxSearch.Select(cboxSearch.Text.Length, 0); }));
        }

        private List<string> SortAndFilterListByQuantity(List<string> list)
        {
            List<int> amountList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                amountList.Add(1);
                for (int ii = i + 1; ii < list.Count; ii++)
                {
                    if (list[i] == list[ii])
                    {
                        amountList[amountList.Count - 1]++;
                        list.RemoveAt(ii);
                        ii--;
                    }
                }
                //MessageBox.Show(list[i].ToString() + ": " + amountList[amountList.Count - 1]);
            }
            string[] sortedArray = list.ToArray();
            int[] amountArray = amountList.ToArray();
            if (list.Count == amountList.Count)
            {
                Array.Sort(amountArray, sortedArray);
                Array.Reverse(amountArray);
                Array.Reverse(sortedArray);
                //MessageBox.Show(sortedArray[0] + ": " + amountArray[0]);
            }
            return sortedArray.ToList();
        }

        private void Mediaplayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerPreventClosing.Enabled)
            {
                //MessageBox.Show("works");
                e.Cancel = true; // this cancels the close event.
            }
            else if (_preventClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                nIconMediaPlayer.Visible = true;
            }
            else
            {
                if (_seriesContainsVideoMarkChar &&
                    (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused ||
                    axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying))
                {
                    //bool fileRenameSucces = false;
                    string oldFile =axWindowsMediaPlayer1.URL.Replace(@"\", "/");
                    if (!string.IsNullOrEmpty(oldFile))
                    {
                        if (StringContainsCorrectExtension(oldFile, _videoExtensions))
                        {
                            if (FileContainsMarkCharacter(oldFile, CurrentMarkChar, true)
                                &&
                                !(axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused ||
                                 axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying))
                            {
                                _seriesContainsVideoMarkChar = false;
                            }
                            else if (!FileContainsMarkCharacter(oldFile, CurrentMarkChar, true))
                            {
                                _seriesContainsVideoMarkChar = true;
                            }
                            if (_seriesContainsVideoMarkChar)
                            {
                                _timing = _seriesTiming;
                            }
                            else
                            {
                                _timing = 0;
                            }
                            string newFile = MarkFile(oldFile, CurrentMarkChar);
                            if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                                newFile != RemoveExtensionsFromString(oldFile))
                            {
                               axWindowsMediaPlayer1.URL = newFile;
                               axWindowsMediaPlayer1.Ctlcontrols.stop();
                                ChangeStringInFileLists(oldFile, newFile);
                                //MessageBox.Show("File succesfully marked/unmarked");
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong. renaming of file:\r\n" + oldFile + "\r\nTo:\r\n" +
                                                newFile +
                                                "\r\nWent wrong");
                            }
                        }
                    }
                }
            }
        }

        public void ReloadMedia()
        {
            _fileEntries = Directory.GetFiles(_location);
            for (int i = 0; i < _fileEntries.Length; i++)
            {
                if (!StringContainsCorrectExtension(_fileEntries[i], _audioExtensions) &&
                    !StringContainsCorrectExtension(_fileEntries[i], _videoExtensions))
                {
                    _fileEntries = _fileEntries.Where(w => w != _fileEntries[i]).ToArray();
                    i--;
                }
            }
            FillCboxSearch();
            lboxMedia.Items.Clear();
            _currentMedia = 0;
            _currentFileEntries.Clear();
            foreach (string fileEntry in _fileEntries)
            {
                if (fileEntry.IndexOf(cboxSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    _currentFileEntries.Add(fileEntry);
                }
            }
            SortSongs();
            if (_currentFileEntries.Count > 0)
            {
               axWindowsMediaPlayer1.settings.autoStart = false;
               axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];

               axWindowsMediaPlayer1.settings.autoStart = true;
                lboxMedia.SetSelected(_currentMedia, true);
                UpdateLabels();
            }
            else
            {
                TitleLabel.Text = "No media files found.";
            }
            string mediaFolder = _location;
        }


        private string RemoveMarkFromString(string file)
        {
            if (file.LastIndexOf(CurrentMarkChar) == file.Length-CurrentMarkChar.Length)
            {
                file = file.Remove(file.LastIndexOf(CurrentMarkChar));
            }
            return file;
        }

        private void cboxTag_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { cboxTag.Select(cboxTag.Text.Length, 0); }));
        }

        private void timerPreventClosing_Tick(object sender, EventArgs e)
        {
            timerPreventClosing.Enabled = false;
        }

        public void PreventClosing()
        {
            timerPreventClosing.Enabled = true;
        }

        private void nIconMediaPlayer_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Mediaplayer_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                nIconMediaPlayer.Visible = true;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                nIconMediaPlayer.Visible = false;
                axWindowsMediaPlayer1.fullScreen = true;
            }
            else
            {
                nIconMediaPlayer.Visible = false;
            }
        }

        private void tsmPlay_Click(object sender, EventArgs e)
        {
           axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void tsmPause_Click(object sender, EventArgs e)
        {
           axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void tsmStop_Click(object sender, EventArgs e)
        {
           axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void tsmMark_Click(object sender, EventArgs e)
        {
            if (_currentFileEntries.Count > 0)
            {
                //bool fileRenameSucces = false;
                string oldFile = _currentFileEntries[_currentMedia].Replace(@"\", "/");
                if (!string.IsNullOrEmpty(oldFile))
                {
                    if (StringContainsCorrectExtension(oldFile, _videoExtensions) ||
                        StringContainsCorrectExtension(oldFile, _audioExtensions))
                    {
                        if (FileContainsMarkCharacter(oldFile, CurrentMarkChar, true)
                            &&
                            !(axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused ||
                             axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying))
                        {
                            _seriesContainsVideoMarkChar = false;
                        }
                        else if (!FileContainsMarkCharacter(oldFile, CurrentMarkChar, true))
                        {
                            _seriesContainsVideoMarkChar = true;
                        }
                        if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                        {
                            _timing = _seriesTiming;
                        }
                        else
                        {
                            _timing = 0;
                        }
                        string newFile = MarkFile(oldFile, CurrentMarkChar);
                        if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                            newFile != RemoveExtensionsFromString(oldFile))
                        {
                           axWindowsMediaPlayer1.URL = newFile;
                           axWindowsMediaPlayer1.Ctlcontrols.stop();
                            ChangeStringInFileLists(oldFile, newFile);
                            //MessageBox.Show("File succesfully marked/unmarked");
                        }
                        else if (newFile == "")
                        {
                            MessageBox.Show("Mark/Unmark only supported with video files");
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong. renaming of file:\r\n" + oldFile + "\r\nTo:\r\n" +
                                            newFile +
                                            "\r\nWent wrong");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not resolve file extension");
                    }
                }
                else
                {
                    MessageBox.Show("No file selected");
                }
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }

        private void tsmTag_Click(object sender, EventArgs e)
        {
            //bool fileRenameSucces = false;
            string oldFile =axWindowsMediaPlayer1.URL.Replace(@"\", "/");
            if (!string.IsNullOrEmpty(oldFile))
            {
                if (StringContainsCorrectExtension(oldFile, _audioExtensions) ||
                    StringContainsCorrectExtension(oldFile, _videoExtensions))
                {
                    _timing = 0;
                    if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                    {
                        _timing = _seriesTiming;
                    }
                    string newFile = MarkFile(oldFile, "[top]");
                    if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                        newFile != RemoveExtensionsFromString(oldFile))
                    {
                       axWindowsMediaPlayer1.URL = newFile;
                       axWindowsMediaPlayer1.Ctlcontrols.stop();
                        if (_timing != 0)
                        {
                           axWindowsMediaPlayer1.Ctlcontrols.play();
                        }
                        ChangeStringInFileLists(oldFile, newFile);
                        //MessageBox.Show("File succesfully marked/unmarked");
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong. renaming of file:\r\n" + oldFile + "\r\nTo:\r\n" +
                                        newFile +
                                        "\r\nWent wrong");
                    }
                }
                else
                {
                    MessageBox.Show("Could not resolve file extension");
                }
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }

        private void tsmClose_Click(object sender, EventArgs e)
        {
            if (_download != null && _preventClosing)
            {
                _waitForClosing = true;
                _download.CancelAll();
            }
            else
            {
                this.Close();
            }
        }

        private void tsmNext_Click(object sender, EventArgs e)
        {
            NextMedia();
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            /*0 = Undefined
            1 = Stopped (by User)
            2 = Paused
            3 = Playing
            4 = Scan Forward
            5 = Scan Backwards
            6 = Buffering
            7 = Waiting
            8 = Media Ended
            9 = Transitioning
            10 = Ready
            11 = Reconnecting
            12 = Last*/
            MaximizeBox = axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying ||
                          axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused;
            if (e.newState == 8) //media ended
            {
                if (axWindowsMediaPlayer1.settings.getMode("loop"))
                {
                    // don't change the song
                }
                else
                {
                    if (FileContainsMarkCharacter(_currentFileEntries[_currentMedia], CurrentMarkChar, true) &&
                        StringContainsCorrectExtension(_currentFileEntries[_currentMedia], _videoExtensions))
                    {
                        //MessageBox.Show(axWindowsMediaPlayer1.URL);
                        string oldFile = _currentFileEntries[_currentMedia].Replace(@"\", "/");
                        if (!string.IsNullOrEmpty(oldFile))
                        {
                            if (_seriesContainsVideoMarkChar)
                            {
                                _timing = _seriesTiming;
                            }
                            else
                            {
                                _timing = 0;
                            }
                            string newFile = MarkFile(oldFile, CurrentMarkChar);
                            if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                                newFile != RemoveExtensionsFromString(oldFile))
                            {
                                ChangeStringInFileLists(oldFile, newFile);
                            }
                        }
                    }
                    _currentMedia++;
                }
                if (_currentMedia >= _currentFileEntries.Count)
                {
                    _currentMedia = 0;
                    if (cBoxSorting.Text=="Shuffle")
                    {
                        SortSongs();
                    }
                }
                if (_currentFileEntries.Count > 0)
                {
                    if (!axWindowsMediaPlayer1.settings.getMode("loop"))
                    {
                        if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                        {
                            _timing = _seriesTiming;
                        }
                        else
                        {
                            _timing = 0;
                        }
                        axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];
                        _doplay = true;
                    }
                    lboxMedia.SetSelected(_currentMedia, true);
                }
                else
                {
                    TitleLabel.Text = "No music files found.";
                    ArtistLabel.Text = "";
                }
            }
            if (e.newState == 10 && _currentFileEntries.Count > 0) //media is ready
            {
                string oldFile = axWindowsMediaPlayer1.URL.Replace(@"\", "/");
                if (!string.IsNullOrEmpty(oldFile) && _seriesContainsVideoMarkChar)
                {
                    if (StringContainsCorrectExtension(oldFile, _videoExtensions) &&
                        !FileContainsMarkCharacter(oldFile, CurrentMarkChar, true) && File.Exists(oldFile))
                    {
                        //MessageBox.Show(oldFile+", \r\n"+_currentFileEntries[_currentMedia]);
                        string newFile = MarkFile(oldFile, CurrentMarkChar);
                        if (newFile != oldFile && !string.IsNullOrEmpty(newFile) &&
                            newFile != RemoveExtensionsFromString(oldFile))
                        {
                            axWindowsMediaPlayer1.URL = newFile;
                            axWindowsMediaPlayer1.Ctlcontrols.stop();
                            ChangeStringInFileLists(oldFile, newFile);
                            //MessageBox.Show("File succesfully marked/unmarked");
                        }
                        else if (newFile == "")
                        {
                            //MessageBox.Show("Mark/Unmark only supported with video files");
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong. renaming of file:\r\n" + oldFile + "\r\nTo:\r\n" +
                                            newFile +
                                            "\r\nWent wrong");
                        }
                    }
                    //MessageBox.Show(axWindowsMediaPlayer1.URL);
                }
                if (_doplay)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    _doplay = false;
                }
                UpdateLabels();
            }
            if (e.newState == 3 && _currentFileEntries.Count > 0) //media playing
            {
                UpdateLabels();
                if (StringContainsCorrectExtension(_currentFileEntries[_currentMedia], _videoExtensions) &&
                    !axWindowsMediaPlayer1.settings.getMode("loop"))
                {
                    //subtitleForm.UpdateText("Hello");
                    this.WindowState = FormWindowState.Maximized;
                    axWindowsMediaPlayer1.fullScreen = true;
                }
                if (axWindowsMediaPlayer1.settings.getMode("loop") && _clickStarted)
                {
                    axWindowsMediaPlayer1.fullScreen = true;
                }
                _clickStarted = false;
                if (_timing > 0 && axWindowsMediaPlayer1.Ctlcontrols.currentPosition < _timing)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = _timing;
                    if (_seriesContainsVideoMarkChar && _currentFileEntries[_currentMedia] != _fileEntries[0])
                    {
                        _timing = _seriesTiming;
                    }
                    else
                    {
                        _timing = 0;
                    }
                }
            }
        }

        private void cBoxSorting_DropDownClosed(object sender, EventArgs e)
        {
            SortSongs();
            bool wasPlaying = _currentFileEntries.Count > 0 &&
                             axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying ||
                             axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused; // check ook op paused
            if (_currentFileEntries.Count > 0)
            {
                lboxMedia.SetSelected(_currentMedia, true);
                if (_currentFileEntries[_currentMedia].Replace(@"/", @"\") !=
                   axWindowsMediaPlayer1.URL.Replace(@"/", @"\"))
                {
                    axWindowsMediaPlayer1.URL = _currentFileEntries[_currentMedia];
                }
            }
            if (!wasPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
            else if (_timing > 0)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        public void DisAllowClosing()
        {
            this._preventClosing = true;
        }

        public void AllowClosing()
        {
            this._preventClosing = false;
            if (_waitForClosing)
            {
                this.Close();
            }
        }

        public void SaveDownloader(Download download)
        {
            this._download = download;
        }

        public Download GetDownload()
        {
            return this._download;
        }

        public void SetDownload(Download download)
        {
            this._download = download;
        }
    }
}
