namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Entities;

    public struct GridCellNeighbourBuffer : IBufferElementData
    {
        public GridCellData Neighbour;
    }

}