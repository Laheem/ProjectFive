using System.Configuration;
using System.Collections.Specialized;
using GTANetworkAPI;
using ProjectFive.CharacterManager;
using ProjectFive.CharacterManager.Service;


namespace ProjectFive.HealthManager.Service
{
    class HealthController
    {
        CharacterEntityService characterEntityService = new CharacterEntityService();
        private const int MAX_HEALTH = 100;


        private void SetPlayerHealth(Player player, int totalHealth)
        {
            Character playerChar = characterEntityService.GetCharacter(player);
            playerChar.Health = totalHealth;
            player.Health = totalHealth;
        }

        private int GetHospitalHealingCost(int currentHealth, int availableFunds)
        {
            int.TryParse(ConfigurationManager.AppSettings.Get("HOSPITAL_HEALTH_PER_POINT"), out int hospitalPrice);
            return (currentHealth - MAX_HEALTH) * 10;
            
        }

        // TODO - Replace mock method.
        public void HospitalHeal()
        {

        }

    }
}
