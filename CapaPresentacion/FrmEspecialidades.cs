using System;
using System.Drawing;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class FrmEspecialidades : Form
    {
        private readonly CN_Especialidad cnEspecialidad = new CN_Especialidad();
        private int idSeleccionado = 0;

        public FrmEspecialidades()
        {
            InitializeComponent();
        }

        private void FrmEspecialidades_Load(object sender, EventArgs e)
        {
            AplicarEstilo();
            CargarEspecialidades();
            LimpiarCampos();
        }

        private void CargarEspecialidades()
        {
            try
            {
                dgvEspecialidades.DataSource = null;
                dgvEspecialidades.DataSource = cnEspecialidad.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar especialidades: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            idSeleccionado = 0;
            txtIdEspecialidad.Text = string.Empty;
            txtNombreEspecialidad.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtNombreEspecialidad.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Especialidad entidad = new Especialidad
                {
                    IdEspecialidad = idSeleccionado,
                    NombreEspecialidad = txtNombreEspecialidad.Text,
                    Descripcion = txtDescripcion.Text
                };

                cnEspecialidad.Guardar(entidad);

                MessageBox.Show("Especialidad guardada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarEspecialidades();
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
                    MessageBox.Show("Selecciona una especialidad de la tabla para eliminar.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "¿Deseas eliminar esta especialidad?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    cnEspecialidad.Eliminar(idSeleccionado);

                    MessageBox.Show("Especialidad eliminada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarEspecialidades();
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

        private void dgvEspecialidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEspecialidades.CurrentRow != null)
            {
                DataGridViewRow fila = dgvEspecialidades.CurrentRow;

                if (fila.Cells["IdEspecialidad"]?.Value != null &&
                    int.TryParse(fila.Cells["IdEspecialidad"].Value.ToString(), out int id))
                {
                    idSeleccionado = id;
                    txtIdEspecialidad.Text = id.ToString();
                }

                txtNombreEspecialidad.Text = fila.Cells["NombreEspecialidad"]?.Value?.ToString() ?? string.Empty;
                txtDescripcion.Text = fila.Cells["Descripcion"]?.Value?.ToString() ?? string.Empty;
            }
        }

        private void AplicarEstilo()
        {
            this.BackColor = Color.FromArgb(245, 243, 250);

            EstilizarEtiquetas(this.Controls);

            Color moradoBoton = Color.FromArgb(106, 27, 154);
            EstilizarBoton(btnGuardar, moradoBoton, Color.White);
            EstilizarBoton(btnEliminar, moradoBoton, Color.White);
            EstilizarBoton(btnLimpiar, moradoBoton, Color.White);

            dgvEspecialidades.BackgroundColor = Color.White;
            dgvEspecialidades.BorderStyle = BorderStyle.None;
            dgvEspecialidades.GridColor = Color.FromArgb(220, 215, 230);
            dgvEspecialidades.EnableHeadersVisualStyles = false;
            dgvEspecialidades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(74, 20, 140);
            dgvEspecialidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEspecialidades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvEspecialidades.DefaultCellStyle.BackColor = Color.White;
            dgvEspecialidades.DefaultCellStyle.ForeColor = Color.Black;
            dgvEspecialidades.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvEspecialidades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 190, 231);
            dgvEspecialidades.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvEspecialidades.RowHeadersVisible = false;
            dgvEspecialidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void EstilizarEtiquetas(Control.ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.FromArgb(74, 20, 140);
                    lbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }

                if (ctrl.HasChildren)
                {
                    EstilizarEtiquetas(ctrl.Controls);
                }
            }
        }

        private void EstilizarBoton(Button btn, Color fondo, Color texto)
        {
            if (btn == null) return;
            btn.BackColor = fondo;
            btn.ForeColor = texto;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void txtNombreEspecialidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvEspecialidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}