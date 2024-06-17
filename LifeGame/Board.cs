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
        public void InitializeBoard()
        {
            boardState = new int[boardRows, boardCols];
        }
        public void PopulateBoard(int seed)
        {
            boardState = new int[boardRows, boardCols];
            Random rand = new(seed);

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardCols; j++)
                {
                    boardState[i, j] = rand.Next(2);
                }
            }
        }

        public void AdvanceOneGeneration()
        {
            int[,]? newBoardState = new int[boardRows, boardCols];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardCols; j++)
                {
                    int adjacentLiveCells = CheckAdjacentCells(i, j);

                    if (boardState[i, j] == 1 && (adjacentLiveCells > 1 && adjacentLiveCells < 4))
                    {
                        newBoardState[i, j] = 1;
                    }
                    else if (boardState[i, j] == 0 && adjacentLiveCells == 3)
                    {
                        newBoardState[i, j] = 1;
                    }
                }
            }
            boardState = newBoardState;
        }

        private int CheckAdjacentCells(int row, int col)
        {
            int liveAdjacentCells = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (row + i == row && col + j == col)
                    {
                        continue;
                    }

                    if (IsIndexInBounds(row + i, col + j))
                    {
                        liveAdjacentCells += boardState[row + i, col + j];
                    }
                }
            }
            return liveAdjacentCells;
        }

        private bool IsIndexInBounds(int row, int col)
        {
            return ((row >= 0 && row < boardRows) && (col >= 0 && col < boardCols));
        }
        public int GetBoardRows() {  return boardRows; }
        public int GetBoardCols() {  return boardCols; }
        public int IsOccupied(int row, int col) 
        { 
            if (boardState != null)
                return boardState[row, col];

            return -1;
        }
    }
}
