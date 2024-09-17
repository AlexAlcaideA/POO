using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class Card
    {
        public enum ECardType
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs,
            Size
        }

        private int num;
        private ECardType cardType;

        public int Num { get { return num; } }
        public ECardType CardType { get { return cardType; } }

        public Card() 
        { 
            num = 0;
            cardType = ECardType.Spades;
        }

        public Card(int _num, ECardType _cardType)
        {
            num = _num;
            cardType = _cardType;
        }
    }
}
