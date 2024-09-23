using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Octagono : Poligono
    {
        private double baseLado;
        public Octagono() { }
        public Octagono(double lado) : base(8)
        {
            baseLado = lado;
        }

        public override double CalcularArea()
        {
            return 2 * (1 + Math.Sqrt(2)) * Math.Pow(baseLado, 2); ;
        }

        public override double CalcularPerimetro()
        {
            return baseLado * numLados;
        }
    }
}
