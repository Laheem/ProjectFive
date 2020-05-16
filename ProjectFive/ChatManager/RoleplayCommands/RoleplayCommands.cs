using GTANetworkAPI;
using ProjectFive.Utils;
using System;

namespace ProjectFive.ChatManager.RpCommands
{
    internal class RoleplayCommands : Script
    {

        public const string MeCommandColour = "!{#cc66ff}";
        public const string PmCommandColour = "!{#F7FD00}";
        public const string WhisperCommandColour = "!{#F7FD00}";
        public const string RpCommandColour = "!{#D858CB}";


        // TODO - Change this to character when we get around to it.
        [Command("me", GreedyArg = true)]
        public void MeMessage(Player player, String message)
        {
            String parsedMessage = $"* {player.Name} {message}";
            ChatUtils.SendChatMessageToPlayersInRange(player, parsedMessage, colour: MeCommandColour);
        }

        [Command("do", GreedyArg = true)]
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


        [Command("pm", GreedyArg = true)]
        public void PmPlayer(Player player, String targetName, string message)
        {
            ChatUtils.SendPrivateMessageToPlayerByName(player, targetName, message, PmCommandColour);
        }


        [Command("rp", GreedyArg = true)]
        public void RpMessage(Player player, String targetName, string message)
        {
            ChatUtils.SendRpMessageToPlayerByName(player, targetName, message, RpCommandColour);
        }

        [Command("w", GreedyArg = true)]
        public void Whisper(Player player, string targetName, string message)
        {
            ChatUtils.SendWhisperToPlayerByName(player, targetName, message, WhisperCommandColour);
        }

    }
}