using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    public class TooMuchMatchesException : Exception
    {
        public TooMuchMatchesException() : this("Matchesが多すぎます。検索パターンを検討しなおしてください。") { }
        public TooMuchMatchesException(string message) : base(message) { }
        public TooMuchMatchesException(string message, Exception inner) : base(message, inner) { }
    }
}
