using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using GameFramework.Model;

namespace GameFramework.Utilities
{
    public class ConfigConverter
    {
        public string Name { get; set; }

        public Func<XmlNode, WorldObject> Converter { get; set; }

        public ConfigConverter(string name, Func<XmlNode, WorldObject> converter)
        {
            Name = name;
            Converter = converter;
        }
    }
}
