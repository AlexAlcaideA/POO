using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    abstract class Poligono : Forma2D
    {
        protected int numLados;

        protected Poligono() { }

        protected Poligono(int numLados)
        {
            this.numLados = numLados;
        }
    }
}
