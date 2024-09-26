using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class PersonalAdministrativo : Persona
    {
        private DateTime fechaContratacion;
        private string puesto;

        public DateTime FechaContratacion { get { return fechaContratacion;} }
        public string Departamento {  get { return puesto; } }
        public PersonalAdministrativo() { }

        public PersonalAdministrativo(string nombre, DateTime fecha, string puesto) : base(nombre) 
        {
            fechaContratacion = fecha;
            this.puesto = puesto;
        }

        public void ModificarDatosAdministrativo(string nombre, DateTime fecha, string puesto)
        {
            this.nombre = nombre;
            fechaContratacion = fecha;
            this.puesto = puesto;
        }

        public override string ToString()
        {
            return $@"{nombre} que empezo el {fechaContratacion.ToShortDateString()} y esta en el puesto de {puesto}";
        }
    }
}
