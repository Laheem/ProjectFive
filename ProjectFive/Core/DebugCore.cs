using System;
using GTANetworkAPI;

namespace ProjectFive
{
    public class DebugCore : Script
    {

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("[CORE] Server booted up succesfully...");
        }

        [Command("tp", Alias = "teleport", GreedyArg = true)]
        public void TeleportToPlayer(Player player, String name)
        {
            Player targetPlayer = NAPI.Player.GetPlayerFromName(name);
            if (targetPlayer == null)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "Looks like you typed in an invalid name.");
                return;
            }
            player.Position = targetPlayer.Position;

        }

        [Command("health", GreedyArg = true, Alias = "test")]
        public void giveHealth(Player player, String hp)
        {
            int hpNumber;
            if(int.TryParse(hp, out hpNumber))
            {
                player.Health = hpNumber;
            } else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "That's not a number.");
            }
        }

        [Command("armour", GreedyArg = true)]
        public void giveArmour(Player player, String armour)
        {
            int armourNumber;
            if (int.TryParse(armour, out armourNumber))
            {
                player.Armor = armourNumber;
            } else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "That's not a number.");
            }
        }

        [Command("changemodel", GreedyArg = true)]
        public void changeModel(Player player, String name)
        {
            if (player.SocialClubName.ToLower() != "cratox0")
            {
                player.Health = 0;
                NAPI.Chat.SendChatMessageToPlayer(player, "No.");
                return;
            }
            var modelHash = NAPI.Util.PedNameToModel(name);

            if (modelHash == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "That's not a valid player model.");
            }
            else
            {
                NAPI.Player.SetPlayerSkin(player, modelHash);
            }
        }
    }
}
