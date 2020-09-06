using System;
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
            return new string(retval.SkipLast(8 - num.Length).ToArray());
        }

        public static string HexToBinary(string hexvalue)
        {
            return Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
        }

        public static bool HexToBool(string hex)
        {
            if (hex.StartsWith("01") && hex.Skip(2).All(x => x == '0'))
            {
                return true;
            }
            return false;
        }

        public static string HexToString(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return Encoding.ASCII.GetString(raw);
        }

        public static void FindNextSection(string hex, ref int index, int levelsDeep)
        {
            var nextHeader = Utils.ReadHeader(hex, index, ref index);
            var nextHeaderPlugin = PluginManager.GetSectionFromId(nextHeader.ID);
            if (nextHeaderPlugin == null)
            {
                ConsoleWriter.Write(levelsDeep, $" ├─ Unknown ({nextHeader.ID})");
            }
            else
            {
                var plugin = nextHeaderPlugin.GetType();
                var obj = (SectionBase)Activator.CreateInstance(plugin);
                obj.Deserialize(Utils.ReadFile(hex, index, nextHeader.Length * 2, ref index), levelsDeep + 1);
            }
        }
    }

    public class RWHeader
    {
        public string ID;
        public int Length;
        public string RWVersion;
    }
}
