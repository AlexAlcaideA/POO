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
        private List<Cita> citasMedicas;
        string especialidad;
        ushort añosExperiencia;

        public string Especialidad { get { return especialidad; } }
        public ushort AñosExperiencia { get { return añosExperiencia; } }

        public Medico()
        {
            
        }

        public Medico(string nombre, string especialidad, ushort años) : base(nombre)
        {
            pacientes = new List<Paciente>();
            citasMedicas = new List<Cita>();

            this.especialidad = especialidad;
            añosExperiencia = años;
        }

        public void AñadirPaciente(Paciente p)
        {
            pacientes.Add(p);
        }

        public void AñadirCitaMedica(Cita c)
        {
            citasMedicas.Add(c);
        }

        public void EliminarPaciente(Paciente p)
        {
            pacientes.Remove(p);
        }

        public void EliminarCitaMedica(Cita c)
        {
            citasMedicas.Remove(c);
        }

        public void ModificarDatos(string nombre, string especialidad, ushort añosExp)
        {
            this.nombre = nombre;
            this.especialidad = especialidad;
            añosExperiencia = añosExp;
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

        public string ListaDeCitasMedicas()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"
El medico: {nombre}
Citas medicas:");

            foreach (Cita c in citasMedicas)
            {
                sb.AppendLine(c.ToString());
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return $@"{nombre} con {añosExperiencia} años de experiencia y con especialidad en {especialidad}";
        }
    }
}
