using GTANetworkAPI;
using ProjectFive.HealthManager.Dto;
using ProjectFive.HealthManager.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.HealthManager
{
    class ArmourHandler : Script
    {

        ArmourController armourController = new ArmourController();


        // TODO - Manage with inventory system when this is working.
        private void EquipArmour(Player player, ArmourType armourType)
        {
            armourController.SetPlayerArmour(player, armourType);
        }


        [Command("setarmour", Alias = "sarm")]
        public void GiveArmour(Player player, String targetPlayer, String armourAmount)
        {
                armourController.AdminSetPlayerArmour(player, targetPlayer, armourAmount);
        }


        // TODO - remove debug command.

        [Command("equiparmour")]
        public void EquipArmourDebug(Player player, String armourType)
        {
            switch (armourType.ToLower())
            {
                case "light":
                    EquipArmour(player, ArmourType.Light);
                    break;
                case "medium":
                    EquipArmour(player, ArmourType.Medium);
                    break;
                case "heavy":
                    EquipArmour(player, ArmourType.Heavy);
                    break;
                case "swat":
                    EquipArmour(player, ArmourType.SWAT);
                    break;
            }
        }



    }
}
