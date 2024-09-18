using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTexasHold_em
{
    internal class PokerGame
    {
        static void Main(string[] args)
        {
            Poker game = new Poker();

            game.GameLoop();
        }
    }
}
