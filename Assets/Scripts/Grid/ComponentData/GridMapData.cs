namespace SkyWalker.DOTS.Grid.ComponentData
{
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    public struct GridMapData : IComponentData
    {
        public Entity GridMapEntity;
        public float2 MapSize;
        public float3 MapCenter;
        public int2 CellCount;
        public uint MapIndex;
        public bool CreateOrUpdateMap;
        public bool UpdateGridVisual;
        public BlobAssetReference<uint> LastMapIndex; 
        public BlobAssetReference<Entity> Visual; 
         

    }


}