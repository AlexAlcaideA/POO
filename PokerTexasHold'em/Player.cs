using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTexasHold_em
{
    internal class Player
    {
        public enum EPlayerType
        {
            Bellboy,
            SmallBlind,
            BigBlind,
            None,
            Folded,
            Count
        }

        public enum ePlayerAction
        {
            Fold,
            Call,
            Raise,
            Bet,
            Check,
            None
        }

        public enum eHandRankings
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

        private Deck playerHand;
        private int playerId;
        private EPlayerType playerType;
        private eHandRankings playerHandRanking;
        private ePlayerAction playerAction;
        private short playerStrongestCard;

        public int money;
        public int lastBet;

        public Deck PlayerHand { get; }
        public int PlayerId { get { return playerId; } }
        public EPlayerType PlayerType { get { return playerType; } set { playerType = value; } }
        public eHandRankings PlayerHandRanking { get; set; }
        public ePlayerAction PlayerAction { get; set; }
        public List<int> PlayerStrongestCard { get; set; } //In case of draw this value tells who has the higher value

        public Player()
        {
            playerHand = new Deck();
            money = 100;
        }

        public Player(int _playerId, EPlayerType _playerType = EPlayerType.None)
        {
            playerId = _playerId;
            playerHand = new Deck();
            playerType = _playerType;
            playerAction = ePlayerAction.None;
            money = 100;
        }

        public Player(Deck _playerHand, int _playerId)
        {
            playerId= _playerId;
            playerHand = _playerHand;
            money = 100;
        }

        public ePlayerAction PlayerSelectAction()
        {
            ePlayerAction selectedPlayerAction;

            while (true)
            {
                Console.WriteLine($@"
Que quieres hacer jugador {playerId}:
1) Retirarte
2) Igualar apuesta
3) Subir apuesta");

                if (Enum.TryParse(Console.ReadLine(), out selectedPlayerAction) && (int)selectedPlayerAction > 0 && (int)selectedPlayerAction < 4)
                    break;
            }

            return --selectedPlayerAction;
        }

        public List<Card> GetCards()
        {
            return playerHand.GetCards();
        }

        public void ClearDeck()
        {
            playerHand.ClearDeck();
        }

    }
}
