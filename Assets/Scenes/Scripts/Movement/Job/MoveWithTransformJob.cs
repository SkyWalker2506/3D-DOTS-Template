using SkyWalker.DOTS.Movement.ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SkyWalker.DOTS.Movement.Job
{
    [BurstCompile]
    public partial struct MoveWithTransformJob : IJobEntity
    {
        [ReadOnly] public float DeltaTime;
        
        [BurstCompile]
        public void Execute(ref LocalTransform localTransform, in DirectionData directionData, in SpeedData speedData, in MoveableTag moveableTag)
        {
            if(directionData.Direction.Equals(float3.zero)) return;
            if(speedData.Speed == 0) return;
            localTransform.Position += math.normalize(directionData.Direction) * speedData.Speed * DeltaTime;
        }
    }
}


