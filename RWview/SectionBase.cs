namespace RWview
{
    public abstract class SectionBase
    {
        public abstract string Name { get; }
        public abstract string SectionId { get; }

        public abstract void Deserialize(string hex, int levelsDeep);
    }
}
