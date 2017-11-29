using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalcGUI
{
    public partial class Form1 : Form
    {
        // a simple calculator for floating point values

        // boolean variables determine current state of textbox content
        // set them to initial state
        bool isErrMsg = false;
        bool isAns = false;

        public Form1()
        {
            InitializeComponent();
            ioBox.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sevenBtn_Click(object sender, EventArgs e)
        {
            printInput("7");
        }

        private void eightBtn_Click(object sender, EventArgs e)
        {
            printInput("8");
        }

        private void nineBtn_Click(object sender, EventArgs e)
        {
            printInput("9");
        }

        private void fourBtn_Click(object sender, EventArgs e)
        {
            printInput("4");
        }

        private void fiveBtn_Click(object sender, EventArgs e)
        {
            printInput("5");
        }

        private void sixBtn_Click(object sender, EventArgs e)
        {
            printInput("6");
        }

        private void oneBtn_Click(object sender, EventArgs e)
        {
            printInput("1");
        }

        private void twoBtn_Click(object sender, EventArgs e)
        {
            printInput("2");
        }

        private void threeBtn_Click(object sender, EventArgs e)
        {
            printInput("3");
        }

        private void zeroBtn_Click(object sender, EventArgs e)
        {
            printInput("0");
        }

        private void pointBtn_Click(object sender, EventArgs e)
        {
            printInput(".");
        }

        private void ceBtn_Click(object sender, EventArgs e)
        {
            // delete final character of textbox content based on its current state 

            string fullString = ioBox.Text;

            if (!isErrMsg && !fullString.Equals("NaN") && !fullString.Equals("Infinity"))
            {
                ioBox.Text = fullString.Remove(fullString.Length - 1);
                isAns = false;
            }

            if (ioBox.Text.Equals(""))
            {
                ioBox.Text = "0";
            } 
        }

        private void clBtn_Click(object sender, EventArgs e)
        {
            // reset textbox content to "0"
            // reset state of textbox content to initial state

            ioBox.Text = "0";
            isAns = false;
            isErrMsg = false;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            printInput("+");
        }

        private void multiplyBtn_Click(object sender, EventArgs e)
        {
            printInput("x");
        }

        private void subtractBtn_Click(object sender, EventArgs e)
        {
            printInput("-");
        }

        private void divideBtn_Click(object sender, EventArgs e)
        {
            printInput("/");
        }

        private void modBtn_Click(object sender, EventArgs e)
        {
            printInput("%");
        }

        private void powerBtn_Click(object sender, EventArgs e)
        {
            printInput("^");
        }

        private void equalsBtn_Click(object sender, EventArgs e)
        {
            // print result in textbox based on current input
            // change state of textbox content based on result of calculation

            List<string> inputList = ProcessInput.splitInput(ioBox.Text);

            if (CheckInput.isInvalid(ioBox.Text, inputList))
            {
                ioBox.Text = "Invalid double";
                isErrMsg = true;
            }
            else if (CheckInput.divZero(inputList))
            {
                ioBox.Text = "Division by zero";
                isErrMsg = true;
            }
            else
            {
                ioBox.Text = Operations.getFinalAns(inputList);
                isAns = true;
            }
        }

        private void ioBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void printInput(string input)
        {
            // replace or append to textbox content based on its current state
            // reset state of textbox content to initial state

            if (isErrMsg || (isAns && (CheckInput.isDouble(input) 
                || (input.Equals(".") && !ioBox.Text.Equals("0")))) 
                || (ioBox.Text.Equals("0") && CheckInput.isDouble(input)))
                ioBox.Text = input;
            else
                ioBox.Text += input;

            isErrMsg = false;
            isAns = false;
        }
    }
}
