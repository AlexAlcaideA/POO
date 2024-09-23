using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Persona
    {
        protected string nombre;
        public string Nombre { get; }

        protected Persona() { }

        protected Persona(string nombre) 
        {
            this.nombre = nombre;
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
