
using Unity.Entities;
using Unity.Mathematics;

namespace SkyWalker.DOTS.Spawner.ComponentData
{
    public struct SpawnerData : IComponentData
    {
        public Entity Prefab;
        public int SpawnAmount;
        public float3 SpawnPosition;
    }
}