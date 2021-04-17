using GameFramework.Utilities;

namespace GameFramework.Model
{
    public enum Interactivity
    {
        None,
        Attackable,
        Removable,
        Lootable
    }

    public class WorldObject
    {
        public string Name { get; set; }
        public Coord Position { get; set; }
        public Interactivity Interactivity { get; set; }
        public World ParentWorld { get; set; }
        public ITracer Tracer { get; set; }

        public WorldObject(string name, Coord position, Interactivity interactivity)
        {
            Name = name;
            Position = position;
            Interactivity = interactivity;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
