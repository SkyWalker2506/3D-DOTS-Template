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
            parallelWriter = state.World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>().CreateCommandBuffer().AsParallelWriter();
            var setRandomDirectionJob = new SetRandomDirectionJob
            {
                Random = Unity.Mathematics.Random.CreateFromIndex(0),
                ECB = parallelWriter
            }.ScheduleParallel(state.Dependency);
            setRandomDirectionJob.Complete();
        }

    }
    

}


