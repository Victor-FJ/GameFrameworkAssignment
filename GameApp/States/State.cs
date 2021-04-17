using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameApp.Model;
using GameFramework.Model;

namespace GameApp.States
{
    public abstract class State
    {
        public abstract void Handle(Bot bot);

        protected bool IsEnemiesNearby(Bot bot, int range)
        {
            return EnemiesNearby(bot, range).Count != 0;
        }

        protected List<WorldObject> EnemiesNearby(Bot bot, int range)
        {
            var nearbyCretures = bot.ParentWorld.NearbyObject(bot, range, Interactivity.Attackable);
            var nearbyEnemies = (from creture in nearbyCretures where creture.GetType() == typeof(Player) select creture).ToList();

            if (bot.HyperAgresive)
                return nearbyCretures.ToList();
            return nearbyEnemies.ToList();
        }
    }
}
