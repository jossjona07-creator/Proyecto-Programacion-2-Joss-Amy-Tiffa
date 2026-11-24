using System;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class CN_HistorialMedico
    {
        private readonly CD_HistorialMedico datos=new CD_HistorialMedico();
        public DataTable Listar(int idPaciente=0){if(idPaciente<0)throw new ArgumentOutOfRangeException("idPaciente");return datos.Listar(idPaciente);}
        public void Registrar(int paciente,int medico,DateTime fecha,string diagnostico,string tratamiento,string observaciones)
        {
            if(paciente<=0)throw new ArgumentException("Seleccione un paciente.");
            if(medico<=0)throw new ArgumentException("Seleccione el médico.");
            if(string.IsNullOrWhiteSpace(diagnostico))throw new ArgumentException("El diagnóstico es obligatorio.");
            if(fecha>DateTime.Now)throw new ArgumentException("La fecha del historial no puede estar en el futuro.");
            datos.Registrar(paciente,medico,fecha,diagnostico,tratamiento,observaciones);
        }
    }
}