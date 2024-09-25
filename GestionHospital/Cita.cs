using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Cita : HistorialMedico
    {
        private Medico medico;
        private Paciente paciente;

        public Cita() { }

        public Cita(DateTime fecha, Medico med, Paciente pac) : base(fecha)
        {
            medico = med;
            paciente = pac;
        }

        public override string ToString()
        {
            return $@"Cita el {fecha.ToShortDateString()} a las {fecha.ToShortTimeString()}
Con el medico {medico} y el paciente {paciente}";
        }
    }
}
