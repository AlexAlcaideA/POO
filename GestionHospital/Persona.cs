using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    abstract class Persona
    {
        protected string nombre;
        public string Nombre { get { return nombre; } }

        protected Persona() { }

        protected Persona(string nombre) 
        {
            this.nombre = nombre;
        }
    }
}
