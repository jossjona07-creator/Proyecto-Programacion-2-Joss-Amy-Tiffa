using System;
using System.Data;
using CapaDatos; 

namespace CapaLogica
{
    public class CN_Paciente
    {
        private CD_Paciente objetoCD = new CD_Paciente();

        public DataTable ListarPacientes()
        {
            return objetoCD.ListarPacientes();
        }

        public void InsertarPaciente(string identificacion, string nombre, string telefono, string correo, string fechaNacimiento)
        {
            // Validación 
            if (string.IsNullOrWhiteSpace(identificacion) || string.IsNullOrWhiteSpace(nombre))
            {
                throw new Exception("La identificación y el nombre son obligatorios.");
            }

            
            objetoCD.InsertarPaciente(identificacion, nombre, telefono, correo, fechaNacimiento);
        }
    }
}