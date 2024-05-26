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
                            Entity = ParallelWriter.CreateEntity(index),
                            GridIndex = new int2(x, y),
                            GridPosition = cellPosition,
                            WorldPosition = worldPosition
                        }
                    };
                    gridCellBuffer.Add(newGridCellBuffer);
                }
            }

            for (int i = 0; i < gridCellBuffer.Length; i++)
            {
                var gridCell = gridCellBuffer[i];
                var gridCellData = gridCell.GridCell;
                var neighbourBuffer = ParallelWriter.AddBuffer<GridCellNeighbourBuffer>(index, gridCellData.Entity);

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        var neighbourIndex = gridCellData.GridIndex + new int2(x, y);
                        if (neighbourIndex.x < 0 || neighbourIndex.x >= gridMapData.CellCount.x || neighbourIndex.y < 0 || neighbourIndex.y >= gridMapData.CellCount.y) continue;
                        GridCellBuffer neighbour = gridCellBuffer[neighbourIndex.x * gridMapData.CellCount.y + neighbourIndex.y];
                        neighbourBuffer.Add(new GridCellNeighbourBuffer {Neighbour = neighbour.GridCell});
                    }
                }
                for (int j = 0; j < neighbourBuffer.Length; j++)
                {
                    Debug.Log("Cell " + gridCellData.GridIndex + " has neighbour " + neighbourBuffer[j].Neighbour.GridIndex);
                }
                //gridCell.GridCell.Neighbours = neighbours;
            }

            gridMapData.CreateOrUpdateMap = false;
            gridMapData.UpdateGridVisual = true;
        }
    }
}


