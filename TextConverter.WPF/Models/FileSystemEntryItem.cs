using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace TextConverter.WPF.Models
{
    public class FileSystemEntryItem : BindableBase
    {
        private string _Path = string.Empty;
        public string Path
        {
            get { return _Path; }
            set { SetProperty(ref _Path, value); }
        }

        public string Name => System.IO.Path.GetFileName(Path);

        public FileSystemEntryItem() { }
        public FileSystemEntryItem(string path)
        {
            Path = path;
        }
    }
}
