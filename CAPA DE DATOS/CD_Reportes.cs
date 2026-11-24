using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Reportes
    {
        private readonly ConexionBD conexionBD=new ConexionBD();
        public DataTable CitasPorDia(DateTime fecha,int idMedico)
        {return Consultar(@"SELECT c.IdCita,p.Nombre AS Paciente,m.Nombre AS Medico,c.FechaHora,c.DuracionMinutos,c.TipoCita,c.EstadoCita,c.Motivo FROM citas c INNER JOIN pacientes p ON p.IdPaciente=c.IdPaciente INNER JOIN medicos m ON m.IdMedico=c.IdMedico WHERE DATE(c.FechaHora)=@Fecha AND (@Medico=0 OR c.IdMedico=@Medico) ORDER BY c.FechaHora",cmd=>{cmd.Parameters.AddWithValue("@Fecha",fecha.Date);cmd.Parameters.AddWithValue("@Medico",idMedico);});}
        public DataTable CitasCanceladas(DateTime desde,DateTime hasta)
        {return Consultar(@"SELECT c.IdCita,p.Nombre AS Paciente,m.Nombre AS Medico,c.FechaHora,c.DuracionMinutos,c.TipoCita,c.EstadoCita,c.Motivo FROM citas c INNER JOIN pacientes p ON p.IdPaciente=c.IdPaciente INNER JOIN medicos m ON m.IdMedico=c.IdMedico WHERE c.EstadoCita='Cancelada' AND c.FechaHora>=@Desde AND c.FechaHora<@Hasta ORDER BY c.FechaHora DESC",cmd=>{cmd.Parameters.AddWithValue("@Desde",desde.Date);cmd.Parameters.AddWithValue("@Hasta",hasta.Date.AddDays(1));});}
        public DataTable PacientesAtendidos(DateTime desde,DateTime hasta)
        {return Consultar(@"SELECT p.IdPaciente,p.Identificacion,p.Nombre,p.Telefono,p.Correo,COUNT(c.IdCita) AS CantidadAtenciones,MAX(c.FechaHora) AS UltimaAtencion FROM citas c INNER JOIN pacientes p ON p.IdPaciente=c.IdPaciente WHERE c.EstadoCita='Atendida' AND c.FechaHora>=@Desde AND c.FechaHora<@Hasta GROUP BY p.IdPaciente,p.Identificacion,p.Nombre,p.Telefono,p.Correo ORDER BY p.Nombre",cmd=>{cmd.Parameters.AddWithValue("@Desde",desde.Date);cmd.Parameters.AddWithValue("@Hasta",hasta.Date.AddDays(1));});}
        private DataTable Consultar(string sql,Action<MySqlCommand> parametros)
        {using(MySqlConnection conexion=conexionBD.ObtenerConexion())using(MySqlCommand cmd=new MySqlCommand(sql,conexion)){parametros(cmd);using(MySqlDataAdapter adaptador=new MySqlDataAdapter(cmd)){DataTable tabla=new DataTable();adaptador.Fill(tabla);return tabla;}}}
    }
}