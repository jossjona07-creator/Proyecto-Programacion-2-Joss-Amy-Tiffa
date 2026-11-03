using System;

namespace CapaLogica
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; } // Ej: 30, 45, 60 min
        public string TipoCita { get; set; }     // Presencial, Virtual, Urgencia
        public string EstadoCita { get; set; }    // Programada, Confirmada, Atendida, Cancelada, etc.
        public string Motivo { get; set; }
    }
}