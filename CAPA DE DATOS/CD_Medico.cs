using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Medico
    {
        private ConexionBD conexionBD = new ConexionBD();

        public DataTable ListarMedicos()
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = @"SELECT IdMedico, Identificacion, Nombre, Telefono, Correo, HorarioAtencion, Estado
                                     FROM medicos
                                     ORDER BY IdMedico DESC";
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
                throw new Exception("No se pudo cargar la lista de médicos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los médicos: " + ex.Message);
            }
            return tabla;
        }

        public DataTable BuscarMedicos(string filtro)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = @"SELECT IdMedico, Identificacion, Nombre, Telefono, Correo, HorarioAtencion, Estado
                                     FROM medicos
                                     WHERE Identificacion LIKE @Filtro OR Nombre LIKE @Filtro OR Estado LIKE @Filtro
                                     ORDER BY IdMedico DESC";
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
                throw new Exception("No se pudo buscar médicos: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al buscar médicos: " + ex.Message);
            }
            return tabla;
        }

        public void InsertarMedico(string identificacion, string nombre, string telefono, string correo, string horarioAtencion, string estado)
        {
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = @"INSERT INTO medicos (Identificacion, Nombre, Telefono, Correo, HorarioAtencion, Estado)
                                     VALUES (@Identificacion, @Nombre, @Telefono, @Correo, @HorarioAtencion, @Estado)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@HorarioAtencion", horarioAtencion);
                        cmd.Parameters.AddWithValue("@Estado", estado);

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
                    throw new Exception("Ya existe un médico registrado con esa identificación.");
                }
                throw new Exception("No se pudo guardar el médico: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al guardar el médico: " + ex.Message);
            }
        }

        public void ActualizarMedico(int idMedico, string identificacion, string nombre, string telefono, string correo, string horarioAtencion, string estado)
        {
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = @"UPDATE medicos
                                     SET Identificacion = @Identificacion,
                                         Nombre = @Nombre,
                                         Telefono = @Telefono,
                                         Correo = @Correo,
                                         HorarioAtencion = @HorarioAtencion,
                                         Estado = @Estado
                                     WHERE IdMedico = @IdMedico";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMedico", idMedico);
                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@HorarioAtencion", horarioAtencion);
                        cmd.Parameters.AddWithValue("@Estado", estado);

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
                    throw new Exception("Ya existe otro médico con esa identificación.");
                }
                throw new Exception("No se pudo actualizar el médico: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al actualizar el médico: " + ex.Message);
            }
        }

        public void EliminarMedico(int idMedico)
        {
            try
            {
                using (MySqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    string query = "DELETE FROM medicos WHERE IdMedico = @IdMedico";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdMedico", idMedico);

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
                    throw new Exception("No puedes eliminar este médico porque tiene especialidades o citas asociadas.");
                }
                throw new Exception("No se pudo eliminar el médico: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al eliminar el médico: " + ex.Message);
            }
        }
    }
}