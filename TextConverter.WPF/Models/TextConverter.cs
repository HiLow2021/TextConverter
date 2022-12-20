using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextConverter.WPF.Models
{
    public class TextConverter
    {
        public string Source { get; set; }
        public string Result { get; private set; }
        public IList<ConvertItem> Items { get; set; } = new List<ConvertItem>();

        public void Convert()
        {
            var source = new StringBuilder(Source);
            var result = new StringBuilder();

            foreach (var item in Items.Where(x => x.IsUsed))
            {
                if (item is RegexItem regexItem)
                {
                    regexItem.IsMatched = Regex.IsMatch(source.ToString(), regexItem.SearchPattern, regexItem.RegexOptions);
                }
                if (item is MatchItem matchItem)
                {
                    Match(ref source, ref result, matchItem);
                }
                else if (item is ReplaceItem replaceItem)
                {
                    Replace(ref source, ref result, replaceItem);
                }
            }

            source.Clear();
            Result = result.ToString();
        }

        private void Match(ref StringBuilder source, ref StringBuilder result, MatchItem item)
        {
            var Results = Regex.Matches(source.ToString(), item.SearchPattern, item.RegexOptions);

            for (int i = 0; i < Results.Count; i++)
            {
                result.Append("Matches[");
                result.Append(i.ToString());
                result.Append("]:");
                result.AppendLine(Results[i].Value);

                for (int j = 0; j < Results[i].Groups.Count; j++)
                {
                    result.Append("   Groups[");
                    result.Append(j.ToString());
                    result.Append("]:");
                    result.AppendLine(Results[i].Groups[j].Value);

                    for (int k = 0; k < Results[i].Groups[j].Captures.Count; k++)
                    {
                        result.Append("      Captures[");
                        result.Append(k.ToString());
                        result.Append("]:");
                        result.AppendLine(Results[i].Groups[j].Captures[k].Value);
                    }
                }

                result.AppendLine();
            }
        }

        private void Replace(ref StringBuilder source, ref StringBuilder result, ReplaceItem item)
        {
            do
            {
                if (item.IsRepeat && !IsRepeat(item.SearchPattern, item.ReplacePattern, item.RegexOptions))
                {
                    throw new CanNotRepeatException();
                }

                result.Clear();
                result.Append(Regex.Replace(source.ToString(), item.SearchPattern, item.ReplacePattern, item.RegexOptions));
                source.Clear();
                source.Append(result.ToString());
            }
            while (item.IsRepeat && Regex.IsMatch(source.ToString(), item.SearchPattern, item.RegexOptions));

            static bool IsRepeat(string SearchPattern, string ReplacePattern, RegexOptions PatternOptions)
            {
                return !Regex.IsMatch(ReplacePattern, SearchPattern, PatternOptions);
            }
        }
    }
}
