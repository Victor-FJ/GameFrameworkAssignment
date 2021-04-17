using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using ConsoleUtilities.Menus;
using GameApp.Model;
using GameApp.States;
using GameFramework.Model;
using GameFramework.Utilities;

namespace GameApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get level file
            const string folderName = "Files";
            Console.CursorVisible = false;
            var files = Directory.EnumerateFiles(folderName);
            Menu<string> menu = new Menu<string>(files)
            {
                ToStringFunc = Path.GetFileNameWithoutExtension
            };
            string path = menu.Pick();
            Console.Clear();
            Console.SetCursorPosition(0, 14);

            //Get worldWorld
            ConfigManager manager = new ConfigManager(ConfigConverters.GetConverters());
            World gameWorld = manager.ReadLevelFile(path);
            GameDisplayer gameDisplayer = new GameDisplayer(gameWorld, 25, 0);

            //Equip cretures with loot that is given at start
            var startTemp = gameWorld.WorldObjects.ToList();
            foreach (WorldObject worldObject in startTemp)
                if (worldObject is Creature creature)
                {
                    var objects = gameWorld.NearbyObject(creature, 0, Interactivity.Lootable);
                    foreach (WorldObject o in objects)
                        if (o is AttackItem a)
                        {
                            creature.Weapon = a;
                            gameWorld.Remove(a);
                        }
                        else if (o is DefenceItem d)
                        {
                            creature.Armor = d;
                            gameWorld.Remove(d);
                        }
                }

            
            //Update funtion for redrawning world and status and clearing trace messages in console
            void Update()
            {
                int y = Console.CursorTop;

                //Write status
                Console.SetCursorPosition(0, 0);
                foreach (WorldObject gameWorldWorldObject in gameWorld.WorldObjects)
                    Console.WriteLine(gameWorldWorldObject.ToString().PadRight(20));
                for (int i = 0; i < 4; i++)
                    Console.WriteLine(new string(' ', 20));
                gameDisplayer.DrawWorld();
                
                Console.SetCursorPosition(0,0);
                Console.SetCursorPosition(0, y);
            }

            Tracer tracer = Tracer.Instance;
            Update();

            //Game loop
            while (true)
            {
                var temp = gameWorld.WorldObjects.ToList();
                foreach (WorldObject worldObject in temp)
                {
                    if (worldObject is Creature creature && creature.ParentWorld != null)
                        creature.PerformTurn();

                    Update();
                }
                    

                //End round
                tracer.TraceEvent("Round done");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    break;
                Console.SetCursorPosition(0, 14);
                for (int i = 0; i < 15; i++)
                    Console.WriteLine(new string(' ', 100));
                Console.SetCursorPosition(0, 14);
            }

            //Close
            tracer.Close();
            Console.WriteLine("Game over");
            Console.ReadKey();
        }
    }
}
