namespace Game.Interface.Core.Strategy
{
    internal class RenderContext
    {
        public RenderStrategy? RenderStrategy { get; set; }

        public void Execute()
        {
            RenderStrategy?.Render();
        }
    }
}
