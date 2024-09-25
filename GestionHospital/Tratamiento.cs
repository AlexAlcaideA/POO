using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Tratamiento : HistorialMedico
    {
        private string medicina;

        public string Medicina { get { return medicina; } }

        public Tratamiento() { }
        public Tratamiento(DateTime fecha, string tratamiento, string medicina) : base(fecha, tratamiento)
        {
            this.medicina = medicina;
        }

        public override string ToString()
        {
            return $"El tratamiento de {texto} con la medicina {medicina} se dio el {fecha.ToShortDateString()}";
        }
    }
}
