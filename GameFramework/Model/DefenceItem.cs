namespace GameFramework.Model
{
    public class DefenceItem : WorldObject
    {
        public int Protection { get; set; }

        public DefenceItem(string name, Coord position, int protection) : base(name, position, Interactivity.Lootable)
        {
            Protection = protection;
        }

        public override string ToString()
        {
            return $"{Name}-{Protection}p";
        }
    }
}
