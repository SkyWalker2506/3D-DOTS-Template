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
        public void Execute(in Entity entity,in GridMapCreationData gridMapData, in DynamicBuffer<CellBuffer> cellBuffer, ref DynamicBuffer<CellVisualBuffer> cellVisualBuffer, in CreateMapVisualData showMapVisualData, [ChunkIndexInQuery] int index)
        {
            cellVisualBuffer.Clear();
            foreach (var buffer in cellBuffer)
            {
                var cell = buffer.GridCell;
                Debug.Log(cell.GridPosition);
                var visual = ECB.Instantiate(0, showMapVisualData.VisualPrefab);
                var size = cell.CellSize * .9f;
                ECB.SetComponent(0, visual, LocalTransform.Identity);
                ECB.SetComponent(0, visual, new PostTransformMatrix {Value = float4x4.Scale(size.x, 1, size.y)});
                cellVisualBuffer.Add(new CellVisualBuffer {CellVisual = new CellVisualData {Visual = visual}});
                ECB.AddComponent(0, visual, new Parent {Value = cell.Entity});
            }
            ECB.SetComponentEnabled<CreateMapVisualData>(0, entity, false);
            
        }
    }
}
