using System;
using System.Collections.Generic;
using System.Text;

namespace RWview
{
    public abstract class SectionBase
    {
        public abstract string Name { get; }
        public abstract string SectionId { get; }

        public abstract void Deserialize(string hex, string consolePrefix1, string consolePrefix2);
    }
}
