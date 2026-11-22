using System;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private CN_Paciente objetoCN = new CN_Paciente();
        private int idPacienteSeleccionado = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        private void FormatearColumnasPacientes()
        {
            if (dataGridView1.Columns["IdPaciente"] != null)
            {
                dataGridView1.Columns["IdPaciente"].Visible = false;
            }

            if (dataGridView1.Columns["Identificacion"] != null)
            {
                dataGridView1.Columns["Identificacion"].HeaderText = "Identificación";
            }

            if (dataGridView1.Columns["Nombre"] != null)
            {
                dataGridView1.Columns["Nombre"].HeaderText = "Nombre completo";
            }

            if (dataGridView1.Columns["Telefono"] != null)
            {
                dataGridView1.Columns["Telefono"].HeaderText = "Teléfono";
            }

            if (dataGridView1.Columns["Correo"] != null)
            {
                dataGridView1.Columns["Correo"].HeaderText = "Correo electrónico";
            }

            if (dataGridView1.Columns["FechaNacimiento"] != null)
            {
                dataGridView1.Columns["FechaNacimiento"].HeaderText = "Fecha de nacimiento";
            }
        }

        private void CargarPacientes()
        {
            try
            {
                dataGridView1.DataSource = objetoCN.ListarPacientes();
                FormatearColumnasPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la lista de pacientes: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCamposPaciente()
        {
            bool valido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "Escribe el nombre completo del paciente.");
                valido = false;
            }
            else
            {
                errorProvider1.SetError(txtNombre, "");
            }

            if (string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                errorProvider1.SetError(txtCedula, "Escribe la cédula o identificación.");
                valido = false;
            }
            else
            {
                errorProvider1.SetError(txtCedula, "");
            }

            if (!string.IsNullOrWhiteSpace(txtFechaNacimiento.Text) &&
                !DateTime.TryParse(txtFechaNacimiento.Text.Trim(), out _))
            {
                errorProvider1.SetError(txtFechaNacimiento, "Pon una fecha válida, por ejemplo 15/05/1998.");
                valido = false;
            }
            else
            {
                errorProvider1.SetError(txtFechaNacimiento, "");
            }

            return valido;
        }

        private void LimpiarCampos()
        {
            idPacienteSeleccionado = 0;
            txtCedula.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtFechaNacimiento.Clear();
            errorProvider1.Clear();
            txtNombre.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarPacientes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = objetoCN.BuscarPacientes(txtBuscar.Text.Trim());
                FormatearColumnasPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo realizar la búsqueda: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposPaciente())
                {
                    MessageBox.Show("Revisa los campos marcados antes de guardar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult resultado = MessageBox.Show(
                    "¿Quieres registrar a este paciente?",
                    "Confirmar registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.Yes)
                {
                    objetoCN.InsertarPaciente(
                        txtCedula.Text.Trim(),
                        txtNombre.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        txtFechaNacimiento.Text.Trim()
                    );

                    MessageBox.Show("Listo, el paciente quedó registrado.", "Registro guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPacientes();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPacienteSeleccionado <= 0)
                {
                    MessageBox.Show("Primero haz clic en un paciente de la tabla para poder actualizarlo.", "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidarCamposPaciente())
                {
                    MessageBox.Show("Revisa los campos marcados antes de actualizar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult resultado = MessageBox.Show(
                    "¿Quieres guardar los cambios de este paciente?",
                    "Confirmar actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.Yes)
                {
                    objetoCN.ActualizarPaciente(
                        idPacienteSeleccionado,
                        txtCedula.Text.Trim(),
                        txtNombre.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        txtFechaNacimiento.Text.Trim()
                    );

                    MessageBox.Show("Listo, los datos del paciente se actualizaron.", "Actualización completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPacientes();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPacienteSeleccionado <= 0)
                {
                    MessageBox.Show("Primero haz clic en un paciente de la tabla para poder eliminarlo.", "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string nombrePaciente = string.IsNullOrWhiteSpace(txtNombre.Text) ? "este paciente" : txtNombre.Text.Trim();

                DialogResult resultado = MessageBox.Show(
                    "¿Seguro que deseas eliminar a " + nombrePaciente + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultado == DialogResult.Yes)
                {
                    objetoCN.EliminarPaciente(idPacienteSeleccionado);
                    MessageBox.Show("El paciente fue eliminado.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPacientes();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
            if (fila.Cells["IdPaciente"].Value == null || fila.Cells["IdPaciente"].Value == DBNull.Value)
            {
                return;
            }

            idPacienteSeleccionado = Convert.ToInt32(fila.Cells["IdPaciente"].Value);
            txtCedula.Text = fila.Cells["Identificacion"].Value?.ToString() ?? "";
            txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
            txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";
            txtCorreo.Text = fila.Cells["Correo"].Value?.ToString() ?? "";

            if (fila.Cells["FechaNacimiento"].Value != null && fila.Cells["FechaNacimiento"].Value != DBNull.Value)
            {
                DateTime fecha;
                if (DateTime.TryParse(fila.Cells["FechaNacimiento"].Value.ToString(), out fecha))
                {
                    txtFechaNacimiento.Text = fecha.ToShortDateString();
                }
                else
                {
                    txtFechaNacimiento.Text = fila.Cells["FechaNacimiento"].Value.ToString();
                }
            }
            else
            {
                txtFechaNacimiento.Clear();
            }

            errorProvider1.Clear();
        }
    }
}