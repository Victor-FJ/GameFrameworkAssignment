using System;
using GameFramework.Model;
using GameFramework.Utilities;

namespace GameApp.Model
{
    public class Player : Creature
    {
        public Player(string name, Coord position, int hitPoints, ITracer tracer) : base(name, position, hitPoints, tracer)
        {
        }

        public override void Act()
        {
            //Act
            Tracer1.TraceEvent($"Press to make {Name} act");
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.A:
                    Hit();
                    break;
                case ConsoleKey.L:
                    Loot();
                    break;
            }

            //Move
            Tracer1.TraceEvent($"Press to move {Name}");
            ConsoleKey key2 = Console.ReadKey(true).Key;
            switch (key2)
            {
                case ConsoleKey.UpArrow:
                    Move(Position.AddY(-1));
                    break;
                case ConsoleKey.DownArrow:
                    Move(Position.AddY(1));
                    break;
                case ConsoleKey.LeftArrow:
                    Move(Position.AddX(-1));
                    break;
                case ConsoleKey.RightArrow:
                    Move(Position.AddX(1));
                    break;
            }
        }
    }
}
