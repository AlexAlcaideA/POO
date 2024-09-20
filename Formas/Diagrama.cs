using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Diagrama
    {
        enum eTiposFormas
        {
            Rectangulo,
            Cuadrado,
            Circulo,
            Elipse,
            Triangulo,
            Octagono,
            Rombo
        }

        private List<Forma2D> figuras;

        public Diagrama() { }
        public Diagrama(int numFiguras) 
        { 
            figuras = new List<Forma2D>();
            CrearFormas(numFiguras);
        }

        private void CrearFormas(int numFiguras = 5)
        {
            Random rnd = new Random();

            for(int i = 0; i < numFiguras; i++)
            {
                eTiposFormas tipoForma = (eTiposFormas)rnd.Next(0, Enum.GetValues(typeof(eTiposFormas)).Length);

                int value1 = rnd.Next(0, 101);
                int value2 = rnd.Next(0, 101);

                switch (tipoForma)
                {
                    case eTiposFormas.Rectangulo:
                        figuras.Add(new Rectangulo(value1, value2));
                        break;
                    case eTiposFormas.Cuadrado:
                        figuras.Add(new Cuadrado(value1));
                        break;
                    case eTiposFormas.Circulo:
                        figuras.Add(new Circulo(value1));
                        break;
                    case eTiposFormas.Elipse:
                        figuras.Add(new Elipse(value1, value2));
                        break;
                    case eTiposFormas.Triangulo:
                        int angulo = rnd.Next(0, 160);

                        figuras.Add(new Triangulo(value1, value2, angulo));
                        break;
                    case eTiposFormas.Octagono:
                        figuras.Add(new Octagono(value1));
                        break;
                    case eTiposFormas.Rombo:
                        figuras.Add(new Rombo(value1, value2));
                        break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            foreach (Forma2D forma in figuras)
            {
                text.Append($@"
El area de {forma.ToString()} es {forma.CalcularArea()} 
y su perimetro {forma.CalcularPerimetro()}");
            }

            return text.ToString();
        }

    }
}
