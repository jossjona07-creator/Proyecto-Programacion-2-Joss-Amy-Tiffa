using System;

namespace CapaLogica
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; } 
        public string TipoCita { get; set; }     
        public string EstadoCita { get; set; }    
        public string Motivo { get; set; }
    }
}