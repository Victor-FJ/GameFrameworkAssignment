using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GameFramework.Model;

namespace GameFramework.Utilities
{
    public class ConfigManager
    {
        public List<ConfigConverter> ConverterFunctions { get; set; }


        public ConfigManager(List<ConfigConverter> converterFunctions)
        {
            ConverterFunctions = converterFunctions ?? new List<ConfigConverter>();
            ConverterFunctions.Add(new ConfigConverter(typeof(AttackItem).Name, GetAttackItem));
            ConverterFunctions.Add(new ConfigConverter(typeof(DefenceItem).Name, GetDefenseItem));
        }

        public World ReadLevelFile(string path)
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(path);

            //Get world
            XmlNode worldNode = configDoc.DocumentElement?.SelectSingleNode("World");
            if (worldNode == null)
                throw new NotSupportedException("World missing");
            int[] worldInfo = worldNode.InnerText.Split(',').Select(int.Parse).ToArray();
            World world = new World(worldInfo[0], worldInfo[1]);


            //Get objects
            XmlNode objectsNode = configDoc.DocumentElement?.SelectSingleNode("Objects");
            if (objectsNode != null)
                foreach (var converter in ConverterFunctions)
                {
                    //Get objects of type
                    XmlNodeList objectNodes = objectsNode.SelectNodes(converter.Name);
                    //If its empty we continue
                    if (objectNodes == null || objectNodes.Count == 0)
                        continue;
                    //Use conversion
                    for (int i = 0; i < objectNodes.Count; i++)
                        world.Add(converter.Converter.Invoke(objectNodes.Item(i)));
                }

            return world;
        }


        private WorldObject GetAttackItem(XmlNode node)
        {
            string[] info = node.InnerText.Split(',');
            return new AttackItem(info[0], new Coord(int.Parse(info[1]), int.Parse(info[2])), 
                int.Parse(info[3]), int.Parse(info[4]), bool.Parse(info[5]));
        }

        private WorldObject GetDefenseItem(XmlNode node)
        {
            string[] info = node.InnerText.Split(',');
            return new DefenceItem(info[0], new Coord(int.Parse(info[1]), int.Parse(info[2])),
                int.Parse(info[3]));
        }
    }
}
