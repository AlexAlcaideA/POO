using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class BatallasDeCartas
    {
        public static List<Deck> playersDeck = new List<Deck>();
        public static Deck gameDeck = new Deck();

        static void Main(string[] args)
        {

        }

        private static void GameLoop()
        {
            gameDeck.InitializeDeck();

            int playersCuantity;

            do
            {
                Console.WriteLine("Cuantos jugadores seran entre de 2 y 5:");

                if (int.TryParse(Console.ReadLine(), out playersCuantity) && playersCuantity > 1 && playersCuantity < 6)
                    break;
                else
                    Console.WriteLine("Numero incorrecto");
            }
            while (true);
            
            for(int i = 0; i < playersCuantity; i++)
            {
                playersDeck.Add(new Deck());
            }

            int playerIndex = 0;

            while(gameDeck.GetDeckCuantity() > 0)
            {
                Card tempCard = gameDeck.GetRandomCard();
                gameDeck.RemoveCard(tempCard);

                playersDeck[playerIndex].AddCard(tempCard);

                playerIndex++;

                if(playerIndex >= playersCuantity)
                    playerIndex = 0;
            }

            while (playersDeck.Count > 1)
            {
                List<Card> playedCards = new List<Card>();

                for(int i = 0; i < playersDeck.Count; i++)
                {
                    playedCards.Add(playersDeck[i].GetNextCard());
                }

                playedCards.Sort((card1, card2) => card2.Num.CompareTo(card1.Num));

                for (int i = 0; i < playersDeck.Count; i++)
                {
                    if (playersDeck[i].HasCard(playedCards[0]))
                    {
                        for(int j = 1; j < playedCards.Count; j++)
                        {
                            playersDeck[i].AddCard(playedCards[j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < playedCards.Count; j++)
                        {
                            playersDeck[i].RemoveCard(playedCards[j]);
                        }
                    }
                }
            }
        }

    }
}
