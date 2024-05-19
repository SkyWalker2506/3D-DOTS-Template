using SkyWalker.DOTS.Movement.ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace SkyWalker.DOTS.Movement.Job
{
    [BurstCompile]
    public partial struct MoveWithPhysicsJob : IJobEntity
    {
        [ReadOnly] public float DeltaTime;
        
        [BurstCompile]
        public void Execute(ref PhysicsVelocity physicsVelocity, in DirectionData directionData, in SpeedData speedData, in PhysicsMovementTag tag)
        {
            if(directionData.Direction.Equals(float3.zero)) return;
            if(speedData.Speed == 0) return;
            physicsVelocity.Linear = directionData.NormalizedDirection * speedData.Speed*DeltaTime;
        }
    }
}


