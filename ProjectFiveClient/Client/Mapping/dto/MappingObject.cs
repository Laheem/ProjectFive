namespace ProjectFiveClient.Client.Mapping
{
    public class MappingObject
    {

        public float XLoc { get; }
        public float YLoc { get; }
        public float ZLoc { get; }
        public float XRot { get; }
        public float YRot { get; }
        public float ZRot { get; }
        public uint Dimension { get; } = 0;
        public uint ModelHash { get; }



        public MappingObject(float xLoc, float yLoc, float zLoc, float xRot, float yRot, float zRot, uint modelHash, uint dimension = 0)
        {
            XLoc = xLoc;
            YLoc = yLoc;
            ZLoc = zLoc;
            XRot = xRot;
            YRot = yRot;
            ZRot = zRot;

            Dimension = dimension;
            ModelHash = modelHash;
        }
    }
}
