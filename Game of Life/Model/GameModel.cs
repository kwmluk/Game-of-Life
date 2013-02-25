//Written by Michael Luk - February 24, 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace Game_of_Life.Model
{
    class GameModel : INotifyPropertyChanged
    {
        #region Property changed
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                // property changed
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private CellGrid cellGrid;
        private int generation;

        public GameModel()
        {
            initializeGame();
        }

        public void initializeGame()
        {
            cellGrid = new CellGrid(15, 15);
            generation = 0;

            initialGameState();
        }

        private void initialGameState()
        {
            //"Toad" oscillator
            cellGrid.setCellsState(3, 9, States.alive);
            cellGrid.setCellsState(3, 10, States.alive);
            cellGrid.setCellsState(3, 11, States.alive);
            cellGrid.setCellsState(4, 10, States.alive);
            cellGrid.setCellsState(4, 11, States.alive);
            cellGrid.setCellsState(4, 12, States.alive);

            //Glider
            cellGrid.setCellsState(2, 1, States.alive);
            cellGrid.setCellsState(2, 2, States.alive);
            cellGrid.setCellsState(2, 3, States.alive);
            cellGrid.setCellsState(1, 3, States.alive);
            cellGrid.setCellsState(0, 2, States.alive);
            
        }

        public void setCellAlive(int row, int col)
        {
            cellGrid.setCellsState(row, col, States.alive);
        }

        public void toggleCellLife(int row, int col)
        {
            if (cellGrid.isCellAlive(row, col))
            {
                cellGrid.setCellsState(row, col, States.dead);
                return;
            }
            cellGrid.setCellsState(row, col, States.alive);

        }

        public void runGame()
       { 
            updateGrid();
            Thread.Sleep(500);
        }

        public void updateGrid()
        {
            CellGrid newGrid = new CellGrid(cellGrid.GridWidth, cellGrid.GridHeight);
            for (int i = 0; i < cellGrid.GridHeight; i++)
            {
                for (int j = 0; j < cellGrid.GridWidth; j++)
                {
                    newGrid.setCellsState(i, j, nextCellState(i,j));
                }
            }

            cellGrid = new CellGrid(newGrid);
            Generation = Generation + 1;
        }

        //Definition of neighbours of cell (x,y) and boundary conditions defined here.
        private List<cellPoint> getNeighbourCellPoints(int row, int col)
        {
            List<cellPoint> neighbours = new List<cellPoint>();

            if ((col + 1) < cellGrid.GridWidth)
            {
                neighbours.Add(new cellPoint(row, col + 1));
                if((row + 1) < cellGrid.GridHeight)
                    neighbours.Add(new cellPoint(row + 1, col + 1));
                if ((row - 1) >= 0)
                neighbours.Add(new cellPoint(row - 1, col + 1));
            }

            if ((row + 1) < cellGrid.GridHeight)
                neighbours.Add(new cellPoint(row + 1, col));

            if ((row - 1) >= 0)
                neighbours.Add(new cellPoint(row - 1, col));

            if ((col - 1) >= 0)
            {
                neighbours.Add(new cellPoint(row, col - 1));
                if ((row + 1) < cellGrid.GridHeight)
                    neighbours.Add(new cellPoint(row + 1, col - 1));
                if ((row - 1) >= 0)
                    neighbours.Add(new cellPoint(row - 1, col - 1));
            }
            

            return neighbours;
        }

        private int countLiveNeighbours(int row, int col)
        {
            int counter = 0;
            
            List<cellPoint> neighbours = getNeighbourCellPoints(row,col);
            foreach (cellPoint n in neighbours)
            {
                if (cellGrid.isCellAlive(n.x, n.y))
                    counter++;
            }

            return counter;
        }

        private States nextCellState(int row, int col)
        {
            States currentState = cellGrid.getCellState(row, col);
            if (currentState == States.dead)
            {
                foreach (int n in GameRules.born)
                {
                    if (countLiveNeighbours(row, col) == n)
                        return States.alive;
                }
            }
            else //if alive
            {
                foreach (int n in GameRules.stay)
                {
                    if (countLiveNeighbours(row, col) == n)
                        return States.alive;
                }
            }
            return States.dead;
        }

        public int Generation
        {
            get { return generation; }
            set
            {
                if (generation == value) return;
                generation = value;
                NotifyPropertyChanged("Generation");
            }
        }

        public CellGrid getCellGrid()
        {
            return cellGrid;
        }
        
    }
}
