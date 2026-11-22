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
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = "SELECT IdPaciente, Identificacion, Nombre, Telefono, Correo, FechaNacimiento FROM pacientes ORDER BY IdPaciente DESC";
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
            }
            catch (MySqlException ex)
            {
                throw new Exception("No se pudo cargar la lista de pacientes desde la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los pacientes: " + ex.Message);
            }
            return tabla;
        }

        public DataTable BuscarPacientes(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = @"SELECT IdPaciente, Identificacion, Nombre, Telefono, Correo, FechaNacimiento
                                     FROM pacientes
                                     WHERE Identificacion LIKE @Filtro OR Nombre LIKE @Filtro
                                     ORDER BY IdPaciente DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                        if (conexion.State == ConnectionState.Closed)
                            conexion.Open();

                        using (MySqlDataReader lector = cmd.ExecuteReader())
                        {
                            tabla.Load(lector);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("No se pudo realizar la búsqueda en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al buscar el paciente: " + ex.Message);
            }
            return tabla;
        }

        public void InsertarPaciente(string identificacion, string nombre, string telefono, string correo, string fechaNacimiento)
        {
            try
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
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("Esa identificación ya está registrada con otro paciente.");
                }
                throw new Exception("No se pudo guardar el paciente en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al guardar el paciente: " + ex.Message);
            }
        }

        public void ActualizarPaciente(int idPaciente, string identificacion, string nombre, string telefono, string correo, string fechaNacimiento)
        {
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = @"UPDATE pacientes
                                     SET Identificacion = @Identificacion,
                                         Nombre = @Nombre,
                                         Telefono = @Telefono,
                                         Correo = @Correo,
                                         FechaNacimiento = @FechaNacimiento
                                     WHERE IdPaciente = @IdPaciente";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);
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
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("Ya existe otro paciente con esa misma identificación.");
                }
                throw new Exception("No se pudieron actualizar los datos en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al actualizar el paciente: " + ex.Message);
            }
        }

        public void EliminarPaciente(int idPaciente)
        {
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = "DELETE FROM pacientes WHERE IdPaciente = @IdPaciente";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                        if (conexion.State == ConnectionState.Closed)
                            conexion.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1451)
                {
                    throw new Exception("No puedes borrar este paciente porque ya tiene citas o historial registrado.");
                }
                throw new Exception("No se pudo eliminar el paciente en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al eliminar el paciente: " + ex.Message);
            }
        }
    }
}