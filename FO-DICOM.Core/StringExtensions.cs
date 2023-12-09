// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;

namespace FellowOakDicom
{

    internal static class StringExtensions
    {
        public static bool IsDigits(this string s)
        {
            foreach (char c in s) if (!char.IsDigit(c)) return false;
            return true;
        }

        public static bool ListContains(this string s, char separator, string value)
        {
            var parts = s.Split(separator);
            foreach (string part in parts)
            {
                if (part.Trim() == value) return true;
            }
            return false;
        }

        /// <summary>
        /// Array of valid wildcards
        /// </summary>
        private static char[] Wildcards = new char[] { '*', '?' };

        /// <summary>
        /// Returns true if the string matches the pattern which may contain * and ? wildcards.
        /// Matching is done without regard to case.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Wildcard(this string s, string pattern)
        {
            return Wildcard(s, pattern, false);
        }

        /// <summary>
        /// Returns true if the string matches the pattern which may contain * and ? wildcards.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="s"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static bool Wildcard(this string s, string pattern, bool caseSensitive)
        {
            if (pattern == "*") return true;

            // if not concerned about case, convert both string and pattern
            // to lower case for comparison
            if (!caseSensitive)
            {
                pattern = pattern.ToLower();
                s = s.ToLower();
            }

            // if pattern doesn't actually contain any wildcards, use simple equality
            if (pattern.IndexOfAny(Wildcards) == -1) return (s == pattern);

            // otherwise do pattern matching
            int i = 0;
            int j = 0;
            while (i < s.Length && j < pattern.Length && pattern[j] != '*')
            {
                if ((pattern[j] != s[i]) && (pattern[j] != '?'))
                {
                    return false;
                }
                i++;
                j++;
            }

            // if we have reached the end of the pattern without finding a * wildcard,
            // the match must fail if the string is longer or shorter than the pattern
            if (j == pattern.Length) return s.Length == pattern.Length;

            int cp = 0;
            int mp = 0;
            while (i < s.Length)
            {
                if (j < pattern.Length && pattern[j] == '*')
                {
                    if ((j++) >= pattern.Length)
                    {
                        return true;
                    }
                    mp = j;
                    cp = i + 1;
                }
                else if (j < pattern.Length && (pattern[j] == s[i] || pattern[j] == '?'))
                {
                    j++;
                    i++;
                }
                else
                {
                    j = mp;
                    i = cp++;
                }
            }

            while (j < pattern.Length && pattern[j] == '*')
            {
                j++;
            }

            return j >= pattern.Length;
        }
    }

    public static class EnumerableExtensions
    {

        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(T item in source)
            {
                action(item);
            }
        }

    }
}
