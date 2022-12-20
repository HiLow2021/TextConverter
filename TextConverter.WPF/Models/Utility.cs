using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using My.IO;

namespace TextConverter.WPF.Models
{
    public static class Utility
    {
        public static string AddSuffixToPath(string path, string suffix)
        {
            var regex = new Regex($@"({suffix})(\d*)(\..*?)?$");
            var match = regex.Match(path);

            if (match.Success)
            {
                var isSucceeded = int.TryParse(match.Groups[2].Value, out int number);

                number = isSucceeded ? number + 1 : 2;

                return regex.Replace(path, $@"{suffix}{number}$3");
            }
            else
            {
                return Regex.Replace(path, @"(.*?)(\..*?)$", $@"$1{suffix}$2");
            }
        }

        public static string RemoveSuffixFromPath(string path, string suffix)
        {
            var regex = new Regex($@"({suffix})(\d*)(\..*?)?$");
            var match = regex.Match(path);

            if (match.Success)
            {
                var isSucceeded = int.TryParse(match.Groups[2].Value, out int number);
                var numberString = isSucceeded && number > 2 ? (number - 1).ToString() : string.Empty;

                return regex.Replace(path, $@"{suffix}{numberString}$3");
            }
            else
            {
                return path;
            }
        }
    }
}
