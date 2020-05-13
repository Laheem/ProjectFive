using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using ProjectFive.Utils;

namespace ProjectFive.ChatManager.RpCommands
{
    class RoleplayCommands : Script
    {
     public const string MeCommandColour = "!{#cc66ff}";


        // TODO - Change this to character when we get around to it.
       [Command("me",GreedyArg = true)]
       public void MeMessage(Player player, String message)
        {
            String parsedMessage = $"* {player.Name} {message}";
            ChatUtils.SendChatMessageToPlayersInRange(player, parsedMessage, colour: MeCommandColour);
        }


       [Command("do",GreedyArg = true)]
       public void DoMessage(Player player, String message)
       {
            String parsedMessage = $"* {message} (({player.Name}))";
            ChatUtils.SendChatMessageToPlayersInRange(player, parsedMessage, colour: MeCommandColour);
       }



        [Command("attempt", GreedyArg = true)]
        public void startAttempt(Player player, string attemptedAction)
        {
            int outcome = new Random().Next(0, 2);

            string textOutcome = "failed";
            if (outcome == 0)
            {
                textOutcome = "succeeded";
            }

            String parsedMessage = $"[ATTEMPT] {player.Name} has attempted to {attemptedAction} and {textOutcome}.";
            ChatUtils.SendChatMessageToPlayersInRange(player, parsedMessage, colour: MeCommandColour);

        }


    }
}
