using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Elipses : Formas2D
    {
        protected double r1;
        protected double r2;

        public Elipses() { }
        public Elipses(double r1, double r2)
        {
            this.r1 = r1;
            this.r2 = r2;
        }

        public override double CalcularArea()
        {
            return Math.PI * r1 * r2; ;
        }

        public override double CalcularPerimetro()
        {
            return Math.PI * (3 * (r1 + r2) - Math.Sqrt((3 * r1 + r2) * (r1 + 3 * r2)));
        }
    }
}
