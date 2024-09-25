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

        public Hospital() 
        {
            personas = new List<Persona>();
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

        public void ListarMedicos()
        {
            foreach (Persona p in personas)
            {
                if (p.GetType() == typeof(Medico))
                    Console.WriteLine(p.ToString());
            }
        }

        public void ListaPacientesDeMedico(Medico m)
        {
            Console.WriteLine(m.ListaDePacientes());
        }

        public void EliminarPaciente(Paciente p)
        {
            personas.Remove(p);

            List<Persona> lista = new List<Persona>(personas);
            lista.RemoveAll(personas => personas.GetType() != typeof(Medico));

            foreach (Medico m in lista)
            {
                m.EliminarPaciente(p);
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

        public void ListaPersonasHospital()
        {
            foreach (Persona p in personas)
            {
                Console.WriteLine($@"{p.ToString()}, esta persona es un {p.GetType().Name}");
            }
        }
    }
}
