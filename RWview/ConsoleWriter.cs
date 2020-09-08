using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWview
{
    public static class ConsoleWriter
    {
        static List<string> LinesToPrint = new List<string>();

        public static void Write(int levelsDeep, string message)
        {
            LinesToPrint.Add(string.Concat(Enumerable.Repeat("####", levelsDeep)) + message);
        }

        public static void WriteStoredLines()
        {
            var tempLines = new List<string>();
            foreach (var line in LinesToPrint)
            {
                var corners = ReplaceChars(line, '#', 4, " └─ ");
                var cleaned = corners.Replace("####", " #  ");
                tempLines.Add(cleaned);
            }

            var bigString = "";
            int longestLineLength = tempLines.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;
            foreach (var line in tempLines)
            {
                bigString += line.PadRight(longestLineLength) + Environment.NewLine; 
            }

            var sb = new StringBuilder(bigString);
            for (int x = 0; x < tempLines.Count; x++)
            {
                sb = new StringBuilder(bigString);
                for (int i = 0; i < bigString.Length; i++)
                {
                    var character = bigString[i];
                    if (character == '└' || character == '│')
                    {
                        var replaceIndex = i - longestLineLength - 2;
                        if (replaceIndex > 0)
                        {
                            if (sb[replaceIndex] == '#')
                            {
                                sb[replaceIndex] = '│';
                            }
                            if (sb[replaceIndex] == '└')
                            {
                                sb[replaceIndex] = '├';
                            }
                        }
                    }
                }
                bigString = sb.ToString();
            }
            bigString = bigString.Replace("#", "");
            
            Console.WriteLine(bigString);
        }

        private static string ReplaceChars(string original, char from, int charAmount, string to)
        {
            var charList = original.ToCharArray().Where(x => x == from).ToList();
            if (charList.Count >= charAmount)
            {
                return new string(charList.SkipLast(4).ToArray()) + to + new string(original.ToCharArray().Where(x => x != from).ToArray());
            }
            return original;
        }
    }
}
