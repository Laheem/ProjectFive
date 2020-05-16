using GTANetworkAPI;
using ProjectFive.ChatManager.MessageGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Utils
{
    class ChatUtils : Script
    {

        const int WHISPER_DISTANCE = 20;
        const string WHISPER_COLOUR = "!{#9D61E3}";

        public static void SendChatMessageToPlayersInRange(Player player, String message, double distance = 50, string colour = "~w~")
        {
            List<Player> allPlayersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(distance, player);

            foreach (var targetPlayer in allPlayersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(targetPlayer, colour + message);
            }
        }

        public static void SendChatMessageToPlayerById(Player sender, int targetPlayerId, string colour = "~w~")
        {
            // TODO - Create an ID system.
        }

        public static void SendPrivateMessageToPlayerByName(Player sender, string targetPlayerName, string message, string colour = "~w~")
        {
            Player targetPlayer = NAPI.Player.GetPlayerFromName(targetPlayerName);
            if(targetPlayer != null)
            {
                NAPI.Chat.SendChatMessageToPlayer(targetPlayer, colour + MessageGenerator.GenerateFromPm(sender.Name, message));
                NAPI.Chat.SendChatMessageToPlayer(sender, colour + MessageGenerator.GenerateToPm(targetPlayerName, message));
                return;
            }

            NAPI.Chat.SendChatMessageToPlayer(sender, "That player doesn't exist or is offline.");
        }

        public static void SendRpMessageToPlayerByName(Player sender, string targetPlayerName, string message, string colour = "~w~")
        {
            Player targetPlayer = NAPI.Player.GetPlayerFromName(targetPlayerName);
            if (targetPlayer != null)
            {
                NAPI.Chat.SendChatMessageToPlayer(targetPlayer, colour + MessageGenerator.GenerateRPFromMessage(sender.Name, message));
                NAPI.Chat.SendChatMessageToPlayer(targetPlayer, colour + MessageGenerator.GenerateRPToMessage(targetPlayerName, message));
                return;
            }

            NAPI.Chat.SendChatMessageToPlayer(sender, "That player doesn't exist or is offline.");
        }

        public static void SendWhisperToPlayerByName(Player sender, string targetPlayerName, string message, string colour = "~w~")
        {
            Player targetPlayer = NAPI.Player.GetPlayerFromName(targetPlayerName);
            if (targetPlayer != null)
            {
                if (targetPlayer.Position.DistanceTo(sender.Position) > WHISPER_DISTANCE )
                {
                    NAPI.Chat.SendChatMessageToPlayer(sender, "You're too far away to whisper to them.");
                    return;
                }

                ChatUtils.SendChatMessageToPlayersInRange(sender, MessageGenerator.GenerateWhisperAreaMessage(sender.Name,targetPlayer.Name), 20, WHISPER_COLOUR);
                NAPI.Chat.SendChatMessageToPlayer(targetPlayer, colour + MessageGenerator.GenerateWhisperFromMessage(sender.Name, message));
                NAPI.Chat.SendChatMessageToPlayer(sender, colour + MessageGenerator.GenerateWhisperToMessage(targetPlayerName, message));
                return;
            }

            NAPI.Chat.SendChatMessageToPlayer(sender, "That player doesn't exist or is offline.");

        }
    }
}
