using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Channels;
using GameFramework.Model;

namespace GameApp
{
    public class GameDisplayer
    {
        public World World { get; }

        public Coord Position { get; }


        public GameDisplayer(World world, int left, int top)
        {
            World = world;
            Position = new Coord(left, top);
        }


        public void DrawWorld()
        {
            //Draw world
            string horizontalLine = "  " + new string('-', World.MaxX * 2) + "  ";
            string middleLine = " |" + new string(' ', World.MaxX * 2) + "| ";

            void Print(Coord coord, string text)
            {
                Console.SetCursorPosition(coord.X * 2, coord.Y);
                Console.Write(text);
            }

            Print(Position, horizontalLine);
            for (int i = 0; i < World.MaxY; i++)
                Print(Position.AddY(i + 1), middleLine);
            Print(Position.AddY(World.MaxY + 1), horizontalLine);

            //Draw objects
            foreach (WorldObject worldObject in World.WorldObjects)
            {
                Coord pos = worldObject.Position + Position;

                char typeChar = worldObject.GetType().Name[0];
                char nameChar = worldObject.Name[0];

                Print(new Coord(pos.X, pos.Y) + new Coord(1, 1), $"{typeChar}{nameChar}");
            }
        }
    }
}
