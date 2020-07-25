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
            RAGE.Events.Add("blackout", fadeOutOnDeath);
            RAGE.Events.Add("fadeInFromDeath", fadeInOnDeath);

        }

        private void fadeInOnDeath(object[] args)
        {
            RAGE.Chat.Output("AAAAAAAAAAB");
            RAGE.Game.Cam.DoScreenFadeIn(3000);
        }

        private void fadeOutOnDeath(object[] args)
        {
            RAGE.Chat.Output("AAAAAAAAAAA");
            RAGE.Game.Cam.DoScreenFadeOut(2000);
        }

        private void OnPlayerDeath(RAGE.Elements.Player player, uint reason, RAGE.Elements.Player killer, Events.CancelEventArgs cancel)
        {
            RAGE.Game.Misc.SetFadeOutAfterDeath(false);
        }

    }
}
