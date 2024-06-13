using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    internal class Board
    {
        private readonly int boardRows = 20;
        private readonly int boardCols = 28;
        private int[,]? boardState;
        private int seed = 0;

        public void InitializeBoard()
        {
            boardState = new int[boardRows, boardCols];
            seed = Environment.TickCount;
            var rand = new Random(seed);

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardCols; j++)
                {
                    boardState[i, j] = rand.Next(2);
                }
            }
        }

        public int GetBoardRows() {  return boardRows; }
        public int GetBoardCols() {  return boardCols; }
        public int GetSeed() { return seed; }
        public int IsOccupied(int row, int col) 
        { 
            if (boardState != null)
                return boardState[row, col];

            return -1;
        }
    }
}
