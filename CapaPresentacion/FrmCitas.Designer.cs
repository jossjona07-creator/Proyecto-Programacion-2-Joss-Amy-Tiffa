namespace CapaPresentacion
{
    partial class FrmCitas
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.GroupBox grpDatosCita;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.ComboBox cmbPaciente;
        private System.Windows.Forms.Label lblMedico;
        private System.Windows.Forms.ComboBox cmbMedico;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
        private System.Windows.Forms.Label lblDuracion;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.Label lblTipoCita;
        private System.Windows.Forms.ComboBox cmbTipoCita;
        private System.Windows.Forms.Label lblEstadoCita;
        private System.Windows.Forms.ComboBox cmbEstadoCita;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnMostrarTodos;
        private System.Windows.Forms.DataGridView dgvCitas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.grpDatosCita = new System.Windows.Forms.GroupBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.cmbPaciente = new System.Windows.Forms.ComboBox();
            this.lblMedico = new System.Windows.Forms.Label();
            this.cmbMedico = new System.Windows.Forms.ComboBox();
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.lblDuracion = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.lblTipoCita = new System.Windows.Forms.Label();
            this.cmbTipoCita = new System.Windows.Forms.ComboBox();
            this.lblEstadoCita = new System.Windows.Forms.Label();
            this.cmbEstadoCita = new System.Windows.Forms.ComboBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnMostrarTodos = new System.Windows.Forms.Button();
            this.dgvCitas = new System.Windows.Forms.DataGridView();

            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCita.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.SuspendLayout();

            // pnlEncabezado
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(102, 45, 145);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(960, 72);
            this.pnlEncabezado.TabIndex = 0;

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Gestión de Citas";

            // lblSubtitulo
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(235, 220, 248);
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 42);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Registro, consulta, reprogramación y control de citas médicas";

            // grpDatosCita
            this.grpDatosCita.Controls.Add(this.lblPaciente);
            this.grpDatosCita.Controls.Add(this.cmbPaciente);
            this.grpDatosCita.Controls.Add(this.lblMedico);
            this.grpDatosCita.Controls.Add(this.cmbMedico);
            this.grpDatosCita.Controls.Add(this.lblFechaHora);
            this.grpDatosCita.Controls.Add(this.dtpFechaHora);
            this.grpDatosCita.Controls.Add(this.lblDuracion);
            this.grpDatosCita.Controls.Add(this.txtDuracion);
            this.grpDatosCita.Controls.Add(this.lblTipoCita);
            this.grpDatosCita.Controls.Add(this.cmbTipoCita);
            this.grpDatosCita.Controls.Add(this.lblEstadoCita);
            this.grpDatosCita.Controls.Add(this.cmbEstadoCita);
            this.grpDatosCita.Controls.Add(this.lblMotivo);
            this.grpDatosCita.Controls.Add(this.txtMotivo);
            this.grpDatosCita.Controls.Add(this.btnGuardar);
            this.grpDatosCita.Controls.Add(this.btnActualizar);
            this.grpDatosCita.Controls.Add(this.btnEliminar);
            this.grpDatosCita.Controls.Add(this.btnLimpiar);
            this.grpDatosCita.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpDatosCita.Location = new System.Drawing.Point(20, 82);
            this.grpDatosCita.Name = "grpDatosCita";
            this.grpDatosCita.Size = new System.Drawing.Size(915, 215);
            this.grpDatosCita.TabIndex = 1;
            this.grpDatosCita.TabStop = false;
            this.grpDatosCita.Text = "Datos de la Cita";

            // Fila 1: Paciente, Médico, Fecha/Hora
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Location = new System.Drawing.Point(18, 28);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Text = "Paciente:";

            this.cmbPaciente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaciente.Location = new System.Drawing.Point(18, 50);
            this.cmbPaciente.Name = "cmbPaciente";
            this.cmbPaciente.Size = new System.Drawing.Size(270, 25);

            this.lblMedico.AutoSize = true;
            this.lblMedico.Location = new System.Drawing.Point(310, 28);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Text = "Médico:";

            this.cmbMedico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedico.Location = new System.Drawing.Point(310, 50);
            this.cmbMedico.Name = "cmbMedico";
            this.cmbMedico.Size = new System.Drawing.Size(270, 25);

            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Location = new System.Drawing.Point(602, 28);
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.Text = "Fecha y hora:";

            this.dtpFechaHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHora.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpFechaHora.Location = new System.Drawing.Point(602, 50);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.Size = new System.Drawing.Size(290, 25);

            // Fila 2: Duración, Tipo, Estado, Motivo
            this.lblDuracion.AutoSize = true;
            this.lblDuracion.Location = new System.Drawing.Point(18, 86);
            this.lblDuracion.Name = "lblDuracion";
            this.lblDuracion.Text = "Duración (min):";

            this.txtDuracion.Location = new System.Drawing.Point(18, 108);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(120, 25);
            this.txtDuracion.Text = "30";

            this.lblTipoCita.AutoSize = true;
            this.lblTipoCita.Location = new System.Drawing.Point(155, 86);
            this.lblTipoCita.Name = "lblTipoCita";
            this.lblTipoCita.Text = "Tipo de cita:";

            this.cmbTipoCita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCita.Items.AddRange(new object[] { "Presencial", "Virtual", "Urgencia" });
            this.cmbTipoCita.Location = new System.Drawing.Point(155, 108);
            this.cmbTipoCita.Name = "cmbTipoCita";
            this.cmbTipoCita.Size = new System.Drawing.Size(150, 25);
            this.cmbTipoCita.SelectedIndex = 0;

            this.lblEstadoCita.AutoSize = true;
            this.lblEstadoCita.Location = new System.Drawing.Point(322, 86);
            this.lblEstadoCita.Name = "lblEstadoCita";
            this.lblEstadoCita.Text = "Estado:";

            this.cmbEstadoCita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoCita.Items.AddRange(new object[] { "Programada", "Confirmada", "Atendida", "Cancelada", "No asistió", "Reprogramada" });
            this.cmbEstadoCita.Location = new System.Drawing.Point(322, 108);
            this.cmbEstadoCita.Name = "cmbEstadoCita";
            this.cmbEstadoCita.Size = new System.Drawing.Size(150, 25);
            this.cmbEstadoCita.SelectedIndex = 0;

            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(489, 86);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Text = "Motivo:";

            this.txtMotivo.Location = new System.Drawing.Point(489, 108);
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(403, 25);

            // Botones CRUD
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(102, 45, 145);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(18, 152);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 38);
            this.btnGuardar.Text = "Guardar";

            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(128, 64, 175);
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(160, 152);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(130, 38);
            this.btnActualizar.Text = "Actualizar";

            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(200, 65, 65);
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(302, 152);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 38);
            this.btnEliminar.Text = "Cancelar cita";

            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(120, 105, 135);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(444, 152);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(130, 38);
            this.btnLimpiar.Text = "Limpiar";

            // Barra de búsqueda
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(20, 312);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Text = "Buscar cita:";

            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtBuscar.Location = new System.Drawing.Point(115, 309);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(390, 25);

            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(102, 45, 145);
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(520, 307);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(130, 32);
            this.btnBuscar.Text = "Buscar";

            this.btnMostrarTodos.BackColor = System.Drawing.Color.FromArgb(102, 45, 145);
            this.btnMostrarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarTodos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnMostrarTodos.ForeColor = System.Drawing.Color.White;
            this.btnMostrarTodos.Location = new System.Drawing.Point(665, 307);
            this.btnMostrarTodos.Name = "btnMostrarTodos";
            this.btnMostrarTodos.Size = new System.Drawing.Size(150, 32);
            this.btnMostrarTodos.Text = "Mostrar todos";

            // dgvCitas
            this.dgvCitas.AllowUserToAddRows = false;
            this.dgvCitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCitas.BackgroundColor = System.Drawing.Color.FromArgb(245, 240, 250);
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Location = new System.Drawing.Point(20, 350);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.ReadOnly = true;
            this.dgvCitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCitas.Size = new System.Drawing.Size(915, 210);

            // FrmCitas
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 580);
            this.Controls.Add(this.dgvCitas);
            this.Controls.Add(this.btnMostrarTodos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.grpDatosCita);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCitas";
            this.Text = "Gestión de Citas";

            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCita.ResumeLayout(false);
            this.grpDatosCita.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}