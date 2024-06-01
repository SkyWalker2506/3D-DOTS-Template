using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Job
{
    [BurstCompile]
    public partial struct CreateGridMapJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

       [BurstCompile]
        public void Execute(ref GridMapData gridMapData, ref DynamicBuffer<CellBuffer> gridCellBuffer, [ChunkIndexInQuery] int index)
        {
            if (!gridMapData.CreateOrUpdateMap) return;

             if (gridMapData.GridMap != Entity.Null)
            {
                Debug.Log("Destroy ");
                ParallelWriter.DestroyEntity(index, gridMapData.GridMap);
            }
            
            gridMapData.GridMap = ParallelWriter.Instantiate(index, gridMapData.GridMapPrefab);
            ParallelWriter.AddComponent(index, gridMapData.GridMap, LocalTransform.Identity); 
            ParallelWriter.AddComponent(index, gridMapData.GridMap, new LocalToWorld()); 

            float2 cellSize = gridMapData.CellSize;
            var mapSize3D = new float3(gridMapData.MapSize.x ,0, gridMapData.MapSize.y);
            var offset = (-mapSize3D + new float3(cellSize.x,0,cellSize.y)) * 0.5f;
            gridCellBuffer.Clear();
            for (int x = 0; x < gridMapData.CellCount.x; x++)
            {
                for (int y = 0; y < gridMapData.CellCount.y; y++)
                {
                    var entity=  ParallelWriter.Instantiate(index, gridMapData.CellPrefab); 
                    var cellPosition = new float2(x * cellSize.x, y * cellSize.y);
                    ParallelWriter.AddComponent(index, entity, LocalTransform.FromPosition(new float3(cellPosition.x, 0, cellPosition.y)));
                    
                    var gridCell = new CellData
                    {
                        Entity = entity,
                        GridIndex = new int2(x, y),
                        GridPosition = new float2(x * cellSize.x, y * cellSize.y),
                        CellSize = cellSize,
                        WorldPosition = new float3(x * cellSize.x, 0, y * cellSize.y) + gridMapData.MapCenter + offset
                    };
                    ParallelWriter.AddComponent(index, entity, gridCell);
                    ParallelWriter.AddComponent(index, entity, new UpdateCellTag());
                    ParallelWriter.AddComponent(index, entity, new Parent {Value = gridMapData.GridMap});
                    gridCellBuffer.Add(new CellBuffer {GridCell = gridCell});
                }
            }

            gridMapData.CreateOrUpdateMap = false;
        }
    }
}


