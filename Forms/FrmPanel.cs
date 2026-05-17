using Google.Protobuf.Collections;
using PermisosVehiculos;
using PermisosVehiculos.Models;
using PermisosVehiculos.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppCirculacion.Forms
{
    public partial class FrmPanel : Form
    {
        private readonly AppContext _appContext;
        private readonly VehiculoService _vehiculoService = new VehiculoService();
        private readonly ContribuyenteService _contribuyenteService = new ContribuyenteService();


        public FrmPanel(AppContext appContext)
        {
            InitializeComponent();
            _appContext = appContext;

            CargarContribuyentes();

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            DialogResult confirmacion = MessageBox.Show(
                "¿Estás seguro que deseas cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                _appContext.MostrarLogin(); // Vuelve al login antes de cerrar
            }
            else
            {
                e.Cancel = true; // Cancela el cierre, la ventana sigue abierta
            }
        }

        private void CargarContribuyentes()
        {
            try
            {
                var lista = _contribuyenteService.ObtenerTodos();

                dataGridContribuyentes.DataSource = lista;

                if (!dataGridContribuyentes.Columns.Contains("btnEditarContribuyente"))
                {
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                    btnEditar.Name = "btnEditarContribuyente";
                    btnEditar.HeaderText = "";
                    btnEditar.Text = "Editar";
                    btnEditar.UseColumnTextForButtonValue = true;
                    dataGridContribuyentes.Columns.Add(btnEditar);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarContribuyentesCombo()
        {
            try
            {
                var lista = _contribuyenteService.ObtenerTodos();
                comboBoxContribuyente.DataSource = lista;
                comboBoxContribuyente.DisplayMember = "Rut";
                comboBoxContribuyente.ValueMember = "Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVehiculos()
        {
            try
            {
                var lista = _vehiculoService.ObtenerTodos();

                dataGridVehiculos.DataSource = lista;

                dataGridVehiculos.Columns["Id"].Visible = false;
                dataGridVehiculos.Columns["Patente"].HeaderText = "Patente";
                dataGridVehiculos.Columns["Marca"].HeaderText = "Marca";
                dataGridVehiculos.Columns["Modelo"].HeaderText = "Modelo";
                dataGridVehiculos.Columns["Color"].HeaderText = "Color";
                dataGridVehiculos.Columns["Anio"].HeaderText = "Año";
                dataGridVehiculos.Columns["RutContribuyente"].HeaderText = "RUT Contribuyente";

                if (!dataGridVehiculos.Columns.Contains("btnEditarVehiculo"))
                {
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                    btnEditar.Name = "btnEditarVehiculo";
                    btnEditar.HeaderText = "";
                    btnEditar.Text = "Editar";
                    btnEditar.UseColumnTextForButtonValue = true;
                    dataGridVehiculos.Columns.Add(btnEditar);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarContribuyentesVehiculos()
        {
            try
            {
                var lista = _vehiculoService.ObtenerTodos();
                dataGridCV.DataSource = lista;
                dataGridCV.Columns["Id"].Visible = false;
                dataGridCV.Columns["Patente"].HeaderText = "Patente";
                dataGridCV.Columns["Marca"].HeaderText = "Marca";
                dataGridCV.Columns["Modelo"].HeaderText = "Modelo";
                dataGridCV.Columns["Color"].HeaderText = "Color";
                dataGridCV.Columns["Anio"].HeaderText = "Año";
                dataGridCV.Columns["RutContribuyente"].HeaderText = "RUT Contribuyente";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    CargarContribuyentes();

                    break;
                case 1:
                    CargarVehiculos();
                    CargarContribuyentesCombo();
                    break;
                case 2:
                    CargarContribuyentesVehiculos();
                    break;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBoxContribuyente.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txt_patente.Text) ||
                string.IsNullOrWhiteSpace(txt_marca.Text) ||
                string.IsNullOrWhiteSpace(txt_modelo.Text) ||
                string.IsNullOrWhiteSpace(txt_color.Text) ||
                string.IsNullOrWhiteSpace(txt_año.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txt_año.Text, out int anio))
            {
                MessageBox.Show("El año debe ser un número válido.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Vehiculo vehiculo = new Vehiculo
                {
                    IdContribuyente = Convert.ToInt32(comboBoxContribuyente.SelectedValue),
                    Patente = txt_patente.Text.Trim().ToUpper(),
                    Marca = txt_marca.Text.Trim(),
                    Modelo = txt_modelo.Text.Trim(),
                    Color = txt_color.Text.Trim(),
                    Anio = anio
                };

                _vehiculoService.Insertar(vehiculo);

                MessageBox.Show("Vehículo guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LimpiarCampos()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = string.Empty;
                }
            }
        }

        private void btn_agregarContr_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_rut.Text) ||
                string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_apellido.Text) ||
                string.IsNullOrWhiteSpace(txt_nacionalidad.Text) ||
                string.IsNullOrWhiteSpace(txt_direccion.Text) ||
                string.IsNullOrWhiteSpace(txt_comuna.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Contribuyente contribuyente = new Contribuyente
                {
                    Rut = txt_rut.Text.Trim(),
                    Nombre = txt_nombre.Text.Trim(),
                    Apellido = txt_apellido.Text.Trim(),
                    Nacionalidad = txt_nacionalidad.Text.Trim(),
                    Direccion = txt_direccion.Text.Trim(),
                    Comuna = txt_comuna.Text.Trim()
                };

                _contribuyenteService.Insertar(contribuyente);
                CargarContribuyentes();

                MessageBox.Show("Contribuyente guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridContribuyentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridContribuyentes.Columns["btnEditarContribuyente"].Index && e.RowIndex >= 0)
            {
                // Obtener el contribuyente de la fila seleccionada
                Contribuyente contribuyente = (Contribuyente)dataGridContribuyentes.Rows[e.RowIndex].DataBoundItem;

                // Abrir formulario de edición pasando el contribuyente
                FrmEditarContribuyente frmEditar = new FrmEditarContribuyente(contribuyente);
                frmEditar.ShowDialog();

                // Recargar la lista al cerrar el formulario
                CargarContribuyentes();
            }
        }

        private void dataGridVehiculos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se hizo clic en la columna Editar y no en el header
            if (e.ColumnIndex == dataGridVehiculos.Columns["btnEditarVehiculo"].Index && e.RowIndex >= 0)
            {
                // Obtener el vehículo de la fila seleccionada
                Vehiculo vehiculo = (Vehiculo)dataGridVehiculos.Rows[e.RowIndex].DataBoundItem;

                // Abrir formulario de edición pasando el vehículo
                FrmEditarVehiculo frmEditar = new FrmEditarVehiculo(vehiculo);
                frmEditar.ShowDialog();

                // Recargar la lista al cerrar el formulario
                CargarVehiculos();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string rutBuscado = txt_buscarCV.Text.Trim();

            if (string.IsNullOrWhiteSpace(rutBuscado))
            {
                MessageBox.Show("Ingresa un RUT para buscar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var lista = _vehiculoService.ObtenerTodos();

                var resultado = lista.FindAll(v => v.RutContribuyente == rutBuscado);

                if (resultado.Count == 0)
                {
                    MessageBox.Show("No se encontraron vehículos para ese RUT.", "Sin resultados",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Vincula el resultado filtrado al DataGrid
                dataGridCV.DataSource = resultado;
                txt_buscarCV.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

                 
