using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokerTexasHold_em.Card;

namespace PokerTexasHold_em
{
    internal class Poker
    {
        enum EGameState
        {
            Start,
            PreFlop,
            Flop,
            Turn,
            River,
            Showdown
        }

        enum EHandRankings
        {
            HighCard,
            Pair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        private List<Player> players;
        private Deck tableCards;
        private Deck revealedCards;
        private EGameState currentGameState;

        private int smallBlind;
        private int largeBlind;
        private int potMoney;

        public List<Player> Players { get { return players; } }
        public Deck TableCards { get { return tableCards; } }

        public Poker()
        {
            players = new List<Player>();
            tableCards = new Deck();
            revealedCards = new Deck();
            currentGameState = EGameState.PreFlop;

            potMoney = 0;

            int numPlayers = AskNumberPlayers();

            SetPlayersType(numPlayers);

            tableCards.InitializeDeck();
            tableCards.Shuffle();
        }

        private int AskNumberPlayers()
        {
            int playersNumber;

            do
            {
                Console.WriteLine("Cuantos jugadores sereis siendo el minimo 2:");

                if (int.TryParse(Console.ReadLine(), out playersNumber) && playersNumber >= 1)
                    return playersNumber;
            }
            while (true);
        }

        private void SetPlayersType(int numPlayers)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                //If there are only 2 players, we can get rid of the Bellboy
                if (numPlayers < 3 && i == 0)
                    i++;

                switch ((Player.EPlayerType)i)
                {
                    case Player.EPlayerType.Bellboy:
                        players.Add(new Player(i, Player.EPlayerType.Bellboy));
                        break;
                    case Player.EPlayerType.SmallBlind:
                        players.Add(new Player(i, Player.EPlayerType.SmallBlind));
                        break;
                    case Player.EPlayerType.BigBlind:
                        players.Add(new Player(i, Player.EPlayerType.BigBlind));
                        break;
                    default:
                        players.Add(new Player(i));
                        break;
                }
            }
        }

        public void GameLoop()
        {
            bool doOnce = true;

            while (players.Count > 2)
            {
                if (doOnce)
                {
                    switch (currentGameState)
                    {
                        case EGameState.Start:
                            StartBets();
                            break;
                        case EGameState.PreFlop:
                            GiveEachPlayerXCards();
                            break;
                        case EGameState.Flop:
                            RevealXCards(3);
                            break;
                        case EGameState.Turn:
                            RevealXCards(1);
                            break;
                        case EGameState.River:
                            RevealXCards(1);
                            break;
                        case EGameState.Showdown:
                            //Restart 
                            currentGameState = EGameState.Start;
                            break;
                    }

                    doOnce = false;
                } 

                AskEachPlayerAction();

                if (CheckConditionNextGamePhase())
                {
                    currentGameState++;
                    doOnce = true;
                }   
            }
        }

        private void StartBets()
        {
            Player smallBlindPlayer = players.Find(player => player.PlayerType == Player.EPlayerType.SmallBlind);

            smallBlind = PlayerBet(smallBlindPlayer);
            smallBlindPlayer.lastBet = smallBlind;

            Player largeBlindPlayer = players.Find(player => player.PlayerType == Player.EPlayerType.BigBlind);

            largeBlind = PlayerBet(largeBlindPlayer, smallBlind * 2);
            largeBlindPlayer.lastBet = largeBlind;

            if (largeBlind <= 0)
            {
                //Restart Round, remember to reset the player type from folder to another
            }
            else
                currentGameState++;
        }

        private void AskEachPlayerAction()
        {
            foreach (Player player in players)
            {
                Player.EPlayerAction playerAction = player.PlayerAction();

                if(player.PlayerType != Player.EPlayerType.Folded)
                {
                    switch (playerAction)
                    {
                        case Player.EPlayerAction.Fold:
                            player.PlayerType = Player.EPlayerType.Folded;
                            break;
                        case Player.EPlayerAction.Call:
                            if(PlayerCall(player) == 0)
                                player.PlayerType = Player.EPlayerType.Folded;
                            break;
                        case Player.EPlayerAction.Raise:
                            //If player can't really Raise
                            if(PlayerRaise(player) == 0)
                            {
                                //If player can at least pay the largeBlind
                                if(player.money >= largeBlind)
                                {
                                    player.money -= largeBlind;
                                    potMoney += largeBlind;
                                }
                                else
                                    player.PlayerType = Player.EPlayerType.Folded;
                            }
                            break;
                    }
                }                
            }
        }

        private int PlayerBet(Player player, int moneyMustBet = 0)
        {
            int moneyBet = 0;

            if(moneyMustBet > 0 && moneyMustBet <= player.money)
                moneyBet = moneyMustBet;
            else
            {
                do
                {
                    Console.WriteLine("Decide cuanto apostar:");

                    if (int.TryParse(Console.ReadLine(), out moneyBet) && moneyBet <= 50 && moneyBet > 0 && moneyBet <= player.money)
                        break;
                }
                while (true);
            }
            
            player.money -= moneyBet;
            potMoney += moneyBet;

            return moneyBet;
        }

        private int PlayerCall(Player player)
        {
            if (player.money >= largeBlind)
            {
                player.lastBet = largeBlind;
                potMoney += largeBlind;

                return player.money -= largeBlind;
            }
            else
                return 0;
        }

        private int PlayerRaise(Player player)
        {
            int moneyRaise = 0;

            do
            {
                Console.WriteLine("Decide a cuanto subir la apuesta:");

                if (int.TryParse(Console.ReadLine(), out moneyRaise) && moneyRaise > largeBlind)
                    break;
            }
            while (true);

            if (moneyRaise > player.money)
                return 0;

            player.money -= moneyRaise;
            player.lastBet = moneyRaise;
            potMoney += moneyRaise;

            largeBlind = moneyRaise;

            return moneyRaise;
        }

        private void GiveEachPlayerXCards(int numberOfCards = 2)
        {
            foreach (Player player in players)
            {
                for (int i = 0; i < numberOfCards; i++)
                {
                    Card selectedCard = tableCards.GetNextCard();

                    player.PlayerHand.AddCard(selectedCard);
                    tableCards.RemoveCard(selectedCard);
                }
            }
        }

        private bool CheckConditionNextGamePhase()
        {
            foreach(Player player in players)
            {
                if(player.PlayerType != Player.EPlayerType.Folded && player.lastBet != largeBlind)
                    return false;
            }
            return true;
        }

        private void RevealXCards(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                Card selectedCard = tableCards.GetNextCard();

                revealedCards.AddCard(selectedCard);
                tableCards.RemoveCard(selectedCard);
            }
        }

        private EHandRankings PlayerHand(Player player)
        {
            foreach(Card revealedCard in revealedCards.GetCards())
            {
                player.PlayerHand.AddCard(revealedCard);
            }

            if (RoyalFlush(player.PlayerHand.GetCards()))
                return EHandRankings.RoyalFlush;


            return EHandRankings.HighCard;
        }

        private bool RoyalFlush(List<Card> hand)
        {
            //Values from a Royal Flush: 10, J (11), Q (12), K (13), A (14)
            int[] valoresEscaleraReal = { 10, 11, 12, 13, 14 };

            //Check if all cards are the same type
            Card.ECardType primerPalo = hand[0].CardType;
            if (hand.All(carta => carta.CardType == primerPalo))
            {
                //Check if the hand has the values of a Royal Flush
                var valoresMano = hand.Select(carta => carta.Num).OrderBy(v => v).ToList();
                if (valoresMano.SequenceEqual(valoresEscaleraReal))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
