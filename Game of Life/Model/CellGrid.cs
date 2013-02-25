//Written by Michael Luk - February 24, 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_Life.Model
{
    public struct cellPoint
    {
        public int x, y;

        public cellPoint(int px, int py)
        {
            x = px;
            y = py;
        }
    }

    class CellGrid
    {
        private Cell[,] grid;
        private int gridWidth, gridHeight;

        public CellGrid()
        {
            gridWidth = 10; gridHeight = 10;
            initialize_Grid();
        }

        public CellGrid(int w, int h)
        {
            gridWidth = w; gridHeight = h;
            initialize_Grid();
        }

        public CellGrid(CellGrid copyGrid)
        {
            gridWidth = copyGrid.GridWidth; gridHeight = copyGrid.GridHeight;

            grid = new Cell[gridHeight, gridWidth];
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    grid[i, j] = new Cell(copyGrid.grid[i,j]);
                }
            }
        }

        private void initialize_Grid()
        {
            grid = new Cell[gridHeight, gridWidth];

            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    grid[i, j] = new Cell();
                }
            }
        }

        //input an array of coordinates to set states for multiple cells at once
        public void setCellsState(cellPoint[] cell, States newState)
        {
            for (int i = 0; i < cell.Length; i++)
            {
                grid[cell[i].x, cell[i].y].setState(newState);
            }
        }

        public void setCellsState(int x, int y, States newState)
        {
            grid[x, y].setState(newState);
        }

        public bool isCellAlive(int row, int col)
        {
            return grid[row, col].isAlive();
        }

        public States getCellState(int row, int col)
        {
            return grid[row, col].getState();
        }

        public int GridWidth
        {
            get { return gridWidth; }
        }

        public int GridHeight
        {
            get { return gridHeight; }
        }

    }
}
