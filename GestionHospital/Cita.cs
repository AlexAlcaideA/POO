using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Cita : HistorialMedico
    {
        private Paciente paciente;

        public Cita() { }

        public Cita(DateTime fecha, Medico med, Paciente pac) : base(fecha, med)
        {
            paciente = pac;
        }

        public void ModificarCita(DateTime fecha, Medico med, Paciente pac)
        {
            this.fecha = fecha;

            if (med != null)
                medico = med;

            if (pac != null)
            {
                EliminarCita();

                pac.AñadirHistorialMedico(this);

                paciente = pac;
            }  
        }

        public void EliminarCita()
        {
            paciente.EliminarDelHistorialMedico(this);
            medico.EliminarCitaMedica(this);
        }

        public override string ToString()
        {
            return $@"Cita el {fecha.ToShortDateString()} a las {fecha.ToShortTimeString()}
Con el medico {medico} y el paciente {paciente}";
        }
    }
}
