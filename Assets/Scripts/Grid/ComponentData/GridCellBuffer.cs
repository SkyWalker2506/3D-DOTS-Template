namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Entities;

    public struct GridCellBuffer : IBufferElementData
    {
        public GridCellData GridCell;
        public GridMapCellVisualData GridCellVisual;
    }
}
