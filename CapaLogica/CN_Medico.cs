using System;
using System.Data;
using System.Globalization;
using CapaDatos;

namespace CapaLogica
{
    public class CN_Medico
    {
        private CD_Medico objetoCD = new CD_Medico();

        public DataTable ListarMedicos()
        {
            return objetoCD.ListarMedicos();
        }

        public DataTable BuscarMedicos(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return objetoCD.ListarMedicos();
            }

            return objetoCD.BuscarMedicos(filtro.Trim());
        }

        public void InsertarMedico(string identificacion, string nombre, string telefono, string correo, string horarioAtencion, string estado)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                throw new ArgumentException("Por favor escribe la identificación del médico.");
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Por favor escribe el nombre del médico.");
            }

            if (string.IsNullOrWhiteSpace(horarioAtencion))
            {
                throw new ArgumentException("Por favor indica el horario de atención (por ejemplo, 08:00-16:00).");
            }

            ValidarHorario(horarioAtencion);

            string estadoFinal = string.IsNullOrWhiteSpace(estado) ? "Activo" : estado.Trim();

            objetoCD.InsertarMedico(
                identificacion.Trim(),
                nombre.Trim(),
                telefono.Trim(),
                correo.Trim(),
                horarioAtencion.Trim(),
                estadoFinal
            );
        }

        public void ActualizarMedico(int idMedico, string identificacion, string nombre, string telefono, string correo, string horarioAtencion, string estado)
        {
            if (idMedico <= 0)
            {
                throw new ArgumentException("Primero selecciona un médico de la tabla para actualizarlo.");
            }

            if (string.IsNullOrWhiteSpace(identificacion))
            {
                throw new ArgumentException("Por favor escribe la identificación del médico.");
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Por favor escribe el nombre del médico.");
            }

            if (string.IsNullOrWhiteSpace(horarioAtencion))
            {
                throw new ArgumentException("Por favor indica el horario de atención.");
            }

            ValidarHorario(horarioAtencion);

            string estadoFinal = string.IsNullOrWhiteSpace(estado) ? "Activo" : estado.Trim();

            objetoCD.ActualizarMedico(
                idMedico,
                identificacion.Trim(),
                nombre.Trim(),
                telefono.Trim(),
                correo.Trim(),
                horarioAtencion.Trim(),
                estadoFinal
            );
        }

        public void EliminarMedico(int idMedico)
        {
            if (idMedico <= 0)
            {
                throw new ArgumentException("Primero selecciona un médico de la tabla para eliminarlo.");
            }

            objetoCD.EliminarMedico(idMedico);
        }

        private static void ValidarHorario(string horario)
        {
            string[] partes = (horario ?? string.Empty).Split('-');
            TimeSpan inicio;
            TimeSpan fin;
            if (partes.Length != 2 ||
                !TimeSpan.TryParseExact(partes[0].Trim(), @"hh\:mm", CultureInfo.InvariantCulture, out inicio) ||
                !TimeSpan.TryParseExact(partes[1].Trim(), @"hh\:mm", CultureInfo.InvariantCulture, out fin) ||
                fin <= inicio)
            {
                throw new ArgumentException("El horario debe tener formato HH:mm-HH:mm, por ejemplo 08:00-16:00.");
            }
        }
    }
}
