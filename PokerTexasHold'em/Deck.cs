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
            for (int i = 0; i < (int)Card.ECardType.Size; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    cards.Add(new Card(j, (Card.ECardType)i));
                }
            }
        }

        public Card GetNextCard()
        {
            return cards[0];
        }

        public void Shuffle()
        {
            List<Card> tempList = new List<Card>(cards);
            cards.Clear();

            while(tempList.Count > 0)
            {
                Random rnd = new Random();
                int randomNumber = rnd.Next(0, tempList.Count);

                Card randomCard = tempList[randomNumber];

                cards.Add(randomCard);
                tempList.Remove(randomCard);
            }
        }

        public Card GetRandomCard()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, cards.Count);

            return cards[randomNumber];
        }

        public Card GetCardInPositionX()
        {
            do
            {
                Console.WriteLine($"Select a number between {1} and {cards.Count}");
                int numberSelected;

                if (int.TryParse(Console.ReadLine(), out numberSelected))
                    return cards[numberSelected];
                else
                    Console.WriteLine("Incorrect number");

            } 
            while (true);
        }

        public int GetDeckCuantity()
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
