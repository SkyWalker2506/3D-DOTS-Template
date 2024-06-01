using SkyWalker.DOTS.Grid.ComponentData;
using SkyWalker.DOTS.Grid.Job;
using Unity.Collections;
using Unity.Entities;

namespace SkyWalker.DOTS.Grid.System
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial struct SetNeighborCellSystem : ISystem
    {
        
        public void OnCreate(ref SystemState state)
        {
            var queryBuilder = new EntityQueryBuilder(Allocator.Temp);
            var query=queryBuilder.WithAll<CellData, CellNeighbourBuffer, UpdateCellTag>().Build(ref state);
            state.RequireForUpdate(query);
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var parallelWriter = ecb.AsParallelWriter();
            var job = new SetNeighborCellJob
            {
                EntityManager = state.World.EntityManager,
            };
            job.Schedule(state.Dependency).Complete();
        }

    }

}