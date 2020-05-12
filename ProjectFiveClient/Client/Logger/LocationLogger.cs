using System;
using System.Collections.Generic;
using System.Text;
using RAGE;

namespace ProjectFiveClient.Client.Logger
{
    class LocationLogger : Events.Script
    {
        public LocationLogger()
        {
            RAGE.Events.Add("logloc", PrintToLocationFile);
        }

        private void PrintToLocationFile(object[] args)
        {
            
        }
    }
}
