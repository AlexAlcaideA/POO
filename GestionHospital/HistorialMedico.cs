using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    abstract class HistorialMedico
    {
        protected Medico medico;
        protected DateTime fecha;
        protected string texto;

        public DateTime Fecha { get { return fecha; } }

        protected HistorialMedico() { }
        protected HistorialMedico(DateTime fecha, Medico medico)
        {
            this.fecha = fecha;
            this.medico = medico;
        }
        protected HistorialMedico(DateTime fecha, Medico med,string texto) : this(fecha, med)
        {
            this.texto = texto;
        }
    }
}
