﻿//Written by Michael Luk - February 24, 2013

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Threading;

namespace Game_of_Life.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        static Game_of_Life.Model.GameModel gameModel;
        //private List<List<int>> cellGridList;
        private int x_toggle, y_toggle;
        private bool runState;
        private bool stopState;

        private int boardUpdateDelay_ms = 300;

        public int X_Toggle
        {
            get
            {
                return x_toggle;
            }
            set
            {
                if (x_toggle != value)
                {
                    x_toggle = value;
                    RaisePropertyChanged("X_Toggle");
                }
            }
        }

        public int Y_Toggle
        {
            get
            {
                return y_toggle;
            }
            set
            {
                if (y_toggle != value)
                {
                    y_toggle = value;
                    RaisePropertyChanged("Y_Toggle");
                }
            }
        }

        public bool StopState
        {
            get { return stopState; }
            set
            {
                //if (stopState != value)
                //{
                    stopState = value;
                    RaisePropertyChanged("StopState");
                //}
            }
        }

        public bool RunState
        {
            get { return runState; }
            set
            {
                //if (runState != value)
                //{
                    runState = value;
                    StopState = !runState;
                    RaisePropertyChanged("RunState");
                    
                //}
            }
        }
        
        public List<List<string>> CellGridList
        {
            get
            {
                return getCellGridList();
            }
        }

        public static int getBoardWidth()
        {
            return gameModel.getCellGrid().GridWidth;
        }

        public static int getBoardHeight()
        {
            return gameModel.getCellGrid().GridHeight;
        }

        private List<List<string>> getCellGridList()
        {
            List<List<string>> cellGrid = new List<List<string>>();

            for (int i = 0; i < gameModel.getCellGrid().GridHeight; i++)
            {
                cellGrid.Add(new List<string>());
                for (int j = 0; j < gameModel.getCellGrid().GridWidth; j++)
                {
                    if (gameModel.getCellGrid().isCellAlive(i, j))
                        cellGrid[i].Add("•");
                    else
                        //cellGrid[i].Add("◦");
                        cellGrid[i].Add(" ");
                }
            }
            
            return cellGrid;
        }

        public int Generation
        {
            get { return gameModel.Generation; }
        }
        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                gameModel = new Model.GameModel();
                X_Toggle = 0; Y_Toggle = 0;
                RunState = false;
                //NextStepCommand=new RelayCommand(new Action<object>(ShowMessage));
                CreateCommands();
            }
        }

        private void CreateCommands()
        {
            NextStepCommand = new RelayCommand<object>(_nextStepCommand);
            StopCommand = new RelayCommand<object>(_stopCommand);
            ToggleStateCommand = new RelayCommand<object>(_toggleStateCommand);
        }

        public RelayCommand<object> NextStepCommand { get; private set; }
        private void _nextStepCommand(object parm)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(continuousRun));
        }

        
        private void continuousRun(object stateinfo)
        {
            RunState = true;
            while(RunState)
            {
                gameModel.updateGrid();
                RaisePropertyChanged("CellGridList");
                Thread.Sleep(boardUpdateDelay_ms);
            }
        }

        public RelayCommand<object> StopCommand { get; private set; }
        private void _stopCommand(object parm)
        {
            stopRun(null);
        }

        private void stopRun(object stateinfo)
        {
            RunState = false;
        }

        public RelayCommand<object> ToggleStateCommand { get; private set; }
        private void _toggleStateCommand(object parm)
        {
            //Welcome = "Lol" + parm.ToString();
            toggleCellState(X_Toggle, Y_Toggle);
        }

        private void toggleCellState(int row, int col)
        {
            gameModel.toggleCellLife(row, col);

            RaisePropertyChanged("CellGridList");
        }
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}