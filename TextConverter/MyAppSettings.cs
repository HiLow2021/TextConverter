using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    [Serializable]
    public class MyAppSettings
    {
        public int Left { get; set; } = 0;
        public int Top { get; set; } = 0;
        public bool IsTopMost { get; set; } = false;
        public bool IsFixedWindowsPos { get; set; } = true;
        public int MethodMode { get; set; } = 0;
        public int ReadEncodingIndex { get; set; } = 0;
        public int WriteEncodingIndex { get; set; } = 0;
        public MyEncoding ReadEncoding { get; set; } = MyEncoding.Shift_JIS;
        public MyEncoding WriteEncoding { get; set; } = MyEncoding.Shift_JIS;
        public bool IsAddOut { get; set; } = false;
        public bool IsDifferentPattern { get; set; } = false;
        public bool IsRepeatMode { get; set; } = false;
        public string LoadFilePath { get; set; } = string.Empty;
        public string SaveFilePath { get; set; } = string.Empty;
    }
}
