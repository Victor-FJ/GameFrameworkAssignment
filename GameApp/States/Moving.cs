using System;
using System.Collections.Generic;
using System.Text;
using GameApp.Model;
using GameFramework.Model;

namespace GameApp.States
{
    public class Moving : State
    {
        private Random _rnd = new Random();
        public override void Handle(Bot bot)
        {
            if (!IsEnemiesNearby(bot, bot.Range))
                bot.State = new Waiting();
            else if (IsEnemiesNearby(bot, bot.Weapon.Range))
            {
                bot.State = new Attacking();
                bot.State.Handle(bot);
            }
            else
            {
                List<WorldObject> enemiesNearby = EnemiesNearby(bot, bot.Range);
                Coord pos = bot.Position;
                Coord creturePos = enemiesNearby[0].Position;
                Coord distance = creturePos - pos;

                if (_rnd.Next(0, 2) == 0)
                    bot.Move(pos.AddX(Movement(distance.X)));
                else
                    bot.Move(pos.AddY(Movement(distance.Y)));
            }
        }

        private int Movement(int distance)
        {
            return distance < 0 ? -1 : 1;
        }
    }
}
