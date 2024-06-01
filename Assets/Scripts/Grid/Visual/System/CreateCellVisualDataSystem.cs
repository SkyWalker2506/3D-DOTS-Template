using SkyWalker.DOTS.Grid.Visual.Job;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.Visual.System
{
    [UpdateInGroup(typeof(SimulationSystemGroup),OrderLast = true)]
    public partial struct CreateCellVisualDataSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

            new CreateCellVisualDataJob
            {
                ECB = ecb.AsParallelWriter()
            }.ScheduleParallel(state.Dependency).Complete();
            
        }

    }   

}