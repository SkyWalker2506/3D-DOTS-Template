using SkyWalker.DOTS.Grid.ComponentData;
using SkyWalker.DOTS.Grid.Job;
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

            foreach (var (gridMapData,entity) in SystemAPI.Query<RefRW<GridMapData>>().WithEntityAccess())
            {
                var gridCellBuffer = entityManager.GetBuffer<CellBuffer>(entity);
                if (!gridMapData.ValueRO.CreateOrUpdateMap) return;

             if (gridMapData.ValueRO.GridMap != Entity.Null)
            {
                Debug.Log("Destroy ");
               ecb.DestroyEntity(gridMapData.ValueRO.GridMap);
            }
            
            gridMapData.ValueRW.GridMap = entityManager.Instantiate(gridMapData.ValueRO.GridMapPrefab);
            ecb.AddComponent( gridMapData.ValueRO.GridMap, LocalTransform.Identity); 
            ecb.AddComponent( gridMapData.ValueRO.GridMap, new LocalToWorld()); 

            float2 cellSize = gridMapData.ValueRO.CellSize;
            var mapSize3D = new float3(gridMapData.ValueRO.MapSize.x ,0, gridMapData.ValueRO.MapSize.y);
            var offset = (-mapSize3D + new float3(cellSize.x,0,cellSize.y)) * 0.5f;
            Debug.Log("offset: "+offset);
            gridCellBuffer.Clear();
            for (int x = 0; x < gridMapData.ValueRO.CellCount.x; x++)
            {
                for (int y = 0; y < gridMapData.ValueRO.CellCount.y; y++)
                {
                    var cellEntity=  entityManager.Instantiate( gridMapData.ValueRO.CellPrefab); 
                    var gridPosition = new float2(x * cellSize.x, y * cellSize.y);
                    
                    var gridCell = new CellData
                    {
                        Entity = cellEntity,
                        GridIndex = new int2(x, y),
                        GridPosition = gridPosition,
                        CellSize = cellSize,
                        WorldPosition = new float3(gridPosition.x, 0, gridPosition.y) + gridMapData.ValueRO.MapCenter + offset
                    };
                    ecb.AddComponent( cellEntity, LocalTransform.FromPosition(gridCell.WorldPosition));
                    ecb.AddComponent( cellEntity, gridCell);
                    ecb.AddComponent( cellEntity, new UpdateCellTag());
                    ecb.AddComponent( cellEntity, new Parent {Value = gridMapData.ValueRO.GridMap});
                    gridCellBuffer.Add(new CellBuffer {GridCell = gridCell});
                }
            }

            gridMapData.ValueRW.CreateOrUpdateMap = false;
            }

        }

        

    }    

}