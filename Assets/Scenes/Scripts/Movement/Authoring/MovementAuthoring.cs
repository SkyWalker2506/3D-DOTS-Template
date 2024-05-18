using System;
using SkyWalker.DOTS.Movement.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


namespace SkyWalker.DOTS.Movement.Authoring
{
    public class MovementAuthoring : MonoBehaviour
    {
        [SerializeField] float speed = 1.0f;
        [SerializeField] bool useRandomMovement = false;
        [SerializeField] float3 direction = new float3(0, 0, 1);


        class MovementBaker : Baker<MovementAuthoring>
        {
            public override void Bake(MovementAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MoveableTag());
                AddComponent(entity, new MoveableData {DoMove = true});
                AddComponent(entity, new SpeedData {Speed = authoring.speed});
                if (authoring.useRandomMovement)
                {
                    AddComponent(entity, new DirectionData {Direction = float3.zero});
                    AddComponent(entity, new SetRandomDirectionTag());
                }
                else
                {
                    AddComponent(entity, new DirectionData {Direction = authoring.direction});
                }
            }
        }
    }
}