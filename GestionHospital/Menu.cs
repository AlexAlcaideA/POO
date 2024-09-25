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
            ModificarDatosMedico = 4,
            ModificarDatosPaciente = 5,
            ModificarDatosAdmin = 6,
            ListarMedicos = 7,
            ListaPacientesDeMedico = 8,
            EliminarPaciente = 9,
            ListaPersonasHospital = 10
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
4) Modificar datos de Medico
5) Modificar datos de Paciente
6) Modificar datos de Personal Administrativo
7) Listar Medicos
8) Lista de Pacientes de un Medico
9) Eliminar Paciente
10) Lista de Personas del Hospital
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

        private void MenuOpcionSeleccionada(eOpcionMenu menu)
        {
            switch (menu)
            {
                case eOpcionMenu.DarDeAltaMedico:
                    DarAltaMedico();
                    break;
                case eOpcionMenu.DarDeAltaPaciente:
                    DarAltaPaciente();
                    break;
                case eOpcionMenu.DarDeAltaPersonalAdmin:
                    DarAltaPersonalAdministrativo();
                    break;
                case eOpcionMenu.ModificarDatosMedico:
                    ModificarDatosMedico();
                    break;
                case eOpcionMenu.ModificarDatosPaciente:
                    ModificarDatosPaciente();
                    break;
                case eOpcionMenu.ModificarDatosAdmin:
                    ModificarDatosAdministrativo();
                    break;
                case eOpcionMenu.ListarMedicos:
                    hospital.ListarMedicos();
                    break;
                case eOpcionMenu.ListaPacientesDeMedico:
                    ListarPacientesMedico();
                    break;
                case eOpcionMenu.EliminarPaciente:
                    EliminarPaciente();
                    break;
                case eOpcionMenu.ListaPersonasHospital:
                    hospital.ListaPersonasHospital();
                    break;
            }
        }

        private void DarAltaMedico()
        {
            Console.WriteLine("Escribe el nombre del medico:");
            string nombre = Console.ReadLine().ToLower();

            ushort? añosExperiencia = ObtenerAños("Cuantos años de experiencia:");

            string especialidad = ObtenerTexto("Nombre de la especialidad:");

            Medico med;

            if (añosExperiencia != null)
                med = new Medico(nombre, especialidad, añosExperiencia.Value);
            else
                return;

            if (!hospital.ContienePersona(med))
                hospital.DarDeAltaMedico(med);
        }

        private void DarAltaPaciente()
        {
            string nombre = ObtenerTexto("Escribe el nombre del paciente:").ToLower();

            string enfermedad = ObtenerTexto("Escribe el nombre de la enfermedad:");

            Paciente pacienteTemporal = hospital.EncontrarPacientePorNombre(nombre);

            ushort? edad = ObtenerAños("Edad del paciente:");

            Console.WriteLine("Personal medico:");
            hospital.ListarMedicos();

            string nombreDoc = ObtenerTexto("Escribe el nombre del medico:").ToLower();
            Medico medicoTemp = hospital.EncontrarMedicoPorNombre(nombreDoc);

            if (!hospital.ContienePersona(pacienteTemporal) && hospital.ContienePersona(medicoTemp) &&
                edad != null)
            {
                Paciente pac = new Paciente(nombre, medicoTemp, edad.Value, enfermedad);

                hospital.DarDeAltaPaciente(pac);
                medicoTemp.AñadirPaciente(pac);
            }
        }

        private void DarAltaPersonalAdministrativo()
        {
            string nombre = ObtenerTexto("Escribe el nombre del administrativo:").ToLower();

            DateTime? fechaContratacion = ObtenerFecha("Cuando lo contrataron, formato(dd/mm/yyyy):");
            string puesto = ObtenerTexto("Nombre del puesto");

            PersonalAdministrativo adm;

            if (fechaContratacion != null)
                adm = new PersonalAdministrativo(nombre, fechaContratacion.Value, puesto);
            else
                return;

            if (!hospital.ContienePersona(adm))
                hospital.DarDeAltaPersonalAdmin(adm);
        }

        public void ModificarDatosMedico()
        {
            Console.WriteLine("Lista medicos:");
            hospital.ListarMedicos();

            string nombreMedico = ObtenerTexto("Nombre del medico a modificar:");

            Medico med = hospital.EncontrarMedicoPorNombre(nombreMedico);

            if (med != null)
            {
                Console.WriteLine($"El medico es: {med.ToString()}");

                string nuevoNombre = ObtenerTexto("Nombre del medico:");
                string especialidad = ObtenerTexto("Escribe la especialidad del medico:");
                ushort? añosExp = ObtenerAños("Escribe cuantos años de experiencia tiene:");

                if (añosExp.HasValue)
                    med.ModificarDatos(nuevoNombre, especialidad, añosExp.Value);
                else
                    return;
            }
        }

        private void ModificarDatosPaciente()
        {
            Console.WriteLine("Lista de pacientes:");
            hospital.ListaPacientes();

            string nombrePaciente = ObtenerTexto("Nombre paciente a modificar:");

            Paciente pac = hospital.EncontrarPacientePorNombre(nombrePaciente);

            if(pac != null)
            {
                Console.WriteLine($"El paciente es: {pac.ToString()}");

                string nombre = ObtenerTexto("Nombre del paciente:");

                string nombreMedico = ObtenerTexto("Nombre del medico al que asignar");
                Medico med = hospital.EncontrarMedicoPorNombre(nombreMedico);

                ushort? edad = ObtenerAños("Edad del paciente:");

                string enfermedad = ObtenerTexto("Enfermedad del paciente:");

                if (med != null && edad != null)
                    pac.EliminarseDelMedico();
                else
                    return;

                pac.ModificarPaciente(nombre, med, edad.Value, enfermedad);
            }
        }

        private void ModificarDatosAdministrativo()
        {

            Console.WriteLine("Lista de administrativos:");
            hospital.ListaAdministrativos();

            string nombrePersonal = ObtenerTexto("Nombre persona Administrativa a modificar:");

            PersonalAdministrativo tempAdministrativo = hospital.EncontrarPersonalAdministrativoPorNombre(nombrePersonal);

            if (tempAdministrativo != null)
            {
                Console.WriteLine($"El personal Administrativo es: {tempAdministrativo.ToString()}");

                string nombre = ObtenerTexto("Nombre:");

                DateTime? fecha = ObtenerFecha("Fecha que lo contrataron, formato(dd/mm/yyyy):");

                string puesto = ObtenerTexto("Puesto en el que trabaja:");

                if (fecha != null)
                    tempAdministrativo.ModificarDatosAdministrativo(nombre, fecha.Value, puesto);
            }
        }

        private void ListarPacientesMedico()
        {
            hospital.ListarMedicos();

            string nombre = ObtenerTexto("Escribe el nombre del medico para ver sus pacientes:").ToLower();

            Medico m = hospital.EncontrarMedicoPorNombre(nombre);

            if (m != null)
                hospital.ListaPacientesDeMedico(m);
        }

        private void EliminarPaciente()
        {
            Console.WriteLine("Lista pacientes:");
            hospital.ListaPacientes();

            Console.WriteLine("Escribe el nombre del paciente a eliminar:");
            string nombre = Console.ReadLine().ToLower();

            Paciente p = hospital.EncontrarPacientePorNombre(nombre);

            if (p != null)
                hospital.EliminarPaciente(p);
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

        private DateTime? ObtenerFecha(string pregunta)
        {
            Console.WriteLine(pregunta);

            DateTime fecha;

            if (DateTime.TryParse(Console.ReadLine(), out fecha))
                return fecha;
            else
                return null;
        }

    }
}
