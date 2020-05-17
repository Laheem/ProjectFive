using GTANetworkAPI;
using MessagePack;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.MappingManager.dto
{
    [MessagePack.MessagePackObject]
    public class MappingObject
    {
        [KeyAttribute(1)]

        public Vector3 Location { get; }
        [KeyAttribute(2)]
        public Vector3 Rotation { get;  }
        [KeyAttribute(3)]
        public uint Dimension { get; } = 0;
        [KeyAttribute(4)]
        public uint ModelHash { get; }

        [SerializationConstructor]

        public MappingObject(Vector3 location, Vector3 rotation, uint modelHash, uint dimension = 0)
        {
            Location = location;
            Rotation = rotation;
            Dimension = dimension;
            ModelHash = modelHash;
        }
    }
}
