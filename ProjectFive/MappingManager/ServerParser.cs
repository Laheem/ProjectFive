using GTANetworkAPI;
using ProjectFive.MappingManager.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ProjectFive.MappingManager
{
    class ServerParser
    {

        protected static String[] getAllMappingFiles()
        {
            return Directory.GetFiles("mapping");
        }

        public static List<MappingObject> getAllMappingObjects()
        {
           List<MappingObject> mappingObjects = new List<MappingObject>();

            foreach (var file in getAllMappingFiles())
            {
                Console.WriteLine($"Now parsing {file}");
                List<MappingObject> newMappingObjects = new List<MappingObject>();

                using (StreamReader sr = new StreamReader(file))
                {

                    try
                    {
                        var mappingXML = XDocument.Parse(sr.ReadToEnd());
                        var placementLocations = mappingXML.Element("SpoonerPlacements").Elements("Placement");
                        foreach(var placementLocation in placementLocations)
                        {
                            var hexObjectValue = placementLocation.Element("ModelHash").Value;
                            var positionVector = placementLocation.Element("PositionRotation");
                            var modelHash = Convert.ToInt32(hexObjectValue, 16);


                            Vector3 location = new Vector3(float.Parse(positionVector.Element("X").Value), float.Parse(positionVector.Element("Y").Value), float.Parse(positionVector.Element("Z").Value));
                            Vector3 rotation = new Vector3(float.Parse(positionVector.Element("Pitch").Value), float.Parse(positionVector.Element("Roll").Value), float.Parse(positionVector.Element("Yaw").Value));

                            newMappingObjects.Add(new MappingObject(location, rotation, modelHash));


                        }

                        mappingObjects.AddRange(newMappingObjects);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An exception occured while parsing {file}, skipping this mapping file. {e}");
                        break;
                    }
                }		


            }

            return mappingObjects;
        }
    }
}
