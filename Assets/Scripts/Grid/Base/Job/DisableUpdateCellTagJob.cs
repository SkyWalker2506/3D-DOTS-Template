using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Burst;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.Job
{
    public partial struct DisableUpdateCellTagJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;

        
        [BurstCompile]
        public void Execute(in Entity entity,in UpdateCellTag updateCellTag)
        {
            ParallelWriter.SetComponentEnabled<UpdateCellTag>(0, entity, false);
        }
    }
}


