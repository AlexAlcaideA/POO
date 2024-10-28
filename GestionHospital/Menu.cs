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
            Salir = 0,
            DarDeAltaMedico = 1,
            DarDeAltaPaciente = 2,
            DarDeAltaPersonalAdmin = 3,
            AñadirCitaMedica = 4,
            AñadirTratamiento = 5,
            AñadirDiagnostico = 6,
            ModificarDatosMedico = 7,
            ModificarDatosPaciente = 8,
            ModificarDatosAdmin = 9,
            ModificarCita = 10,
            ModificarTratamiento = 11,
            ModificarDiagnostico = 12,
            ListarMedicos = 13,
            ListaPacientesDeMedico = 14,
            ListaCitasMedico = 15,
            ListaPersonasHospital = 16,
            ListaDeCitas = 17,
            ListaHistorialPaciente = 18,
            EliminarPaciente = 19,
            EliminarCita = 20
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
4) Añadir cita medica
5) Añadir tratamiento a paciente
6) Añadir diagnostico a paciente
7) Modificar datos de Medico
8) Modificar datos de Paciente
9) Modificar datos de Personal Administrativo
10) Modificar cita
11) Modificar tratamiendo del paciente
12) Modificar diagnostico del paciente
13) Listar Medicos
14) Lista de Pacientes de un Medico
15) Lista citas de un medico
16) Lista de Personas del Hospital
17) Lista de citas
18) Lista historial medico de paciente
19) Eliminar Paciente
20) Eliminar cita medica
0) Salir");

                string opcionUsuario = Console.ReadLine();
                eOpcionMenu menu;


                if (!Enum.TryParse(opcionUsuario, out menu))
                    continue;
                else if (menu == eOpcionMenu.Salir)
                    break;

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
                case eOpcionMenu.AñadirCitaMedica:
                    AñadirCitaMedica();
                    break;
                case eOpcionMenu.AñadirTratamiento:
                    AñadirTratamientoMedico();
                    break;
                case eOpcionMenu.AñadirDiagnostico:
                    AñadirDiagnostico();
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
                case eOpcionMenu.ModificarCita:
                    ModificarCita();
                    break;
                case eOpcionMenu.ModificarTratamiento:
                    ModificarTratamientoAPaciente();
                    break;
                case eOpcionMenu.ModificarDiagnostico:
                    ModificarDiagnosticoAPaciente();
                    break;
                case eOpcionMenu.ListarMedicos:
                    hospital.ListarMedicos();
                    break;
                case eOpcionMenu.ListaPacientesDeMedico:
                    ListarPacientesMedico();
                    break;
                case eOpcionMenu.ListaCitasMedico:
                    ListaDeCitasDeMedico();
                    break;
                case eOpcionMenu.ListaPersonasHospital:
                    hospital.ListaPersonasHospital();
                    break;
                case eOpcionMenu.ListaDeCitas:
                    hospital.ListaCitas();
                    break;
                case eOpcionMenu.ListaHistorialPaciente:
                    ListaHistorialPaciente();
                    break;
                case eOpcionMenu.EliminarPaciente:
                    EliminarPaciente();
                    break;
                case eOpcionMenu.EliminarCita:
                    EliminarCita();
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
                hospital.DarDeAlta(med);
        }

        private void DarAltaPaciente()
        {
            string nombre = ObtenerTexto("Escribe el nombre del paciente:").ToLower();

            string enfermedad = ObtenerTexto("Escribe el nombre de la enfermedad:");

            Paciente pacienteTemporal = hospital.EncontrarPerosnaPorNombre<Paciente>(nombre);

            ushort? edad = ObtenerAños("Edad del paciente:");

            Console.WriteLine("Personal medico:");
            hospital.ListarMedicos();

            string nombreDoc = ObtenerTexto("Escribe el nombre del medico:").ToLower();
            Medico medicoTemp = hospital.EncontrarPerosnaPorNombre<Medico>(nombreDoc);

            if (!hospital.ContienePersona(pacienteTemporal) && hospital.ContienePersona(medicoTemp) &&
                edad != null)
            {
                Paciente pac = new Paciente(nombre, medicoTemp, edad.Value, enfermedad);

                hospital.DarDeAlta(pac);
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
                hospital.DarDeAlta(adm);
        }

        public void AñadirCitaMedica()
        {
            DateTime? fecha = ObtenerFecha("Inserte la fecha y hora, formato: dd/mm/yyyy hh:mm");

            Console.WriteLine("Lista de medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Escribe el nombre del medico:");

            Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            Console.WriteLine("Lista de pacientes:");
            hospital.ListaPacientes();
            string nombrePaciente = ObtenerTexto("Escribe el nombre del paciente:");

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            if (fecha != null && med != null && pac != null)
                hospital.AñadirCitaMedica(fecha.Value, med, pac);
        }

        public void AñadirTratamientoMedico()
        {
            DateTime? fecha = ObtenerFecha("Inserte la fecha y hora, formato: dd/mm/yyyy");

            Console.WriteLine("Lista de medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Escribe el nombre del medico:");

            Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            Console.WriteLine("Lista de pacientes:");
            hospital.ListaPacientes();
            string nombrePaciente = ObtenerTexto("Escribe el nombre del paciente:");

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            string tratamiento = ObtenerTexto("Descripcion del tratamiento:");

            string medicamento = ObtenerTexto("Medicamentos necesarios:");

            if (fecha != null && med != null && pac != null)
                hospital.AñadirTratamientoPaciente(fecha.Value, tratamiento, medicamento, med, pac);
        }

        public void AñadirDiagnostico()
        {
            DateTime? fecha = ObtenerFecha("Inserte la fecha y hora, formato: dd/mm/yyyy");

            Console.WriteLine("Lista de medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Escribe el nombre del medico:");

            Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            Console.WriteLine("Lista de pacientes:");
            hospital.ListaPacientes();
            string nombrePaciente = ObtenerTexto("Escribe el nombre del paciente:");

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            string notas = ObtenerTexto("Nota del medico:");

            if (fecha != null && med != null && pac != null)
                hospital.AñadirDiagnosticoPaciente(fecha.Value, notas, med, pac);
        }

        public void ModificarDatosMedico()
        {
            Console.WriteLine("Lista medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Nombre del medico a modificar:");

            Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            if (med != null)
            {
                Console.WriteLine($"El medico actual es: {med.ToString()}");

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

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            if(pac != null)
            {
                Console.WriteLine($"El paciente es: {pac.ToString()}");

                string nombre = ObtenerTexto("Nombre del paciente:");

                Console.WriteLine("Lista de medicos:");
                hospital.ListarMedicos();

                string nombreMedico = ObtenerTexto("Nombre del medico al que asignar");
                Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

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

            PersonalAdministrativo tempAdministrativo = hospital.EncontrarPerosnaPorNombre<PersonalAdministrativo>(nombrePersonal);

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

        public void ModificarCita()
        {
            Console.WriteLine("Lista de citas:");
            hospital.ListaCitas();

            DateTime? fecha = ObtenerFecha("Escribe la fecha de la cita que buscas, de formato dd/mm/yyyy hh:mm");

            if (fecha == null)
                return;

            Cita tempCita = hospital.EncontrarCitaPorFecha(fecha.Value);

            if (tempCita == null)
                return;

            Console.WriteLine($@"
Datos de la cita:
{tempCita.ToString()}");

            fecha = ObtenerFecha("Escribe la nueva fecha, de formato dd/mm/yyyy hh:mm");

            Console.WriteLine("Lista medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Escribe el nombre del nuevo medico:");

            Console.WriteLine("Lista pacientes:");
            hospital.ListaPacientes();
            string nombrePaciente = ObtenerTexto("Escribe el nombre del nuevo paciente:");

            Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            if (fecha != null && med != null && pac != null)
                tempCita.ModificarCita(fecha.Value, med, pac);
        }

        public void ModificarTratamientoAPaciente()
        {
            Console.WriteLine("Lista de paciente:");
            hospital.ListaPacientes();

            string nombrePaciente = ObtenerTexto("Escribe el nombre del paciente al que modificar su tratamiento:");

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            if (pac == null)
                return;

            Console.WriteLine($@"
Lista de tratamientos del paciente:
{pac.ListaTratamientos()}");

            DateTime? fechaTratamiento = ObtenerFecha("Escriba la fecha del tratamiento a modificar, formato: dd/mm/yyyy");
            
            if(fechaTratamiento == null || !pac.HayTratamientoEnFecha(fechaTratamiento.Value))
                return;

            Tratamiento trat = pac.ObtenTratamientoEnFecha(fechaTratamiento.Value);

            Console.WriteLine($@"
Tratamiento:
{trat.ToString()}");

            Console.WriteLine("Lista medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Nombre del nuevo medico:");

            Medico tempMedico = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            string tratamiento = ObtenerTexto("Nuevo tratamiento:");

            string medicina = ObtenerTexto("Nuevas medicinas:");

            if (tempMedico != null)
                pac.ModificarTratamiento(fechaTratamiento.Value, tempMedico, tratamiento, medicina);
        }

        public void ModificarDiagnosticoAPaciente()
        {
            Console.WriteLine("Lista de paciente:");
            hospital.ListaPacientes();

            string nombrePaciente = ObtenerTexto("Escribe el nombre del paciente al que modificar su diagnostico:");

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            if (pac == null)
                return;

            Console.WriteLine($@"
Lista de diagnosticos del paciente:
{pac.ListaDiagnosticos()}");

            DateTime? fechaDiagnostico = ObtenerFecha("Escriba la fecha del diagnostico a modificar, formato: dd/mm/yyyy");

            if (fechaDiagnostico == null || !pac.HayDiagnosticoEnFecha(fechaDiagnostico.Value))
                return;

            Diagnostico diag = pac.ObtenDiagnosticoEnFecha(fechaDiagnostico.Value);

            Console.WriteLine($@"
Diagnostico:
{diag.ToString()}");

            Console.WriteLine("Lista medicos:");
            hospital.ListarMedicos();
            string nombreMedico = ObtenerTexto("Nombre del nuevo medico:");

            Medico tempMedico = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            string notas = ObtenerTexto("Nuevas notas:");

            if (tempMedico != null)
                pac.ModificarDiagnostico(fechaDiagnostico.Value, tempMedico, notas);
        }

        private void ListarPacientesMedico()
        {
            Console.WriteLine("Lista medicos:");
            hospital.ListarMedicos();

            string nombre = ObtenerTexto("Escribe el nombre del medico para ver sus pacientes:").ToLower();

            Medico m = hospital.EncontrarPerosnaPorNombre<Medico>(nombre);

            if (m != null)
                hospital.ListaPacientesDeMedico(m);
        }

        public void ListaDeCitasDeMedico()
        {
            Console.WriteLine("Lista de medicos:");
            hospital.ListarMedicos();

            string nombreMedico = ObtenerTexto("Escribe el nombre del medico para saber sus citas:");

            Medico med = hospital.EncontrarPerosnaPorNombre<Medico>(nombreMedico);

            if (med != null)
                hospital.ListaDeCitasDelMedico(med);
        }

        public void ListaHistorialPaciente()
        {
            Console.WriteLine("Lista de pacientes:");
            hospital.ListaPacientes();

            string nombrePaciente = ObtenerTexto("Escribe el nombre del paciente para mirar su historial:");

            Paciente pac = hospital.EncontrarPerosnaPorNombre<Paciente>(nombrePaciente);

            if (pac != null)
                Console.WriteLine(pac.ListaHistorialMed());
        }

        private void EliminarPaciente()
        {
            Console.WriteLine("Lista pacientes:");
            hospital.ListaPacientes();

            Console.WriteLine("Escribe el nombre del paciente a eliminar:");
            string nombre = Console.ReadLine().ToLower();

            Paciente p = hospital.EncontrarPerosnaPorNombre<Paciente>(nombre);

            if (p != null)
                hospital.EliminarPaciente(p);
        }

        public void EliminarCita()
        {
            Console.WriteLine("Lista de citas:");
            hospital.ListaCitas();

            DateTime? fecha = ObtenerFecha("Escribe la fecha de la cita que buscas, de formato dd/mm/yyyy hh:mm");

            if (fecha == null)
                return;

            Cita tempCita = hospital.EncontrarCitaPorFecha(fecha.Value);

            hospital.EliminarCita(tempCita);
        }

        /// <summary>
        /// Devuelve un ushort del valor que ponga el usuario por la consola.
        /// Tambien se escribe en la consola la pregunta que se le hace
        /// </summary>
        /// <param name="pregunta"></param>
        /// <returns></returns>
        private ushort? ObtenerAños(string pregunta)
        {
            Console.WriteLine(pregunta);

            ushort año;

            if (ushort.TryParse(Console.ReadLine(), out año))
                return año;
            else
                return null;
        }

        /// <summary>
        /// Se hace una pregunta y devuelve lo escrito en la consola.
        /// </summary>
        /// <param name="pregunta"></param>
        /// <returns></returns>
        private string ObtenerTexto(string pregunta)
        {
            Console.WriteLine(pregunta);

            return Console.ReadLine();
        }

        /// <summary>
        /// Obtiene una fecha con un Console.ReadLine, de la cual se le hace un TryParse.
        /// Se le pasa un string que se muestra en consola que es lo que pregunta por la fecha
        /// </summary>
        /// <param name="pregunta"></param>
        /// <returns></returns>
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
