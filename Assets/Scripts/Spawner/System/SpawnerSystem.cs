using SkyWalker.DOTS.Movement.Job;
using SkyWalker.DOTS.Spawner.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;

namespace SkyWalker.DOTS.Movement.System
{

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct SpawnerSystem : ISystem
    {
        EntityCommandBuffer entityCommandBuffer; 
        

        public void OnUpdate(ref SystemState state)
        {
            entityCommandBuffer = state.World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>().CreateCommandBuffer();
         
            var ParallelWriter = entityCommandBuffer.AsParallelWriter();

            foreach (var (spawnerData, entity) in SystemAPI.Query<RefRO<SpawnerData>>().WithEntityAccess())
            {
                var spawnerJob = new SpawnerJob
                {
                    Prefab = spawnerData.ValueRO.Prefab,
                    SpawnPosition = spawnerData.ValueRO.SpawnPosition,
                    ECB = ParallelWriter
                }.ScheduleParallel(spawnerData.ValueRO.SpawnAmount, spawnerData.ValueRO.SpawnAmount/1000, state.Dependency);
                spawnerJob.Complete();
                entityCommandBuffer.RemoveComponent<SpawnerData>(entity);
            }
    
        }
    }
}


