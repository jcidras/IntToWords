// Written by Jason Cidras
// Date: 09/08/2015
// Integer to Words 
namespace IntToWords
{
    #region Using Statements
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Enums;
    #endregion

    #region Exceptions
    public class NoParameterException : Exception
    {
        public NoParameterException() : base("No incoming parameters. Aborting conversion.")
        {

        }
    }

    public class NonPositiveIntegerException : Exception
    {
        public NonPositiveIntegerException() : base("Invalid conversion operation, integer must be a non-negative integer. Aborting conversion.")
        {

        }        
    }
    #endregion

    public class ConvertIntToWords
    {
        #region Constant Variables 

        private const string SPACE = " ";
        private const string TEEN = "TEEN";
        private const string TEN = "X";
        private const string TEN_THOUSAND = "X Thousand";
        private const string TEN_MILLION = "X Million";
        #endregion

        /// <summary>
        /// ConvertIntoToWords brings in multiple non-negative integers as strings and returns their English counterparts.
        /// </summary>
        /// <param name="args">Array of <c>string</c> as non-negative integers.</param>
        /// <exception cref="System.Exception">NoParameterException if there are not any items in argument.</exception>
        /// <exception cref="System.Exception">NonPositiveIntegerException if any strings in arugment cannot be parsed or is a negative integer.</exception>
        /// <returns>Array of <c>string</c> of converted integers as English words</returns>
        public static string[] ConvertToWords(string[] args)
        {
            int tempNum = -1;
            List<string> convertedIntToWords = new List<string>();
            if (args.Count() == 0)
            {
                throw new NoParameterException();
            }
            else
            {           
                foreach (string num in args)
                {
                    if (Int32.TryParse(num, out tempNum) && tempNum > 0)
                    {
                        convertedIntToWords.Add(ConvertToEnglishWord(tempNum));
                    }
                    else
                    {
                        throw new NonPositiveIntegerException();
                    }                    
                }                
            }
            return convertedIntToWords.ToArray();
        }

        private static string ConvertToEnglishWord(int input)
        {
            string numToString = input.ToString();
            int[] numArray = ToIntArray(numToString) ;
            string convertedNumberToWord = "";
            int count = numArray.Count();
            string place = "";

            for (int position = 0; position < numArray.Count(); position++, count--)
            {
                int num = numArray[position]; // Gets the current number in the iteration
                if (position != numArray.Count())
                {                    
                    place = GetPosition(num, count);
                    if (place.Contains(TEN) || place.Contains(TEEN)) // If place contains any placeholders
                    {
                        string numToReplacePlaceholder = "";
                        switch (place)
                        {
                            case TEEN:                                
                            case TEN:
                                numToReplacePlaceholder = ReplacePlaceholder(num, position, numArray, place);
                                if (place == TEEN) 
                                {
                                    return (convertedNumberToWord += SPACE + place.Replace(TEEN, numToReplacePlaceholder)).Trim(); // We're done here
                                }   
                                break;                            
                            case TEN_THOUSAND:                                
                            case TEN_MILLION:
                                numToReplacePlaceholder = ReplacePlaceholder(num, ref position, ref count, numArray);
                                break;
                            default:
                                break;
                        }                        
                        convertedNumberToWord += SPACE + place.Replace(TEN, numToReplacePlaceholder);
                    }
                    else if (place == Place.Hundred.ToString())
                    { // Special occasion such as 1011
                        if (num == 0)
                        { 
                            convertedNumberToWord += SPACE + place.Replace("Hundred", "And"); 
                        }
                        else
                        { 
                            convertedNumberToWord += SPACE + GetSingleDigit(num) + SPACE + place; 
                        }
                    }
                    else if (place == Place.HundredMillion.ToString() || place == Place.HundredThousand.ToString())
                    {
                        int nextNumber = numArray[position + 1]; // Gets the next number
                        int nextTwoNumbers = numArray[position + 2]; // Gets the number after next
                        if (nextNumber != 0 && nextTwoNumbers != 0) // Checks if they are not 0, 102,000 = one hundred and 2 thousand..
                        {
                            if (place == Place.HundredMillion.ToString())
                            { 
                                place = place.Replace("Million", "And"); 
                            }
                            else
                            { 
                                place = place.Replace("Thousand", "And"); 
                            }
                        }
                        convertedNumberToWord += SPACE + GetSingleDigit(num) + SPACE + place;
                    }
                    else
                    {
                        convertedNumberToWord += SPACE + GetSingleDigit(num) + SPACE + place;
                    }
                }
                else
                {
                    convertedNumberToWord += SPACE + GetSingleDigit(num);
                }                
            }
            return convertedNumberToWord.Trim();
        }

        #region Helpers
        private static string GetPosition(int number, int count)
        {
            return Place.Compare(number, count);
        }
        
        private static string GetSingleDigit(int number)
        {
            return SingleDigit.Compare(number); 
        }

        private static string GetTeens(int number)
        {
            return Teen.Compare(number);
        }

        private static string GetTens(int number, int nextNumber)
        {
            return DoubleDigit.Compare(number, nextNumber);
        }

        private static string ReplacePlaceholder(int num, int position, int[] numArray, string place)
        {
            if (place == Place.Ten.ToString())
            {
                return GetTens(num, numArray[position + 1]);
            }
            else
            { 
                return GetTeens(numArray[position + 1]);
            }
        }

        private static string ReplacePlaceholder(int num, ref int position, ref int count, int[] numArray)
        {
            string numToReplacePlaceholder = "";
            int nextNumber = numArray[position + 1];
            numToReplacePlaceholder = GetTens(num, nextNumber);
            if (nextNumber != 0 && num != 1) // twenty-one, twenty-two, etc
            {
                if (num != 0)
                {
                    numToReplacePlaceholder += "-";
                }
                numToReplacePlaceholder += GetSingleDigit(nextNumber);
            }
            count--; position++;
            return numToReplacePlaceholder;
        }
        // Returns Int array from string
        private static int[] ToIntArray(string str)
        {
            int[] arr = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                arr[i] = Convert.ToInt32(str[i].ToString());
            }
            return arr;
        }
        #endregion
    }
}
