using SkyWalker.DOTS.Grid.Job;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.System
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial struct DisableUpdateCellTagSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var parallelWriter = ecb.AsParallelWriter();
            
            new DisableUpdateCellTagJob
            {
                ParallelWriter = parallelWriter
            }.ScheduleParallel(state.Dependency).Complete();

        }

    }
    
}