//Written by Michael Luk - February 24, 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_Life.Model
{
    enum States { dead, alive };

    class Cell
    {
        private States state;

        public Cell()
        {
            state = States.dead;
        }

        public Cell(Cell copyCell)
        {
            state = copyCell.state;
        }

        public void setState(States newState)
        {
            state = newState;
        }

        public States getState()
        {
            return state;
        }

        public bool isAlive()
        {
            if (state == States.alive) return true;

            return false;
        }
    }
}
