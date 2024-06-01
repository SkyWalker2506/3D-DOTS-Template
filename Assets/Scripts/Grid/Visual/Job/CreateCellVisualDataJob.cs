using SkyWalker.DOTS.Grid.ComponentData;
using SkyWalker.DOTS.Grid.Visual.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SkyWalker.DOTS.Grid.Visual.Job
{
    public partial struct CreateCellVisualDataJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParalelWriter;

        public void Execute(in Entity entity, in DynamicBuffer<GridCellBuffer> gridCellBuffer, in CreateMapVisualData ShowMapVisualData, [ChunkIndexInQuery] int index)
        {
           // Debug.Log(gridCellBuffer.Length);
            foreach (var buffer in gridCellBuffer)
            {
                var gridCell = buffer.GridCell;
               // Debug.Log(gridCell.GridPosition);
               // ECB.AddComponent(index, gridCell.Entity, new CellVisualData {Visual =  ShowMapVisualData.VisualPrefab});
                var visual = ParalelWriter.Instantiate(index, ShowMapVisualData.VisualPrefab);
               
              
                var size = gridCell.CellSize * .9f;
                //ParalelWriter.AddComponent(index, visual, LocalTransform.FromPosition(new float3(gridCell.WorldPosition.x, 0, gridCell.WorldPosition.y)));
                ParalelWriter.SetComponent(index, visual, new PostTransformMatrix {Value = float4x4.Scale(size.x, 1, size.y)});
                ParalelWriter.SetComponent(index, gridCell.Entity, new CellVisualData {Visual = visual});
                //ParalelWriter.AddComponent(index, gridCell.Entity, new CellVisualData {Visual = visual});
                //ParalelWriter.AddComponent(index, visual, new Parent {Value = gridCell.Entity});
                //Debug.Log(gridCell.Entity);

            }
            ParalelWriter.SetComponentEnabled<CreateMapVisualData>(index, entity, false);
            
        }
    }
}


