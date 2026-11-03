using System;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class ConexionBD
    {
        private string cadenaConexion = "Server=localhost;Database=AgendaMedicaDB;Uid=root;Pwd=;";

        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }
            return conexion;
        }
    }
}