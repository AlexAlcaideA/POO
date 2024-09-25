using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class PersonalAdministrativo : Persona
    {
        private ushort añosContrato;

        public ushort AñosContrato { get { return añosContrato;} }
        public PersonalAdministrativo() { }

        public PersonalAdministrativo(string nombre, ushort años) : base(nombre) 
        {
            añosContrato = años;
        }

        public override string ToString()
        {
            return $@"{nombre} con unos {añosContrato} años de contrato";
        }
    }
}
