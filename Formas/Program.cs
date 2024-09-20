using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Formas2D> list = new List<Formas2D>();

            list.Add(new Octagono(8));
            list.Add(new Triangulo(4, 5, 60));
            list.Add(new Cuadrado(5));
            list.Add(new Elipses(10, 6));

            ShowFiguresList(list);

            Console.ReadKey();
        }

        public static void ShowFiguresList(List<Formas2D> list)
        {
            foreach(Formas2D forma in list)
            {
                Console.WriteLine($@"El area de {forma.ToString()} es {forma.CalcularArea()} 
y su perimetro {forma.CalcularPerimetro()}");
            }
        }

        public static Formas2D GetSelectedTypeFromList( List<Formas2D> list, Type askedType)
        {
            return list.Find(type => type.GetType() == askedType);
        }
    }
}
