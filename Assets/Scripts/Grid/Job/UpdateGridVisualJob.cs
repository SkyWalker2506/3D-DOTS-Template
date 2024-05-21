using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Job
{
    public partial struct UpdateGridVisualJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

        public void Execute(in Entity entity, ref GridMapData gridMapData, ref DynamicBuffer<GridCellBuffer> gridCellBuffer)
        {
            if (!gridMapData.UpdateGridVisual) return;
            if (gridMapData.Visual==Entity.Null) return;
            Debug.Log("UpdateGridVisualJobStart");
            var size = gridMapData.MapSize/gridMapData.CellCount;
            Debug.Log("size: "+size);

            for (int i = 0; i < gridCellBuffer.Length; i++)
            {
                var cellIndex = i;
                if (gridCellBuffer[i].GridCellVisual.Visual != Entity.Null)
                {
                    ParallelWriter.DestroyEntity(cellIndex, gridCellBuffer[i].GridCellVisual.Visual);
                }
                if(gridCellBuffer[cellIndex].GridCellVisual.Visual != Entity.Null)
                {
                    ParallelWriter.DestroyEntity(cellIndex, gridCellBuffer[cellIndex].GridCellVisual.Visual);
                }

                Entity visual = ParallelWriter.Instantiate(cellIndex, gridMapData.Visual);


                ParallelWriter.SetComponent(cellIndex, visual, new LocalTransform
                {
                    Position = gridCellBuffer[cellIndex].GridCell.WorldPosition,
                    Scale = 1
                });

                ParallelWriter.AddComponent(cellIndex, visual, new PostTransformMatrix 
                {
                    Value = float4x4.Scale(new float3(.95f*size.x, .1f, .95f*size.y))
                });

                gridCellBuffer[cellIndex] = new GridCellBuffer 
                {
                    GridCell = gridCellBuffer[cellIndex].GridCell,
                    GridCellVisual = new GridMapCellVisualData { Visual = visual } 
                };
            }

            gridMapData.UpdateGridVisual = false;
            UnityEngine.Debug.Log("UpdateGridVisualJobEnd");
        }
    }
}


