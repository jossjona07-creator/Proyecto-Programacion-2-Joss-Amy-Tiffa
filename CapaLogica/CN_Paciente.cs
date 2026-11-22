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

        public DataTable BuscarPacientes(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return objetoCD.ListarPacientes();
            }

            return objetoCD.BuscarPacientes(filtro.Trim());
        }

        public void InsertarPaciente(string identificacion, string nombre, string telefono, string correo, string fechaNacimiento)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Por favor escribe el nombre completo del paciente.");
            }

            if (string.IsNullOrWhiteSpace(identificacion))
            {
                throw new ArgumentException("Por favor escribe la cédula o identificación del paciente.");
            }

            objetoCD.InsertarPaciente(
                identificacion.Trim(),
                nombre.Trim(),
                telefono.Trim(),
                correo.Trim(),
                fechaNacimiento.Trim()
            );
        }

        public void ActualizarPaciente(int idPaciente, string identificacion, string nombre, string telefono, string correo, string fechaNacimiento)
        {
            if (idPaciente <= 0)
            {
                throw new ArgumentException("Primero selecciona un paciente de la tabla para poder actualizarlo.");
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Por favor escribe el nombre completo del paciente.");
            }

            if (string.IsNullOrWhiteSpace(identificacion))
            {
                throw new ArgumentException("Por favor escribe la cédula o identificación del paciente.");
            }

            objetoCD.ActualizarPaciente(
                idPaciente,
                identificacion.Trim(),
                nombre.Trim(),
                telefono.Trim(),
                correo.Trim(),
                fechaNacimiento.Trim()
            );
        }

        public void EliminarPaciente(int idPaciente)
        {
            if (idPaciente <= 0)
            {
                throw new ArgumentException("Primero selecciona un paciente de la tabla para poder eliminarlo.");
            }

            objetoCD.EliminarPaciente(idPaciente);
        }
    }
}