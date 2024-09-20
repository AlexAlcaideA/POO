using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    abstract class Poligonos : Formas2D
    {
        protected int numLados;

        protected Poligonos() { }

        protected Poligonos(int numLados)
        {
            this.numLados = numLados;
        }
    }
}
