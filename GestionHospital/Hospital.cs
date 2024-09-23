using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Hospital
    {
        public enum eOpcionMenu
        {
            DarDeAltaMedico = 1,
            DarDeAltaPaciente = 2,
            DarDeAltaPersonalAdmin = 3,
            ListarMedicos = 4,
            ListaPacientesDeMedico = 5,
            EliminarPaciente = 6,
            ListaPersonasHospital = 7
        }

        private List<Persona> personas;

        public Hospital() 
        {
            personas = new List<Persona>();
        }

        public void Menu()
        {
            do
            {
                Console.WriteLine(@"
Que opcion desea escoger:
1) DarDeAltaMedico
2) DarDeAltaPaciente
3) DarDeAltaPersonalAdmin
4) ListarMedicos
5) ListaPacientesDeMedico
6) EliminarPaciente
7) ListaPersonasHospital
0) Salir");
                string opcionUsuario = Console.ReadLine();
                eOpcionMenu menu;

                if (opcionUsuario == "0")
                    break;
                else if (!Enum.TryParse(opcionUsuario, out menu))
                    continue;

                Console.Clear();

                MenuOpcionSeleccionada(menu);
            }
            while (true);
        }

        public void MenuOpcionSeleccionada(eOpcionMenu menu)
        {
            string nombre;
            switch (menu)
            {
                case eOpcionMenu.DarDeAltaMedico:
                    Console.WriteLine("Escribe el nombre del medico:");
                    nombre = Console.ReadLine().ToLower();

                    Medico med = new Medico(nombre);

                    if(!personas.Contains(med))
                        DarDeAltaMedico(new Medico(nombre));
                    break;
                case eOpcionMenu.DarDeAltaPaciente:
                    Console.WriteLine("Escribe el nombre del paciente:");
                    nombre = Console.ReadLine().ToLower();

                    Paciente pac = new Paciente(nombre);

                    Console.WriteLine("Escribe el nombre del medico:");
                    nombre = Console.ReadLine().ToLower();
                    Medico medico1 = personas.Find(medico => medico.Nombre == nombre);

                    if (!personas.Contains(pac) && personas.Any(medico => medico.Nombre == nombre))
                    {
                        DarDeAltaPaciente(pac);
                        
                    }
                        
                    break;
                case eOpcionMenu.DarDeAltaPersonalAdmin:
                    Console.WriteLine("Escribe el nombre del administrativo:");
                    nombre = Console.ReadLine().ToLower();

                    PersonalAdministrativo adm = new PersonalAdministrativo(nombre);

                    if (!personas.Contains(adm))
                        DarDeAltaPersonalAdmin(adm);
                    break;
                case eOpcionMenu.ListarMedicos:
                    ListarMedicos();
                    break;
                case eOpcionMenu.ListaPacientesDeMedico:
                    Console.WriteLine("Escribe el nombre del medico para ver sus pacientes:");
                    nombre = Console.ReadLine().ToLower();

                    Medico m = (Medico)personas.Find(medico => medico.Nombre == nombre);

                    if(m != null)
                        ListaPacientesDeMedico(m);
                    break;
                case eOpcionMenu.EliminarPaciente:
                    Console.WriteLine("Escribe el nombre del paciente a eliminar:");
                    nombre = Console.ReadLine().ToLower();

                    Paciente p = (Paciente)personas.Find(paciente => paciente.Nombre == nombre);

                    if(p != null)
                        EliminarPaciente(p);
                    break;
                case eOpcionMenu.ListaPersonasHospital:
                    ListaPersonasHospital();
                    break;
            }
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
                if(p.GetType() == typeof(Medico))
                    Console.WriteLine(p.ToString());
            }
        }

        public void ListaPacientesDeMedico(Medico m)
        {
            m.ListaDePacientes();
        }

        public void EliminarPaciente(Paciente p)
        {
            personas.Remove(p);

            List<Persona> lista = new List<Persona>(personas);
            lista.RemoveAll(personas => personas.GetType() != typeof(Medico));

            foreach(Medico m in lista)
            {
                m.EliminarPaciente(p);
            }
        }

        public void ListaPersonasHospital()
        {
            foreach(Persona p in personas)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
