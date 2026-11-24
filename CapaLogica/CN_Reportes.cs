using System;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class CN_Reportes
    {
        private readonly CD_Reportes datos=new CD_Reportes();
        public DataTable CitasPorDia(DateTime fecha,int idMedico){if(idMedico<0)throw new ArgumentOutOfRangeException("idMedico");return datos.CitasPorDia(fecha,idMedico);}
        public DataTable CitasCanceladas(DateTime desde,DateTime hasta){ValidarRango(desde,hasta);return datos.CitasCanceladas(desde,hasta);}
        public DataTable PacientesAtendidos(DateTime desde,DateTime hasta){ValidarRango(desde,hasta);return datos.PacientesAtendidos(desde,hasta);}
        private static void ValidarRango(DateTime desde,DateTime hasta){if(desde.Date>hasta.Date)throw new ArgumentException("La fecha inicial no puede ser posterior a la final.");}
    }
}