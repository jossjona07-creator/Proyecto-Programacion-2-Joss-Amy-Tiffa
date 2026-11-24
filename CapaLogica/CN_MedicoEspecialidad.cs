using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class CN_MedicoEspecialidad
    {
        private readonly CD_MedicoEspecialidad datos=new CD_MedicoEspecialidad();
        public DataTable ListarMedicos(){return datos.ListarMedicos();}
        public DataTable ListarEspecialidades(){return datos.ListarEspecialidades();}
        public List<int> ObtenerIds(int medico){if(medico<=0)throw new ArgumentException("Seleccione un médico.");return datos.ObtenerIds(medico);}
        public void Guardar(int medico,IList<int> especialidades){if(medico<=0)throw new ArgumentException("Seleccione un médico.");if(especialidades==null||especialidades.Count==0)throw new ArgumentException("Asigne al menos una especialidad.");datos.Guardar(medico,especialidades);}
    }
}