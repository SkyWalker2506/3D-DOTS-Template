using SkyWalker.DOTS.Grid.Visual.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Visual.Authoring
{
    public class MapVisualAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject visualPrefab;
        class MapVisualBaker : Baker<MapVisualAuthoring>
        {
            public override void Bake(MapVisualAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.NonUniformScale);
                var visual = GetEntity(authoring.visualPrefab, TransformUsageFlags.Renderable|TransformUsageFlags.NonUniformScale|TransformUsageFlags.WorldSpace);
                AddComponent(entity, new CreateMapVisualData
                {
                    VisualPrefab = visual
                });
            }
        }
    }
}