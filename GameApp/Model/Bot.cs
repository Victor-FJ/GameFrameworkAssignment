using System;
using System.Collections.Generic;
using System.Text;
using GameApp.States;
using GameFramework.Model;
using GameFramework.Utilities;

namespace GameApp.Model
{
    public class Bot : Creature
    {
        public int Range { get; set; }

        private State _state;
        public State State
        {
            get => _state;
            set
            {
                _state = value;
                Tracer1.TraceEvent($"{Name} is {_state.GetType().Name}");
            } 
        }

        public bool HyperAgresive { get; set; }

        public Bot(string name, Coord position, int hitPoints, ITracer tracer, int range, State state) : base(name, position, hitPoints, tracer)
        {
            Range = range;
            State = state;
        }

        public Bot(string name, Coord position, int hitPoints, ITracer tracer, int range, State state, string weaponName, int damage) : this(name, position, hitPoints, tracer, range, state)
        {
            if (!string.IsNullOrWhiteSpace(weaponName))
            {
                Weapon = new AttackItem(weaponName, new Coord(0, 0), damage, 1, false);
            }
        }

        public override void Act()
        {
            State.Handle(this);
        }
    }
}
