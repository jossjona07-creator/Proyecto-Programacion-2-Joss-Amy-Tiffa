using System;
using System.Data;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class FrmCitas : Form
    {
        private CN_Cita objetoCN = new CN_Cita();
        private CN_Paciente objetoCNPaciente = new CN_Paciente();
        private CN_Medico objetoCNMedico = new CN_Medico();
        private int idCitaSeleccionada = 0;
        private DateTime? fechaHoraOriginal;

        public FrmCitas()
        {
            InitializeComponent();
            AsignarEventos();
        }

        private void AsignarEventos()
        {
            this.Load += FrmCitas_Load;
            this.btnGuardar.Click += btnGuardar_Click;
            this.btnActualizar.Click += btnActualizar_Click;
            this.btnEliminar.Click += btnEliminar_Click;
            this.btnLimpiar.Click += btnLimpiar_Click;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnMostrarTodos.Click += btnMostrarTodos_Click;
            this.dgvCitas.CellClick += dgvCitas_CellClick;
        }

        private void FrmCitas_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ListarCitas();
            LimpiarCampos();
        }

        private DataTable PrepararTablaCombo(DataTable origen, string colId, string colTexto, string textoInicial)
        {
            DataTable dtCombo = new DataTable();
            dtCombo.Columns.Add(colId, typeof(int));
            dtCombo.Columns.Add(colTexto, typeof(string));

            dtCombo.Rows.Add(0, textoInicial);

            if (origen != null)
            {
                foreach (DataRow row in origen.Rows)
                {
                    int id = Convert.ToInt32(row[colId]);
                    string texto = row[colTexto]?.ToString() ?? string.Empty;
                    dtCombo.Rows.Add(id, texto);
                }
            }

            return dtCombo;
        }

        private void CargarCombos()
        {
            DataTable dtPacientes = objetoCNPaciente.ListarPacientes();
            cmbPaciente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaciente.DataSource = null;
            cmbPaciente.DataSource = PrepararTablaCombo(dtPacientes, "IdPaciente", "Nombre", "-- Seleccione un paciente --");
            cmbPaciente.DisplayMember = "Nombre";
            cmbPaciente.ValueMember = "IdPaciente";
            cmbPaciente.SelectedIndex = 0;

            DataTable dtMedicos = objetoCNMedico.ListarMedicos();
            cmbMedico.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedico.DataSource = null;
            cmbMedico.DataSource = PrepararTablaCombo(dtMedicos, "IdMedico", "Nombre", "-- Seleccione un médico --");
            cmbMedico.DisplayMember = "Nombre";
            cmbMedico.ValueMember = "IdMedico";
            cmbMedico.SelectedIndex = 0;
        }

        private void ListarCitas()
        {
            dgvCitas.DataSource = objetoCN.ListarCitas();
            if (dgvCitas.Columns.Contains("IdPaciente"))
                dgvCitas.Columns["IdPaciente"].Visible = false;
            if (dgvCitas.Columns.Contains("IdMedico"))
                dgvCitas.Columns["IdMedico"].Visible = false;
        }

        private void LimpiarCampos()
        {
            idCitaSeleccionada = 0;
            fechaHoraOriginal = null;

            if (cmbPaciente.Items.Count > 0)
                cmbPaciente.SelectedIndex = 0;

            if (cmbMedico.Items.Count > 0)
                cmbMedico.SelectedIndex = 0;

            dtpFechaHora.Value = DateTime.Now;
            txtDuracion.Text = "30";
            cmbTipoCita.SelectedIndex = 0;
            cmbEstadoCita.SelectedIndex = 0;
            txtMotivo.Clear();
            txtBuscar.Clear();
            dgvCitas.ClearSelection();
        }

        private int ObtenerIdCombo(ComboBox combo)
        {
            if (combo.SelectedIndex <= 0 || combo.SelectedValue == null) return 0;
            if (combo.SelectedValue is DataRowView fila)
                return Convert.ToInt32(fila[combo.ValueMember]);
            return Convert.ToInt32(combo.SelectedValue);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idPaciente = ObtenerIdCombo(cmbPaciente);
                int idMedico = ObtenerIdCombo(cmbMedico);

                if (idPaciente <= 0)
                {
                    MessageBox.Show("Selecciona un paciente válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (idMedico <= 0)
                {
                    MessageBox.Show("Selecciona un médico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtDuracion.Text.Trim(), out int duracion))
                {
                    MessageBox.Show("Escribe un número válido para la duración en minutos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                objetoCN.InsertarCita(
                    idPaciente,
                    idMedico,
                    dtpFechaHora.Value,
                    duracion,
                    cmbTipoCita.Text,
                    cmbEstadoCita.Text,
                    txtMotivo.Text
                );

                MessageBox.Show("Cita guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarCitas();
                LimpiarCampos();
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
                if (idCitaSeleccionada <= 0)
                {
                    MessageBox.Show("Primero selecciona una cita de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPaciente = ObtenerIdCombo(cmbPaciente);
                int idMedico = ObtenerIdCombo(cmbMedico);

                if (idPaciente <= 0)
                {
                    MessageBox.Show("Selecciona un paciente válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (idMedico <= 0)
                {
                    MessageBox.Show("Selecciona un médico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtDuracion.Text.Trim(), out int duracion))
                {
                    MessageBox.Show("Escribe un número válido para la duración en minutos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (fechaHoraOriginal.HasValue && dtpFechaHora.Value != fechaHoraOriginal.Value)
                {
                    objetoCN.ReprogramarCita(idCitaSeleccionada,idPaciente,idMedico,dtpFechaHora.Value,duracion,cmbTipoCita.Text,txtMotivo.Text);
                    MessageBox.Show("La cita anterior quedó marcada como reprogramada y se creó un nuevo turno.", "Cita reprogramada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (string.Equals(cmbEstadoCita.Text,"Reprogramada",StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Para reprogramar, cambie la fecha y hora del turno.","Validación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    objetoCN.ActualizarCita(idCitaSeleccionada,idPaciente,idMedico,dtpFechaHora.Value,duracion,cmbTipoCita.Text,cmbEstadoCita.Text,txtMotivo.Text);
                    MessageBox.Show("Cita actualizada correctamente.","Éxito",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }                ListarCitas();
                LimpiarCampos();
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
                if (idCitaSeleccionada <= 0)
                {
                    MessageBox.Show("Primero selecciona una cita de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                objetoCN.EliminarCita(idCitaSeleccionada);
                MessageBox.Show("La cita se canceló correctamente y quedó en el historial.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarCitas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvCitas.DataSource = objetoCN.BuscarCitas(txtBuscar.Text);
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            ListarCitas();
        }

        private void dgvCitas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvCitas.Rows[e.RowIndex];
                idCitaSeleccionada = Convert.ToInt32(fila.Cells["IdCita"].Value);
                cmbPaciente.SelectedValue = Convert.ToInt32(fila.Cells["IdPaciente"].Value);
                cmbMedico.SelectedValue = Convert.ToInt32(fila.Cells["IdMedico"].Value);
                dtpFechaHora.Value = Convert.ToDateTime(fila.Cells["FechaHora"].Value);
                fechaHoraOriginal = dtpFechaHora.Value;
                txtDuracion.Text = fila.Cells["DuracionMinutos"].Value?.ToString();
                cmbTipoCita.Text = fila.Cells["TipoCita"].Value?.ToString();
                cmbEstadoCita.Text = fila.Cells["EstadoCita"].Value?.ToString();
                txtMotivo.Text = fila.Cells["Motivo"].Value?.ToString();
            }
        }
    }
}