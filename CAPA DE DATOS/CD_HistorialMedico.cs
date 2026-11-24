using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_HistorialMedico
    {
        private readonly ConexionBD conexionBD = new ConexionBD();
        public DataTable Listar(int idPaciente)
        {
            using (MySqlConnection c=conexionBD.ObtenerConexion())
            using (MySqlCommand cmd=new MySqlCommand(@"SELECT h.IdHistorial,h.IdPaciente,p.Nombre AS Paciente,h.IdMedico,m.Nombre AS Medico,h.FechaRegistro,h.Diagnostico,h.Tratamiento,h.Observaciones FROM historialmedico h INNER JOIN pacientes p ON p.IdPaciente=h.IdPaciente INNER JOIN medicos m ON m.IdMedico=h.IdMedico WHERE (@Paciente=0 OR h.IdPaciente=@Paciente) ORDER BY h.FechaRegistro DESC",c))
            { cmd.Parameters.AddWithValue("@Paciente",idPaciente);using(MySqlDataAdapter a=new MySqlDataAdapter(cmd)){DataTable t=new DataTable();a.Fill(t);return t;} }
        }
        public void Registrar(int paciente,int medico,DateTime fecha,string diagnostico,string tratamiento,string observaciones)
        {
            using(MySqlConnection c=conexionBD.ObtenerConexion())
            using(MySqlCommand cmd=new MySqlCommand("INSERT INTO historialmedico(IdPaciente,IdMedico,FechaRegistro,Diagnostico,Tratamiento,Observaciones) VALUES(@Paciente,@Medico,@Fecha,@Diagnostico,@Tratamiento,@Observaciones)",c))
            { cmd.Parameters.AddWithValue("@Paciente",paciente);cmd.Parameters.AddWithValue("@Medico",medico);cmd.Parameters.AddWithValue("@Fecha",fecha);cmd.Parameters.AddWithValue("@Diagnostico",diagnostico.Trim());cmd.Parameters.AddWithValue("@Tratamiento",string.IsNullOrWhiteSpace(tratamiento)?(object)DBNull.Value:tratamiento.Trim());cmd.Parameters.AddWithValue("@Observaciones",string.IsNullOrWhiteSpace(observaciones)?(object)DBNull.Value:observaciones.Trim());cmd.ExecuteNonQuery(); }
        }
    }
}