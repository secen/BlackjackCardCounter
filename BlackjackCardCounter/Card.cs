using System.Collections.Generic;
namespace BlackjackCardCounter
{
    public class Card
    {
       Dictionary<int, string> dictionary = new Dictionary<int, string>() { { 1, "Clubs"  }, {2, "Hearts"}, { 3, "Diamonds" }, { 4, "Spades" }  };
        public int Value { get; set; }
        public int Type { get; set; }
        public string Text { get { return ToString(); } }

        public override string ToString()
        {
            if (Value == 1)
                return "Ace of " + dictionary[Type];
            if (Value >10)
            {
                switch(Value)
                {
                    case 11:
                        return "Jack of" + dictionary[Type];
                        break;
                    case 12:
                        return "Queen of " + dictionary[Type];
                        break;
                    case 13:
                        return "King of " + dictionary[Type];
                        break;
                }
            }    
            return Value.ToString() + " of " + dictionary[Type];
        }
    }
}