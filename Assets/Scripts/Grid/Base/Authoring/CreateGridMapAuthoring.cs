
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
        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject mapPrefab;

        class CreateGridMapBaker : Baker<CreateGridMapAuthoring>
        {
            public override void Bake(CreateGridMapAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var mapPrefab = GetEntity(authoring.mapPrefab,TransformUsageFlags.WorldSpace);
                var cellPrefab = GetEntity(authoring.cellPrefab, TransformUsageFlags.WorldSpace);

                AddComponent(entity, new GridMapData
                {
                    GridMapPrefab = mapPrefab,
                    CellPrefab = cellPrefab,
                    MapSize = authoring.mapSize,
                    MapCenter = authoring.mapCenter,
                    CellCount = authoring.cellCount,
                    CreateOrUpdateMap = authoring.createMap,
                });

                AddBuffer<GridCellBuffer>(entity);

            }
        }
    }
}