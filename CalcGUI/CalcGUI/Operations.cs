using System;
using System.Collections.Generic;

namespace CalcGUI
{
    class Operations
    {
        // class contains all mathematical operation-related functions

        public static string getFinalAns(List<string> input)
        {
            // loop through final formatted list
            // pass numbers and operator into calculate function 

            List<string> formattedList = ProcessInput.formatInput(input);

            for (int i = 0; i < formattedList.Count; i++)
            {
                if (formattedList[i].Equals("^"))
                {
                    formattedList = calculate(formattedList, i);
                    --i;
                }
            }

            for (int i = 0; i < formattedList.Count; i++)
            {
                if (CheckInput.timesDivOrMod(formattedList[i]))
                {
                    formattedList = calculate(formattedList, i);
                    --i;
                }
            }

            for (int i = 0; i < formattedList.Count; i++)
            {
                if (CheckInput.plusOrMinus(formattedList[i]))
                {
                    formattedList = calculate(formattedList, i);
                    --i;
                }
            }
            return "" + double.Parse(formattedList[0]);
        }

        private static List<string> calculate(List<string> formattedList, int i)
        {
            // perform mathematical operation based on sign / calculate answers

            double prevNum = double.Parse(formattedList[i - 1]);
            double nextNum = double.Parse(formattedList[i + 1]);

            switch (formattedList[i])
            {
                case "^": formattedList[i] = pow(prevNum, nextNum); break;
                case "x": formattedList[i] = multiply(prevNum, nextNum); break;
                case "/": formattedList[i] = divide(prevNum, nextNum); break;
                case "%": formattedList[i] = mod(prevNum, nextNum); break;
                case "+": formattedList[i] = add(prevNum, nextNum); break;
                case "-": formattedList[i] = subtract(prevNum, nextNum); break;
            }

            formattedList[i - 1] = "";
            formattedList[i + 1] = "";
            formattedList.RemoveAll(string.IsNullOrEmpty);
            return formattedList;
        }

        private static string add(double number1, double number2)
        {
            double sum = number1 + number2;
            return "" + sum;
        }

        private static string subtract(double number1, double number2)
        {
            double difference = number1 - number2;
            return "" + difference;
        }

        private static string multiply(double number1, double number2)
        {
            double product = number1 * number2;
            return "" + product;
        }

        private static string divide(double number1, double number2)
        {
            double quotient = number1 / number2;
            return "" + quotient;
        }

        private static string mod(double number1, double number2)
        {
            double remainder = number1 % number2;
            return "" + remainder;
        }

        private static string pow(double number1, double number2)
        {
            double result = Math.Pow(number1, number2);
            return "" + result;
        }
    }
}
