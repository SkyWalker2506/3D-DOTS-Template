using SkyWalker.DOTS.Grid.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SkyWalker.DOTS.Grid.System
{
    public partial struct CreateGridMapSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var entityManager = state.World.EntityManager;

            foreach (var gridMapData in SystemAPI.Query<RefRW<GridMapCreationData>>())
            {
                var gridCellBuffer = entityManager.GetBuffer<CellBuffer>(gridMapData.ValueRO.SelfEntity);
                if (!gridMapData.ValueRO.CreateOrUpdateMap) return;

                if (gridMapData.ValueRO.MapEntity != Entity.Null)
                {
                    ecb.DestroyEntity(gridMapData.ValueRO.MapEntity);
                }
                
                gridMapData.ValueRW.MapEntity = entityManager.Instantiate(gridMapData.ValueRO.GridMapPrefab);
                ecb.AddComponent( gridMapData.ValueRO.MapEntity, LocalTransform.Identity); 
                ecb.AddComponent( gridMapData.ValueRO.MapEntity, new LocalToWorld()); 

                float2 cellSize = gridMapData.ValueRO.CellSize;
                var mapSize3D = new float3(gridMapData.ValueRO.MapSize.x ,0, gridMapData.ValueRO.MapSize.y);
                var offset = (-mapSize3D + new float3(cellSize.x,0,cellSize.y)) * 0.5f;
                gridCellBuffer.Clear();
                for (int x = 0; x < gridMapData.ValueRO.CellCount.x; x++)
                {
                    for (int y = 0; y < gridMapData.ValueRO.CellCount.y; y++)
                    {
                        var cellEntity=  entityManager.Instantiate( gridMapData.ValueRO.CellPrefab); 
                        var gridPosition = new float2(x * cellSize.x, y * cellSize.y);
                        
                        var gridCell = new CellData
                        {
                            OwnerMap = gridMapData.ValueRW,
                            Entity = cellEntity,
                            GridIndex = new int2(x, y),
                            GridPosition = gridPosition,
                            CellSize = cellSize,
                            WorldPosition = new float3(gridPosition.x, 0, gridPosition.y) + gridMapData.ValueRO.MapCenter + offset
                        };
                        Debug.Log(gridCell.GridIndex);
                        ecb.AddComponent( cellEntity, LocalTransform.FromPosition(gridCell.WorldPosition));
                        ecb.AddComponent( cellEntity, gridCell);
                        ecb.AddComponent( cellEntity, new Parent {Value = gridMapData.ValueRO.MapEntity});
                        ecb.AddBuffer<CellNeighbourBuffer>(cellEntity);
                        ecb.AddComponent( cellEntity, new UpdateCellTag());
                        gridCellBuffer.Add(new CellBuffer {GridCell = gridCell});
                    }
                }

                gridMapData.ValueRW.CreateOrUpdateMap = false;
            }

        }

        

    }    

}