﻿using Game.Domain.Model;
using Game.Interface.Core;

namespace Game.Interface
{
    internal class ConsoleRender : BaseRender
    {
        private readonly GameOfLife _currentGameOfLife;

        public ConsoleRender(GameOfLife currentGameOfLife)
        {
            _currentGameOfLife = currentGameOfLife;
            Setup();
        }

        public override void Render()
        {
            for (int column = 0; column < _currentGameOfLife.Cols; column++)
            {
                for(int row = 0;  row < _currentGameOfLife.Rows; row++)
                {
                    if (_currentGameOfLife.CurrentBoardGeneration[column, row] == GameOfLife.ALIVE)
                        Console.Write("██");
                    else
                        Console.Write("  ");
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(0, 0);
        }

        private void Setup()
        {
            Console.CursorVisible = false;
        }
    }
}
