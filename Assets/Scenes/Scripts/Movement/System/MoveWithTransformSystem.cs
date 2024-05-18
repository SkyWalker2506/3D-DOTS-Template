using SkyWalker.DOTS.Movement.Job;
using Unity.Burst;
using Unity.Entities;

namespace SkyWalker.DOTS.Movement.System
{
    [BurstCompile]
    public partial struct MoveWithTransformSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var moveJob = new MoveWithTransformJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel(state.Dependency);
            moveJob.Complete();
        }

    }

}


