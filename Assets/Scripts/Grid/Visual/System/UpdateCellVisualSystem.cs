using SkyWalker.DOTS.Grid.Visual.Job;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.Visual.System
{
    public partial struct UpdateCellVisualSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            return;
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var parallelWriter = ecb.AsParallelWriter();
            

            var updateVisualJob = new UpdateCellVisualJob
            {
                ECB = parallelWriter
            }.ScheduleParallel(state.Dependency);
            
            updateVisualJob.Complete();
        }

    }   

}