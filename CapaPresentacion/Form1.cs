using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGENDA_MEDICA
{
    public partial class Form1 : Form
    {
        // Instanciamos pacientes creado en la CapaLogica
        private ServicioPaciente servicio = new ServicioPaciente();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Creamos el objeto Paciente y le asignamos los valores de las cajas de texto
                Paciente nuevoPaciente = new Paciente();
                nuevoPaciente.Identificacion = txtIdentificacion.Text;
                nuevoPaciente.Nombre = txtNombre.Text;
                nuevoPaciente.Apellidos = txtApellidos.Text;
                nuevoPaciente.AnioNacimiento = int.Parse(txtAnioNacimiento.Text);
                nuevoPaciente.Telefono = txtTelefono.Text;
                nuevoPaciente.Correo = txtCorreo.Text;
                nuevoPaciente.Direccion = txtDireccion.Text;

                // Guardamos el paciente usando el servicio de la Capa Lógica
                servicio.AgregarPaciente(nuevoPaciente);

                //Actualizamos el DataGridView para que refleje la lista actualizada
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = servicio.ObtenerPacientes();

                // Mensaje de éxito y limpieza de campos
                MessageBox.Show("¡Paciente guardado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtIdentificacion.Clear();
                txtNombre.Clear();
                txtApellidos.Clear();
                txtAnioNacimiento.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtDireccion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}