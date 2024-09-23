using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Rectangulo : Poligono
    {
        protected double baseRect;
        protected double alturaRect;

        public Rectangulo() { }

        public Rectangulo(double baseRect, double alturaRect) : base(4)
        {
            this.baseRect = baseRect;
            this.alturaRect = alturaRect;
        }

        public override double CalcularArea()
        {
            return baseRect * alturaRect;
        }

        public override double CalcularPerimetro()
        {
            return baseRect * 2 + alturaRect * 2;
        }
    }
}
