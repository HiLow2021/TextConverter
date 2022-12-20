using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter.WPF.Models
{
    public class DirectoryItem : FileSystemEntryItem
    {
        public ObservableCollection<DirectoryItem> Directories { get; set; } = new ObservableCollection<DirectoryItem>();
        public ObservableCollection<FileItem> Files { get; set; } = new ObservableCollection<FileItem>();
        public IList<FileSystemEntryItem> Items => Directories?.Cast<FileSystemEntryItem>().Concat(Files).ToList();

        public DirectoryItem() : base() { }
        public DirectoryItem(string path) : base(path) { }
    }
}
