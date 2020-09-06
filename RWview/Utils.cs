using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWview
{
    public static class Utils
    {
        public const int HeaderSize = 24;

        public static RWHeader ReadHeader(string file, int start, ref int index)
        {
            var header = new RWHeader
            {
                ID = ReadFile(file, start, 8, ref index),
                Length = HexToInt(ReadFile(file, index, 8, ref index), true),
                RWVersion = ReadFile(file, index, 8, ref index)
            };
            return header;
        }

        public static int HexToInt(string hex, bool little)
        {
            if (little)
            {
                return int.Parse(LittleEndian(hex), System.Globalization.NumberStyles.HexNumber);
            }
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string ReadFile(string file, int start, int length, ref int index)
        {
            var result = new string(file.Skip(start).Take(length).ToArray());
            index += length;
            return result;
        }

        static string LittleEndian(string num)
        {
            int number = Convert.ToInt32(num, 16);
            byte[] bytes = BitConverter.GetBytes(number);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            return retval;
        }
    }

    public class RWHeader
    {
        public string ID;
        public int Length;
        public string RWVersion;
    }
}
