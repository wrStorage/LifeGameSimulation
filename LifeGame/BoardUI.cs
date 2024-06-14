using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace LifeGame
{
    internal class BoardUI
    {
        private readonly int offset = 20;

        public void DrawBoardUI(Board gameState, Canvas canvas)
        {
            canvas.Children.Clear();
            for (int i = 0; i < gameState.GetBoardRows(); i++)
            {
                int xOffset = offset * i;

                for (int j = 0; j < gameState.GetBoardCols(); j++)
                {
                    int yOffset = offset * j;
                    Rectangle rectangle = new();
                    rectangle.Width = 20;
                    rectangle.Height = 20;
                    rectangle.Stroke = System.Windows.Media.Brushes.Black;

                    if (gameState.IsOccupied(i, j) == 1)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.SkyBlue;
                    }

                    canvas.Children.Add(rectangle);
                    Canvas.SetTop(rectangle, yOffset);
                    Canvas.SetLeft(rectangle, xOffset);
                }
            }
        }
    }
}
