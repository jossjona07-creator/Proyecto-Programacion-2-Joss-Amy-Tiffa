using System;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Cita
    {
        private readonly ConexionBD conexionBD = new ConexionBD();
        private const string EstadosQueLiberanHorario = "'Cancelada','Reprogramada'";
        private const string ConsultaBase = @"SELECT c.IdCita,c.IdPaciente,p.Nombre AS Paciente,p.Identificacion AS CedulaPaciente,c.IdMedico,m.Nombre AS Medico,c.FechaHora,c.DuracionMinutos,c.TipoCita,c.EstadoCita,c.Motivo FROM citas c INNER JOIN pacientes p ON c.IdPaciente=p.IdPaciente INNER JOIN medicos m ON c.IdMedico=m.IdMedico";

        public DataTable ListarCitas()
        {
            using (MySqlConnection conexion=conexionBD.ObtenerConexion())
            using (MySqlDataAdapter adaptador=new MySqlDataAdapter(ConsultaBase+" ORDER BY c.FechaHora DESC",conexion))
            { DataTable tabla=new DataTable(); adaptador.Fill(tabla); return tabla; }
        }

        public DataTable BuscarCitas(string filtro)
        {
            using (MySqlConnection conexion=conexionBD.ObtenerConexion())
            using (MySqlCommand cmd=new MySqlCommand(ConsultaBase+@" WHERE p.Nombre LIKE @Filtro OR p.Identificacion LIKE @Filtro OR m.Nombre LIKE @Filtro OR c.TipoCita LIKE @Filtro OR c.EstadoCita LIKE @Filtro OR c.Motivo LIKE @Filtro OR DATE_FORMAT(c.FechaHora,'%Y-%m-%d') LIKE @Filtro ORDER BY c.FechaHora DESC",conexion))
            {
                cmd.Parameters.AddWithValue("@Filtro","%"+(filtro??string.Empty).Trim()+"%");
                using(MySqlDataAdapter adaptador=new MySqlDataAdapter(cmd)){DataTable tabla=new DataTable();adaptador.Fill(tabla);return tabla;}
            }
        }

        public void InsertarCita(int idPaciente,int idMedico,DateTime fechaHora,int duracionMinutos,string tipoCita,string estadoCita,string motivo)
        {
            using(MySqlConnection conexion=conexionBD.ObtenerConexion())
            using(MySqlTransaction tx=conexion.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    ValidarCita(conexion,tx,0,idPaciente,idMedico,fechaHora,duracionMinutos,estadoCita);
                    using(MySqlCommand cmd=new MySqlCommand("INSERT INTO citas (IdPaciente,IdMedico,FechaHora,DuracionMinutos,TipoCita,EstadoCita,Motivo) VALUES (@Paciente,@Medico,@Fecha,@Duracion,@Tipo,@Estado,@Motivo)",conexion,tx))
                    { AgregarParametros(cmd,idPaciente,idMedico,fechaHora,duracionMinutos,tipoCita,estadoCita,motivo);cmd.ExecuteNonQuery(); }
                    tx.Commit();
                }
                catch { tx.Rollback();throw; }
            }
        }

        public void ActualizarCita(int idCita,int idPaciente,int idMedico,DateTime fechaHora,int duracionMinutos,string tipoCita,string estadoCita,string motivo)
        {
            using(MySqlConnection conexion=conexionBD.ObtenerConexion())
            using(MySqlTransaction tx=conexion.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    ValidarCita(conexion,tx,idCita,idPaciente,idMedico,fechaHora,duracionMinutos,estadoCita);
                    using(MySqlCommand cmd=new MySqlCommand("UPDATE citas SET IdPaciente=@Paciente,IdMedico=@Medico,FechaHora=@Fecha,DuracionMinutos=@Duracion,TipoCita=@Tipo,EstadoCita=@Estado,Motivo=@Motivo WHERE IdCita=@IdCita",conexion,tx))
                    { AgregarParametros(cmd,idPaciente,idMedico,fechaHora,duracionMinutos,tipoCita,estadoCita,motivo);cmd.Parameters.AddWithValue("@IdCita",idCita);if(cmd.ExecuteNonQuery()==0)throw new InvalidOperationException("La cita ya no existe."); }
                    tx.Commit();
                }
                catch { tx.Rollback();throw; }
            }
        }

        public void ReprogramarCita(int idCita,int idPaciente,int idMedico,DateTime fechaHora,int duracionMinutos,string tipoCita,string motivo)
        {
            using(MySqlConnection conexion=conexionBD.ObtenerConexion())
            using(MySqlTransaction tx=conexion.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    ValidarCita(conexion,tx,idCita,idPaciente,idMedico,fechaHora,duracionMinutos,"Programada");
                    using(MySqlCommand actualizar=new MySqlCommand("UPDATE citas SET EstadoCita='Reprogramada' WHERE IdCita=@Id AND EstadoCita NOT IN ('Cancelada','Reprogramada')",conexion,tx))
                    {actualizar.Parameters.AddWithValue("@Id",idCita);if(actualizar.ExecuteNonQuery()==0)throw new InvalidOperationException("La cita ya fue cancelada o reprogramada.");}
                    using(MySqlCommand insertar=new MySqlCommand("INSERT INTO citas(IdPaciente,IdMedico,FechaHora,DuracionMinutos,TipoCita,EstadoCita,Motivo) VALUES(@Paciente,@Medico,@Fecha,@Duracion,@Tipo,'Programada',@Motivo)",conexion,tx))
                    {AgregarParametros(insertar,idPaciente,idMedico,fechaHora,duracionMinutos,tipoCita,"Programada",motivo);insertar.ExecuteNonQuery();}
                    tx.Commit();
                }
                catch{tx.Rollback();throw;}
            }
        }
        public void CancelarCita(int idCita)
        {
            using(MySqlConnection conexion=conexionBD.ObtenerConexion())
            using(MySqlCommand cmd=new MySqlCommand("UPDATE citas SET EstadoCita='Cancelada' WHERE IdCita=@Id AND EstadoCita<>'Cancelada'",conexion))
            { cmd.Parameters.AddWithValue("@Id",idCita);if(cmd.ExecuteNonQuery()==0)throw new InvalidOperationException("La cita no existe o ya fue cancelada."); }
        }

        private static void AgregarParametros(MySqlCommand cmd,int paciente,int medico,DateTime fecha,int duracion,string tipo,string estado,string motivo)
        {
            cmd.Parameters.AddWithValue("@Paciente",paciente);cmd.Parameters.AddWithValue("@Medico",medico);cmd.Parameters.AddWithValue("@Fecha",fecha);cmd.Parameters.AddWithValue("@Duracion",duracion);cmd.Parameters.AddWithValue("@Tipo",tipo);cmd.Parameters.AddWithValue("@Estado",estado);cmd.Parameters.AddWithValue("@Motivo",string.IsNullOrWhiteSpace(motivo)?(object)DBNull.Value:motivo.Trim());
        }

        private static void ValidarCita(MySqlConnection conexion,MySqlTransaction tx,int excluir,int paciente,int medico,DateTime inicio,int duracion,string estadoCita)
        {
            if(paciente<=0||medico<=0)throw new ArgumentException("Seleccione un paciente y un médico válidos.");
            bool estadoFinal=string.Equals(estadoCita,"Atendida",StringComparison.OrdinalIgnoreCase)||string.Equals(estadoCita,"No asistió",StringComparison.OrdinalIgnoreCase)||string.Equals(estadoCita,"Cancelada",StringComparison.OrdinalIgnoreCase)||string.Equals(estadoCita,"Reprogramada",StringComparison.OrdinalIgnoreCase);
            if(!estadoFinal&&inicio<=DateTime.Now)throw new ArgumentException("La fecha y hora de la cita deben estar en el futuro.");
            if(duracion!=30&&duracion!=45&&duracion!=60)throw new ArgumentException("La duración debe ser de 30, 45 o 60 minutos.");
            string horario,estado;
            using(MySqlCommand cmd=new MySqlCommand("SELECT HorarioAtencion,Estado FROM medicos WHERE IdMedico=@Id FOR UPDATE",conexion,tx))
            {cmd.Parameters.AddWithValue("@Id",medico);using(MySqlDataReader rd=cmd.ExecuteReader()){if(!rd.Read())throw new InvalidOperationException("El médico seleccionado ya no está registrado.");horario=rd.IsDBNull(0)?string.Empty:rd.GetString(0);estado=rd.IsDBNull(1)?string.Empty:rd.GetString(1);}}
            if(!string.Equals(estado,"Activo",StringComparison.OrdinalIgnoreCase))throw new InvalidOperationException("No se pueden reservar citas con un médico inactivo.");
            string[] partes=(horario??string.Empty).Split('-');TimeSpan desde,hasta;
            if(partes.Length!=2||!TimeSpan.TryParseExact(partes[0].Trim(),@"hh\:mm",CultureInfo.InvariantCulture,out desde)||!TimeSpan.TryParseExact(partes[1].Trim(),@"hh\:mm",CultureInfo.InvariantCulture,out hasta)||hasta<=desde)throw new InvalidOperationException("El horario registrado para este médico está incompleto o inválido. Corríjalo en Médicos con formato HH:mm-HH:mm, por ejemplo 08:00-16:00.");
            if(!estadoFinal&&(inicio.TimeOfDay<desde||inicio.AddMinutes(duracion).TimeOfDay>hasta||inicio.Date!=inicio.AddMinutes(duracion).Date))throw new InvalidOperationException("La cita completa debe quedar dentro del horario de atención del médico.");
            using(MySqlCommand cmd=new MySqlCommand("SELECT IdPaciente FROM pacientes WHERE IdPaciente=@Id FOR UPDATE",conexion,tx)){cmd.Parameters.AddWithValue("@Id",paciente);if(cmd.ExecuteScalar()==null)throw new InvalidOperationException("El paciente seleccionado ya no está registrado.");}
            if(estadoFinal)return;
            const string sql=@"SELECT COUNT(*) FROM citas WHERE EstadoCita NOT IN ("+EstadosQueLiberanHorario+@") AND (@Excluir=0 OR IdCita<>@Excluir) AND FechaHora<@Fin AND DATE_ADD(FechaHora,INTERVAL DuracionMinutos MINUTE)>@Inicio AND {0}=@Entidad";
            VerificarConflicto(conexion,tx,string.Format(CultureInfo.InvariantCulture,sql,"IdMedico"),medico,excluir,inicio,inicio.AddMinutes(duracion),"El médico ya tiene otra cita en ese horario.");
            VerificarConflicto(conexion,tx,string.Format(CultureInfo.InvariantCulture,sql,"IdPaciente"),paciente,excluir,inicio,inicio.AddMinutes(duracion),"El paciente ya tiene otra cita en ese horario.");
        }

        private static void VerificarConflicto(MySqlConnection conexion,MySqlTransaction tx,string sql,int entidad,int excluir,DateTime inicio,DateTime fin,string mensaje)
        {
            using(MySqlCommand cmd=new MySqlCommand(sql,conexion,tx)){cmd.Parameters.AddWithValue("@Entidad",entidad);cmd.Parameters.AddWithValue("@Excluir",excluir);cmd.Parameters.AddWithValue("@Inicio",inicio);cmd.Parameters.AddWithValue("@Fin",fin);if(Convert.ToInt32(cmd.ExecuteScalar())>0)throw new InvalidOperationException(mensaje);}
        }
    }
}
