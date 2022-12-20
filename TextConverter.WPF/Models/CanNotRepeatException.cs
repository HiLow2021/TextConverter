using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter.WPF.Models
{
    public class CanNotRepeatException : Exception
    {
        public CanNotRepeatException() : this("無限ループに入ってしまいます。検索パターンか置換パターンを検討しなおしてください。") { }
        public CanNotRepeatException(string message) : base(message) { }
        public CanNotRepeatException(string message, Exception inner) : base(message, inner) { }
    }
}
