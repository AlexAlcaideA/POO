using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Circulo : Elipses
    {
        public Circulo() { }

        public Circulo(double radio)
        { 
            r1 = radio;
        }

        public override double CalcularArea()
        {
            return Math.PI * Math.Pow(r1, 2);
        }

        public override double CalcularPerimetro()
        {
            return Math.PI * r1 * 2;
        }
    }
}
