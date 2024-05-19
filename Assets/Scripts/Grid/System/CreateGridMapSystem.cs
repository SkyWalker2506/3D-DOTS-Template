using SkyWalker.DOTS.Grid.Job;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.System
{
    public partial struct CreateGridMapSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {

            var ecb = state.World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>().CreateCommandBuffer();
            var parallelWriter = ecb.AsParallelWriter();
            var createGridMapJob = new CreateGridMapJob
            {
                ParallelWriter = parallelWriter
            }.ScheduleParallel(state.Dependency);
            createGridMapJob.Complete();

            var updateVisualJob = new UpdateGridVisualJob
            {
                ParallelWriter = parallelWriter
            }.ScheduleParallel(createGridMapJob);
            updateVisualJob.Complete(); 

        }

    }

}

