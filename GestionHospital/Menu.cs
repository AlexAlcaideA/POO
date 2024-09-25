using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHospital
{
    internal class Menu
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

        private Hospital hospital;

        public Menu()
        {
            hospital = new Hospital();
        }

        public void MenuLoop()
        {
            do
            {
                Console.WriteLine(@"
Que opcion desea escoger:
1) Dar de alta al Medico
2) Dar de alta al Paciente
3) Dar de alta al Personal Administrativo
4) Listar Medicos
5) Lista de Pacientes de un Medico
6) Eliminar Paciente
7) Lista de Personas del Hospital
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

                    ushort? añosExperiencia = ObtenerAños("Cuantos años de experiencia:");

                    Medico med;

                    if (añosExperiencia != null)
                        med = new Medico(nombre, añosExperiencia.Value);
                    else
                        break;

                    if (!hospital.ContienePersona(med))
                        hospital.DarDeAltaMedico(med);
                    break;
                case eOpcionMenu.DarDeAltaPaciente:
                    nombre = ObtenerTexto("Escribe el nombre del paciente:").ToLower();

                    string enfermedad = ObtenerTexto("Escribe el nombre de la enfermedad:");

                    Paciente pacienteTemporal = hospital.EncontrarPacientePorNombre(nombre);

                    Console.WriteLine("Personal medico:");
                    hospital.ListarMedicos();

                    Console.WriteLine("Escribe el nombre del medico:");
                    string nombreDoc = Console.ReadLine().ToLower();
                    Medico medicoTemp = hospital.EncontrarMedicoPorNombre(nombreDoc);

                    if (!hospital.ContienePersona(pacienteTemporal) && hospital.ContienePersona(medicoTemp))
                    {
                        Paciente pac = new Paciente(nombre, medicoTemp, enfermedad);

                        hospital.DarDeAltaPaciente(pac);
                        medicoTemp.AñadirPaciente(pac);
                    }

                    break;
                case eOpcionMenu.DarDeAltaPersonalAdmin:
                    nombre = ObtenerTexto("Escribe el nombre del administrativo:").ToLower();

                    ushort? añosContrato = ObtenerAños("Años de contrato:");

                    PersonalAdministrativo adm;

                    if (añosContrato != null)
                        adm = new PersonalAdministrativo(nombre, añosContrato.Value);
                    else
                        break;

                    if (!hospital.ContienePersona(adm))
                        hospital.DarDeAltaPersonalAdmin(adm);
                    break;
                case eOpcionMenu.ListarMedicos:
                    hospital.ListarMedicos();
                    break;
                case eOpcionMenu.ListaPacientesDeMedico:
                    hospital.ListarMedicos();

                    nombre = ObtenerTexto("Escribe el nombre del medico para ver sus pacientes:").ToLower();

                    Medico m = hospital.EncontrarMedicoPorNombre(nombre);

                    if (m != null)
                        hospital.ListaPacientesDeMedico(m);
                    break;
                case eOpcionMenu.EliminarPaciente:
                    Console.WriteLine("Lsita pacientes:");
                    hospital.ListaPacientes();

                    Console.WriteLine("Escribe el nombre del paciente a eliminar:");
                    nombre = Console.ReadLine().ToLower();

                    Paciente p = hospital.EncontrarPacientePorNombre(nombre);

                    if (p != null)
                        hospital.EliminarPaciente(p);
                    break;
                case eOpcionMenu.ListaPersonasHospital:
                    hospital.ListaPersonasHospital();
                    break;
            }
        }

        private ushort? ObtenerAños(string pregunta)
        {
            Console.WriteLine(pregunta);

            ushort año;

            if (ushort.TryParse(Console.ReadLine(), out año))
                return año;
            else
                return null;
        }

        private string ObtenerTexto(string pregunta)
        {
            Console.WriteLine(pregunta);

            return Console.ReadLine();
        }

    }
}
