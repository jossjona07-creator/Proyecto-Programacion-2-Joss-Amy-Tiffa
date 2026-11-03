using System;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class CN_Paciente
    {
        private CD_Paciente objetoCD = new CD_Paciente();

        // Método que comunica la presentación con los datos
        public DataTable ListarPacientes()
        {
            return objetoCD.ListarPacientes();
        }
    }
}