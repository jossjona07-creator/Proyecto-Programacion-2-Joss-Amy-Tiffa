using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Paciente
    {
        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ListarPacientes()
        {
            DataTable tabla = new DataTable();
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string query = "SELECT * FROM pacientes";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    if (conexion.State == ConnectionState.Closed)
                        conexion.Open();

                    using (MySqlDataReader lector = cmd.ExecuteReader())
                    {
                        tabla.Load(lector);
                    }
                }
            }
            return tabla;
        }

        public void InsertarPaciente(string identificacion, string nombre, string telefono, string correo, string fechaNacimiento)
        {
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string query = "INSERT INTO pacientes (Identificacion, Nombre, Telefono, Correo, FechaNacimiento) VALUES (@Identificacion, @Nombre, @Telefono, @Correo, @FechaNacimiento)";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    DateTime fechaValida;
                    if (DateTime.TryParse(fechaNacimiento, out fechaValida))
                    {
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaValida);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);
                    }

                    if (conexion.State == ConnectionState.Closed)
                        conexion.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}