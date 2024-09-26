using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Diagnostico : HistorialMedico
    {
        public Diagnostico() { }
        public Diagnostico(DateTime fecha, Medico med, string notas) : base(fecha, med, notas) { }

        public void ModificarDiagnostico(DateTime fecha, Medico med, string notas)
        {
            this.fecha = fecha;
            medico = med;
            texto = notas;
        }

        public override string ToString()
        {
            return $"Se diagnostico {texto} el {fecha.ToShortDateString()} por el medico {medico}";
        }
    }
}
