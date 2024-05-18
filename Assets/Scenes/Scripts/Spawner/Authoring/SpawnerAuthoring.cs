
using Unity.Entities;
using UnityEngine;

namespace SkyWalker.DOTS.Spawner.Authoring
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int spawnAmount = 10000;

        class MovementBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(authoring.prefab, TransformUsageFlags.None);

            }
        }
    }
}