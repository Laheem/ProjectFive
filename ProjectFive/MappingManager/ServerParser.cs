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

                using (StreamReader sr = new StreamReader(file))
                {

                    
                    try
                    {
                        var mappingXML = XDocument.Parse(sr.ReadToEnd());
                        var placementLocation = mappingXML.Element("SpoonerPlacements").Element("ReferenceCoords");
                        var rotationVector = mappingXML.Element("SpoonerPlacements").Element("Placement").Element("PositionRotation");
                        var hexObjectValue = mappingXML.Element("SpoonerPlacements").Element("Placement").Element("ModelHash").Value;
                        var modelHash = Convert.ToInt32(hexObjectValue, 16);
                        Vector3 location = new Vector3(float.Parse(placementLocation.Element("X").Value), float.Parse(placementLocation.Element("Y").Value), float.Parse(placementLocation.Element("Z").Value));
                        Vector3 rotation = new Vector3(float.Parse(rotationVector.Element("X").Value), float.Parse(rotationVector.Element("Y").Value), float.Parse(rotationVector.Element("Z").Value));



                        mappingObjects.Add(new MappingObject(location, rotation, modelHash));
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"An exception occured while parsing {file}, skipping this mapping file. {e}");
                    }
                }		


            }

            return mappingObjects;
        }
    }
}
