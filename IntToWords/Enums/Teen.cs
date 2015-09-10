namespace IntToWords.Enums
{
    using System.Collections.Generic;
    using System.Linq;

    public enum TeensValue
    {
        ELEVEN = 1,
        TWELVE = 2,
        THIRTEEN = 3,
        FOURTEEN = 4,
        FIFTEEN = 5,
        SIXTEEN = 6,
        SEVENTEEN = 7,
        EIGHTEEN = 8,
        NINETEEN = 9
    }

    public class Teen
    {
        private static Dictionary<int, Teen> TeensDictionary = new Dictionary<int, Teen>();
        
        private readonly static Teen Eleven = new Teen(TeensValue.ELEVEN, "Eleven");
        private readonly static Teen Twelve = new Teen(TeensValue.TWELVE, "Twelve");
        private readonly static Teen Thirteen = new Teen(TeensValue.THIRTEEN, "Thirteen");
        private readonly static Teen Fourteen = new Teen(TeensValue.FOURTEEN, "Fourteen");
        private readonly static Teen Fifteen = new Teen(TeensValue.FIFTEEN, "Fifteen");
        private readonly static Teen Sixteen = new Teen(TeensValue.SIXTEEN, "Sixteen");
        private readonly static Teen Seventeen = new Teen(TeensValue.SEVENTEEN, "Seventeen");
        private readonly static Teen Eighteen = new Teen(TeensValue.EIGHTEEN, "Eighteen");
        private readonly static Teen Nineteen = new Teen(TeensValue.NINETEEN, "Nineteen");

        private string Value { get; set; }        

        public Teen()
        { 
        }

        public Teen(TeensValue value, string name)
        {
            Teen teen = new Teen { Value = name };
            TeensDictionary.Add((int)value, teen);
        }

        public static string Compare(int number)
        {
            return TeensDictionary.Where(t => t.Key == number).First().Value.ToString();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
