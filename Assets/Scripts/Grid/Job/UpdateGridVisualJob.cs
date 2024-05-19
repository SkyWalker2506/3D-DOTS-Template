using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SkyWalker.DOTS.Grid.Job
{
    public partial struct UpdateGridVisualJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

        public void Execute(in Entity entity, ref GridMapData gridMapData, ref DynamicBuffer<GridCellBuffer> gridCellBuffer, [ChunkIndexInQuery] int index)
        {
            if (!gridMapData.UpdateGridVisual) return;

            var size = gridMapData.MapSize/gridMapData.CellCount;
            if(gridCellBuffer[index].GridCellVisual.Visual != Entity.Null)
            {
                ParallelWriter.DestroyEntity(index, gridCellBuffer[index].GridCellVisual.Visual);
            }
            ParallelWriter.SetBuffer<GridCellBuffer>(index, entity);
            var visual = ParallelWriter.Instantiate(index, gridMapData.Visual.Value);
            ParallelWriter.SetComponent(index, visual, new LocalTransform
            {
                Position = gridCellBuffer[index].GridCell.WorldPosition,
            });

            ParallelWriter.AddComponent(index, visual, new PostTransformMatrix 
            {
                Value = float4x4.Scale(new float3(size.x, .1f, size.y))
            });

            ParallelWriter.SetComponent(index, visual, new Parent { Value = entity });


            gridCellBuffer[index] = new GridCellBuffer 
            {
                GridCell = gridCellBuffer[index].GridCell,
                GridCellVisual = new GridMapCellVisualData { Visual = visual } 
            };
            gridMapData.UpdateGridVisual = false;
        }
    }
}


