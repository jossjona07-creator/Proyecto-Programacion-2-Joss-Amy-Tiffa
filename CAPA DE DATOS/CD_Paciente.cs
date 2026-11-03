using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Paciente
    {
        // Método temporal de prueba mientras se conecta la base de datos
        public DataTable ListarPacientes()
        {
            DataTable tabla = new DataTable();

            // Creamos columnas de ejemplo para que la tabla las muestre en pantalla
            tabla.Columns.Add("IdPaciente", typeof(int));
            tabla.Columns.Add("Cedula", typeof(string));
            tabla.Columns.Add("Nombre", typeof(string));
            tabla.Columns.Add("Apellido", typeof(string));
            tabla.Columns.Add("Telefono", typeof(string));

            // Agregamos un registro de prueba
            tabla.Rows.Add(1, "101110222", "Juan", "Pérez", "8888-8888");

            return tabla;
        }
    }
}