namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Entities;
    using Unity.Mathematics;

    public struct CellData : IComponentData
    {
        public GridMapCreationData OwnerMap;
        public Entity Entity;
        public int2 GridIndex;
        public float2 GridPosition;
        public float2 CellSize;
        public float3 WorldPosition;
    }
}
