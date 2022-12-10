using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextConverter.Models
{
    public class RegexItem : ConvertItem
    {
        private string _SearchPattern = string.Empty;
        public string SearchPattern
        {
            get { return _SearchPattern; }
            set { SetProperty(ref _SearchPattern, value); }
        }

        public RegexOptions RegexOptions
        {
            get
            {
                var options = RegexOptions.None;

                if (IsIgnoreCase)
                {
                    options |= RegexOptions.IgnoreCase;
                }
                if (IsMultiline)
                {
                    options |= RegexOptions.Multiline;
                }
                if (IsExplicitCapture)
                {
                    options |= RegexOptions.ExplicitCapture;
                }
                if (IsCompiled)
                {
                    options |= RegexOptions.Compiled;
                }
                if (IsSingleline)
                {
                    options |= RegexOptions.Singleline;
                }
                if (IsIgnorePatternWhitespace)
                {
                    options |= RegexOptions.IgnorePatternWhitespace;
                }
                if (IsRightToLeft)
                {
                    options |= RegexOptions.RightToLeft;
                }
                if (IsECMAScript)
                {
                    options |= RegexOptions.ECMAScript;
                }
                if (IsCultureInvariant)
                {
                    options |= RegexOptions.CultureInvariant;
                }

                return options;
            }
        }

        private bool _IsIgnoreCase;
        public bool IsIgnoreCase
        {
            get { return _IsIgnoreCase; }
            set { SetProperty(ref _IsIgnoreCase, value); }
        }

        private bool _IsMultiline;
        public bool IsMultiline
        {
            get { return _IsMultiline; }
            set { SetProperty(ref _IsMultiline, value); }
        }

        private bool _IsExplicitCapture;
        public bool IsExplicitCapture
        {
            get { return _IsExplicitCapture; }
            set { SetProperty(ref _IsExplicitCapture, value); }
        }

        private bool _IsCompiled;
        public bool IsCompiled
        {
            get { return _IsCompiled; }
            set { SetProperty(ref _IsCompiled, value); }
        }

        private bool _IsSingleline;
        public bool IsSingleline
        {
            get { return _IsSingleline; }
            set { SetProperty(ref _IsSingleline, value); }
        }

        private bool _IsIgnorePatternWhitespace;
        public bool IsIgnorePatternWhitespace
        {
            get { return _IsIgnorePatternWhitespace; }
            set { SetProperty(ref _IsIgnorePatternWhitespace, value); }
        }

        private bool _IsRightToLeft;
        public bool IsRightToLeft
        {
            get { return _IsRightToLeft; }
            set { SetProperty(ref _IsRightToLeft, value); }
        }

        private bool _IsECMAScript;
        public bool IsECMAScript
        {
            get { return _IsECMAScript; }
            set { SetProperty(ref _IsECMAScript, value); }
        }

        private bool _IsCultureInvariant;
        public bool IsCultureInvariant
        {
            get { return _IsCultureInvariant; }
            set { SetProperty(ref _IsCultureInvariant, value); }
        }
    }
}
