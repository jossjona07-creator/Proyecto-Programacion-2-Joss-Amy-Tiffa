using System;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class CN_Cita
    {
        private readonly CD_Cita objetoCD = new CD_Cita();
        private static readonly string[] Tipos = { "Presencial", "Virtual", "Urgencia" };
        private static readonly string[] Estados = { "Programada", "Confirmada", "Atendida", "Cancelada", "No asistió", "Reprogramada" };

        public DataTable ListarCitas() { return objetoCD.ListarCitas(); }
        public DataTable BuscarCitas(string filtro) { return string.IsNullOrWhiteSpace(filtro) ? objetoCD.ListarCitas() : objetoCD.BuscarCitas(filtro.Trim()); }

        public void InsertarCita(int idPaciente,int idMedico,DateTime fechaHora,int duracionMinutos,string tipoCita,string estadoCita,string motivo)
        {
            ValidarCatalogos(duracionMinutos,tipoCita,estadoCita);
            objetoCD.InsertarCita(idPaciente,idMedico,fechaHora,duracionMinutos,tipoCita.Trim(),estadoCita.Trim(),motivo);
        }

        public void ActualizarCita(int idCita,int idPaciente,int idMedico,DateTime fechaHora,int duracionMinutos,string tipoCita,string estadoCita,string motivo)
        {
            if(idCita<=0)throw new ArgumentException("Seleccione una cita para actualizarla.");
            ValidarCatalogos(duracionMinutos,tipoCita,estadoCita);
            objetoCD.ActualizarCita(idCita,idPaciente,idMedico,fechaHora,duracionMinutos,tipoCita.Trim(),estadoCita.Trim(),motivo);
        }

        public void ReprogramarCita(int idCita,int idPaciente,int idMedico,DateTime nuevaFecha,int duracion,string tipo,string motivo)
        {
            if(idCita<=0)throw new ArgumentException("Seleccione la cita que desea reprogramar.");
            ValidarCatalogos(duracion,tipo,"Programada");
            objetoCD.ReprogramarCita(idCita,idPaciente,idMedico,nuevaFecha,duracion,tipo.Trim(),motivo);
        }
        public void EliminarCita(int idCita)
        {
            if(idCita<=0)throw new ArgumentException("Seleccione una cita para cancelarla.");
            objetoCD.CancelarCita(idCita);
        }

        private static void ValidarCatalogos(int duracion,string tipo,string estado)
        {
            if(duracion!=30&&duracion!=45&&duracion!=60)throw new ArgumentException("La duración debe ser 30, 45 o 60 minutos.");
            if(Array.IndexOf(Tipos,(tipo??string.Empty).Trim())<0)throw new ArgumentException("El tipo debe ser Presencial, Virtual o Urgencia.");
            if(Array.IndexOf(Estados,(estado??string.Empty).Trim())<0)throw new ArgumentException("Seleccione un estado válido para la cita.");
        }
    }
}