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
        private readonly Dictionary<String, String> fullZoneNames;

        public Hud()
        {
            Events.Tick += Tick;
            fullZoneNames = GenerateFullZoneDictionary();
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
            var currentLocation = localPlayer.Position;
            RAGE.Game.Pathfind.GetStreetNameAtCoord(currentLocation.X, currentLocation.Y, currentLocation.Z, ref streetNameHash, ref crossingRoadHash);
            String streetNameText = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)streetNameHash);
            String crossingRoadText = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)crossingRoadHash);
            if (fullZoneNames.TryGetValue(RAGE.Game.Zone.GetNameOfZone(localPlayer.Position.X, localPlayer.Position.Y, localPlayer.Position.Z), out string area))
            {
                UIResText.Draw(area, X_HUD_PLACEMENT_COMPASS_STREETNAMES, 1010, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            }
            else
            {
                UIResText.Draw(RAGE.Game.Zone.GetNameOfZone(localPlayer.Position.X, localPlayer.Position.Y, localPlayer.Position.Z), X_HUD_PLACEMENT_COMPASS_STREETNAMES, 1010, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);

            }


            if (crossingRoadText.Length == 0)
            {
                UIResText.Draw($"{streetNameText}", X_HUD_PLACEMENT_COMPASS_STREETNAMES, 970, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            }
            else
            {
                UIResText.Draw($"{streetNameText} / {crossingRoadText}", X_HUD_PLACEMENT_COMPASS_STREETNAMES, 970, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_GENERIC, Color.White, UIResText.Alignment.Left, true, true, 0);
            }

            UIResText.Draw(GetCompassDirection((int)localPlayer.GetHeading()), X_HUD_PLACEMENT_COMPASS_STRING, 960, Font.ChaletComprimeCologne, SCALE_HUD_PLACEMENT_COMPASS, Color.White, UIResText.Alignment.Centered, true, true, 0);
        }

        private String GetCompassDirection(int heading)
        {
            if (heading == 0)
            {
                return "N";
            }
            int headingAngle = heading / 45;

            return headingAngle switch
            {
                0 => "N",
                7 => "NE",
                6 => "E",
                5 => "SE",
                4 => "S",
                3 => "SW",
                2 => "W",
                1 => "NW",
                8 => "N",
                _ => "N",
            };
        }
            private Dictionary<String,String> GenerateFullZoneDictionary()
            {
                Dictionary<String, String> fullZoneNames = new Dictionary<String, String>();
                fullZoneNames.Add("AIRP", "Los Santos International Airport");
                fullZoneNames.Add("ALAMO", "Alamo Sea");
                fullZoneNames.Add("ALTA", "Alta");
                fullZoneNames.Add("ARMYB", "Fort Zancudo");
                fullZoneNames.Add("BANHAMC", "Banham Canyon Dr");
                fullZoneNames.Add("BANNING", "Banning");
                fullZoneNames.Add("BEACH", "Vespucci Beach");
                fullZoneNames.Add("BHAMCA", "Banham Canyon");
                fullZoneNames.Add("BRADP", "Braddock Pass");
                fullZoneNames.Add("BRADT", "Braddock Tunnel");
                fullZoneNames.Add("BURTON", "Burton");
                fullZoneNames.Add("CALAFB", "Calafia Bridge");
                fullZoneNames.Add("CANNY", "Raton Canyon");
                fullZoneNames.Add("CCREAK", "Cassidy Creek");
                fullZoneNames.Add("CHAMH", "Chamberlain Hills");
                fullZoneNames.Add("CHIL", "Vinewood Hills");
                fullZoneNames.Add("CHU", "Chumash");
                fullZoneNames.Add("CMSW", "Chiliad Mountain State Wilderness");
                fullZoneNames.Add("CYPRE", "Cypress Flats");
                fullZoneNames.Add("DAVIS", "Davis");
                fullZoneNames.Add("DELBE", "Del Perro Beach");
                fullZoneNames.Add("DELPE", "Del Perro");
                fullZoneNames.Add("DELSOL", "La Puerta");
                fullZoneNames.Add("DESRT", "Grand Senora Desert");
                fullZoneNames.Add("DOWNT", "Downtown");
                fullZoneNames.Add("DTVINE", "Downtown Vinewood");
                fullZoneNames.Add("EAST_V", "East Vinewood");
                fullZoneNames.Add("EBURO", "El Burro Heights");
                fullZoneNames.Add("ELGORL", "El Gordo Lighthouse");
                fullZoneNames.Add("ELYSIAN", "Elysian Island");
                fullZoneNames.Add("GALFISH", "Galilee");
                fullZoneNames.Add("GOLF", "GWC and Golfing Society");
                fullZoneNames.Add("GRAPES", "Grapeseed");
                fullZoneNames.Add("GREATC", "Great Chaparral");
                fullZoneNames.Add("HARMO", "Harmony");
                fullZoneNames.Add("HAWICK", "Hawick");
                fullZoneNames.Add("HORS", "Vinewood Racetrack");
                fullZoneNames.Add("HUMLAB", "Humane Labs and Research");
                fullZoneNames.Add("JAIL", "Bolingbroke Penitentiary");
                fullZoneNames.Add("KOREAT", "Little Seoul");
                fullZoneNames.Add("LACT", "Land Act Reservoir");
                fullZoneNames.Add("LAGO", "Lago Zancudo");
                fullZoneNames.Add("LDAM", "Land Act Dam");
                fullZoneNames.Add("LEGSQU", "Legion Square");
                fullZoneNames.Add("LMESA", "La Mesa");
                fullZoneNames.Add("LOSPUER", "La Puerta");
                fullZoneNames.Add("MIRR", "Mirror Park");
                fullZoneNames.Add("MORN", "Morningwood");
                fullZoneNames.Add("MOVIE", "Richards Majestic");
                fullZoneNames.Add("MTCHIL", "Mount Chiliad");
                fullZoneNames.Add("MTGORDO", "Mount Gordo");
                fullZoneNames.Add("MTJOSE", "Mount Josiah");
                fullZoneNames.Add("MURRI", "Murrieta Heights");
                fullZoneNames.Add("NCHU", "North Chumash");
                fullZoneNames.Add("NOOSE", "N.O.O.S.E");
                fullZoneNames.Add("OCEANA", "Pacific Ocean");
                fullZoneNames.Add("OBSERV", "Galileo Observatory");
                fullZoneNames.Add("PALCOV", "Paleto Cove");
                fullZoneNames.Add("PALETO", "Paleto Bay");
                fullZoneNames.Add("PALFOR", "Paleto Forest");
                fullZoneNames.Add("PALHIGH", "Palomino Highlands");
                fullZoneNames.Add("PALMPOW", "Palmer-Taylor Power Station");
                fullZoneNames.Add("PBLUFF", "Pacific Bluffs");
                fullZoneNames.Add("PBOX", "Pillbox Hill");
                fullZoneNames.Add("PROCOB", "Procopio Beach");
                fullZoneNames.Add("RANCHO", "Rancho");
                fullZoneNames.Add("RGLEN", "Richman Glen");
                fullZoneNames.Add("RICHM", "Richman");
                fullZoneNames.Add("ROCKF", "Rockford Hills");
                fullZoneNames.Add("RTRAK", "Redwood Lights Track");
                fullZoneNames.Add("SANAND", "San Andreas");
                fullZoneNames.Add("SANCHIA", "San Chianski Mountain Range");
                fullZoneNames.Add("SANDY", "Sandy Shores");
                fullZoneNames.Add("SKID", "Mission Row");
                fullZoneNames.Add("SLAB", "Stab City");
                fullZoneNames.Add("STAD", "Maze Bank Arena");
                fullZoneNames.Add("STRAW", "Strawberry");
                fullZoneNames.Add("TATAMO", "Tataviam Mountains");
                fullZoneNames.Add("TERMINA", "Terminal");
                fullZoneNames.Add("TEXTI", "Textile City");
                fullZoneNames.Add("TONGVAH", "Tongva Hills");
                fullZoneNames.Add("TONGVAV", "Tongva Valley");
                fullZoneNames.Add("VCANA", "Vespucci Canals");
                fullZoneNames.Add("VESP", "Vespucci");
                fullZoneNames.Add("VINE", "Vinewood");
                fullZoneNames.Add("WINDF", "Ron Alternates Wind Farm");
                fullZoneNames.Add("WVINE", "West Vinewood");
                fullZoneNames.Add("ZANCUDO", "Zancudo River");
                fullZoneNames.Add("ZP_ORT", "Port of South Los Santos");
                fullZoneNames.Add("ZQ_UAR", "Davis Quartz");

                return fullZoneNames;
            }
        }
    }