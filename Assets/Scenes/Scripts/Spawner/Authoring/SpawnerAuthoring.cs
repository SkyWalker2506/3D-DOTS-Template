
using SkyWalker.DOTS.Spawner.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SkyWalker.DOTS.Spawner.Authoring
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int spawnAmount = 10000;
        [SerializeField] float3 spawnPosition = new float3(0, 0, 0);

        class MovementBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var spawnEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnerData 
                {
                    Prefab = spawnEntity,
                    SpawnAmount = authoring.spawnAmount,
                    SpawnPosition = authoring.spawnPosition
                });
            }
        }
    }
}