namespace IntToWords.Enums
{
    using System.Collections.Generic;
    using System.Linq;

    public enum DoubleDigitValue
    { 
        Ten = 1,
        Twenty = 2,
        Thirty = 3,
        Fourty = 4,
        Fifty = 5,
        Sixty = 6,
        Seventy = 7,
        Eighty = 8,
        Ninty = 9
    }

    class DoubleDigit
    {
        private static Dictionary<int, DoubleDigit> DoubeDigitDictionary = new Dictionary<int, DoubleDigit>();

        private readonly static DoubleDigit Ten = new DoubleDigit(DoubleDigitValue.Ten, "Ten");
        private readonly static DoubleDigit Twenty = new DoubleDigit(DoubleDigitValue.Twenty, "Twenty");
        private readonly static DoubleDigit Thirty = new DoubleDigit(DoubleDigitValue.Thirty, "Thirty");
        private readonly static DoubleDigit Fourty = new DoubleDigit(DoubleDigitValue.Fourty, "Fourty");
        private readonly static DoubleDigit Fifty = new DoubleDigit(DoubleDigitValue.Fifty, "Fifty");
        private readonly static DoubleDigit Sixty = new DoubleDigit(DoubleDigitValue.Sixty, "Sixty");
        private readonly static DoubleDigit Seventy = new DoubleDigit(DoubleDigitValue.Seventy, "Seventy");
        private readonly static DoubleDigit Eighty = new DoubleDigit(DoubleDigitValue.Eighty, "Eighty");
        private readonly static DoubleDigit Ninty = new DoubleDigit(DoubleDigitValue.Ninty, "Ninty");

        private string Value { get; set; }

        public DoubleDigit()
        {
        }

        public DoubleDigit(DoubleDigitValue value, string name)
        {
            this.Value = name;            
            DoubeDigitDictionary.Add((int)value, new DoubleDigit { Value = name });
        }

        public static string Compare(int number, int nextNumber)
        {
            string value = string.Empty;
            if (number == 1)
            {                
                if (nextNumber == 0)
                { // It's ten
                    value = Ten.ToString();
                }
                else
                { // Or it's eleven, twelve... etc
                    value = Teen.Compare(nextNumber);
                }
            }
            else
            {
                var doubleDigit = DoubeDigitDictionary.Where(k => k.Key == number).FirstOrDefault();
                if (doubleDigit.Value != null)
                {
                    value = doubleDigit.Value.ToString();
                }
                else
                {
                    value = "And"; // It's zero, so it's going to be AND something
                }
            }
            return value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
