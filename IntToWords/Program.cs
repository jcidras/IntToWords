// Written by Jason Cidras
// Date: 09/08/2015
// Integer to Words 
namespace IntToWords
{
    #region Using Statements
    using System;
    using System.Linq;
    using Enums;
    #endregion

    public class Program
    {
        #region Constant Variables 

        private static string SPACE = " ";

        #endregion

        static void Main(string[] args)
        {
            int tempNum = -1;

            if (args.Count() == 0)
            {
                Console.WriteLine("Enter a number:");
                string num = Console.ReadLine();
                if (Int32.TryParse(num, out tempNum))
                {
                    Console.WriteLine(ConvertToEnglishWord(tempNum));
                }
                else
                {
                    Console.WriteLine("Not a number.");
                }
            }
            else
            { // Using as an api
                foreach (string num in args)
                {
                    if (Int32.TryParse(num, out tempNum))
                    {
                        Console.WriteLine(ConvertToEnglishWord(tempNum));
                    }
                    else
                    {
                        Console.WriteLine("Not a number.");
                    }
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static string ConvertToEnglishWord(int number)
        {
            string numToString = number.ToString();
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
                    if (place.Contains("X") || place.Contains("TEEN")) // If place contains any placeholders
                    {
                        string numToReplacePlaceholder = "";
                        if (place == Place.Ten.ToString())
                        { 
                            numToReplacePlaceholder = GetTens(num, numArray[position + 1]); 
                        }
                        else if (place == Place.TenMillion.ToString() || place == Place.TenThousand.ToString())
                        {                            
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
                        }                        
                        else if (place == Place.Teen.ToString())
                        {
                            numToReplacePlaceholder = GetTeens(numArray[position + 1]);
                            return (convertedNumberToWord += SPACE + place.Replace("TEEN", numToReplacePlaceholder)).Trim(); // We're done here
                        }
                        convertedNumberToWord += SPACE + place.Replace("X", numToReplacePlaceholder);
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
    }
}
