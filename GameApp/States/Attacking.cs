using System;
using System.Collections.Generic;
using System.Text;
using GameApp.Model;

namespace GameApp.States
{
    public class Attacking : State
    {
        public override void Handle(Bot bot)
        {
            if (!IsEnemiesNearby(bot, bot.Range))
                bot.State = new Waiting();
            else if (IsEnemiesNearby(bot, bot.Weapon.Range))
                bot.Hit();
            else
            {
                bot.State = new Moving();
                bot.State.Handle(bot);
            }
        }
    }
}
