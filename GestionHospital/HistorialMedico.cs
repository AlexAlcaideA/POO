using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    abstract class HistorialMedico
    {
        protected DateTime fecha;
        protected string texto;

        public DateTime Fecha { get { return fecha; } }

        protected HistorialMedico() { }
        protected HistorialMedico(DateTime fecha)
        {
            this.fecha = fecha;
        }
        protected HistorialMedico(DateTime fecha, string texto) : this(fecha)
        {
            this.texto = texto;
        }
    }
}
