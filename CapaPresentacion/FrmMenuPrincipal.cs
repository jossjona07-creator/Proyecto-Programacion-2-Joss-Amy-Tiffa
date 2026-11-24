using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmMenuPrincipal : Form
    {
        private Button botonActivo;
        private readonly Button btnHistorial = new Button();
        private readonly Button btnReportes = new Button();
        private readonly Button btnAsignarEspecialidades = new Button();

        public FrmMenuPrincipal()
        {
            InitializeComponent();
            ConfigurarNavegacionAdicional();
        }

        private void ConfigurarNavegacionAdicional()
        {
            PrepararBoton(btnHistorial,"Historial médico",btnHistorial_Click);
            PrepararBoton(btnReportes,"Reportes",btnReportes_Click);
            PrepararBoton(btnAsignarEspecialidades,"Asignar especialidades",btnAsignarEspecialidades_Click);
            pnlMenuLateral.Controls.Add(btnHistorial);
            pnlMenuLateral.Controls.Add(btnReportes);
            pnlMenuLateral.Controls.Add(btnAsignarEspecialidades);
            btnHistorial.BringToFront();
            btnReportes.BringToFront();
            btnAsignarEspecialidades.BringToFront();
        }

        private void PrepararBoton(Button boton,string texto,EventHandler click)
        {
            boton.BackColor=Color.Plum;boton.Dock=DockStyle.Top;boton.FlatStyle=FlatStyle.Flat;
            boton.ForeColor=SystemColors.ControlLightLight;boton.Size=new Size(pnlMenuLateral.Width,45);
            boton.Text=texto;boton.Click+=click;
        }

        private void AbrirFormularioEnPanel(Form formularioHijo,Button botonSeleccionado)
        {
            foreach(Control ctrl in pnlContenedor.Controls)ctrl.Dispose();
            pnlContenedor.Controls.Clear();
            formularioHijo.TopLevel=false;formularioHijo.FormBorderStyle=FormBorderStyle.None;formularioHijo.Dock=DockStyle.Fill;
            pnlContenedor.Controls.Add(formularioHijo);pnlContenedor.Tag=formularioHijo;formularioHijo.Show();ResaltarBoton(botonSeleccionado);
        }

        private void ResaltarBoton(Button seleccionado)
        {
            if(botonActivo!=null){botonActivo.BackColor=Color.Plum;botonActivo.Font=new Font(botonActivo.Font,FontStyle.Regular);}
            if(seleccionado!=null){botonActivo=seleccionado;botonActivo.BackColor=Color.FromArgb(120,45,175);botonActivo.Font=new Font(botonActivo.Font,FontStyle.Bold);}
        }

        private void btnPacientes_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new Form1(),btnPacientes);}
        private void btnEspecialidades_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new FrmEspecialidades(),btnEspecialidades);}
        private void btnMedicos_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new FrmMedicos(),btnMedicos);}
        private void btnCitas_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new FrmCitas(),btnCitas);}
        private void btnHistorial_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new FrmHistorialMedico(),btnHistorial);}
        private void btnReportes_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new FrmReportes(),btnReportes);}
        private void btnAsignarEspecialidades_Click(object sender,EventArgs e){AbrirFormularioEnPanel(new FrmAsignarEspecialidades(),btnAsignarEspecialidades);}
        private void pnlContenedor_Paint(object sender,PaintEventArgs e) { }
    }
}