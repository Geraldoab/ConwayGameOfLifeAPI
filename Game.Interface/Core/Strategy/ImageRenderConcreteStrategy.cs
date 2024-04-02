﻿using Game.Domain.Core;
using Game.Domain.Model;

namespace Game.Interface.Core.Strategy
{
    internal class ImageRenderConcreteStrategy : RenderStrategy
    {
        private readonly BaseGameOfLife _currentGameOfLife;

        public ImageRenderConcreteStrategy(BaseGameOfLife currentGameOfLife)
        {
            _currentGameOfLife = currentGameOfLife;
        }

        public override void Render()
        {
            Console.WriteLine("Not implemented yet ;(");
        }
    }
}
