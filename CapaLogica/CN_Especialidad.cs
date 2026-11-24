using System;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class CN_Especialidad
    {
        private CD_Especialidad cdEspecialidad = new CD_Especialidad();

        public DataTable Listar()
        {
            return cdEspecialidad.Listar();
        }

        public void Guardar(Especialidad especialidad)
        {
            if (especialidad == null)
            {
                throw new ArgumentException("Los datos de la especialidad son obligatorios.");
            }

            string nombreLimpio = (especialidad.NombreEspecialidad ?? string.Empty).Trim();
            string descripcionLimpia = (especialidad.Descripcion ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(nombreLimpio))
            {
                throw new ArgumentException("El nombre de la especialidad es obligatorio.");
            }

            if (nombreLimpio.Length > 100)
            {
                throw new ArgumentException("El nombre de la especialidad no puede superar los 100 caracteres.");
            }

            if (especialidad.IdEspecialidad == 0)
            {
                if (cdEspecialidad.ExisteNombre(nombreLimpio))
                {
                    throw new InvalidOperationException("Ya existe una especialidad registrada con ese nombre.");
                }

                cdEspecialidad.Insertar(nombreLimpio, descripcionLimpia);
            }
            else
            {
                if (cdEspecialidad.ExisteNombre(nombreLimpio, especialidad.IdEspecialidad))
                {
                    throw new InvalidOperationException("Ya existe otra especialidad con ese nombre.");
                }

                cdEspecialidad.Actualizar(especialidad.IdEspecialidad, nombreLimpio, descripcionLimpia);
            }
        }

        public void Eliminar(int idEspecialidad)
        {
            if (idEspecialidad <= 0)
            {
                throw new ArgumentException("Debe seleccionar una especialidad válida para eliminar.");
            }

            // No borra una especialidad asignada a un médico.
            if (cdEspecialidad.TieneMedicosAsociados(idEspecialidad))
            {
                throw new InvalidOperationException("No se puede eliminar la especialidad porque tiene médicos asociados (RN-06).");
            }

            cdEspecialidad.Eliminar(idEspecialidad);
        }
    }
}