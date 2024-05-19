namespace SkyWalker.DOTS.Movement.ComponentData
{
    using Unity.Entities;
    using Unity.Mathematics;

    public struct DirectionData : IComponentData
    {
        public float3 NormalizedDirection;
        float3 direction;
        public float3 Direction{ 
            get 
            {
                return direction;
            }
            set 
            {
                direction = value;
                NormalizedDirection = math.normalize(value);
            }
        }
        
    }
    
}
