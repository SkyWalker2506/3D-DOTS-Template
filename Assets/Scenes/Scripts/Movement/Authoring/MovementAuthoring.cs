using System;
using SkyWalker.DOTS.Movement.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Authoring;
using Unity.VisualScripting;
using UnityEngine;


namespace SkyWalker.DOTS.Movement.Authoring
{
    public class MovementAuthoring : MonoBehaviour
    {
        [SerializeField] float speed = 1.0f;
        [SerializeField] float3 direction = new float3(0, 0, 1);
        [SerializeField] bool useRandomMovement = false;
        [SerializeField] MovementType movementType;


        class MovementBaker : Baker<MovementAuthoring>
        {
            public override void Bake(MovementAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
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
                switch (authoring.movementType)
                {
                    case MovementType.Transform:
                        AddComponent(entity, new TransformMovementTag());
                        break;
                    case MovementType.Physics:
                        AddComponent(entity, new PhysicsMovementTag());
                        AddComponent(entity, new PhysicsMass {InverseMass = 1.0f});
                        AddComponent(entity, new PhysicsVelocity {Linear = float3.zero});
                        break;
                }
            }
        }
        enum MovementType
        {
            Transform,
            Physics
        }
    }
}
