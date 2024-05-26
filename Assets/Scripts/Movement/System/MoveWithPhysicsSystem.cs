using SkyWalker.DOTS.Movement.Job;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics.Systems;

namespace SkyWalker.DOTS.Movement.System
{
    [BurstCompile]
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    public partial struct MoveWithPhysicsSystem : ISystem
    {

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var moveJob = new MoveWithPhysicsJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel(state.Dependency);
            moveJob.Complete();
        }

    }


}


