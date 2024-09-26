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
        public Tratamiento(DateTime fecha, Medico med, string tratamiento, string medicina) : base(fecha, med, tratamiento)
        {
            this.medicina = medicina;
        }

        public void ModificarTratamiento(DateTime fecha, Medico med, string tratamiento, string medicina)
        {
            this.fecha = fecha;
            medico = med;
            texto = tratamiento;
            this.medicina = medicina;
        }

        public override string ToString()
        {
            return $@"El tratamiento de {texto} con la medicina {medicina} se dio el {fecha.ToShortDateString()} 
Por el medico {medico}";
        }
    }
}
