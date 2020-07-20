using System.Configuration;
using System.Collections.Specialized;
using GTANetworkAPI;
using ProjectFive.CharacterManager;
using ProjectFive.CharacterManager.Service;
using ProjectFive.Utils;
using Newtonsoft.Json;

namespace ProjectFive.HealthManager.Service
{
    class HealthController
    {
        CharacterEntityService characterEntityService = new CharacterEntityService();
        private const int MAX_HEALTH = 100;
        private const int HOSPITAL_COST_PER_POINT = 10;



        private void SetPlayerHealth(Player player, int totalHealth)
        {
            Character playerChar = characterEntityService.GetCharacter(player);
            playerChar.Health = totalHealth;
            player.Health = totalHealth;
        }

        private int GetHospitalHealingCost(int currentHealth, int availableFunds)
        {
            int.TryParse(ConfigurationManager.AppSettings.Get("HOSPITAL_HEALTH_PER_POINT"), out int hospitalPrice);
            return (MAX_HEALTH - currentHealth) * HOSPITAL_COST_PER_POINT;
            
        }

        // TODO - Replace mock method.
        public void HospitalHeal(Player player, int availableMoney)
        {
            // TODO - This could be wrapped in a constant, but we might want to have dynamic pricing updates which would cause issues with constants. Leave this as is and come back to it.
            int.TryParse(ConfigurationManager.AppSettings.Get("HOSPITAL_HEALTH_PER_POINT"), out int hospitalPrice);
            if(HOSPITAL_COST_PER_POINT == 0)
            {
                NAPI.Util.ConsoleOutput("[HEALTH MANAGER] - HOSPITAL PRICE IS SET TO ZERO. THIS WILL CAUSE ISSUES!");
                return;
            }
            Character playerCharacter = characterEntityService.GetCharacter(player);
            if(player.Health >= 100)
            {
                ChatUtils.SendInfoMessage(player, "You don't look injured to us. We've not charged anything for the checkup.");
                return;
            }
            if(availableMoney < HOSPITAL_COST_PER_POINT)
            {
                ChatUtils.SendInfoMessage(player, "Sorry, you don't have any money to pay for your hospital bills. We can't help.");
                
            } else
            {
                int requiredMoney = GetHospitalHealingCost(player.Health, availableMoney);
                if(availableMoney > requiredMoney)
                {
                    ChatUtils.SendInfoMessage(player, $"You're all healed up. We've took ${requiredMoney} from your funds.");
                    SetPlayerHealth(player, 100);
                } else
                {
                    ChatUtils.SendInfoMessage(player, $"Fixed you up the best we could. Stay safe out there!");
                    SetPlayerHealth(player, player.Health + (availableMoney / HOSPITAL_COST_PER_POINT));
                }
            }
        }

    }
}
