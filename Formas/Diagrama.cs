using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Diagrama
    {
        public enum eTiposFormas
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

        public Diagrama() 
        { 
            figuras = new List<Forma2D>();
        }
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

        public void PreguntarPorFormas()
        {
            do
            {
                eTiposFormas formaSeleccionada;

                Console.WriteLine(@"
Para salir por '*'
Elige que figura añadir a la lista:
1)Rectangulo
2)Cuadrado,
3)Circulo,
4)Elipse,
5)Triangulo,
6)Octagono,
7)Rombo
");

                string textoLeido = Console.ReadLine();

                if(textoLeido == "*")
                    break;
                else if (!Enum.TryParse(textoLeido, out formaSeleccionada))
                    continue;

                Console.Clear();

                if (!SeCreaFormaSeleccionada(formaSeleccionada))
                    Console.WriteLine("Forma no creada");            
            }
            while (true);
        }

        private bool SeCreaFormaSeleccionada(eTiposFormas tiposFormas)
        {
            List<double> valoresLista = new List<double>();

            switch (--tiposFormas)
            {
                case eTiposFormas.Rectangulo:
                    Console.WriteLine("Escribe la base y la altura: Ej: 5,8");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 2);

                    if (valoresLista == null)
                        return false;

                    Rectangulo rectangulo = new Rectangulo(valoresLista[0], valoresLista[1]);
                    AddFiguras(rectangulo);
                    break;
                case eTiposFormas.Cuadrado:
                    Console.WriteLine("Escribe la base y la altura: Ej: 5");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 1);

                    if (valoresLista == null)
                        return false;

                    Cuadrado cuadrado = new Cuadrado(valoresLista[0]);
                    AddFiguras(cuadrado);
                    break;
                case eTiposFormas.Circulo:
                    Console.WriteLine("Escribe el radio: Ej: 5");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 1);

                    if (valoresLista == null)
                        return false;

                    Circulo circulo = new Circulo(valoresLista[0]);
                    AddFiguras(circulo);
                    break;
                case eTiposFormas.Elipse:
                    Console.WriteLine("Escribe el radio mayor y menor: Ej: 5,8");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 2);

                    if (valoresLista == null)
                        return false;

                    Elipse elipse = new Elipse(valoresLista[0], valoresLista[1]);
                    AddFiguras(elipse);
                    break;
                case eTiposFormas.Triangulo:
                    Console.WriteLine("Escribe la base, la altura y el angulo: Ej: 5,8,60");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 3);

                    if (valoresLista == null)
                        return false;

                    Triangulo triangulo = new Triangulo(valoresLista[0], valoresLista[1], valoresLista[2]);
                    AddFiguras(triangulo);
                    break;
                case eTiposFormas.Octagono:
                    Console.WriteLine("Escribe el tamaño del lado: Ej: 8");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 1);

                    if (valoresLista == null)
                        return false;

                    Octagono octagono = new Octagono(valoresLista[0]);
                    AddFiguras(octagono);
                    break;
                case eTiposFormas.Rombo:
                    Console.WriteLine("Escribe la diagonal mayor y la diagonal menor: Ej: 5,8");
                    valoresLista = RecogerValoresLeidos(Console.ReadLine(), 2);

                    if (valoresLista == null)
                        return false;

                    Rombo rombo = new Rombo(valoresLista[0], valoresLista[1]);
                    AddFiguras(rombo);
                    break;
            }

            return true;
        }

        private List<Double> RecogerValoresLeidos(string valores, int numValores)
        {
            string[] listaDeTextos = valores.Split(',');
            List<Double> listaValores = new List<Double>();

            if (numValores != listaDeTextos.Length)
                return null;

            foreach (string str in listaDeTextos)
            {
                double valor;

                if (double.TryParse(str, out valor))
                    listaValores.Add(valor);
                else
                    break;
            }

            if (numValores != listaValores.Count)
                return null;

            return listaValores;
        }

        public void AddFiguras(Forma2D forma2D)
        {
            figuras.Add(forma2D);
        }

        public double CalculaAreaTotal()
        {
            double areaTotal = 0;

            foreach(Forma2D forma2D in figuras)
            {
                areaTotal += forma2D.CalcularArea();
            }

            return areaTotal;
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
