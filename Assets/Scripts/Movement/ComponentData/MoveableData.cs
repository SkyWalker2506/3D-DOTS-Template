namespace SkyWalker.DOTS.Movement.ComponentData
{
    using Unity.Entities;

    //Note: Add also MoveableTag component to same entity to control the movement
    public struct MoveableData : IComponentData
    {
        public bool DoMove;
    }

}