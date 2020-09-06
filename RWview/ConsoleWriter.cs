using System;
using System.Linq;

namespace RWview
{
    public static class ConsoleWriter
    {
        public static void Write(int levelsDeep, string message, bool newConnection = false)
        {
            if (!newConnection)
            {
                Console.WriteLine(string.Concat(Enumerable.Repeat(" │  ", levelsDeep)) + message);
                return;
            }
            Console.WriteLine(string.Concat(Enumerable.Repeat(" │  ", levelsDeep - 1)) + " ├─ " + message);
        }
    }
}
