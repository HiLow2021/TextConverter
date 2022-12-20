using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter.WPF.Models
{
    public class FileItem : FileSystemEntryItem
    {
        public FileItem() : base() { }
        public FileItem(string path) : base(path) { }
    }
}
