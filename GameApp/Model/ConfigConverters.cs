using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using GameApp.States;
using GameFramework.Model;
using GameFramework.Utilities;

namespace GameApp.Model
{
    public static class ConfigConverters
    {
        public static List<ConfigConverter> GetConverters()
        {
            return new List<ConfigConverter>
            {
                new ConfigConverter(typeof(Player).Name, GetPlayer),
                new ConfigConverter(typeof(Bot).Name, GetAgent)
            };
        }

        private static WorldObject GetPlayer(XmlNode node)
        {
            string[] info = node.InnerText.Split(',');
            return new Player(info[0], new Coord(int.Parse(info[1]), int.Parse(info[2])),
                int.Parse(info[3]), Tracer.Instance);
        }

        private static WorldObject GetAgent(XmlNode node)
        {
            string[] info = node.InnerText.Split(',');
            return new Bot(info[0], new Coord(int.Parse(info[1]), int.Parse(info[2])),
                int.Parse(info[3]), Tracer.Instance, int.Parse(info[4]), new Waiting(),
                info.Length > 5 ? info[5] : "",
                info.Length > 5 ? int.Parse(info[6]) : 0);
        }
    }
}
