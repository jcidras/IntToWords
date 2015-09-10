namespace IntToWords.Enums
{
    using System.Collections.Generic;
    using System.Linq;

    public enum SingleDigitValue
    { 
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9
    }

    public class SingleDigit
    {
        private static Dictionary<int, SingleDigit> SingleDigitDictionary = new Dictionary<int, SingleDigit>();

        private readonly static SingleDigit One = new SingleDigit(SingleDigitValue.One, "One");
        private readonly static SingleDigit Two = new SingleDigit(SingleDigitValue.Two, "Two");
        private readonly static SingleDigit Three = new SingleDigit(SingleDigitValue.Three, "Three");
        private readonly static SingleDigit Four = new SingleDigit(SingleDigitValue.Four, "Four");
        private readonly static SingleDigit Five = new SingleDigit(SingleDigitValue.Five, "Five");
        private readonly static SingleDigit Six = new SingleDigit(SingleDigitValue.Six, "Six");
        private readonly static SingleDigit Seven = new SingleDigit(SingleDigitValue.Seven, "Seven");
        private readonly static SingleDigit Eight = new SingleDigit(SingleDigitValue.Eight, "Eight");
        private readonly static SingleDigit Nine = new SingleDigit(SingleDigitValue.Nine, "Nine");

        private string Value { get; set; }
        
        public SingleDigit()
        {
        }

        public SingleDigit(SingleDigitValue value, string name)
        {
            SingleDigit singleDigit = new SingleDigit { Value = name };
            SingleDigitDictionary.Add((int)value, singleDigit);
        }

        public static string Compare(int number)
        {
            string value = string.Empty;
            var singleDigit = SingleDigitDictionary.Where(k => k.Key == number).FirstOrDefault();
            if (singleDigit.Value != null)
            {
                value = singleDigit.Value.ToString();
            }
            return value;                
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
