using System;
using System.Windows.Forms;
using CapaLogica; 

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private CN_Paciente objetoCN = new CN_Paciente();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamamos a la lógica y cargamos los datos en el DataGridView
                dataGridView1.DataSource = objetoCN.ListarPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamamos a la capa lógica
                objetoCN.InsertarPaciente(
                    txtCedula.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtFechaNacimiento.Text.Trim()
                );

                MessageBox.Show("¡Paciente registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizamos la tabla automáticamente
                dataGridView1.DataSource = objetoCN.ListarPacientes();

                // Limpiamos las cajas de texto
                txtCedula.Clear();
                txtNombre.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtFechaNacimiento.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }
    }
}