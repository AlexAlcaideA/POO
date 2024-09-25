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
        public Diagnostico(DateTime fecha, string notas) : base(fecha, notas) { }

        public override string ToString()
        {
            return $"Se diagnostico {texto} el {fecha.ToShortDateString()}";
        }
    }
}
