using GameFramework.Utilities;

namespace GameFramework.Model
{
    public abstract class Creature : WorldObject
    {
        public int HitPoints { get; set; }

        public AttackItem Weapon { get; set; }
        public DefenceItem Armor { get; set; }

        public ITracer Tracer1 { get; set; }

        protected Creature(string name, Coord position, int hitPoints, ITracer tracer) : base(name, position, Interactivity.Attackable)
        {
            HitPoints = hitPoints;
            Tracer1 = tracer;
        }


        public abstract void Act();

        public void PerformTurn()
        {
            Tracer1.TraceEvent($"{Name}'s turn");
            Act();
        }

        public void Hit()
        {
            if (Weapon == null)
                return;

            var cretures = ParentWorld.NearbyObject(this, Weapon.Range, Interactivity.Attackable);
            
            foreach (WorldObject worldObject in cretures)
                if (worldObject is Creature creature)
                {
                    Tracer1.TraceEvent($"{this} attacked {creature}");
                    creature.ReceiveHit(Weapon.Damage);
                    if (!Weapon.Splash)
                        break;
                }
        }

        public void Loot()
        {
            var aPileOfLoot = ParentWorld.NearbyObject(this, 1, Interactivity.Lootable);

            if (aPileOfLoot.Count > 0)
            {
                WorldObject loot = aPileOfLoot[0];
                if (loot is AttackItem weapon)
                {
                    if (Weapon != null)
                    {
                        ParentWorld.Add(Weapon);
                        Weapon.Position = Position;
                    }
                    ParentWorld.Remove(weapon);
                    Weapon = weapon;
                    Tracer1.TraceEvent($"{this} looted a weapon");
                }
                else if (loot is DefenceItem armor)
                {
                    if (Armor != null)
                    {
                        ParentWorld.Add(Armor);
                        Armor.Position = Position;
                    }
                    ParentWorld.Remove(armor);
                    Armor = armor;
                    Tracer1.TraceEvent($"{this} looted a piece of armor");
                }
            }
        }

        public void ReceiveHit(int hitpoints)
        {
            int damage = hitpoints / Armor?.Protection ?? hitpoints;
            HitPoints -= damage;
            if (HitPoints <= 0)
            {
                Interactivity = Interactivity.Removable;
                Name += " (Dead)";

                if (Weapon != null || Armor != null)
                {
                   var loot = (WorldObject)Armor ?? Weapon;
                   loot.Position = Position;
                   ParentWorld.Add(loot);
                }
                    

                ParentWorld.Remove(this);
                Tracer1.TraceEvent($"{this} died");
                ParentWorld = null;
            }
        }

        public void Move(Coord pos)
        {
            if (pos.X < 0)
                pos = new Coord(0, pos.Y);
            if (pos.Y < 0)
                pos = new Coord(pos.X, 0);
            if (pos.X >= ParentWorld.MaxX)
                pos = new Coord(ParentWorld.MaxX - 1, pos.Y);
            if (pos.Y >= ParentWorld.MaxY)
                pos = new Coord(pos.X ,ParentWorld.MaxY - 1);
            Position = pos;
        }

        public override string ToString()
        {
            return $"{Name} with {HitPoints} life";
        }
    }
}
