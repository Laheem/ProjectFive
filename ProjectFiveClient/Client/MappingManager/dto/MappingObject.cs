using MessagePack;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.MappingManager.dto
{
    [MessagePackObject]
    public class MappingObject
    {
        [KeyAttribute(0)]
        public Vector3 Location { get; }
        [KeyAttribute(1)]
        public Vector3 Rotation { get;  }
        [KeyAttribute(3)]
        public uint Dimension { get; } = 0;
        [KeyAttribute(2)]
        public int ModelHash { get; }

        [SerializationConstructor]
        public MappingObject(Vector3 location, Vector3 rotation, int modelHash, uint dimension = 0)
        {
            Location = location;
            Rotation = rotation;
            Dimension = dimension;
            ModelHash = modelHash;
        }
    }
}
