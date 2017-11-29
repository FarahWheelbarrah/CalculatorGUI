using System.Collections.Generic;
using System.Linq;

namespace CalcGUI
{
    class ProcessInput
    {
        // class contains all functions related to transforming string input into list form

        public static List<string> splitInput(string input)
        {
            // split string input into list of subStrings

            List<string> stringList = new List<string>();
            string stringForList = "";

            for (int i = 0; i < input.Length; i++)
            {
                string subString = "" + input[i];
                if (CheckInput.isOperator(subString) && i != 0)
                {
                    stringList.Add(stringForList);
                    stringForList = "";
                    stringList.Add(subString);
                }
                else
                    stringForList += subString;
            }
            stringList.Add(stringForList);
            stringList.RemoveAll(string.IsNullOrEmpty);
            return stringList;
        }

        public static List<string> toNumList(string input)
        {
            // convert numbers in string input into list form 

            string[] numArray = input.Split(new char[] { '+', '-', 'x', '/', '%', '^' });
            List<string> numList = numArray.ToList();
            numList.RemoveAll(string.IsNullOrEmpty);
            return numList;
        }

        public static List<string> formatInput(List<string> inputList)
        {
            // return completely formatted / processed input (in list form) 

            return concatSignToNum(reduceSigns(inputList));
        }

        private static List<string> reduceSigns(List<string> inputList)
        {
            // remove any occurences of "+" or "-" followed by "+" or "-" in input
            // replace with single "+" or "-"

            while (CheckInput.notReduced(inputList))
            {
                for (int i = 0; i < inputList.Count; i++)
                {
                    if (CheckInput.plusOrMinus(inputList[i]) && CheckInput.plusOrMinus(inputList[i + 1]))
                    {
                        inputList[i] = changeSign(inputList[i], inputList[i + 1]);
                        inputList[i + 1] = "";
                        inputList.RemoveAll(string.IsNullOrEmpty);
                    }
                }
            }
            if (CheckInput.plusOrMinus(inputList[0]))
            {
                inputList[1] = inputList[0] + inputList[1];
                inputList[0] = "";
                inputList.RemoveAll(string.IsNullOrEmpty);
            }
            return inputList;
        }

        private static string changeSign(string sign1, string sign2)
        {
            // return replacement sign ("+" or "-") based on parameters

            if (sign1.Equals("+") && sign2.Equals("+"))
                return "+";
            else if (sign1.Equals("+") && sign2.Equals("-"))
                return "-";
            else if (sign1.Equals("-") && sign2.Equals("-"))
                return "+";
            else
                return "-";
        }

        private static List<string> concatSignToNum(List<string> inputList)
        {
            // concatenate any "+" or "-" preceded by "x", "/" or "%", to the following double

            for (int i = 0; i < inputList.Count; i++)
                if (CheckInput.plusOrMinus(inputList[i]) && CheckInput.timesDivModOrPow(inputList[i - 1]))
                {
                    inputList[i] = inputList[i] + inputList[i + 1];
                    inputList[i + 1] = "";
                }
            inputList.RemoveAll(string.IsNullOrEmpty);

            return inputList;
        }
    }
}
