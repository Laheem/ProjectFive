using GTANetworkAPI;
using ProjectFive.ChatManager.MessageGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.Utils
{
    class ChatUtils : Script
    {
        const int WHISPER_DISTANCE = 2;
        const int GENERIC_CHAT_DISTANCE = 6;
        const int GENERIC_ROLEPLAY_COMMAND_DISTANCE = 8;
        const int GENERIC_SHOUT_DISTANCE = 16;
        const string WHISPER_COLOUR = "!{#9D61E3}";

        private static void SendChatMessageToPlayersInRange(Player player, String message, double distance = GENERIC_CHAT_DISTANCE, string colour = "~w~")
        {
            List<Player> allPlayersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(distance, player);

            foreach (var targetPlayer in allPlayersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(targetPlayer, colour + message);
            }
        }

        public static void SendMeMessage(Player player, String message, string colour)
        {
            SendChatMessageToPlayersInRange(player, MessageGenerator.GenerateMeMessage(player.Name, message), distance: GENERIC_ROLEPLAY_COMMAND_DISTANCE, colour: colour);
        }

        public static void SendDoMessage(Player player, String message, string colour)
        {
            SendChatMessageToPlayersInRange(player, MessageGenerator.GenerateDoMessage(player.Name, message), distance: GENERIC_ROLEPLAY_COMMAND_DISTANCE, colour: colour);
        }

        public static void SendAttemptMessage(Player player, String action, String colour)
        {
            SendChatMessageToPlayersInRange(player, MessageGenerator.GenerateAttemptMessage(player.Name, action), distance: GENERIC_ROLEPLAY_COMMAND_DISTANCE, colour: colour);
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
                NAPI.Chat.SendChatMessageToPlayer(sender, colour + MessageGenerator.GenerateRPToMessage(targetPlayerName, message));
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

        public static void SendShout(Player sender, string message)
        {
            ChatUtils.SendChatMessageToPlayersInRange(sender, MessageGenerator.GenerateShoutMessage(sender.Name,message), distance: GENERIC_SHOUT_DISTANCE);
        }

        public static void SendLow(Player sender, string message, string colour)
        {
            ChatUtils.SendChatMessageToPlayersInRange(sender, MessageGenerator.GenerateLowMessage(sender.Name, message), distance: WHISPER_DISTANCE, colour: colour);
        }

        public static void SendB(Player sender, string message, string colour)
        {
            ChatUtils.SendChatMessageToPlayersInRange(sender, MessageGenerator.GenerateBMessage(sender.Name, message), colour: colour);
        }
    }
}
