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
            Dictionary<String, String> fullZoneNames = new Dictionary<String, String>
            { { "AIRP", "Los Santos International Airport" },
                { "ALAMO", "Alamo Sea" },
                { "ALTA", "Alta" },
                { "ARMYB", "Fort Zancudo" },
                { "BANHAMC", "Banham Canyon Dr" },
                { "BANNING", "Banning" },
                { "BEACH", "Vespucci Beach" },
                { "BHAMCA", "Banham Canyon" },
                { "BRADP", "Braddock Pass" },
                { "BRADT", "Braddock Tunnel" },
                { "BURTON", "Burton" },
                { "CALAFB", "Calafia Bridge" },
                { "CANNY", "Raton Canyon" },
                { "CCREAK", "Cassidy Creek" },
                { "CHAMH", "Chamberlain Hills" },
                { "CHIL", "Vinewood Hills" },
                { "CHU", "Chumash" },
                { "CMSW", "Chiliad Mountain State Wilderness" },
                { "CYPRE", "Cypress Flats" },
                { "DAVIS", "Davis" },
                { "DELBE", "Del Perro Beach" },
                { "DELPE", "Del Perro" },
                { "DELSOL", "La Puerta" },
                { "DESRT", "Grand Senora Desert" },
                { "DOWNT", "Downtown" },
                { "DTVINE", "Downtown Vinewood" },
                { "EAST_V", "East Vinewood" },
                { "EBURO", "El Burro Heights" },
                { "ELGORL", "El Gordo Lighthouse" },
                { "ELYSIAN", "Elysian Island" },
                { "GALFISH", "Galilee" },
                { "GOLF", "GWC and Golfing Society" },
                { "GRAPES", "Grapeseed" },
                { "GREATC", "Great Chaparral" },
                { "HARMO", "Harmony" },
                { "HAWICK", "Hawick" },
                { "HORS", "Vinewood Racetrack" },
                { "HUMLAB", "Humane Labs and Research" },
                { "JAIL", "Bolingbroke Penitentiary" },
                { "KOREAT", "Little Seoul" },
                { "LACT", "Land Act Reservoir" },
                { "LAGO", "Lago Zancudo" },
                { "LDAM", "Land Act Dam" },
                { "LEGSQU", "Legion Square" },
                { "LMESA", "La Mesa" },
                { "LOSPUER", "La Puerta" },
                { "MIRR", "Mirror Park" },
                { "MORN", "Morningwood" },
                { "MOVIE", "Richards Majestic" },
                { "MTCHIL", "Mount Chiliad" },
                { "MTGORDO", "Mount Gordo" },
                { "MTJOSE", "Mount Josiah" },
                { "MURRI", "Murrieta Heights" },
                { "NCHU", "North Chumash" },
                { "NOOSE", "N.O.O.S.E" },
                { "OCEANA", "Pacific Ocean" },
                { "OBSERV", "Galileo Observatory" },
                { "PALCOV", "Paleto Cove" },
                { "PALETO", "Paleto Bay" },
                { "PALFOR", "Paleto Forest" },
                { "PALHIGH", "Palomino Highlands" },
                { "PALMPOW", "Palmer-Taylor Power Station" },
                { "PBLUFF", "Pacific Bluffs" },
                { "PBOX", "Pillbox Hill" },
                { "PROCOB", "Procopio Beach" },
                { "RANCHO", "Rancho" },
                { "RGLEN", "Richman Glen" },
                { "RICHM", "Richman" },
                { "ROCKF", "Rockford Hills" },
                { "RTRAK", "Redwood Lights Track" },
                { "SANAND", "San Andreas" },
                { "SANCHIA", "San Chianski Mountain Range" },
                { "SANDY", "Sandy Shores" },
                { "SKID", "Mission Row" },
                { "SLAB", "Stab City" },
                { "STAD", "Maze Bank Arena" },
                { "STRAW", "Strawberry" },
                { "TATAMO", "Tataviam Mountains" },
                { "TERMINA", "Terminal" },
                { "TEXTI", "Textile City" },
                { "TONGVAH", "Tongva Hills" },
                { "TONGVAV", "Tongva Valley" },
                { "VCANA", "Vespucci Canals" },
                { "VESP", "Vespucci" },
                { "VINE", "Vinewood" },
                { "WINDF", "Ron Alternates Wind Farm" },
                { "WVINE", "West Vinewood" },
                { "ZANCUDO", "Zancudo River" },
                { "ZP_ORT", "Port of South Los Santos" },
                { "ZQ_UAR", "Davis Quartz" }
            };

            return fullZoneNames;
            }
        }
    }