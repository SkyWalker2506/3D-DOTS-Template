using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Job
{
    [BurstCompile]
    public partial struct CreateGridMapJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

       [BurstCompile]
        public void Execute(ref GridMapData gridMapData, ref DynamicBuffer<GridCellBuffer> gridCellBuffer, [ChunkIndexInQuery] int index)
        {
            if (!gridMapData.CreateOrUpdateMap) return;

            if (gridMapData.GridMapEntity != Entity.Null)
            {
                ParallelWriter.DestroyEntity(index, gridMapData.GridMapEntity);
            }
            else 
            {
                gridMapData.MapIndex = ++gridMapData.LastMapIndex.Value;
            }

            gridMapData.GridMapEntity = ParallelWriter.CreateEntity(index);
            float2 cellSize = gridMapData.MapSize / gridMapData.CellCount;
            var mapSize3D = new float3(gridMapData.MapSize.x ,0, gridMapData.MapSize.y);
            var offset = (-mapSize3D + new float3(cellSize.x,0,cellSize.y)) * 0.5f;
            gridCellBuffer.Clear();
            for (int x = 0; x < gridMapData.CellCount.x; x++)
            {
                for (int y = 0; y < gridMapData.CellCount.y; y++)
                {
                    var cellPosition = new float2(x * cellSize.x, y * cellSize.y);
                    var worldPosition = new float3(cellPosition.x, 0, cellPosition.y) + gridMapData.MapCenter + offset;
                    var newGridCellBuffer = new GridCellBuffer
                    {
                        GridCell = new GridCellData
                        {
                            GridIndex = new int2(x, y),
                            GridPosition = cellPosition,
                            WorldPosition = worldPosition
                        }
                    };
                    gridCellBuffer.Add(newGridCellBuffer);
                    
                }
            }

            gridMapData.CreateOrUpdateMap = false;
            gridMapData.UpdateGridVisual = true;
        }
    }
}


