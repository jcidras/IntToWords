// Written by Jason Cidras
// Date: 09/08/2015
namespace IntToWords
{
    #region Using Statements
    using System;
    using System.Linq;    
    #endregion

    public class Program
    {
        #region Constant Variables

        #region Single Digits
        private static string ONE = "One";
        private static string TWO = "Two";
        private static string THREE = "Three";
        private static string FOUR = "Four";
        private static string FIVE = "Five";
        private static string SIX = "Six";
        private static string SEVEN = "Seven";
        private static string EIGHT = "Eight";
        private static string NINE = "Nine";
        #endregion

        #region Teens
        private static string ELEVEN = "Eleven";
        private static string TWELVE = "Twelve";
        private static string THIRTEEN = "Thirteen";
        private static string FOURTEEN = "Fourteen";
        private static string FIFTEEN = "Fifteen";
        private static string SIXTEEN = "Sixteen";
        private static string SEVENTEEN = "Seventeen";
        private static string EIGHTEEN = "Eighteen";
        private static string NINETEEN = "Nineteen";
        #endregion

        #region Tens
        private static string TEN = "Ten";
        private static string TWENTY = "Twenty";
        private static string THIRTY = "Thirty";
        private static string FOURTY = "Fourty";
        private static string FIFTY = "Fifty";
        private static string SIXTY = "Sixty";
        private static string SEVENTY = "Seventy";
        private static string EIGHTY = "Eighty";
        private static string NINTY = "Ninty";
        #endregion

        #region Hundred+
        private static string TEENS = "TEEN"; // Placeholder
        private static string TENTH = "X";
        private static string HUNDRED = "Hundred";
        private static string THOUSAND = "Thousand";
        private static string TEN_THOUSAND = "X Thousand";
        private static string HUNDRED_THOUSAND = "Hundred Thousand";
        private static string MILLION = "Million";
        private static string TEN_MILLION = "X Million";
        private static string HUNDRED_MILLION = "Hundred Million";
        #endregion

        private static string SPACE = " ";

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number:");
            int tempNum = -1;
            string numberInput = Console.ReadLine();
            if (Int32.TryParse(numberInput, out tempNum))
            {
                Console.WriteLine(ConvertToEnglishWord(tempNum));
            }
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
                int temp = numArray[position]; // Gets the current number in the iteration                                   
                                
                if (numArray[position] != numArray.Last())
                {                    
                    place = GetPosition(temp, count);
                    if (place.Contains("X") || place.Contains("TEEN"))
                    {
                        string placeHolder = "";
                        if (place == TENTH)
                        {
                            placeHolder = GetTens(temp, numArray[position + 1]);
                        }
                        else if (place == TEN_MILLION || place == TEN_THOUSAND)
                        {                            
                            int nextNumber = numArray[position + 1];
                            placeHolder = GetTens(temp, nextNumber);                                                        
                            if (nextNumber != 0 && temp != 1) // twenty-one, twenty-two, etc
                            {
                                if (temp != 0)
                                {
                                    placeHolder += "-";
                                }
                                placeHolder += GetSingleDigit(nextNumber);
                            }
                            count--; position++;
                        }                        
                        else if (place == TEENS)
                        {
                            placeHolder = GetTeens(numArray[position + 1]);
                            return (convertedNumberToWord += SPACE + place.Replace("TEEN", placeHolder)).Trim();
                        }
                        convertedNumberToWord += SPACE + place.Replace("X", placeHolder);
                    }
                    else if (place == HUNDRED_MILLION || place == HUNDRED_THOUSAND)
                    {
                        int nextNumber = numArray[position + 1]; // Gets the next number
                        int nextTwoNumbers = numArray[position + 2]; // Gets the number after next
                        if (nextNumber != 0 && nextTwoNumbers != 0) // Checks if they are not 0, 102,000 = one hundred and 2 thousand..
                        {
                            if (place == HUNDRED_MILLION) 
                            { place = place.Replace("Million", "And"); }
                            else 
                            { place = place.Replace("Thousand", "And"); }                            
                        }
                        convertedNumberToWord += SPACE + GetSingleDigit(temp) + SPACE + place;
                    }
                    else
                    {
                        convertedNumberToWord += SPACE + GetSingleDigit(temp) + SPACE + place;
                    }
                }
                else
                {
                    convertedNumberToWord += SPACE + GetSingleDigit(temp);
                }                
            }
            return convertedNumberToWord.Trim();
        }

        private static string GetPosition(int number, int count)
        {   
            switch (count)
            {   
                case 2:
                    if (number == 1) // eleven, twelve, etc
                    {
                        return TEENS;
                    }
                    else // twenty, thirty, etc
                    {
                        return TENTH;
                    }                    
                case 3:
                    return HUNDRED;
                case 4:
                    return THOUSAND;
                case 5:
                    return TEN_THOUSAND;
                case 6:
                    return HUNDRED_THOUSAND;
                case 7:
                    return MILLION;
                case 8:
                    return TEN_MILLION;
                case 9:
                    return HUNDRED_MILLION;
            }
            return "";
        }        

        private static string GetTens(int number, int nextNumber)
        {
            switch (number)
            {                
                case 1:
                    if (nextNumber == 0)
                    {
                        return TEN;
                    }
                    else 
                    {
                        return GetTeens(nextNumber);
                    }
                case 2:
                    return TWENTY;
                case 3:
                    return THIRTY;
                case 4:
                    return FOURTY;
                case 5:
                    return FIFTY;
                case 6:
                    return SIXTY;
                case 7:
                    return SEVENTY;
                case 8:
                    return EIGHTY;
                case 9:
                    return NINTY;
                default:
                    return "";
            }    
        }

        private static string GetTeens(int number)
        {
            switch (number)
            {                
                case 1:
                    return ELEVEN;
                case 2:
                    return TWELVE;
                case 3:
                    return THIRTEEN;
                case 4:
                    return FOURTEEN;
                case 5:
                    return FIFTEEN;
                case 6:
                    return SIXTEEN;
                case 7:
                    return SEVENTEEN;
                case 8:
                    return EIGHTEEN;
                case 9:
                    return NINETEEN;
                default:
                    return "";
            }       
        }

        private static string GetSingleDigit(int number)
        {   
            switch (number)
            {                 
                case 1:
                    return ONE;
                case 2:
                    return TWO;
                case 3:
                    return THREE;
                case 4:
                    return FOUR;
                case 5:
                    return FIVE;
                case 6:
                    return SIX;
                case 7:
                    return SEVEN;
                case 8:
                    return EIGHT;
                case 9:
                    return NINE;
                default :
                    return "";                    
            }            
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
