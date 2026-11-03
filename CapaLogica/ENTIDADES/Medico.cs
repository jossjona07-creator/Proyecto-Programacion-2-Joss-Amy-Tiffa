using System;

namespace CapaLogica
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string HorarioAtencion { get; set; }
        public string Estado { get; set; } // Activo, Inactivo
    }
}