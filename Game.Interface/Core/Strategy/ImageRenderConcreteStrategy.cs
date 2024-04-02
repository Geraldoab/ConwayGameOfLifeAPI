using Game.Domain.Model;

namespace Game.Interface.Core.Strategy
{
    internal class ImageRenderConcreteStrategy : RenderStrategy
    {
        private readonly GameOfLife _currentGameOfLife;

        public ImageRenderConcreteStrategy(GameOfLife currentGameOfLife)
        {
            _currentGameOfLife = currentGameOfLife;
        }

        public override void Render()
        {
            Console.WriteLine("Not implemented yet ;(");
        }
    }
}
