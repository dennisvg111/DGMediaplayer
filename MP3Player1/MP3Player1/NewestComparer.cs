using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DGMediaplayer
{
    class NewestComparer : IComparer<String>
    {
        public int Compare(string a, string b)
        {
            if (File.Exists(a) && File.Exists(b))
            {
                return File.GetLastWriteTime(a).CompareTo(File.GetLastWriteTime(b)) * -1;
            }
            if (File.Exists(a))
            {
                return -1;
            }
            if (File.Exists(b))
            {
                return 1;
            }
            return a.CompareTo(b);
        }
    }
}
