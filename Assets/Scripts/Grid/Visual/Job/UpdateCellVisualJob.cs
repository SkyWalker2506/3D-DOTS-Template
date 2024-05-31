using System.Diagnostics;
using SkyWalker.DOTS.Grid.ComponentData;
using SkyWalker.DOTS.Grid.Visual.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SkyWalker.DOTS.Grid.Visual.Job
{
    [BurstCompile]
    public partial struct UpdateCellVisualJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        public void Execute(in UpdateCellTag updateCellTag, ref GridCellData gridCellData, ref CellVisualData gridCellVisualData, [ChunkIndexInQuery] int index)
        {

        }
    }
}


