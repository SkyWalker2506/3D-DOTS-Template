namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Entities;
    using Unity.Mathematics;

    public struct GridCellData : IComponentData
    {
        public int2 GridIndex;
        public float2 GridPosition;
        public float3 WorldPosition;

    }
}
