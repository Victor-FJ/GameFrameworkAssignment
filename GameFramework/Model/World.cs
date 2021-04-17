using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Model
{
    public class World
    {
        public int MaxX { get; }
        public int MaxY { get; }

        private readonly List<WorldObject> _worldObjects;
        public IReadOnlyList<WorldObject> WorldObjects => _worldObjects.AsReadOnly();

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY; 
            _worldObjects = new List<WorldObject>();
        }

        public void Add(WorldObject @object)
        {
            @object.ParentWorld = this;
            _worldObjects.Add(@object);
        }

        public void Remove(WorldObject @object)
        {
            _worldObjects.Remove(@object);
        }

        public List<WorldObject> NearbyObject(WorldObject @object, int range, Interactivity interactivity)
        {
            var objects = from worldObject in _worldObjects
                where worldObject.Interactivity == interactivity && (int) Math.Round(@object.Position.Distance(worldObject.Position)) <= range && worldObject != @object
                orderby @object.Position.Distance(worldObject.Position)
                select worldObject;
            return objects.ToList();
        }
    }
}
