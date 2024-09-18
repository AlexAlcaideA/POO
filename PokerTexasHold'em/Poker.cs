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
                        //case EGameState.Showdown:
                            
                        //    //Restart 
                        //    currentGameState = EGameState.Start;
                        //    break;
                    }

                    doOnce = false;
                } 


                ShowTable();

                AskEachPlayerAction();

                if (CheckConditionNextGamePhase())
                {
                    if(currentGameState == EGameState.Showdown)
                    {
                        PlayerHand(); //Sets what type of handRanking each player has

                        List<Player> sortPlayers = new List<Player>();

                        foreach (Player p in players)
                        {
                            if(p.PlayerType != Player.EPlayerType.Folded)
                                sortPlayers.Add(p);
                        }

                        sortPlayers = (List<Player>)sortPlayers.OrderByDescending(p => p.PlayerHandRanking);

                        Player playerToUpdate = players.Find(p => p.Equals(sortPlayers[0]));
                        playerToUpdate.money += potMoney;

                        potMoney = 0;

                        //Restart 
                        currentGameState = EGameState.Start;
                    }

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

        private void ShowTable()
        {
            ShowRevealedCards();
            ShowPlayersHands();
        }

        private void ShowPlayersHands()
        {
            foreach (Player player in players)
            {
                Console.WriteLine(player.PlayerHand.ToString());
            }
        }

        private void ShowRevealedCards()
        {
            Console.WriteLine(revealedCards.ToString());
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

        private void PlayerHand()
        {
            foreach (Player player in players)
            {
                if (RoyalFlush(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.RoyalFlush;
                else if (IsStraightFlush(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.StraightFlush;
                else if (IsFourOfAKind(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.FourOfAKind;
                else if (IsFullHouse(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.FullHouse;
                else if (IsFlush(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.Flush;
                else if (IsStraight(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.Straight;
                else if (IsThreeOfAKind(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.ThreeOfAKind;
                else if (IsTwoPair(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.TwoPair;
                else if (IsPair(player.PlayerHand.GetCards()))
                    player.PlayerHandRanking = Player.eHandRankings.Pair;
                else
                    player.PlayerHandRanking = Player.eHandRankings.HighCard;
            }
        }

        private bool RoyalFlush(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            // Los valores que corresponden a un Royal Flush
            int[] royalFlushValues = { 10, 11, 12, 13, 14 };

            // Agrupar las cartas por tipo de palo
            var groupedBySuit = allCards.GroupBy(c => c.CardType);

            // Verificar si existe algún grupo con al menos 5 cartas del mismo palo
            foreach (var group in groupedBySuit)
            {
                if (group.Count() >= 5)
                {
                    // Extraer los valores de las cartas de este grupo
                    List<int> cardValues = group.Select(c => c.Num).OrderBy(v => v).ToList();

                    // Verificar si los valores de las cartas forman un Royal Flush y si al menos una carta es de la mano del jugador
                    if (royalFlushValues.All(v => cardValues.Contains(v)) && group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                        return true; // Si encontramos un Royal Flush, devolvemos true
                }
            }

            return false; // Si no se encuentra ningún Royal Flush
        }

        public bool IsStraightFlush(List<Card> playerHand)
        {
            List<Card> sortCards = new List<Card>(playerHand);
            
            sortCards.AddRange(revealedCards.GetCards());

            // Ordenar las cartas por número y luego por tipo de palo
            sortCards = sortCards.OrderBy(c => c.Num).ThenBy(c => c.CardType).ToList();

            List<Card> consecutiveCardsList = new List<Card>();
            int consecutiveCardsNumber = 1;

            for (int i = 0; i < sortCards.Count - 1; i++)
            {
                // Verificamos que la siguiente carta sea consecutiva y del mismo palo
                if ((int)sortCards[i + 1].Num == (int)sortCards[i].Num + 1 &&
                    sortCards[i + 1].CardType == sortCards[i].CardType)
                {
                    if (!consecutiveCardsList.Contains(sortCards[i + 1]))
                        consecutiveCardsList.Add(sortCards[i + 1]);

                    if (!consecutiveCardsList.Contains(sortCards[i]))
                        consecutiveCardsList.Add(sortCards[i]);

                    consecutiveCardsNumber++;
                }
                else
                {
                    consecutiveCardsList.Clear();
                    consecutiveCardsNumber = 1; // Si no es consecutiva, reiniciamos el contador
                }

                if(consecutiveCardsNumber >= 5)
                    break;
            }

            // Caso especial: A, 2, 3, 4, 5 (el As como valor bajo)
            bool isLowStraight = sortCards[0].Num == 2 &&
                                  sortCards[1].Num == 3 &&
                                  sortCards[2].Num == 4 &&
                                  sortCards[3].Num == 5 &&
                                  sortCards[4].Num == 14;

            //// Verificar que todas las cartas tienen el mismo palo
            //bool esFlush = sortCards.All(c => c.CardType == sortCards[0].CardType);

            //if (!esFlush)
            //    return false;

            return consecutiveCardsList.Any(cardPlayer => playerHand.Contains(cardPlayer)) && 
                (consecutiveCardsNumber >= 5 || isLowStraight);
        }

        private bool IsFourOfAKind(List<Card> playerHand)
        {
            List<Card> sortCards = new List<Card>(playerHand);

            sortCards.AddRange(revealedCards.GetCards());

            sortCards = sortCards.OrderBy(c => c.Num).ToList();

            List<Card> consecutiveCardsList = new List<Card>();
            int consecutiveCardsNumber = 1;

            for (int i = 0; i < sortCards.Count - 1; i++)
            {
                if ((int)sortCards[i + 1].Num == (int)sortCards[i].Num)
                {
                    if (!consecutiveCardsList.Contains(sortCards[i + 1]))
                        consecutiveCardsList.Add(sortCards[i + 1]);

                    if (!consecutiveCardsList.Contains(sortCards[i]))
                        consecutiveCardsList.Add(sortCards[i]);

                    consecutiveCardsNumber++;
                }
                else
                {
                    consecutiveCardsList.Clear();

                    consecutiveCardsNumber = 1; // Si no es consecutiva, reiniciamos el contador
                }

                if (consecutiveCardsNumber >= 4 && consecutiveCardsList.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                    return true;
            }

            return false;
        }

        private bool IsFullHouse(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            // Agrupar las cartas por tipo de valor
            var groupedBySuit = allCards.GroupBy(c => c.Num);

            bool thereIsTrio = false;
            bool thereIsDuo = false;
            bool containsPlayerCard = false;

            // Comprobar si hay un grupo de tamaño 3 y otro de tamaño 2
            foreach (var group in groupedBySuit)
            {
                if(group.Count() >= 3)
                {
                    thereIsTrio = true;

                    if (group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                        containsPlayerCard = true;
                }
                else if(group.Count() >= 2)
                {
                    thereIsDuo = true;

                    if (group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                        containsPlayerCard = true;
                }
            }

            return thereIsDuo && thereIsTrio && containsPlayerCard;
        }

        private bool IsFlush(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            // Agrupar las cartas por tipo de CardType
            var groupedBySuit = allCards.GroupBy(c => c.CardType);

            // Comprobar si hay un grupo de tamaño 5 y que haya una carta de la mano del jugador
            foreach (var group in groupedBySuit)
            {
                if (group.Count() >= 5 && group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                    return true;
            }

            return false;
        }

        private bool IsStraight(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            allCards = allCards.OrderBy(c => c.Num).ToList();

            List<Card> consecutiveCardsList = new List<Card>();
            int consecutiveCardsNumber = 1;

            for (int i = 0; i < allCards.Count - 1; i++)
            {
                if ((int)allCards[i + 1].Num == (int)allCards[i].Num + 1)
                {
                    if (!consecutiveCardsList.Contains(allCards[i + 1]))
                        consecutiveCardsList.Add(allCards[i + 1]);

                    if (!consecutiveCardsList.Contains(allCards[i]))
                        consecutiveCardsList.Add(allCards[i]);

                    consecutiveCardsNumber++;
                }
                else
                {
                    consecutiveCardsList.Clear();

                    consecutiveCardsNumber = 1; // Si no es consecutiva, reiniciamos el contador
                }

                if (consecutiveCardsNumber >= 5 && consecutiveCardsList.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                    return true;
            }

            return false;
        }

        private bool IsThreeOfAKind(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            // Agrupar las cartas por tipo de valor
            var groupedByNum = allCards.GroupBy(c => c.Num);

            foreach (var group in groupedByNum)
            {
                if (group.Count() == 3 && group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                    return true;
            }

            return false;
        }

        private bool IsTwoPair(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            // Agrupar las cartas por tipo de valor
            var groupedByNum = allCards.GroupBy(c => c.Num);

            int numberOfPairGroups = 0;

            bool hasPlayerHands = false;

            foreach (var group in groupedByNum)
            {
                if (group.Count() == 2)
                {
                    numberOfPairGroups++;

                    if (group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                        hasPlayerHands = true;
                }
            }

            return numberOfPairGroups >= 2 && hasPlayerHands;
        }

        private bool IsPair(List<Card> playerHand)
        {
            // Agregar las cartas reveladas a la mano del jugador
            List<Card> allCards = new List<Card>(playerHand);
            allCards.AddRange(revealedCards.GetCards());

            // Agrupar las cartas por tipo de valor
            var groupedByNum = allCards.GroupBy(c => c.Num);

            foreach (var group in groupedByNum)
            {
                if (group.Count() == 2 && group.Any(cardPlayer => playerHand.Contains(cardPlayer)))
                    return true;
            }

            return false;
        }
    }
}
