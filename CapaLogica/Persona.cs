using System;

namespace AGENDA_MEDICA
{
    public class Persona
    {
        // Propiedades automáticas (Encapsulamiento)
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int AnioNacimiento { get; set; }

        // Atributo calculado 
        public int EdadCalculada => DateTime.Now.Year - AnioNacimiento;
    }
}