using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
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

        public int Num { get { return num; } }
        public eCardType CardType { get { return cardType; } }

        public Card() 
        { 

        }

        public Card(int num, eCardType _cardType)
        {
            this.num = num;
            cardType = _cardType;
        }
    }
}
