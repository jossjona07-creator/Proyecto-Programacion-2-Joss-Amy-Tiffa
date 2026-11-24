using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_MedicoEspecialidad
    {
        private readonly ConexionBD conexionBD=new ConexionBD();
        public DataTable ListarEspecialidades(){using(MySqlConnection c=conexionBD.ObtenerConexion())using(MySqlDataAdapter a=new MySqlDataAdapter("SELECT IdEspecialidad,NombreEspecialidad FROM especialidades ORDER BY NombreEspecialidad",c)){DataTable t=new DataTable();a.Fill(t);return t;}}
        public DataTable ListarMedicos(){using(MySqlConnection c=conexionBD.ObtenerConexion())using(MySqlDataAdapter a=new MySqlDataAdapter("SELECT IdMedico,Nombre FROM medicos WHERE Estado='Activo' ORDER BY Nombre",c)){DataTable t=new DataTable();a.Fill(t);return t;}}
        public List<int> ObtenerIds(int medico){var ids=new List<int>();using(MySqlConnection c=conexionBD.ObtenerConexion())using(MySqlCommand cmd=new MySqlCommand("SELECT IdEspecialidad FROM medicoespecialidad WHERE IdMedico=@Id",c)){cmd.Parameters.AddWithValue("@Id",medico);using(MySqlDataReader r=cmd.ExecuteReader())while(r.Read())ids.Add(r.GetInt32(0));}return ids;}
        public void Guardar(int medico,IList<int> especialidades)
        {
            using(MySqlConnection c=conexionBD.ObtenerConexion())using(MySqlTransaction tx=c.BeginTransaction())
            {try{using(MySqlCommand del=new MySqlCommand("DELETE FROM medicoespecialidad WHERE IdMedico=@Id",c,tx)){del.Parameters.AddWithValue("@Id",medico);del.ExecuteNonQuery();}foreach(int id in especialidades)using(MySqlCommand ins=new MySqlCommand("INSERT INTO medicoespecialidad(IdMedico,IdEspecialidad) VALUES(@Medico,@Especialidad)",c,tx)){ins.Parameters.AddWithValue("@Medico",medico);ins.Parameters.AddWithValue("@Especialidad",id);ins.ExecuteNonQuery();}tx.Commit();}catch{tx.Rollback();throw;}}
        }
    }
}