using System;
using GTANetworkAPI;

namespace ProjectFive
{
    public class Class1 : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("fucking finally.");
        }

        public Class1()
        {
            Console.WriteLine("WOOO");
        }
    }
}
