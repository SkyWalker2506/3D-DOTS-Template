namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Entities;
    using Unity.Mathematics;

    public struct GridMapCreationData : IComponentData
    {
        public Entity SelfEntity;
        public Entity GridMapPrefab;
        public Entity CellPrefab;
        public Entity MapEntity;
        public float2 MapSize;
        public float3 MapCenter;
        public int2 CellCount;
        public float2 CellSize => MapSize / CellCount;
        public uint MapIndex;
        public bool CreateOrUpdateMap;
    }

}