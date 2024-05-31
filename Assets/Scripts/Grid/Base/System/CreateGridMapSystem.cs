using SkyWalker.DOTS.Grid.Job;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.System
{
    public partial struct CreateGridMapSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var parallelWriter = ecb.AsParallelWriter();
            
            var createGridMapJob = new CreateGridMapJob
            {
                ParallelWriter = parallelWriter
            };
            
            var createGridMapHandle= createGridMapJob.ScheduleParallel(state.Dependency);
            createGridMapHandle.Complete();

        }

    }    

}