namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Entities;

    public struct CellNeighbourBuffer : IBufferElementData
    {
        public CellData Neighbour;
    }

}