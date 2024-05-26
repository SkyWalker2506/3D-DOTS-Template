using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SkyWalker.DOTS.Grid.Job
{
    [BurstCompile]
    public partial struct UpdateGridVisualJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

        [BurstCompile]
        public void Execute(in Entity entity, ref GridMapData gridMapData, in DynamicBuffer<GridCellBuffer> gridCellBuffer,ref DynamicBuffer<GridCellVisualBuffer> gridCellVisualBuffer, [ChunkIndexInQuery] int index)
        {
            if (!gridMapData.UpdateGridVisual) return;
            if (gridMapData.Visual == Entity.Null) return;
            var size = gridMapData.MapSize/gridMapData.CellCount;
            for (int i = 0; i < gridCellVisualBuffer.Length; i++)
            {
                if (gridCellVisualBuffer[i].GridCellVisual != Entity.Null)
                {
                    ParallelWriter.DestroyEntity(index, gridCellVisualBuffer[i].GridCellVisual);
                }
            }

            gridCellVisualBuffer.Clear();

            for (int i = 0; i < gridCellBuffer.Length; i++)
            {
                Entity visual = ParallelWriter.Instantiate(index, gridMapData.Visual);

                ParallelWriter.SetComponent(index, visual, LocalTransform.FromPosition(gridCellBuffer[i].GridCell.WorldPosition));

                ParallelWriter.AddComponent(index, visual, new PostTransformMatrix 
                {
                    Value = float4x4.Scale(new float3(.95f*size.x, .1f, .9f*size.y))
                });

                gridCellVisualBuffer.Add(new GridCellVisualBuffer 
                {
                    GridCellVisual =  visual  
                });
            }

            gridMapData.UpdateGridVisual = false;
        }
    }
}


