using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Triangulo : Poligonos
    {
        private double baseTri;
        private double alturaTri;
        private double anguloTri;
        public Triangulo() { }

        public Triangulo(double baseTriangulo, double altura, double angulo) : base(3)
        {
            baseTri = baseTriangulo;
            alturaTri = altura;
            anguloTri = angulo;
        }

        public override double CalcularArea()
        {
            return (baseTri * alturaTri) / 2;
        }

        public override double CalcularPerimetro()
        {
            double anguloRadianes = anguloTri * (Math.PI / 180);

            double ladoOblicuo = alturaTri / Math.Tan(anguloRadianes);

            double hipotenusa = Math.Sqrt(Math.Pow(alturaTri, 2) + Math.Pow(ladoOblicuo, 2));

            return baseTri + ladoOblicuo + hipotenusa;
        }
    }
}
