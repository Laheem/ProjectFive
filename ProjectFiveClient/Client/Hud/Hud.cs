using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjectFiveClient.Client.Hud
{
    class Hud : RAGE.Events.Script
    {
        RAGE.Elements.Player localPlayer = RAGE.Elements.Player.LocalPlayer;
        public Hud()
        {
            Events.Tick += Tick;
        }

        private void Tick(List<Events.TickNametagData> nametags)
        {
            if (localPlayer.Vehicle != null)
            {
                double vehSpeed = localPlayer.Vehicle.GetSpeed() * 2.236936;
                UIResText.Draw("Fuel: 100%", 306, 890, Font.ChaletComprimeCologne, 0.7f, Color.White, UIResText.Alignment.Left, true, true, 0);
                UIResText.Draw($"MPH: {(int)vehSpeed}", 306, 930, Font.ChaletComprimeCologne, 0.7f, Color.White, UIResText.Alignment.Left, true, true, 0);
            }
            int streetNameHash = 0;
            int crossingRoadHash = 0;
            RAGE.Game.Pathfind.GetStreetNameAtCoord(localPlayer.Position.X, localPlayer.Position.Y, localPlayer.Position.Z, ref streetNameHash, ref crossingRoadHash);
            String streetNameText = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)streetNameHash);
            String crossingRoadText = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)crossingRoadHash);



            UIResText.Draw(streetNameText, 390, 970, Font.ChaletComprimeCologne, 0.7f, Color.White, UIResText.Alignment.Left, true, true, 0);
            UIResText.Draw(crossingRoadText, 390, 1010, Font.ChaletComprimeCologne, 0.7f, Color.White, UIResText.Alignment.Left, true, true, 0);
            UIResText.Draw(GetCompassDirection((int) localPlayer.GetHeading()), 340, 960, Font.ChaletComprimeCologne, 1.6f, Color.White, UIResText.Alignment.Centered, true, true, 0);


        }

        private String GetCompassDirection(int heading) 
        {
            if(heading == 0)
            {
                return "N";
            }
            int headingAngle = heading / 45;

            switch (headingAngle)
            {
                case 0:
                    return "N";
                case 7:
                    return "NE";
                case 6:
                    return "E";
                case 5:
                    return "SE";
                case 4:
                    return "S";
                case 3:
                    return "SW";
                case 2:
                    return "W";
                case 1:
                    return "NW";
                case 8:
                    return "N";
                default:
                    return "N";
            }

        }
    }
}
