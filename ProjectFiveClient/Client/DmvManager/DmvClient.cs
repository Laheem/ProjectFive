using System;
using ProjectFive.Utils;
using RAGE;
using RAGE.Elements;

namespace ProjectFiveClient.Client.DmvManager
{
    internal class DmvClient : RAGE.Events.Script
    {
        private const String IS_IN_DMV_TEST_KEY = "dmvteststarted";

        public DmvClient()
        {
            RAGE.Events.OnPlayerStartEnterVehicle += OnPlayerStartEnterVehicle;
            RAGE.Events.OnPlayerJoin += TestFunc;



            void TestFunc(Player player)
            {
                RAGE.Chat.Output("xd");
            }

            void OnPlayerStartEnterVehicle(Vehicle vehicle, int seatId, Events.CancelEventArgs cancel)
            {
                RAGE.Chat.Output("xd");
                if (vehicle.GetSharedData(VehicleTypeConstants.VEHICLE_TYPE_KEY) as String == VehicleTypeConstants.DMV_VEHICLE_TYPE_VALUE)
                {

                    if (!(bool)Player.LocalPlayer.GetSharedData(IS_IN_DMV_TEST_KEY))
                    {
                        RAGE.Chat.Output("You're not doing a driving test.");
                        cancel.Cancel = true;
                    }

                }
            }

        }
    }
}
