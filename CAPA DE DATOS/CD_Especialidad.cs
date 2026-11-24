using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Especialidad
    {
        private ConexionBD conexionBD = new ConexionBD();

        // Lista las especialidades.
        public DataTable Listar()
        {
            DataTable tabla = new DataTable();
            MySqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = "SELECT IdEspecialidad, NombreEspecialidad, Descripcion FROM especialidades ORDER BY NombreEspecialidad ASC;";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
                adaptador.Fill(tabla);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar especialidades: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return tabla;
        }

        // Revisa si el nombre ya existe.
        public bool ExisteNombre(string nombre, int? idExcluir = null)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = "SELECT COUNT(*) FROM especialidades WHERE LOWER(TRIM(NombreEspecialidad)) = LOWER(TRIM(@Nombre))";

                if (idExcluir.HasValue)
                {
                    query += " AND IdEspecialidad <> @IdExcluir";
                }

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Nombre", nombre);

                if (idExcluir.HasValue)
                {
                    cmd.Parameters.AddWithValue("@IdExcluir", idExcluir.Value);
                }

                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                return cantidad > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar duplicado de especialidad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Guarda una especialidad nueva.
        public void Insertar(string nombreEspecialidad, string descripcion)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = "INSERT INTO especialidades (NombreEspecialidad, Descripcion) VALUES (@Nombre, @Descripcion);";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Nombre", nombreEspecialidad);
                cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar especialidad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Actualiza la especialidad.
        public void Actualizar(int idEspecialidad, string nombreEspecialidad, string descripcion)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = "UPDATE especialidades SET NombreEspecialidad = @Nombre, Descripcion = @Descripcion WHERE IdEspecialidad = @Id;";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Id", idEspecialidad);
                cmd.Parameters.AddWithValue("@Nombre", nombreEspecialidad);
                cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(descripcion) ? (object)DBNull.Value : descripcion);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar especialidad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Revisa si algún médico tiene asignada esta especialidad.
        public bool TieneMedicosAsociados(int idEspecialidad)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = "SELECT COUNT(*) FROM medicoespecialidad WHERE IdEspecialidad = @Id;";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Id", idEspecialidad);
                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                return cantidad > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar médicos asociados a la especialidad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        // Elimina una especialidad.
        public void Eliminar(int idEspecialidad)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = conexionBD.ObtenerConexion();
                string query = "DELETE FROM especialidades WHERE IdEspecialidad = @Id;";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Id", idEspecialidad);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                throw new Exception("No se puede eliminar la especialidad porque está asociada a uno o más médicos (RN-06).");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar especialidad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    }
}