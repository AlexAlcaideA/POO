using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class Deck
    {
        private List<Card> cards;

        public Deck()
        {  
            cards = new List<Card>();
        }

        public Deck(List<Card> _cards)
        {
            cards = _cards;
        }

        public void InitializeDeck()
        {
            foreach(Card.eCardType cardType in Enum.GetValues(typeof(Card.eCardType)))
            {
                for (int i = 1; i <= 13; i++)
                {
                    cards.Add(new Card(i, cardType));
                }
            }
        }
        public void Shuffle()
        {
            List<Card> tempList = new List<Card>(cards);
            cards.Clear();
            Random rnd = new Random();

            while(tempList.Count > 0)
            {
                int randomNumber = rnd.Next(0, tempList.Count);

                Card randomCard = tempList[randomNumber];

                cards.Add(randomCard);
                tempList.Remove(randomCard);
            }
        }


        public Card GetNextCard()
        {
            return cards[0];
        }

        public Card GetRandomCard()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, cards.Count);

            return cards[randomNumber];
        }

        public Card GetCardInPositionX(int position)
        {
            if(position >= 0 && position < cards.Count)
                return cards[position];

            return null;
        }

        public int GetDeckQuantity()
        {
            return cards.Count;
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        public bool HasCard(Card card)
        {
            return cards.Contains(card);
        }

    }
}
