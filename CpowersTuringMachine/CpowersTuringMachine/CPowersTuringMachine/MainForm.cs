//***************************************************************************************************
// Name: Colin Powers
// Instructor: Professor Woodcock
// Assingment: Turing machine
// Description: This program is an example of a Turing Machine. It will have a set of rules
//              and ask the user to enter a word. The program then checks the word against the rules.
//              The program then moves right or left depending on the rules.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPowersTuringMachine
{
    public partial class MainForm : Form
    {
      
        List<Rule> rulesList;   // This list holds the rules.
        int startingState;  // Int for which state.
        int acceptingState; // Int for accepting state
        public MainForm()
        {
            InitializeComponent();
            grpWord.Enabled = false;
            startingState = 1;
            acceptingState = 2;
        }

        // Button click to close the form
        private void btnExit_Click(object sender, EventArgs e)
        {
            // close the form.
            this.Close();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            try
            {
                // initilize the list
                rulesList = new List<Rule>();
                // create a stream
                Stream myStream;
                // File Loader
                OpenFileDialog myDialog = new OpenFileDialog();
                myDialog.ShowDialog();
                myStream = myDialog.OpenFile();

                // create a delem to parse the data.
                char[] delim = { 'G', '(', ')', ',', '=' };
                // pass stream to stream reader
                StreamReader fin = new StreamReader(myStream);
                // split the line
                string myStr;
                string[] set;
                // will hold data in the loop ahead
                int state, nextState;
                string read, write, move;
                // loop that will parse the file
                while (!fin.EndOfStream)
                {
                    // pass the line to the string
                    myStr = fin.ReadLine();
                    // split my string
                    set = myStr.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                    // convert values to correct format
                    state = int.Parse(set[0]);
                    read = set[1].ToLower();
                    nextState = int.Parse(set[2]);
                    write = set[3].ToLower();
                    move = set[4].ToLower();
                    // create the rule
                    Rule newRule = new Rule(state, read, nextState, write, move);
                    // add the rule
                    rulesList.Add(newRule);
                }
                // Show message
                MessageBox.Show("Rules Successfully Loaded!");
                // enable word input
                grpWord.Enabled = true;
            } // catch errors
            catch (Exception)
            {
                MessageBox.Show("Operation Cancelled or Error Occured!");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            // List to hold infinitly long word
            List<string> word = new List<string>();

            // Hold the word entered
            string str = txtInput.Text.ToLower();
            word.Add(" ");
            // iterate through the word
            foreach (char ch in str)
            {
                word.Add(ch.ToString());
            }
            // index will go right or left
            int index = 1;
            word.Add(" ");
            // set current state to start state.
            int curState = startingState;

            // as long as there are more characters, keep looping
            while(index < word.Count)
            {
                
                // rule checking
                int check = index;
                // cycle until rule is found
                for(int ix = 0; ix < rulesList.Count; ix++)
                {
                    // check if current state is in the rule
                    if(curState == rulesList.ElementAt(ix).currentState && word.ElementAt(index) == rulesList.ElementAt(ix).readNext)
                    {
                        // current state becomes next state
                        curState = rulesList.ElementAt(ix).nextState;
                        // we write the new letter
                        word[index] = rulesList.ElementAt(ix).writeNext;
                        // check the index to go left or right
                        index += rulesList.ElementAt(ix).nextMove;
                        break;
                    }// end if
                }// end for
                // break if not a word
                if(check == index)
                {
                    break;
                }
            }// end while

            // if current state is accepting
            if(curState == acceptingState)
            {
                lblOutput.Text = " Congratz! The word you entered is valid";
            }
            // otherwise
            else
            {
                lblOutput.Text = "Sorry, that word is not valid, please try another.";
            }
        }// end method
    }
}
