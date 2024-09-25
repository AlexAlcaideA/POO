using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Hospital
    {
        private List<Persona> personas;
        private List<Cita> citasMedicas;

        public Hospital() 
        {
            personas = new List<Persona>();
            citasMedicas = new List<Cita>();
        }

        public Persona EncontrarPersonaPorNombre(string nombre)
        {
            return personas.Find(pers => pers.Nombre == nombre);
        }

        public Medico EncontrarMedicoPorNombre(string nombre)
        {
            return (Medico)personas.Find(pers => pers.Nombre == nombre && pers.GetType() == typeof(Medico));
        }

        public Paciente EncontrarPacientePorNombre(string nombre)
        {
            return (Paciente)personas.Find(pers => pers.Nombre == nombre && pers.GetType() == typeof(Paciente));
        }

        public PersonalAdministrativo EncontrarPersonalAdministrativoPorNombre(string nombre)
        {
            return (PersonalAdministrativo)personas.Find(pers => pers.Nombre == nombre && 
            pers.GetType() == typeof(PersonalAdministrativo));
        }

        public bool ContienePersona(Persona persona)
        {
            return personas.Contains(persona);
        }

        public void DarDeAltaMedico(Medico m)
        {
            personas.Add(m);
        }

        public void DarDeAltaPaciente(Paciente p)
        {
            personas.Add(p);
        }

        public void DarDeAltaPersonalAdmin(PersonalAdministrativo adm)
        {
            personas.Add(adm);
        }

        public void AñadirCitaMedica(DateTime fecha, Medico med, Paciente pac)
        {
            Cita cita = new Cita(fecha, med, pac);

            citasMedicas.Add(cita);
            pac.AñadirHistorialMedico(cita);
        }

        public void AñadirTratamientoPaciente(DateTime fecha, string tratamiento, string medicamento, Paciente pac)
        {
            pac.AñadirHistorialMedico(new Tratamiento(fecha, tratamiento, medicamento));
        }

        public void AñadirDiagnosticoPaciente(DateTime fecha, string notas, Paciente pac)
        {
            pac.AñadirHistorialMedico(new Diagnostico(fecha, notas));
        }

        public void ModificarCitaMedica(DateTime fecha)
        {
            Cita cita = citasMedicas.Find(c => c.Fecha == fecha);
        }

        public void ListarMedicos()
        {
            foreach (Persona p in personas)
            {
                if (p.GetType() == typeof(Medico))
                    Console.WriteLine(p.ToString());
            }
        }

        public void ListaPacientes()
        {
            foreach(Persona p in personas)
            {
                if(p.GetType() == typeof(Paciente))
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }

        public void ListaAdministrativos()
        {
            foreach (Persona p in personas)
            {
                if (p.GetType() == typeof(PersonalAdministrativo))
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }

        public void ListaPacientesDeMedico(Medico m)
        {
            Console.WriteLine(m.ListaDePacientes());
        }

        public void ListaPersonasHospital()
        {
            foreach (Persona p in personas)
            {
                Console.WriteLine($@"{p.ToString()}, esta persona es un {p.GetType().Name}");
            }
        }

        public void EliminarPaciente(Paciente p)
        {
            personas.Remove(p);

            p.EliminarseDelMedico();
        }
    }
}
