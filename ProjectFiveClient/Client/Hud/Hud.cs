using RAGE;
using RAGE.Game;
using RAGE.NUI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProjectFiveClient.Client.Hud
{
    internal class Hud : RAGE.Events.Script
    {
        private RAGE.Elements.Player localPlayer = RAGE.Elements.Player.LocalPlayer;
        private const float SCALE_HUD_PLACEMENT_COMPASS = 1.6f;
        private const float SCALE_HUD_PLACEMENT_GENERIC = 0.7f;
        private const float SCALE_FACTOR_DIFFERENCE = SCALE_HUD_PLACEMENT_COMPASS - SCALE_HUD_PLACEMENT_GENERIC;
        private const int X_HUD_PLACEMENT_COMPASS_STREETNAMES = 395;
        private const int X_HUD_PLACEMENT_COMPASS_STRING = 345;
        private const int X_HUD_PLACEMENT_SCALED = (int)(X_HUD_PLACEMENT_COMPASS_STRING * SCALE_FACTOR_DIFFERENCE);

        public Hud()
        {
            Events.Tick += Tick;
        }

        private void Tick(List<Events.TickNametagData> nametags)
        {
            if (localPlayer.Vehicle != null)
            {
                double vehSpeed = localPlayer.Vehicle.GetSpeed() * 2.236936;
                UIResText.Draw("Fuel: 100%", X_HUD_PLACEMENT_SCALED, 890, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
                UIResText.Draw($"MPH: {(int)vehSpeed}", X_HUD_PLACEMENT_SCALED, 930, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            }
            int streetNameHash = 0;
            int crossingRoadHash = 0;
            RAGE.Game.Pathfind.GetStreetNameAtCoord(localPlayer.Position.X, localPlayer.Position.Y, localPlayer.Position.Z, ref streetNameHash, ref crossingRoadHash);
            String streetNameText = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)streetNameHash);
            String crossingRoadText = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)crossingRoadHash);
            String area = RAGE.Game.Zone.GetNameOfZone(localPlayer.Position.X, localPlayer.Position.Y, localPlayer.Position.Z);

            if (crossingRoadText.Length == 0)
            {
                UIResText.Draw($"{streetNameText}", X_HUD_PLACEMENT_COMPASS_STREETNAMES, 970, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            }
            else
            {
                UIResText.Draw($"{streetNameText} / {crossingRoadText}", X_HUD_PLACEMENT_COMPASS_STREETNAMES, 970, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            }

            UIResText.Draw(area, X_HUD_PLACEMENT_COMPASS_STREETNAMES, 1010, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            UIResText.Draw(GetCompassDirection((int)localPlayer.GetHeading()), X_HUD_PLACEMENT_COMPASS_STRING, 960, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_COMPASS, Color.White, UIResText.Alignment.Centered, true, true, 0);
        }

        private String GetCompassDirection(int heading)
        {
            if (heading == 0)
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