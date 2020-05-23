using GTANetworkAPI;
using ProjectFive.Utils;
using System;

namespace ProjectFive.ChatManager.RpCommands
{
    internal class RoleplayCommands : Script
    {

        public const string GENERIC_RP_COLOUR = "!{#cc66ff}";
        public const string PM_COMMAND_COLOUR = "!{#F7FD00}";
        public const string WHISPER_COMMAND_COLOUR = "!{#F7FD00}";
        public const string RP_COMMAND_COLOUR = "!{#D858CB}";
        public const string LOW_COMMAND_COLOUR = "!{#7CE1F0}";
        public const string B_COMMAND_COLOUR = "!{#ABAAAA}";



        // TODO - Change this to character when we get around to it.
        [Command("me", GreedyArg = true)]
        public void MeMessage(Player player, String message)
        {
            ChatUtils.SendMeMessage(player, message, GENERIC_RP_COLOUR);
        }

        [Command("do", GreedyArg = true)]
        public void DoMessage(Player player, String message)
        {
            ChatUtils.SendDoMessage(player, message, GENERIC_RP_COLOUR);
        }

        [Command("attempt", GreedyArg = true)]
        public void startAttempt(Player player, string attemptedAction)
        {
            ChatUtils.SendAttemptMessage(player, attemptedAction, GENERIC_RP_COLOUR);
        }


        [Command("pm", GreedyArg = true)]
        public void PmPlayer(Player player, String targetName, string message)
        {
            ChatUtils.SendPrivateMessageToPlayerByName(player, targetName, message, PM_COMMAND_COLOUR);
        }


        [Command("rp", GreedyArg = true)]
        public void RpMessage(Player player, String targetName, string message)
        {
            ChatUtils.SendRpMessageToPlayerByName(player, targetName, message, RP_COMMAND_COLOUR);
        }

        [Command("whisper", GreedyArg = true, Alias = "w")]
        public void Whisper(Player player, string targetName, string message)
        {
            ChatUtils.SendWhisperToPlayerByName(player, targetName, message, WHISPER_COMMAND_COLOUR);
        }

        [Command("shout",Alias = "s", GreedyArg = true)]
        public void Shout(Player player, string message)
        {
            ChatUtils.SendShout(player, message);
        }

        [Command("low", GreedyArg = true)]
        public void LowCommand(Player player, string message)
        {
            ChatUtils.SendLow(player, message, LOW_COMMAND_COLOUR);
        }

        [Command("b", GreedyArg = true)]
        public void BCommand(Player player, string message)
        {
            ChatUtils.SendB(player, message, B_COMMAND_COLOUR);
        }

    }
}