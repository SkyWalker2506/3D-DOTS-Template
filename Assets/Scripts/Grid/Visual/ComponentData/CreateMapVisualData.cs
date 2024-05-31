namespace SkyWalker.DOTS.Grid.Visual.ComponentData
{
    using Unity.Entities;

    public struct CreateMapVisualData : IComponentData, IEnableableComponent
    {
        public Entity VisualPrefab; 
    }
}