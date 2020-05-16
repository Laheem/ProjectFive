using GTANetworkAPI;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.MappingManager.dto
{
    class MappingObject
    {
        
        public Vector3 Location { get; }
        public Vector3 Rotation { get;  }
        public uint Dimension { get; } = 0;
        public int ModelHash { get; }


        public MappingObject(Vector3 location, Vector3 rotation, int modelHash, uint dimension = 0)
        {
            Location = location;
            Rotation = rotation;
            Dimension = dimension;
            ModelHash = modelHash;
        }
    }
}
