using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace TextConverter.Models
{
    public abstract class ConvertItem : BindableBase
    {
        private bool _IsUsed = true;
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { SetProperty(ref _IsUsed, value); }
        }

        private bool _IsMatched;
        public bool IsMatched
        {
            get { return _IsMatched; }
            set { SetProperty(ref _IsMatched, value); }
        }

        private string _Memo = string.Empty;
        public string Memo
        {
            get { return _Memo; }
            set { SetProperty(ref _Memo, value); }
        }
    }
}
