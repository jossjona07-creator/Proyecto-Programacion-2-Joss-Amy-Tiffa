using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public class FrmAsignarEspecialidades : Form
    {
        private readonly CN_MedicoEspecialidad servicio=new CN_MedicoEspecialidad();
        private readonly ComboBox medico=new ComboBox();
        private readonly CheckedListBox especialidades=new CheckedListBox();
        private bool cargando;
        public FrmAsignarEspecialidades()
        {
            Text="Especialidades de médicos";BackColor=Color.White;Padding=new Padding(24);
            Controls.Add(new Label{Text="Asignar especialidades a médicos",Font=new Font("Segoe UI",18,FontStyle.Bold),ForeColor=Color.FromArgb(74,20,140),AutoSize=true,Location=new Point(24,22)});
            Controls.Add(new Label{Text="Médico",Location=new Point(24,84),AutoSize=true});medico.Location=new Point(100,80);medico.Width=380;medico.DropDownStyle=ComboBoxStyle.DropDownList;medico.SelectedIndexChanged+=MedicoCambiado;Controls.Add(medico);
            Controls.Add(new Label{Text="Especialidades (seleccione una o varias)",Location=new Point(24,128),AutoSize=true});especialidades.Location=new Point(24,154);especialidades.Size=new Size(520,300);especialidades.CheckOnClick=true;Controls.Add(especialidades);
            var guardar=new Button{Text="Guardar asignación",Location=new Point(24,470),Size=new Size(180,38),BackColor=Color.FromArgb(106,27,154),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};guardar.Click+=Guardar;Controls.Add(guardar);Load+=Cargar;
        }
        private void Cargar(object s,EventArgs e)
        {
            try
            {
                cargando=true;
                medico.DataSource=null;medico.DisplayMember="Nombre";medico.ValueMember="IdMedico";medico.DataSource=servicio.ListarMedicos();
                especialidades.DataSource=null;especialidades.DisplayMember="NombreEspecialidad";especialidades.ValueMember="IdEspecialidad";especialidades.DataSource=servicio.ListarEspecialidades();
            }
            catch(Exception ex){Aviso(ex.Message);}
            finally{cargando=false;}
            CargarSeleccion();
        }
        private int IdMedico()
        {
            object valor=medico.SelectedValue;
            if(valor is DataRowView fila)valor=fila[medico.ValueMember];
            return int.TryParse(Convert.ToString(valor),out int id)?id:0;
        }
        private void MedicoCambiado(object s,EventArgs e){if(!cargando&&especialidades.DataSource!=null)CargarSeleccion();}
        private void CargarSeleccion(){try{var ids=servicio.ObtenerIds(IdMedico());for(int i=0;i<especialidades.Items.Count;i++){DataRowView row=especialidades.Items[i] as DataRowView;if(row!=null)especialidades.SetItemChecked(i,ids.Contains(Convert.ToInt32(row["IdEspecialidad"])));}}catch(Exception ex){Aviso(ex.Message);}}
        private void Guardar(object s,EventArgs e){try{var ids=especialidades.CheckedItems.Cast<DataRowView>().Select(r=>Convert.ToInt32(r["IdEspecialidad"])).ToList();servicio.Guardar(IdMedico(),ids);MessageBox.Show("Las especialidades del médico se guardaron.","Agenda médica",MessageBoxButtons.OK,MessageBoxIcon.Information);}catch(Exception ex){Aviso(ex.Message);}}
        private void Aviso(string m){MessageBox.Show(m,"Agenda médica",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
    }
}
