using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Medico : Persona
    {
        private List<Paciente> pacientes;
        ushort añosExperiencia;

        public ushort AñosExperiencia { get { return añosExperiencia; } }

        public Medico()
        {
            
        }

        public Medico(string nombre, ushort años) : base(nombre)
        {
            pacientes = new List<Paciente>();
            añosExperiencia = años;
        }

        public void AñadirPaciente(Paciente p)
        {
            pacientes.Add(p);
        }

        public void EliminarPaciente(Paciente p)
        {
            pacientes.Remove(p);
        }

        public bool TieneAlPaciente(Paciente p)
        {
            return pacientes.Contains(p);
        }

        public string ListaDePacientes()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"
El medico: {nombre}
Con pacientes:");

            foreach (Paciente p in pacientes)
            {
                sb.AppendLine(p.ToString());
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return $@"{nombre} con {añosExperiencia} años de experiencia";
        }
    }
}
