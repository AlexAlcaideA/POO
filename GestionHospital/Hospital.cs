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

        public T EncontrarPerosnaPorNombre<T>(string nombre) where T : Persona
        {
            return personas.Find(pers => pers.Nombre == nombre && pers is T) as T;
        }

        public Cita EncontrarCitaPorFecha(DateTime fecha)
        {
            return citasMedicas.Find(c => c.Fecha == fecha);
        }

        public bool ContienePersona(Persona persona)
        {
            return personas.Contains(persona);
        }

        public void DarDeAlta(Persona p)
        {
            personas.Add(p);
        }

        public void AñadirCitaMedica(DateTime fecha, Medico med, Paciente pac)
        {
            Cita cita = new Cita(fecha, med, pac);

            citasMedicas.Add(cita);
            pac.AñadirHistorialMedico(cita);
            med.AñadirCitaMedica(cita);
        }

        public void AñadirTratamientoPaciente(DateTime fecha, string tratamiento, string medicamento, Medico med, Paciente pac)
        {
            pac.AñadirHistorialMedico(new Tratamiento(fecha,  med, tratamiento, medicamento));
        }

        public void AñadirDiagnosticoPaciente(DateTime fecha, string notas, Medico med, Paciente pac)
        {
            pac.AñadirHistorialMedico(new Diagnostico(fecha, med, notas));
        }

        public void ModificarCitaMedica(Cita citaModificar, DateTime fechaNueva, Medico medNuevo = null, Paciente pacNuevo = null)
        {
            Console.WriteLine(citaModificar);

            citaModificar.ModificarCita(fechaNueva, medNuevo, pacNuevo);
        }

        public void EliminarCita(Cita citaEliminar)
        {
            citaEliminar.EliminarCita();
            citasMedicas.Remove(citaEliminar);
        }

        public void EliminarPaciente(Paciente p)
        {
            personas.Remove(p);

            p.EliminarseDelMedico();
        }

        public void ListarMedicos()
        {
            foreach (Persona p in personas)
            {
                if (p is Medico)
                    Console.WriteLine(p);
            }
        }

        public void ListaPacientes()
        {
            foreach(Persona p in personas)
            {
                if(p is Paciente)
                {
                    Console.WriteLine(p);
                }
            }
        }

        public void ListaAdministrativos()
        {
            foreach (Persona p in personas)
            {
                if (p is PersonalAdministrativo)
                {
                    Console.WriteLine(p);
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
                Console.WriteLine($@"{p}, esta persona es un {p.GetType().Name}");
            }
        }

        public void ListaCitas()
        {
            foreach(Cita c in citasMedicas)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void ListaDeCitasDelMedico(Medico m)
        {
            Console.WriteLine(m.ListaDeCitasMedicas());
        }
    }
}
