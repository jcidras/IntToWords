namespace IntToWords.Enums
{
    using System.Collections.Generic;
    using System.Linq;

    public enum PlaceValue
    {
        TEEN = 0,
        TEN = 2,
        HUNDRED = 3,
        THOUSAND = 4,
        TEN_THOUSAND = 5,
        HUNDRED_THOUSAND = 6,
        MILLION = 7,
        TEN_MILLION = 8,
        HUNDRED_MILLION = 9
    }

    public class Place
    {
        private static Dictionary<int, Place> PlaceDictionary = new Dictionary<int, Place>();

        public readonly static Place Teen = new Place(PlaceValue.TEEN, "TEEN");
        public readonly static Place Ten = new Place(PlaceValue.TEN, "X");
        public readonly static Place Hundred = new Place(PlaceValue.HUNDRED, "Hundred");
        public readonly static Place Thousand = new Place(PlaceValue.THOUSAND, "Thousand");
        public readonly static Place TenThousand = new Place(PlaceValue.TEN_THOUSAND, "X Thousand");
        public readonly static Place HundredThousand = new Place(PlaceValue.HUNDRED_THOUSAND, "Hundred Thousand");
        public readonly static Place Million = new Place(PlaceValue.MILLION, "Million");
        public readonly static Place TenMillion = new Place(PlaceValue.TEN_MILLION, "X Million");
        public readonly static Place HundredMillion = new Place(PlaceValue.HUNDRED_MILLION, "Hundred Million");

        private string Value { get; set; }

        public Place()
        {
        }

        public Place(PlaceValue value, string name)
        {
            Value = name;
            PlaceDictionary.Add((int)value, new Place { Value = name });
        }

        public static string Compare(int number, int count)
        {
            string value = string.Empty;
            if (count == 2)
            {
                if (number == 1)
                {
                    value = Teen.ToString();
                }
                else
                {
                    value = Ten.ToString();
                }                
            }            
            else
            {
                var place = PlaceDictionary.Where(k => k.Key == count).FirstOrDefault();
                if (place.Value != null)
                {
                    value = place.Value.ToString();
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
