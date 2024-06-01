using System;
using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Job
{
    public partial struct SetNeighborCellJob : IJobEntity
    {
        public EntityManager EntityManager;
        
        [BurstCompile]
        public void Execute(ref CellData cellData, ref DynamicBuffer<CellNeighbourBuffer> cellNeighbourBuffer, in UpdateCellTag updateCellTag)
        {
            cellNeighbourBuffer.Clear();
            var gridIndex = cellData.GridIndex;
            var xMin = Math.Max(0,gridIndex.x-1);
            var xMax = Math.Min(gridIndex.x+1,cellData.OwnerMap.CellCount.x-1);
            var yMin = Math.Max(0,gridIndex.y-1);
            var yMax = Math.Min(gridIndex.y+1,cellData.OwnerMap.CellCount.y-1);
            var cells = EntityManager.GetBuffer<CellBuffer>(cellData.OwnerMap.SelfEntity);
           
            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    if (x == gridIndex.x && y == gridIndex.y)
                    {
                        continue;
                    }
                    var neighbourIndex = new int2(x, y);
                    var neighbourEntity = cells[neighbourIndex.y * cellData.OwnerMap.CellCount.x + neighbourIndex.x].GridCell;
                    cellNeighbourBuffer.Add(new CellNeighbourBuffer {Neighbour = neighbourEntity});
                }
            }
        }
    }
}


