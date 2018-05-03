using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**************************************************************************
// Rule Class
// Tells the program what the rules are and whether to move left or right.
//**************************************************************************
namespace CPowersTuringMachine
{
    class Rule
    {
        public int nextMove; // Where to move next.
        public string readNext; // what to read next.
        public string writeNext;    // what to write next.
        public int currentState;    // Current state.
        public int nextState;   // Next state.

        public Rule(int myCState, string myNextRead, int myNextState, string myNextWrite, string myNext)
        {
            // initilize object elements
            currentState = myCState;
            nextState = myNextState;
            readNext = myNextRead;
            writeNext = myNextWrite;
         
            if(myNext == "r")   // Move right 1 space.
            {
                nextMove = 1;
            }
            else // else move left 1 space.
            {
                nextMove = -1;
            }
        }
    }
}
