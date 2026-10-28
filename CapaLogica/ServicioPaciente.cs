using System.Collections.Generic;

namespace AGENDA_MEDICA
{
    public class ServicioPaciente
    {
        // Lista en memoria para almacenar los pacientes
        private List<Paciente> listaPacientes = new List<Paciente>();

        // Para agregar un paciente nuevo a la lista
        public void AgregarPaciente(Paciente nuevoPaciente)
        {
            listaPacientes.Add(nuevoPaciente);
        }

        // Obtener la lista completa y mostrarla en un DataGridView
        public List<Paciente> ObtenerPacientes()
        {
            return listaPacientes;
        }
    }
}