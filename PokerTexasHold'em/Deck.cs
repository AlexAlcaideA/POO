﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTexasHold_em
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
            for (int i = 0; i < (int)Card.eCardType.Size; i++)
            {
                for (int j = 2; j <= 14; j++)
                {
                    cards.Add(new Card(j, (Card.eCardType)i));
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

            Random rnd = new Random();

            while (tempList.Count > 0)
            {
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

        public void ShowCards()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine($"Carta {card.Num} de {card.CardType}");
            }
        }

        public List<Card> GetCards()
        {
            return cards;
        }

        public void ClearDeck()
        {
            cards.Clear();
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            foreach (Card card in cards)
            {
                text.Append(card.ToString() + "\n");
            }

            return text.ToString();
        }

    }
}
