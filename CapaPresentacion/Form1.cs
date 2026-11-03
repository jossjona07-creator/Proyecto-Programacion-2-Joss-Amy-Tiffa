using System;
using System.Windows.Forms;
using CapaLogica; // Importamos la capa lógica para usar CN_Paciente

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
    }
}