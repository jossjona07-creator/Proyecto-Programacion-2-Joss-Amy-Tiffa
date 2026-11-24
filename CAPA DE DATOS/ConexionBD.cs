using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class ConexionBD
    {
        public MySqlConnection ObtenerConexion()
        {
            ConnectionStringSettings configuracion = ConfigurationManager.ConnectionStrings["AgendaMedica"];
            if (configuracion == null || string.IsNullOrWhiteSpace(configuracion.ConnectionString))
            {
                throw new InvalidOperationException("No está configurada la conexión 'AgendaMedica'. Revise CapaPresentacion/App.config.");
            }

            MySqlConnection conexion = new MySqlConnection(configuracion.ConnectionString);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                conexion.Dispose();
                throw new InvalidOperationException("No se pudo conectar a MySQL. Verifique que el servicio esté iniciado, que exista AgendaMedicaDB y que los datos de acceso estén correctos.", ex);
            }
        }
    }
}
