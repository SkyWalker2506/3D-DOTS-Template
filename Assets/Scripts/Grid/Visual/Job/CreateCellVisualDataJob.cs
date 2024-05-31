using SkyWalker.DOTS.Grid.ComponentData;
using SkyWalker.DOTS.Grid.Visual.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.Visual.Job
{
    [BurstCompile]
    public partial struct CreateCellVisualDataJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        public void Execute(in Entity entity, in DynamicBuffer<GridCellBuffer> gridCellBuffer, in CreateMapVisualData ShowMapVisualData, [ChunkIndexInQuery] int index)
        {
            Debug.Log(gridCellBuffer.Length);
            foreach (var buffer in gridCellBuffer)
            {
                var gridCell = buffer.GridCell;
                Debug.Log(gridCell.GridPosition);
               // ECB.AddComponent(index, gridCell.Entity, new CellVisualData {Visual =  ShowMapVisualData.VisualPrefab});
                var visual = ECB.Instantiate(0, ShowMapVisualData.VisualPrefab);
               
              
                var size = gridCell.CellSize * .9f;
                ECB.AddComponent(0, visual, LocalTransform.Identity);
                ECB.SetComponent(0, visual, new PostTransformMatrix {Value = float4x4.Scale(size.x, 1, size.y)});
                ECB.AddComponent(0,gridCell.Entity, new CellVisualData {Visual = visual});
                ECB.AddComponent(0, visual, new Parent {Value = gridCell.Entity});
                Debug.Log(gridCell.Entity);


            }
                ECB.SetComponentEnabled<CreateMapVisualData>(0, entity, false);
            
        }
    }
}


