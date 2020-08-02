using System;
using GTANetworkAPI;
using ProjectFive.AccountManager;
using ProjectFive.DatabaseManager;
using ProjectFive.Utils;

namespace ProjectFive
{
    public class DebugCore : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("[CORE] Server booted up succesfully...");
        }

        AccountEntityService accountEntityService = new AccountEntityService();
        AccountService accountService = new AccountService();

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
            if (!string.Equals(player.SocialClubName, "cratox0", StringComparison.OrdinalIgnoreCase))
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

        [Command("goto", GreedyArg = true)]
        public void Teleport(Player player, String xLoc, String yLoc, String zLoc)
        {
            double x;
            double y;
            double z;

            if (double.TryParse(xLoc, out x) && double.TryParse(yLoc, out y) && double.TryParse(zLoc, out z))
            {
                player.Position = new Vector3(x, y, z);
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "USAGE: /goto x y z");
            }
        }

        [Command("g",GreedyArg = true)]
        public void GlobalChat(Player player, String message)
        {
            NAPI.Chat.SendChatMessageToAll($"{player.Name}: {message}");
        }

        [Command("password",GreedyArg = true)]
        public void resetPassword(Player player, String password)
        {
            Account account = accountService.GetAccount(player);
            if (account != null)
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(password);
                accountService.UpdateAccount(account);
                ChatUtils.SendInfoMessage(player, "[Password] - Your password has been reset.");
            } else
            {
                ChatUtils.SendInfoMessage(player, "stop trying to break it!!!!!!!!!!!!!!!!!!!");
            }
        }

    }
}
