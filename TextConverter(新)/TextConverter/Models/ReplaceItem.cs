using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter.Models
{
    public class ReplaceItem : RegexItem
    {
        private string _ReplacePattern = string.Empty;
        public string ReplacePattern
        {
            get { return _ReplacePattern; }
            set { SetProperty(ref _ReplacePattern, value); }
        }

        private bool _IsRepeat;
        public bool IsRepeat
        {
            get { return _IsRepeat; }
            set { SetProperty(ref _IsRepeat, value); }
        }
    }
}
