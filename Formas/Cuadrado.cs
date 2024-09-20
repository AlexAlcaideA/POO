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

        public Cuadrado(double lado) : base(lado, lado) { }
    }
}
