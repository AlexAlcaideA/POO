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
            Diagrama diagrama = new Diagrama(10);

            Console.WriteLine(diagrama.ToString());
            Console.ReadKey();
        }
    }
}
