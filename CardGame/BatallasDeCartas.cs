using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class BatallasDeCartas
    {
        public static List<Player> players = new List<Player>();
        public static Deck gameDeck = new Deck();

        static void Main(string[] args)
        {
            GameLoop();
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
                players.Add(new Player(i));
            }

            int playerIndex = 0;

            while(gameDeck.GetDeckCuantity() > 0)
            {
                Card tempCard = gameDeck.GetRandomCard();
                gameDeck.RemoveCard(tempCard);

                players[playerIndex].PlayerHand.AddCard(tempCard);

                playerIndex++;

                if(playerIndex >= playersCuantity)
                    playerIndex = 0;
            }

            while (players.Count > 1)
            {
                List<Card> playedCards = new List<Card>();

                for(int i = 0; i < players.Count; i++)
                {
                    playedCards.Add(players[i].PlayerHand.GetNextCard());
                }

                playedCards.Sort((card1, card2) => card2.Num.CompareTo(card1.Num));

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].PlayerHand.HasCard(playedCards[0]))
                    {
                        for(int j = 1; j < playedCards.Count; j++)
                        {
                            players[i].PlayerHand.AddCard(playedCards[j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < playedCards.Count; j++)
                        {
                            players[i].PlayerHand.RemoveCard(playedCards[j]);
                        }
                    }

                    if (players[i].PlayerHand.GetDeckCuantity() <= 0)
                        players.Remove(players[i]);
                }
            }
            Console.WriteLine($"Jugador {players[0].PlayerId + 1} ha ganado");
            Console.ReadKey();
        }
    }
}
