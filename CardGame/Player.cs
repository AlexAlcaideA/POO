using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class Player
    {
        private Deck playerHand;
        private int playerId;

        public Deck PlayerHand { get { return playerHand; } }
        public int PlayerId { get { return playerId; } }

        public Player()
        {
            playerHand = new Deck();
        }

        public Player(int _playerId)
        {
            playerId = _playerId;
            playerHand = new Deck();
        }

        public Player(Deck _playerHand, int _playerId)
        {
            playerId= _playerId;
            playerHand = _playerHand;
        }
    }
}
