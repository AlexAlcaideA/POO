using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Rombo : Poligono
    {
        private double diagonalMen;
        private double diagonalMay;

        public Rombo() { }

        public Rombo(double diagonal1, double diagonal2) : base(4)
        {
            diagonalMen = diagonal1;
            diagonalMay = diagonal2;
        }

        public override double CalcularArea()
        {
            return (diagonalMen * diagonalMay) / 2;
        }

        public override double CalcularPerimetro()
        {
            return 4* (Math.Sqrt(Math.Pow(diagonalMen / 2, 2) + Math.Pow(diagonalMay / 2, 2)));
        }

    }
}
