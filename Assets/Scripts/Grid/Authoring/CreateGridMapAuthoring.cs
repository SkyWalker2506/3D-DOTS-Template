
using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Authoring
{
    public class CreateGridMapAuthoring : MonoBehaviour
    {

        [SerializeField] float2 mapSize = new float2(100, 100);
        [SerializeField] float3 mapCenter = new float3(0, 0, 0);
        [SerializeField] int2 cellCount = new int2(10, 10);
        [SerializeField] bool createMap = true;
        [SerializeField] bool showGridVisual = true;
        [SerializeField] GameObject gridCellVisualPrefab;

        class CreateGridMapBaker : Baker<CreateGridMapAuthoring>
        {
            public override void Bake(CreateGridMapAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
            
                var builder = new BlobBuilder(Allocator.Temp);
                ref var root = ref builder.ConstructRoot<uint>();
                BlobAssetReference<uint> lastMapIndex = builder.CreateBlobAssetReference<uint>(Allocator.Persistent);
                BlobAssetReference<Entity> visual = builder.CreateBlobAssetReference<Entity>(Allocator.Persistent);
                visual.Value = GetEntity(authoring.gridCellVisualPrefab, TransformUsageFlags.Renderable);
                AddComponent(entity, new GridMapData
                {
                    MapSize = authoring.mapSize,
                    MapCenter = authoring.mapCenter,
                    CellCount = authoring.cellCount,
                    CreateOrUpdateMap = authoring.createMap,
                    UpdateGridVisual= authoring.showGridVisual,
                    LastMapIndex = lastMapIndex,
                    Visual = visual
                });

                if (authoring.showGridVisual)
                {
                    AddComponent(entity, new GridMapCellVisualData());
                }

                AddBuffer<GridCellBuffer>(entity);
            }
        }
    }
}