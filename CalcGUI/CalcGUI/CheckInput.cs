using System.Collections.Generic;

namespace CalcGUI
{
    class CheckInput
    {
        // class contains all methods for validation of input

        public static bool isInvalid(string input, List<string> inputList)
        {
            // return true if input has any invalidity

            if (illegalStart(input) || illegalEnd(input) || hasNonDouble(input) || illegalOrder(inputList))
                return true;
            return false;
        }

        public static bool isOperator(string subString)
        {
            // check whether subString is operator

            string[] operators = { "+", "-", "x", "/", "%", "^" };

            foreach (string sign in operators)
                if (subString.Equals(sign))
                    return true;
            return false;
        }

        public static bool plusOrMinus(string sign)
        {
            return sign.Equals("+") || sign.Equals("-");
        }

        public static bool timesDivOrMod(string sign)
        {
            return sign.Equals("x") || sign.Equals("/") || sign.Equals("%");
        }

        public static bool timesDivModOrPow(string sign)
        {
            return sign.Equals("x") || sign.Equals("/") || sign.Equals("%") || sign.Equals("^");
        }

        public static bool notReduced(List<string> inputList)
        {
            // check for any occurences of "+" or "-" followed by "+" or "-"

            for (int i = 0; i < inputList.Count; i++)
                if (plusOrMinus(inputList[i]) && plusOrMinus(inputList[i + 1]))
                    return true;
            return false;
        }

        private static bool illegalOrder(List<string> inputList)
        {
            // return true if there are any illegal sequences of operators

            return illegal1(inputList) || illegal2(inputList);
        }

        private static bool illegal1(List<string> inputList)
        {
            // check for any occurences of "+" or "-" followed by "x", "/", "%" or "^"

            for (int i = 0; i < inputList.Count; i++)
                if (plusOrMinus(inputList[i]) && timesDivModOrPow(inputList[i + 1]))
                    return true;
            return false;
        }

        private static bool illegal2(List<string> inputList)
        {
            // check for any occurences of "x", "/", "%" or "^" followed by "x", "/", "%" or "^"

            for (int i = 0; i < inputList.Count; i++)
                if (timesDivModOrPow(inputList[i]) && timesDivModOrPow(inputList[i + 1]))
                    return true;
            return false;
        }

        private static bool illegalStart(string input)
        {
            // check whether input starts with illegal operator

            string[] startSigns = { "/", "x", "%", "^" };

            foreach (string sign in startSigns)
                if (input.StartsWith(sign))
                    return true;
            return false;
        }

        private static bool illegalEnd(string input)
        {
            // check whether input ends with illegal operator

            string[] endSigns = { "+", "-", "/", "x", "%", "^" };

            foreach (string sign in endSigns)
                if (input.EndsWith(sign))
                    return true;
            return false;
        }

        private static bool hasNonDouble(string input)
        {
            // check whether there is a non-double value in input

            List<string> numList = ProcessInput.toNumList(input);

            foreach (string value in numList)
                if (!(isDouble(value)))
                    return true;
            return false;
        }

        public static bool divZero(List<string> inputList)
        {
            // check whether there is a "division by 0" expression in input

            List<string> formattedList = ProcessInput.formatInput(inputList);

            for (int i = 0; i < formattedList.Count; i++)
                if ((formattedList[i].Equals("/") || formattedList[i].Equals("%"))
                    && double.Parse(formattedList[i + 1]) == 0)
                    return true;
            return false;
        }

        public static bool isDouble(string subString)
        {
            // check whether subString is a double value

            double number;
            return double.TryParse(subString, out number);
        }
    }
}
