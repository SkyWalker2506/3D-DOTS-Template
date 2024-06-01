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
        public void Execute(in Entity entity,in GridMapData gridMapData, in DynamicBuffer<CellBuffer> cellBuffer, ref DynamicBuffer<CellVisualBuffer> cellVisualBuffer, in CreateMapVisualData showMapVisualData, [ChunkIndexInQuery] int index)
        {

            Debug.Log(cellVisualBuffer.Length);
            cellVisualBuffer.Clear();
            foreach (var buffer in cellBuffer)
            {
                var gridCell = buffer.GridCell;
                Debug.Log(gridCell.GridPosition);
                var visual = ECB.Instantiate(0, showMapVisualData.VisualPrefab);
                var size = gridCell.CellSize * .9f;
                ECB.SetComponent(0, visual, LocalTransform.FromPosition(new float3(gridCell.WorldPosition.x, 0, gridCell.WorldPosition.z)));
                ECB.SetComponent(0, visual, new PostTransformMatrix {Value = float4x4.Scale(size.x, 1, size.y)});
                cellVisualBuffer.Add(new CellVisualBuffer {CellVisual = new CellVisualData {Visual = visual}});
               // ECB.AddComponent(0, visual, new Parent {Value = entity});
            }
            ECB.SetComponentEnabled<CreateMapVisualData>(0, entity, false);
            
        }
    }
}
