using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Circulo : Elipse
    {
        public Circulo() { }

        public Circulo(double radio) : base(radio, radio) { }
    }
}
