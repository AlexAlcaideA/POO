using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Cuadrado : Rectangulo
    {
        public Cuadrado() { }

        public Cuadrado(double lado) : base()
        {
            baseRect = lado;
        }

        public override double CalcularArea()
        {
            return Math.Pow(baseRect, 2);
        }

        public override double CalcularPerimetro()
        {
            return numLados * baseRect;
        }
    }
}
