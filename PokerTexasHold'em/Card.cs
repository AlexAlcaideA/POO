using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTexasHold_em
{
    internal class Card
    {
        public enum eCardType
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs,
            Size
        }

        private int num;
        private eCardType cardType;

        public int Num { get; }
        public eCardType CardType { get; }

        public Card() 
        { 
            num = 0;
            cardType = eCardType.Spades;
        }

        public Card(int _num, eCardType _cardType)
        {
            num = _num;
            cardType = _cardType;
        }

        public override string ToString()
        {
            string text = $@"Card number {num} of {cardType}";
            return text;
        }

    }
}
