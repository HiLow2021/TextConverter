using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ICSharpCode.AvalonEdit.Document;

namespace TextConverter.WPF.Models
{
    public class ActionInformation : BindableBase
    {
        public ObservableCollection<FileSystemEntryItem> RootDirectoryItems { get; } = new ObservableCollection<FileSystemEntryItem>();
        public ObservableCollection<ReplaceItem> Patterns { get; } = new ObservableCollection<ReplaceItem>();

        private ReplaceItem _SelectedPattern;
        public ReplaceItem SelectedPattern
        {
            get { return _SelectedPattern; }
            set { SetProperty(ref _SelectedPattern, value); }
        }

        private int _SelectedPatternIndex = -1;
        public int SelectedPatternIndex
        {
            get { return _SelectedPatternIndex; }
            set { SetProperty(ref _SelectedPatternIndex, value); }
        }

        private string _InputPath = string.Empty;
        public string InputPath
        {
            get { return _InputPath; }
            set
            {
                SetProperty(ref _InputPath, value);

                if (IsOutputSuffix)
                {
                    OutputPath = Utility.AddSuffixToPath(value, "_out");
                }
            }
        }

        private string _OutputPath = string.Empty;
        public string OutputPath
        {
            get { return _OutputPath; }
            set { SetProperty(ref _OutputPath, value); }
        }

        private bool _IsOutputSuffix = true;
        public bool IsOutputSuffix
        {
            get { return _IsOutputSuffix; }
            set
            {
                SetProperty(ref _IsOutputSuffix, value);
                OutputPath = value ? Utility.AddSuffixToPath(InputPath, "_out") : InputPath;
            }
        }

        private TextDocument _InputDocument = new TextDocument();
        [IgnoreDataMember]
        public TextDocument InputDocument
        {
            get { return _InputDocument; }
            set { SetProperty(ref _InputDocument, value); }
        }

        private TextDocument _OutputDocument = new TextDocument();
        [IgnoreDataMember]
        public TextDocument OutputDocument
        {
            get { return _OutputDocument; }
            set { SetProperty(ref _OutputDocument, value); }
        }
    }
}
