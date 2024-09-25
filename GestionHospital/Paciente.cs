using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Paciente : Persona
    {
        private Medico medico;
        private string enfermedad;
        public Medico Medico { get { return medico; } }
        public string Enfermedad { get { return enfermedad; } }
        public Paciente() { }

        public Paciente(string name, Medico med, string enfermedad) : base(name) 
        {
            medico = med;
            this.enfermedad = enfermedad;
        }

        public override string ToString()
        {
            return $@"{nombre} con {enfermedad} y el medico que lo cuida es {medico.Nombre}";
        }
    }
}
