using SkyWalker.DOTS.Movement.Job;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

namespace SkyWalker.DOTS.Movement.System
{
    [BurstCompile]
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    public partial struct PhysicsVelocityAdderSystem : ISystem
    {
        EntityCommandBuffer ecb; 

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if(Time.time < 5) return;
            ecb = state.World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>().CreateCommandBuffer();
            var setRandomDirectionJob = new PhysicsVelocityAddJob
            {
                ECB = ecb
            }.Schedule(state.Dependency);
            setRandomDirectionJob.Complete();
        }


    }


}


