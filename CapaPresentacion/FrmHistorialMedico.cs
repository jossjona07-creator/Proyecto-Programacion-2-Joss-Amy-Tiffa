using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public class FrmHistorialMedico : Form
    {
        private readonly CN_HistorialMedico historial=new CN_HistorialMedico();
        private readonly CN_Paciente pacientes=new CN_Paciente();
        private readonly CN_Medico medicos=new CN_Medico();
        private readonly ComboBox paciente=new ComboBox(), medico=new ComboBox(), filtroPaciente=new ComboBox();
        private readonly DateTimePicker fecha=new DateTimePicker();
        private readonly TextBox diagnostico=new TextBox(), tratamiento=new TextBox(), observaciones=new TextBox();
        private readonly DataGridView grid=new DataGridView();
        private bool cargandoCombos;
        public FrmHistorialMedico()
        {
            Text="Historial médico";BackColor=Color.White;Padding=new Padding(18);MinimumSize=new Size(820,600);Dock=DockStyle.Fill;
            var titulo=new Label{Text="Historial médico",Font=new Font("Segoe UI",18,FontStyle.Bold),ForeColor=Color.FromArgb(74,20,140),AutoSize=true,Location=new Point(18,14)};Controls.Add(titulo);
            AddLabel("Paciente",18,62);AddCombo(paciente,110,58,250);AddLabel("Médico",385,62);AddCombo(medico,470,58,250);
            AddLabel("Fecha",18,101);fecha.Location=new Point(110,97);fecha.Width=250;fecha.Format=DateTimePickerFormat.Custom;fecha.CustomFormat="dd/MM/yyyy HH:mm";fecha.MaxDate=DateTime.Now;Controls.Add(fecha);
            AddLabel("Diagnóstico",18,143);diagnostico.Location=new Point(110,139);diagnostico.Size=new Size(610,58);diagnostico.Multiline=true;Controls.Add(diagnostico);
            AddLabel("Tratamiento",18,209);tratamiento.Location=new Point(110,205);tratamiento.Size=new Size(610,48);tratamiento.Multiline=true;Controls.Add(tratamiento);
            AddLabel("Observaciones",18,264);observaciones.Location=new Point(110,260);observaciones.Size=new Size(610,48);observaciones.Multiline=true;Controls.Add(observaciones);
            var guardar=ButtonAt("Registrar",110,318);guardar.Click+=GuardarClick;var limpiar=ButtonAt("Limpiar",220,318);limpiar.Click+=(s,e)=>{diagnostico.Clear();tratamiento.Clear();observaciones.Clear();};
            AddLabel("Filtrar paciente",18,372);AddCombo(filtroPaciente,130,368,280);filtroPaciente.SelectedIndexChanged+=(s,e)=>{if(!cargandoCombos)Cargar();};
            grid.Location=new Point(18,408);grid.Size=new Size(760,230);grid.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;grid.ReadOnly=true;grid.AllowUserToAddRows=false;grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;grid.SelectionMode=DataGridViewSelectionMode.FullRowSelect;Controls.Add(grid);
            Load+=CargarDatos;
        }
        private void AddLabel(string text,int x,int y){Controls.Add(new Label{Text=text+":",Location=new Point(x,y),AutoSize=true,Font=new Font("Segoe UI",9,FontStyle.Bold)});}
        private void AddCombo(ComboBox b,int x,int y,int w){b.Location=new Point(x,y);b.Width=w;b.DropDownStyle=ComboBoxStyle.DropDownList;Controls.Add(b);}
        private Button ButtonAt(string text,int x,int y){var b=new Button{Text=text,Location=new Point(x,y),Size=new Size(100,32),BackColor=Color.FromArgb(106,27,154),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};Controls.Add(b);return b;}
        private static DataTable TablaCombo(DataTable datos,string id,string nombre){DataTable t=new DataTable();t.Columns.Add("Id",typeof(int));t.Columns.Add("Nombre");t.Rows.Add(0,"-- Todos / seleccionar --");foreach(DataRow r in datos.Rows)t.Rows.Add(Convert.ToInt32(r[id]),r[nombre].ToString());return t;}
        private void CargarDatos(object s,EventArgs e)
        {
            try
            {
                cargandoCombos=true;
                ConfigurarCombo(paciente,TablaCombo(pacientes.ListarPacientes(),"IdPaciente","Nombre"));
                ConfigurarCombo(medico,TablaCombo(medicos.ListarMedicos(),"IdMedico","Nombre"));
                ConfigurarCombo(filtroPaciente,TablaCombo(pacientes.ListarPacientes(),"IdPaciente","Nombre"));
            }
            catch(Exception ex){Aviso(ex.Message);}
            finally{cargandoCombos=false;}
            Cargar();
        }
        private static void ConfigurarCombo(ComboBox combo,DataTable datos)
        {
            combo.DataSource=null;
            combo.DisplayMember="Nombre";
            combo.ValueMember="Id";
            combo.DataSource=datos;
            if(combo.Items.Count>0)combo.SelectedIndex=0;
        }
        private static int Id(ComboBox b)
        {
            object valor=b.SelectedValue;
            if(valor is DataRowView fila)valor=fila[b.ValueMember];
            return int.TryParse(Convert.ToString(valor),out int id)?id:0;
        }
        private void Cargar(){try{if(!cargandoCombos&&filtroPaciente.DataSource!=null)grid.DataSource=historial.Listar(Id(filtroPaciente));}catch(Exception ex){Aviso(ex.Message);}}
        private void GuardarClick(object s,EventArgs e){try{historial.Registrar(Id(paciente),Id(medico),fecha.Value,diagnostico.Text,tratamiento.Text,observaciones.Text);MessageBox.Show("El historial se registró.","Agenda médica",MessageBoxButtons.OK,MessageBoxIcon.Information);diagnostico.Clear();tratamiento.Clear();observaciones.Clear();Cargar();}catch(Exception ex){Aviso(ex.Message);}}
        private void Aviso(string mensaje){MessageBox.Show(mensaje,"Agenda médica",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
    }
}
