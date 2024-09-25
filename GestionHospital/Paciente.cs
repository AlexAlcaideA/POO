using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Paciente : Persona
    {
        private Medico medico;
        private ushort edad;
        private string enfermedad;
        private List<HistorialMedico> historial;
        public Medico Medico { get { return medico; } }
        public ushort Edad {  get { return edad; } }
        public string Enfermedad { get { return enfermedad; } }
        public Paciente() { }

        public Paciente(string name, Medico med, ushort edad, string enfermedad) : base(name) 
        {
            historial = new List<HistorialMedico>();

            medico = med;
            this.edad = edad;
            this.enfermedad = enfermedad;
        }

        public void ModificarPaciente(string nombre, Medico med, ushort edad, string enfermedad)
        {
            this.nombre = nombre;
            medico = med;
            this.edad = edad;
            this.enfermedad = enfermedad;
        }

        public void AñadirHistorialMedico(HistorialMedico hisMed)
        {
            historial.Add(hisMed);
        }

        public void EliminarseDelMedico()
        {
            medico.EliminarPaciente(this);
        }

        public override string ToString()
        {
            return $@"{nombre} con {enfermedad}, edad {edad} y el medico que lo cuida es {medico.Nombre}";
        }
    }
}
