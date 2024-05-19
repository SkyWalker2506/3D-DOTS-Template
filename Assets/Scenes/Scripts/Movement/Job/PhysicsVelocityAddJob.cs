using SkyWalker.DOTS.Movement.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace SkyWalker.DOTS.Movement.Job
{
    [BurstCompile]
    public partial struct PhysicsVelocityAddJob : IJobEntity
    {
        public EntityCommandBuffer ECB;

        public void Execute(in AddPhysicsVelocityTag addPhysicsVelocityTag, in Entity entity)
        {
            ECB.AddComponent(entity, new Unity.Physics.PhysicsVelocity {Linear = float3.zero, Angular = float3.zero});
            ECB.RemoveComponent<AddPhysicsVelocityTag>(entity);
        }
    }


}


