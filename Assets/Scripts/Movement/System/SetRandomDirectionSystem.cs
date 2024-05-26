using SkyWalker.DOTS.Movement.Job;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace SkyWalker.DOTS.Movement.System
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct SetRandomDirectionSystem : ISystem
    {
        EntityCommandBuffer.ParallelWriter parallelWriter; 
        
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
          
            var setRandomDirectionJob = new SetRandomDirectionJob
            {
                Random = Unity.Mathematics.Random.CreateFromIndex(0),
                ECB =  ecb.AsParallelWriter()
            }.ScheduleParallel(state.Dependency);
            setRandomDirectionJob.Complete();
        }

    }
    

}


