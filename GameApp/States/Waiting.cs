using System;
using System.Collections.Generic;
using System.Text;
using GameApp.Model;
using GameFramework.Model;

namespace GameApp.States
{
    public class Waiting : State
    {
        public override void Handle(Bot bot)
        {
            if (IsEnemiesNearby(bot, bot.Range))
            {
                bot.State = new Moving();
                bot.State.Handle(bot);
            }
        }
    }
}
