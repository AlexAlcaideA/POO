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

        public enum EPlayerAction
        {
            Fold,
            Call,
            Raise,
            Bet,
            Check,
            Count
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

        public int money;
        public int lastBet;

        public Deck PlayerHand { get { return playerHand; } }
        public int PlayerId { get { return playerId; } }
        public EPlayerType PlayerType { get { return playerType; } set { playerType = value; } }
        public eHandRankings PlayerHandRanking { get; set; }

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
            money = 100;
        }

        public Player(Deck _playerHand, int _playerId)
        {
            playerId= _playerId;
            playerHand = _playerHand;
            money = 100;
        }

        public EPlayerAction PlayerAction()
        {
            EPlayerAction playerAction;

            while (true)
            {
                Console.WriteLine($@"
Que quieres hacer jugador {playerId}:
1) Retirarte
2) Igualar apuesta
3) Subir apuesta");

                if (Enum.TryParse(Console.ReadLine(), out playerAction) && (int)playerAction > 0 && (int)playerAction < 4)
                    break;
            }

            return --playerAction;
        }
    }
}
