using RAGE;
using RAGE.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFiveClient.Client.DeathManager
{
    class DeathClient : RAGE.Events.Script
    {

        public DeathClient()
        {
            Events.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath(RAGE.Elements.Player player, uint reason, RAGE.Elements.Player killer, Events.CancelEventArgs cancel)
        {
            RAGE.Game.Misc.SetFadeOutAfterDeath(false);
        }

    }
}
