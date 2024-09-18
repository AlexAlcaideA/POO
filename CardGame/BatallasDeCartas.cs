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

            while(gameDeck.GetDeckQuantity() > 0)
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

                foreach (Player player in players)
                {
                    playedCards.Add(player.PlayerHand.GetNextCard());
                }

                playedCards.Sort((card1, card2) => card2.Num.CompareTo(card1.Num));

                foreach (Player player in players)
                {
                    Card masAlta = playedCards[0];

                    if (player.PlayerHand.HasCard(masAlta))
                    {
                        foreach (Card card in playedCards)
                        {
                            if(!player.PlayerHand.HasCard(card))
                                player.PlayerHand.AddCard(card);
                        }
                    }
                    else
                    {
                        foreach (Card card in playedCards)
                        {
                            player.PlayerHand.RemoveCard(card);
                        }
                    }
                }

                players.RemoveAll(p => p.PlayerHand.GetDeckQuantity() <= 0);
            }
            Console.WriteLine($"Jugador {players[0].PlayerId + 1} ha ganado");
            Console.ReadKey();
        }
    }
}
