
using System;
using SkyWalker.DOTS.Movement.ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace SkyWalker.DOTS.Movement.Job
{
    [BurstCompile]
    public partial struct SetRandomDirectionJob : IJobEntity
    {
        [ReadOnly] public Unity.Mathematics.Random Random;
        public EntityCommandBuffer.ParallelWriter ECB;
        
        [BurstCompile]
        public void Execute(in Entity entity,in SetRandomDirectionTag setRandomDirectionTag, ref DirectionData directionData,  [ChunkIndexInQuery] int index)
        {
           directionData.Direction = new float3(Random.NextFloat()* 2 - 1, 0, Random.NextFloat()* 2 - 1);
           ECB.RemoveComponent<SetRandomDirectionTag>(index, entity);
        }
 
    }
}


