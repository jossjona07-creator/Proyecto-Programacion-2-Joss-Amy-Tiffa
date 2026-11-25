using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public class FrmReportes : Form
    {
        private readonly CN_Reportes reportes = new CN_Reportes();
        private readonly CN_Medico medicos = new CN_Medico();
        private readonly ComboBox tipo = new ComboBox();
        private readonly ComboBox medico = new ComboBox();
        private readonly DateTimePicker desde = new DateTimePicker();
        private readonly DateTimePicker hasta = new DateTimePicker();
        private readonly DataGridView grid = new DataGridView();
        private DateTime fechaDia = DateTime.Today;
        private DateTime fechaDesde = DateTime.Today.AddDays(-30);
        private DateTime fechaHasta = DateTime.Today;
        private int tipoAnterior;

        public FrmReportes()
        {
            Text = "Reportes";
            BackColor = Color.White;
            Padding = new Padding(18);
            MinimumSize = new Size(800, 520);

            Controls.Add(new Label
            {
                Text = "Reportes de agenda",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(74, 20, 140),
                AutoSize = true,
                Location = new Point(18, 14)
            });

            Controls.Add(new Label { Text = "Reporte", Location = new Point(18, 66), AutoSize = true });
            tipo.Location = new Point(90, 62);
            tipo.Width = 230;
            tipo.DropDownStyle = ComboBoxStyle.DropDownList;
            tipo.Items.AddRange(new object[] { "Citas por día y médico", "Citas canceladas", "Pacientes atendidos" });
            tipo.SelectedIndex = 0;
            Controls.Add(tipo);

            Controls.Add(new Label { Text = "Médico", Location = new Point(340, 66), AutoSize = true });
            medico.Location = new Point(395, 62);
            medico.Width = 210;
            medico.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(medico);

            Controls.Add(new Label { Text = "Desde / día", Location = new Point(18, 107), AutoSize = true });
            desde.Location = new Point(110, 103);
            desde.Width = 200;
            desde.Format = DateTimePickerFormat.Short;
            desde.Value = DateTime.Today;
            Controls.Add(desde);

            Controls.Add(new Label { Text = "Hasta", Location = new Point(330, 107), AutoSize = true });
            hasta.Location = new Point(385, 103);
            hasta.Width = 200;
            hasta.Format = DateTimePickerFormat.Short;
            hasta.Value = DateTime.Today;
            hasta.Enabled = false;
            Controls.Add(hasta);

            var buscar = new Button
            {
                Text = "Consultar",
                Location = new Point(620, 99),
                Size = new Size(110, 34),
                BackColor = Color.FromArgb(106, 27, 154),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            buscar.Click += Consultar;
            Controls.Add(buscar);

            grid.Location = new Point(18, 150);
            grid.Size = new Size(760, 320);
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Controls.Add(grid);

            tipo.SelectedIndexChanged += TipoCambiado;
            Load += Cargar;
        }

        private void TipoCambiado(object sender, EventArgs e)
        {
            if (tipoAnterior == 0)
            {
                fechaDia = desde.Value.Date;
            }
            else
            {
                fechaDesde = desde.Value.Date;
                fechaHasta = hasta.Value.Date;
            }

            bool esConsultaDiaria = tipo.SelectedIndex == 0;
            medico.Enabled = esConsultaDiaria;
            hasta.Enabled = !esConsultaDiaria;

            if (esConsultaDiaria)
            {
                desde.Value = fechaDia;
            }
            else
            {
                desde.Value = fechaDesde;
                hasta.Value = fechaHasta;
            }

            tipoAnterior = tipo.SelectedIndex;
        }

        private void Cargar(object sender, EventArgs e)
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("Id", typeof(int));
                tabla.Columns.Add("Nombre");
                tabla.Rows.Add(0, "Todos los médicos");

                foreach (DataRow fila in medicos.ListarMedicos().Rows)
                {
                    if (string.Equals(fila["Estado"].ToString(), "Activo", StringComparison.OrdinalIgnoreCase))
                        tabla.Rows.Add(Convert.ToInt32(fila["IdMedico"]), fila["Nombre"].ToString());
                }

                medico.DataSource = null;
                medico.DisplayMember = "Nombre";
                medico.ValueMember = "Id";
                medico.DataSource = tabla;
            }
            catch (Exception ex)
            {
                Aviso(ex.Message);
            }
        }

        private static int IdSeleccionado(ComboBox combo)
        {
            object valor = combo.SelectedValue;
            if (valor is DataRowView fila)
                valor = fila[combo.ValueMember];

            return int.TryParse(Convert.ToString(valor), out int id) ? id : 0;
        }

        private void Consultar(object sender, EventArgs e)
        {
            try
            {
                DataTable resultado;
                if (tipo.SelectedIndex == 0)
                    resultado = reportes.CitasPorDia(desde.Value, IdSeleccionado(medico));
                else if (tipo.SelectedIndex == 1)
                    resultado = reportes.CitasCanceladas(desde.Value, hasta.Value);
                else
                    resultado = reportes.PacientesAtendidos(desde.Value, hasta.Value);

                grid.DataSource = resultado;
                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros con los filtros seleccionados.", "Reportes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Aviso(ex.Message);
            }
        }

        private void Aviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}