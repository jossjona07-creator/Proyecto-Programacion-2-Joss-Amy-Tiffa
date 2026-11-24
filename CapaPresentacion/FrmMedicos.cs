using System;
using System.Drawing;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class FrmMedicos : Form
    {
        private readonly CN_Medico cnMedico = new CN_Medico();
        private int idSeleccionado = 0;

        public FrmMedicos()
        {
            InitializeComponent();
        }

        private void FrmMedicos_Load(object sender, EventArgs e)
        {
            AplicarEstilo();
            CargarMedicos();
            LimpiarCampos();
        }

        private void CargarMedicos()
        {
            try
            {
                dgvMedicos.DataSource = null;
                dgvMedicos.DataSource = cnMedico.ListarMedicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar médicos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            idSeleccionado = 0;
            if (txtIdMedico != null) txtIdMedico.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtHorarioAtencion.Text = string.Empty;
            if (txtBuscar != null) txtBuscar.Text = string.Empty;

            if (cmbEstado.Items.Count > 0)
                cmbEstado.SelectedIndex = 0;
            else
                cmbEstado.Text = "Activo";

            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                cnMedico.InsertarMedico(
                    txtIdentificacion.Text,
                    txtNombre.Text,
                    txtTelefono.Text,
                    txtCorreo.Text,
                    txtHorarioAtencion.Text,
                    cmbEstado.Text
                );

                MessageBox.Show("Médico registrado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarMedicos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                cnMedico.ActualizarMedico(
                    idSeleccionado,
                    txtIdentificacion.Text,
                    txtNombre.Text,
                    txtTelefono.Text,
                    txtCorreo.Text,
                    txtHorarioAtencion.Text,
                    cmbEstado.Text
                );

                MessageBox.Show("Médico actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarMedicos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado <= 0)
                {
                    MessageBox.Show("Selecciona un médico de la tabla para eliminar.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "¿Deseas eliminar este médico?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    cnMedico.EliminarMedico(idSeleccionado);

                    MessageBox.Show("Médico eliminado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarMedicos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvMedicos.DataSource = null;
                dgvMedicos.DataSource = cnMedico.BuscarMedicos(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            if (txtBuscar != null) txtBuscar.Text = string.Empty;
            CargarMedicos();
        }

        private void dgvMedicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMedicos.CurrentRow != null)
            {
                DataGridViewRow fila = dgvMedicos.CurrentRow;

                if (fila.Cells["IdMedico"]?.Value != null &&
                    int.TryParse(fila.Cells["IdMedico"].Value.ToString(), out int id))
                {
                    idSeleccionado = id;
                    if (txtIdMedico != null) txtIdMedico.Text = id.ToString();
                }

                txtIdentificacion.Text = fila.Cells["Identificacion"]?.Value?.ToString() ?? string.Empty;
                txtNombre.Text = fila.Cells["Nombre"]?.Value?.ToString() ?? string.Empty;
                txtTelefono.Text = fila.Cells["Telefono"]?.Value?.ToString() ?? string.Empty;
                txtCorreo.Text = fila.Cells["Correo"]?.Value?.ToString() ?? string.Empty;
                txtHorarioAtencion.Text = fila.Cells["HorarioAtencion"]?.Value?.ToString() ?? string.Empty;
                cmbEstado.Text = fila.Cells["Estado"]?.Value?.ToString() ?? "Activo";
            }
        }

        private void AplicarEstilo()
        {
            dgvMedicos.BackgroundColor = Color.White;
            dgvMedicos.BorderStyle = BorderStyle.None;
            dgvMedicos.GridColor = Color.FromArgb(220, 215, 230);
            dgvMedicos.EnableHeadersVisualStyles = false;
            dgvMedicos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(74, 20, 140);
            dgvMedicos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMedicos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvMedicos.DefaultCellStyle.BackColor = Color.White;
            dgvMedicos.DefaultCellStyle.ForeColor = Color.Black;
            dgvMedicos.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvMedicos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 190, 231);
            dgvMedicos.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMedicos.RowHeadersVisible = false;
            dgvMedicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FrmMedicos_Load_1(object sender, EventArgs e)
        {

        }
    }
}