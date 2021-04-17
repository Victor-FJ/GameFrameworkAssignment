namespace GameFramework.Model
{
    public class AttackItem : WorldObject
    {
        public int Damage { get; set; }
        public int Range { get; set; }
        public bool Splash { get; set; }

        public AttackItem(string name, Coord position, int damage, int range, bool splash) : base(name, position, Interactivity.Lootable)
        {
            Damage = damage;
            Range = range;
            Splash = splash;
        }

        public override string ToString()
        {
            return $"{Name}-{Damage}d, {Range}r" + (Splash ? ", s" : "");
        }
    }
}
